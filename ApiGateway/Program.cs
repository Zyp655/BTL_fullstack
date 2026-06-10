using System.Text;
using System.Text.Json.Nodes;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add JWT Authentication
var jwtSecret = builder.Configuration["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret is missing in configuration.");
var key = Encoding.UTF8.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"] ?? "TrainingCenter",
        ValidAudience = builder.Configuration["JwtSettings:Audience"] ?? "TrainingCenterApp",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Add CORS
var corsSettings = builder.Configuration.GetSection("CorsSettings");
var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
var preflightMaxAge = corsSettings.GetValue<int>("PreflightMaxAgeInMinutes", 10);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionCorsPolicy", policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(preflightMaxAge))
              .WithExposedHeaders("Content-Disposition", "X-Pagination");
    });
});

// Add HttpClient for BFF Aggregation
builder.Services.AddHttpClient();

// Add Ocelot
builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("ProductionCorsPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();

// BFF Aggregated Request Endpoint
app.MapGet("/api/v1/portal/student-summary/{userId:int}", async (int userId, HttpContext httpContext, IConfiguration configuration, HttpClient httpClient) =>
{
    var studentServiceUrl = configuration["ServiceUrls:StudentService"] ?? "http://localhost:5002";
    var paymentServiceUrl = configuration["ServiceUrls:PaymentService"] ?? "http://localhost:5003";

    var authHeader = httpContext.Request.Headers["Authorization"].ToString();

    var createRequest = (string url) =>
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        if (!string.IsNullOrEmpty(authHeader))
        {
            request.Headers.Add("Authorization", authHeader);
        }
        return request;
    };

    // 1. Fetch Student Profile by UserId
    var profileRequest = createRequest($"{studentServiceUrl}/api/v1/students/by-user/{userId}");
    var profileResponse = await httpClient.SendAsync(profileRequest);
    
    JsonNode? profileJson = null;
    if (profileResponse.IsSuccessStatusCode)
    {
        profileJson = await profileResponse.Content.ReadFromJsonAsync<JsonNode>();
    }
    else
    {
        // Self-healing: try to find the student by user's email/fullname and link them
        try
        {
            var userProfileRequest = createRequest($"{paymentServiceUrl}/api/v1/auth/profile");
            var userProfileResponse = await httpClient.SendAsync(userProfileRequest);
            if (userProfileResponse.IsSuccessStatusCode)
            {
                var userProfile = await userProfileResponse.Content.ReadFromJsonAsync<JsonNode>();
                var email = userProfile?["email"]?.GetValue<string>();
                if (!string.IsNullOrEmpty(email))
                {
                    // Search student by email
                    var searchUrl = $"{studentServiceUrl}/api/v1/students?search={Uri.EscapeDataString(email)}&pageSize=5";
                    var searchRequest = createRequest(searchUrl);
                    var searchResponse = await httpClient.SendAsync(searchRequest);
                    if (searchResponse.IsSuccessStatusCode)
                    {
                        var searchResult = await searchResponse.Content.ReadFromJsonAsync<JsonNode>();
                        var items = searchResult?["items"]?.AsArray();
                        if (items != null && items.Count > 0)
                        {
                            // Find the student matching email
                            var matchedStudent = items.FirstOrDefault(item => 
                                item?["email"]?.GetValue<string>()?.ToLower() == email.ToLower());
                            
                            if (matchedStudent != null)
                            {
                                int studentIdToLink = matchedStudent["studentId"]!.GetValue<int>();
                                
                                // Call PUT /api/v1/students/{studentId} to link it to this userId
                                var updatePayload = new
                                {
                                    id = studentIdToLink,
                                    userId = userId,
                                    fullName = matchedStudent["fullName"]?.GetValue<string>(),
                                    dateOfBirth = matchedStudent["dateOfBirth"]?.GetValue<string>(),
                                    gender = matchedStudent["gender"]?.GetValue<string>() ?? "Nam",
                                    phone = matchedStudent["phone"]?.GetValue<string>(),
                                    email = matchedStudent["email"]?.GetValue<string>(),
                                    address = matchedStudent["address"]?.GetValue<string>()
                                };
                                
                                var updateRequest = createRequest($"{studentServiceUrl}/api/v1/students/{studentIdToLink}");
                                updateRequest.Method = HttpMethod.Put;
                                updateRequest.Content = JsonContent.Create(updatePayload);
                                
                                var updateResponse = await httpClient.SendAsync(updateRequest);
                                if (updateResponse.IsSuccessStatusCode)
                                {
                                    // Successfully linked! Fetch the student profile again
                                    var refetchRequest = createRequest($"{studentServiceUrl}/api/v1/students/by-user/{userId}");
                                    var refetchResponse = await httpClient.SendAsync(refetchRequest);
                                    if (refetchResponse.IsSuccessStatusCode)
                                    {
                                        profileJson = await refetchResponse.Content.ReadFromJsonAsync<JsonNode>();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log and ignore to fall back to not found
            Console.WriteLine($"Self-healing failed: {ex.Message}");
        }
    }

    if (profileJson == null)
    {
        return Results.NotFound(new { message = "Không tìm thấy hồ sơ học viên" });
    }

    int studentId = 0;
    if (profileJson["studentId"] != null)
    {
        studentId = profileJson["studentId"]!.GetValue<int>();
    }
    else if (profileJson["StudentId"] != null)
    {
        studentId = profileJson["StudentId"]!.GetValue<int>();
    }
    else
    {
        return Results.BadRequest(new { message = "Không tìm thấy StudentId trong hồ sơ học viên" });
    }

    // 2. Fetch the remaining data in parallel
    var paymentsRequest = createRequest($"{paymentServiceUrl}/api/v1/payments/student/{userId}");
    var enrollmentsRequest = createRequest($"{studentServiceUrl}/api/v1/students/{studentId}/enrollments");
    var attendanceRequest = createRequest($"{studentServiceUrl}/api/v1/attendances/student/{studentId}/summary");
    var creditsRequest = createRequest($"{studentServiceUrl}/api/v1/enrollments/student-credits/{studentId}");

    var paymentsTask = httpClient.SendAsync(paymentsRequest);
    var enrollmentsTask = httpClient.SendAsync(enrollmentsRequest);
    var attendanceTask = httpClient.SendAsync(attendanceRequest);
    var creditsTask = httpClient.SendAsync(creditsRequest);

    await Task.WhenAll(paymentsTask, enrollmentsTask, attendanceTask, creditsTask);

    JsonNode? payments = null;
    if (paymentsTask.Result.IsSuccessStatusCode)
    {
        payments = await paymentsTask.Result.Content.ReadFromJsonAsync<JsonNode>();
    }

    JsonNode? enrollments = null;
    if (enrollmentsTask.Result.IsSuccessStatusCode)
    {
        enrollments = await enrollmentsTask.Result.Content.ReadFromJsonAsync<JsonNode>();
    }

    JsonNode? attendance = null;
    if (attendanceTask.Result.IsSuccessStatusCode)
    {
        attendance = await attendanceTask.Result.Content.ReadFromJsonAsync<JsonNode>();
    }

    JsonNode? credits = null;
    if (creditsTask.Result.IsSuccessStatusCode)
    {
        credits = await creditsTask.Result.Content.ReadFromJsonAsync<JsonNode>();
    }

    return Results.Ok(new
    {
        profile = profileJson,
        payments = payments,
        enrollments = enrollments,
        attendanceSummary = attendance,
        creditSummary = credits
    });
}).RequireAuthorization();

app.UseEndpoints(endpoints => { });

await app.UseOcelot();

app.Run();

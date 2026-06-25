$loginBody = @{
  username = "admin"
  password = "admin123"
} | ConvertTo-Json

$loginRes = Invoke-RestMethod -Uri "http://localhost:5000/api/v1/auth/login" -Method Post -Body $loginBody -ContentType "application/json"
$token = $loginRes.token
Write-Host "Token: $token"

$headers = @{
  Authorization = "Bearer $token"
}

try {
  $res = Invoke-RestMethod -Uri "http://localhost:5000/api/v1/lessons/class/6" -Method Get -Headers $headers
  Write-Host "Success GET lessons:"
  Write-Host ($res | ConvertTo-Json)
} catch {
  Write-Host "Failed GET lessons:"
  Write-Host $_.Exception.Message
  if ($_.Exception.Response) {
    $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
    Write-Host "Response body: $($reader.ReadToEnd())"
  }
}

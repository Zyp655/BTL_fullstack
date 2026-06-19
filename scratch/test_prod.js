const https = require('https');

function post(url, data, token) {
  return new Promise((resolve, reject) => {
    const parsedUrl = new URL(url);
    const postData = data ? JSON.stringify(data) : '';
    const options = {
      hostname: parsedUrl.hostname,
      port: parsedUrl.port || 443,
      path: parsedUrl.pathname + parsedUrl.search,
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Content-Length': Buffer.byteLength(postData)
      }
    };
    if (token) {
      options.headers['Authorization'] = `Bearer ${token}`;
    }

    const req = https.request(options, (res) => {
      let body = '';
      res.on('data', (chunk) => body += chunk);
      res.on('end', () => {
        try {
          resolve({
            statusCode: res.statusCode,
            headers: res.headers,
            data: body ? JSON.parse(body) : null
          });
        } catch (e) {
          resolve({
            statusCode: res.statusCode,
            headers: res.headers,
            data: body
          });
        }
      });
    });

    req.on('error', (err) => reject(err));
    req.write(postData);
    req.end();
  });
}

function get(url, token) {
  return new Promise((resolve, reject) => {
    const parsedUrl = new URL(url);
    const options = {
      hostname: parsedUrl.hostname,
      port: parsedUrl.port || 443,
      path: parsedUrl.pathname + parsedUrl.search,
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    };
    if (token) {
      options.headers['Authorization'] = `Bearer ${token}`;
    }

    const req = https.request(options, (res) => {
      let body = '';
      res.on('data', (chunk) => body += chunk);
      res.on('end', () => {
        try {
          resolve({
            statusCode: res.statusCode,
            headers: res.headers,
            data: body ? JSON.parse(body) : null
          });
        } catch (e) {
          resolve({
            statusCode: res.statusCode,
            headers: res.headers,
            data: body
          });
        }
      });
    });

    req.on('error', (err) => reject(err));
    req.end();
  });
}

async function test() {
  const baseUrl = 'https://apigateway-production-424c.up.railway.app';
  try {
    console.log("Logging in as admin...");
    const loginRes = await post(`${baseUrl}/api/v1/auth/login`, {
      username: 'admin',
      password: 'admin123'
    });
    
    if (loginRes.statusCode !== 200) {
      console.error("Login failed:", loginRes.statusCode, loginRes.data);
      return;
    }
    
    const token = loginRes.data.token;
    console.log("Login successful! Token acquired.");
    try {
      const payload = JSON.parse(Buffer.from(token.split('.')[1], 'base64').toString());
      console.log("Token Payload:", payload);
    } catch (err) {
      console.error("Failed to decode token payload:", err);
    }

    console.log("\nTriggering apply-migrations...");
    const applyRes = await post(`${baseUrl}/api/v1/course-evaluations/apply-migrations`, null, token);
    console.log("Response status (apply-migrations):", applyRes.statusCode);
    console.log("Response data (apply-migrations):", JSON.stringify(applyRes.data, null, 2));

  } catch (e) {
    console.error("Error running test:", e);
  }
}

test();

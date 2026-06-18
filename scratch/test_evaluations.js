const http = require('http');

function post(url, data, token) {
  return new Promise((resolve, reject) => {
    const parsedUrl = new URL(url);
    const options = {
      hostname: parsedUrl.hostname,
      port: parsedUrl.port,
      path: parsedUrl.pathname + parsedUrl.search,
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      }
    };
    if (token) {
      options.headers['Authorization'] = `Bearer ${token}`;
    }

    const req = http.request(options, (res) => {
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
    if (data) {
      req.write(JSON.stringify(data));
    }
    req.end();
  });
}

function get(url, token) {
  return new Promise((resolve, reject) => {
    const parsedUrl = new URL(url);
    const options = {
      hostname: parsedUrl.hostname,
      port: parsedUrl.port,
      path: parsedUrl.pathname + parsedUrl.search,
      method: 'GET',
      headers: {}
    };
    if (token) {
      options.headers['Authorization'] = `Bearer ${token}`;
    }

    const req = http.request(options, (res) => {
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
  try {
    console.log("Logging in as admin...");
    const loginRes = await post('http://localhost:5000/api/v1/auth/login', {
      username: 'admin',
      password: 'admin123'
    });
    
    if (loginRes.statusCode !== 200) {
      console.error("Login failed:", loginRes.statusCode, loginRes.data);
      return;
    }
    
    const token = loginRes.data.token;
    console.log("Login successful! Token acquired.");

    console.log("\nFetching all evaluations from Gateway (port 5000)...");
    const evalsRes = await get('http://localhost:5000/api/v1/teacher-evaluations/all', token);
    console.log("Status:", evalsRes.statusCode);
    console.log("Data:", JSON.stringify(evalsRes.data, null, 2));

    console.log("\nFetching directly from Student Service (port 5002)...");
    const directRes = await get('http://localhost:5002/api/v1/teacher-evaluations/all', token);
    console.log("Status:", directRes.statusCode);
    console.log("Data:", JSON.stringify(directRes.data, null, 2));
  } catch (e) {
    console.error("Error running test:", e);
  }
}

test();

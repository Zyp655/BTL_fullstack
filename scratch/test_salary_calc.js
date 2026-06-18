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

    console.log("\nCalculating payroll for March 2026...");
    const calcJuneRes = await post('http://localhost:5000/api/v1/teachers/salary/slips/calculate', {
      month: 3,
      year: 2026
    }, token);

    if (calcJuneRes.statusCode === 200 && Array.isArray(calcJuneRes.data)) {
      console.log("March 2026 - All Teacher Salaries:");
      calcJuneRes.data.forEach(s => {
        console.log(`- ${s.teacherName} (ID ${s.teacherId}): sessionsTaught=${s.sessionsTaught}, totalStudentSessions=${s.totalStudentSessions}, totalAmount=${s.totalAmount} đ`);
      });
    } else {
      console.log("March 2026 Response status:", calcJuneRes.statusCode);
      console.log("March 2026 Response data:", calcJuneRes.data);
    }

    console.log("\nCalculating payroll for July 2026...");
    const calcJulyRes = await post('http://localhost:5000/api/v1/teachers/salary/slips/calculate', {
      month: 7,
      year: 2026
    }, token);

    if (calcJulyRes.statusCode === 200 && Array.isArray(calcJulyRes.data)) {
      console.log("July 2026 - Selected Teacher Salaries:");
      const hoa = calcJulyRes.data.find(s => s.teacherId === 5);
      console.log("Lê Thị Hoa (ID 5):", JSON.stringify(hoa, null, 2));
    } else {
      console.log("July 2026 Response status:", calcJulyRes.statusCode);
      console.log("July 2026 Response data:", calcJulyRes.data);
    }
  } catch (e) {
    console.error("Error running test:", e);
  }
}

test();

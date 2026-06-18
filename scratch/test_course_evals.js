const http = require('http');

function get(url) {
  return new Promise((resolve, reject) => {
    const parsedUrl = new URL(url);
    const options = {
      hostname: parsedUrl.hostname,
      port: parsedUrl.port,
      path: parsedUrl.pathname + parsedUrl.search,
      method: 'GET',
      headers: {}
    };

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
    console.log("Fetching course evaluations for Course 1...");
    const res1 = await get('http://localhost:5000/api/v1/course-evaluations/course/1');
    console.log("Status:", res1.statusCode);
    console.log("Data:", JSON.stringify(res1.data, null, 2));

    console.log("\nFetching course evaluations for Course 3...");
    const res2 = await get('http://localhost:5000/api/v1/course-evaluations/course/3');
    console.log("Status:", res2.statusCode);
    console.log("Data:", JSON.stringify(res2.data, null, 2));
  } catch (e) {
    console.error("Error running test:", e);
  }
}

test();

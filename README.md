# Hệ thống quản lý khóa học & học viên trung tâm

## ĐỀ TÀI 04 — Microservices Architecture

### Tech Stack
- **Backend**: ASP.NET Core 8 Web API
- **Frontend**: VueJS 3 + Vuetify 3 + Pinia
- **Database**: SQL Server (Docker container riêng)
- **Container**: Docker + docker-compose

---

## Course & Schedule Service (N1)

### Chạy với Docker

```bash
# Khởi động service + SQL Server
cd CourseService
docker-compose up -d --build

# Kiểm tra
# Swagger UI: http://localhost:5001/swagger
# API: http://localhost:5001/api/courses
```

### Chạy không Docker (dev)

```bash
# Cần SQL Server chạy trên localhost:1433
cd CourseService
dotnet run

# Swagger: http://localhost:5001/swagger
```

### API Endpoints

| Method | Endpoint | Mô tả |
|--------|----------|-------|
| GET | `/api/courses` | Danh sách khóa học |
| GET | `/api/courses/{id}` | Chi tiết khóa học |
| POST | `/api/courses` | Tạo khóa học |
| PUT | `/api/courses/{id}` | Cập nhật khóa học |
| DELETE | `/api/courses/{id}` | Xóa khóa học |
| GET | `/api/classes` | Danh sách lớp học |
| GET | `/api/classes/{id}` | Chi tiết lớp học |
| POST | `/api/classes` | Mở lớp mới |
| PUT | `/api/classes/{id}` | Cập nhật lớp |
| PUT | `/api/classes/{id}/status` | Đổi trạng thái |
| GET | `/api/classes/{id}/schedules` | Lịch học của lớp |
| POST | `/api/classes/{id}/schedules` | Thêm lịch |

---

## Frontend

```bash
cd Frontend
npm install
npm run dev

# Truy cập: http://localhost:5173
```

---

## Kiến trúc

```
BTL_fullstack/
├── CourseService/          # N1 — Backend + Docker
│   ├── docker-compose.yml
│   ├── Dockerfile
│   ├── Controllers/
│   ├── Models/
│   ├── DTOs/
│   └── Data/
├── Frontend/               # VueJS 3 + Vuetify 3
│   └── src/
└── README.md
```

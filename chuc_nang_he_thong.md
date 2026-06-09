# Bảng Phân Tích Chức Năng và Yêu Cầu Kỹ Thuật Hệ Thống

Dưới đây là bảng tổng hợp cấu trúc các dịch vụ (Microservices), chức năng chi tiết và luồng xử lý kỹ thuật của hệ thống **Quản lý Khóa học & Học viên trung tâm** được xây dựng dựa trên cấu trúc dự án thực tế.

| STT | Nhóm dịch vụ | Chức năng / Tiêu chí chấm điểm | Chi tiết yêu cầu kỹ thuật (Luồng xử lý) |
| :--- | :--- | :--- | :--- |
| **I** | **Nhóm 1 — Course & Schedule** | **Phần chức năng riêng của dịch vụ** | |
| 1 | N1 - Course Service | CRUD Khóa học & Lớp học | Thêm, sửa, xóa (soft delete), xem thông tin chi tiết và danh sách khóa học/lớp học trong hệ thống. |
| 2 | N1 - Course Service | Quản lý trạng thái lớp học | Theo dõi và cập nhật trạng thái lớp học (Mở mới, Đang diễn ra, Tạm dừng, Kết thúc) phục vụ hoạt động vận hành của trung tâm. |
| 3 | N1 - Course Service | Tìm kiếm & lọc nâng cao | **Trọng tâm logic N1:** Xử lý luồng lọc khóa học đa tiêu chí (theo từ khóa tìm kiếm, phân loại theo category, cấp độ level, và trạng thái kích hoạt isActive) có phân trang (pagination). |
| 4 | N1 - Course Service | Quản lý lịch học lớp học | Thiết lập và cập nhật thời gian biểu (thứ trong tuần, giờ bắt đầu/kết thúc, phòng học) cho từng lớp học cụ thể. |
| **II** | **Nhóm 2 — Student & Attendance** | **Phần chức năng riêng của dịch vụ** | |
| 5 | N2 - Student Service | Quản lý hồ sơ học viên | CRUD thông tin cá nhân học viên (Họ tên, ngày sinh, giới tính, thông tin liên lạc, mã UserId liên kết với tài khoản hệ thống). |
| 6 | N2 - Student Service | Đăng ký lớp học (Enrollment) | Tiếp nhận yêu cầu đăng ký vào lớp học, kiểm tra điều kiện sĩ số, ghi nhận trạng thái đăng ký và phát đi sự kiện `StudentEnrolledEvent` phục vụ thanh toán. |
| 7 | N2 - Student Service | Xử lý chuyên cần (Điểm danh) | Điểm danh học viên theo ngày học, hỗ trợ điểm danh hàng loạt (batch) cho cả lớp và thống kê tỷ lệ đi học/nghỉ học (Attendance Summary). |
| 8 | N2 - Student Service | Kết quả học tập & Nhập điểm | **Trọng tâm logic N2:** Xử lý luồng nhập điểm, sửa điểm thi (Exam Results) cho học viên và tự động tính toán tổng kết điểm trung bình lớp học (Class Result Summary) để đánh giá. |
| **III** | **Nhóm 3 — Identity & Payment** | **Phần chức năng riêng của dịch vụ** | |
| 9 | N3 - Identity & Payment Service | Xác thực hệ thống (JWT Auth) | Đăng ký (chỉ Admin), đăng nhập xác thực bằng mật khẩu đã mã hóa, trả về JWT Token và phân quyền người dùng (Admin, GiaoVien, HocVien). |
| 10 | N3 - Identity & Payment Service | Hồ sơ người dùng & Đổi mật khẩu | Lưu trữ thông tin cá nhân của người dùng; xử lý logic lấy thông tin Profile từ token và thực hiện thay đổi mật khẩu an toàn. |
| 11 | N3 - Identity & Payment Service | Quản lý hóa đơn & Thanh toán | Ghi nhận hóa đơn học phí tự động khi có học viên đăng ký lớp học; quản lý lịch sử giao dịch (Transactions) thủ công và theo dõi công nợ (Debts). |
| 12 | N3 - Identity & Payment Service | Tự động thanh toán qua Webhook | **Trọng tâm logic N3:** Tự động phân tích mã hóa đơn từ nội dung chuyển khoản thông qua webhook liên kết ngoài (giả lập cổng Sepay) để tự động ghi nhận thanh toán hóa đơn. |
| 13 | N3 - Identity & Payment Service | Báo cáo & Dashboard Admin | Thu thập và kết xuất dữ liệu thống kê doanh thu theo thời gian (tháng/năm), doanh thu theo khóa học/lớp học, và trực quan hóa dữ liệu trên biểu đồ Dashboard. |
| **IV** | **Kết nối giữa các nhóm** | **Tích hợp hệ thống và đồng bộ luồng dữ liệu** | |
| 14 | Kết nối & Tích hợp | Gọi dịch vụ liên thông (Luồng End-to-End) | **Luồng liên thông qua API Gateway & Message Broker:** <br>- **Bất đồng bộ:** Khi học viên đăng ký lớp học ở N2 (`student-service`), hệ thống sẽ phát event `StudentEnrolledEvent` qua RabbitMQ. <br>- **Đồng bộ:** Dịch vụ N3 (`payment-service`) nhận event, thực hiện gọi API (HttpClient) sang N1 (`course-service`) để lấy thông tin chi phí lớp học, sau đó tự động tạo hóa đơn học phí cho học viên tương ứng. <br>- **API Gateway:** Điều phối định tuyến tập trung toàn bộ các yêu cầu từ Frontend. |

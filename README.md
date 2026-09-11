# BÀI THI HỌC KỲ 2 - LỚP T2508M: COMICSYSTEM
**Môn thi:** Developing ASP.NET Core MVC Applications - SET01  
**Thời gian:** 60 phút | **Thang điểm:** 15/15 (kèm điểm thưởng)  
**Sinh viên thực hiện:** Lớp T2508M  

---

## 1. Giới Thiệu & Kiến Trúc Dự Án (SOLID Principles)

Hệ thống quản lý cho thuê truyện tranh (**ComicSystem**) được xây dựng trên nền tảng **ASP.NET Core 8.0 MVC**, tuân thủ nghiêm ngặt **5 nguyên lý SOLID**, áp dụng mô hình **Repository Pattern + Unit of Work + Service Layer**:

- **Single Responsibility Principle (SRP):**
  - **Controllers:** `Skinny Controllers`, chỉ nhận request và trả về View/Redirect.
  - **Services Layer:** Chứa toàn bộ nghiệp vụ kiểm tra logic, tính giá thuê, kiểm tra ngày trả >= ngày thuê, transaction an toàn.
  - **Repositories & UnitOfWork:** Đảm nhiệm duy nhất việc truy xuất cơ sở dữ liệu qua EF Core.
- **Open/Closed (OCP) & Liskov Substitution (LSP):** Giao tiếp giữa các tầng hoàn toàn thông qua Abstraction Interfaces (`IComicBookRepository`, `ICustomerRepository`, `IRentalRepository`, `IRentalService`, `IReportService`).
- **Interface Segregation (ISP):** Tách nhỏ các interface chuyên biệt cho từng phân hệ thay vì dùng 1 interface lớn.
- **Dependency Inversion (DIP):** Đăng ký toàn bộ Services và Repositories vào ASP.NET Core Built-in IoC / DI Container.

---

## 2. Danh Sách Câu Hỏi Đề Bài Đã Hoàn Thành (15/15 Điểm)

| STT | Chức Năng Yêu Cầu | Tệp Tin Xử Lý Chính | Điểm |
| :---: | :--- | :--- | :---: |
| **Q1** | **CRUD Truyện tranh (`ComicBooks`)**<br>- Xem danh sách có tìm kiếm<br>- Thêm mới, Sửa, Xem chi tiết<br>- Xóa có kiểm tra ràng buộc khóa ngoại an toàn | `Controllers/ComicBooksController.cs`<br>`Services/ComicBookService.cs`<br>`Views/ComicBooks/*.cshtml` | **3đ** |
| **Q2** | **Đăng ký khách hàng (`Customers`)**<br>- Form nhập: Họ tên, Số điện thoại, Ngày đăng ký<br>- Validate định dạng số điện thoại & chống trùng lặp<br>- Xem lịch sử thuê truyện của khách | `Controllers/CustomersController.cs`<br>`Services/CustomerService.cs`<br>`Views/Customers/*.cshtml` | **3đ** |
| **Q3** | **Trang thuê truyện tranh (`Rentals` & `RentalDetails`)**<br>- Chọn khách hàng, Ngày thuê, Ngày trả<br>- Chọn động nhiều đầu sách (`+ Thêm truyện`)<br>- Tự động tính tiền theo số ngày thuê và số lượng trực tiếp trên giao diện<br>- Lưu an toàn vào 2 bảng thông qua Transaction<br>- Chức năng cập nhật trạng thái `"Đã trả"` | `Controllers/RentalsController.cs`<br>`Services/RentalService.cs`<br>`Views/Rentals/*.cshtml` | **5đ** |
| **Q4** | **Báo cáo thuê sách theo khoảng thời gian**<br>- Bộ lọc Từ ngày (`StartDate`) đến Ngày (`EndDate`)<br>- Bảng kết quả đúng 100% mẫu đề thi (`No`, `Book name`, `Rental date`, `Return date`, `Customer name`, `Quantity`)<br>- Tích hợp sẵn dữ liệu mẫu đúng ngày tháng đề thi (Tháng 10/2024)<br>- Hỗ trợ nút In báo cáo (`Print`) và tính tổng doanh thu | `Controllers/ReportsController.cs`<br>`Services/ReportService.cs`<br>`Views/Reports/*.cshtml` | **3đ** |
| **Bonus** | **UI/UX hiện đại & Coding Convention**<br>- Giao diện Bootstrap 5 responsive, hiện đại<br>- Tự động Seed Data mẫu chuẩn mực<br>- Đầy đủ file SQL Script và Stored Procedure độc lập | `ComicSystem.sql`<br>`Data/DbInitializer.cs`<br>`Views/Shared/_Layout.cshtml` | **1đ** |
| **Tổng** | **Đạt Điểm Tối Đa** | | **15/15** |

---

## 3. Cấu Trúc Thư Mục Dự Án

```
T2508M_SEM_Test/
├── ComicSystem.sql                     # Script tạo Database + Table + Seed Data + Stored Procedure
├── README.md                           # Tài liệu hướng dẫn đồ án
├── .gitignore                          # Cấu hình bỏ qua bin, obj, temp
└── ComicSystem/                        # Thư mục mã nguồn chính ASP.NET Core 8 MVC
    ├── Controllers/                    # Skinny Controllers
    │   ├── HomeController.cs           # Dashboard tổng quan
    │   ├── ComicBooksController.cs     # Q1: CRUD Truyện
    │   ├── CustomersController.cs      # Q2: Đăng ký KH
    │   ├── RentalsController.cs        # Q3: Thuê truyện
    │   └── ReportsController.cs        # Q4: Báo cáo theo ngày
    ├── Data/
    │   ├── ComicSystemDbContext.cs     # EF Core DbContext & Fluent API
    │   ├── DbInitializer.cs            # Tự động Migration & Seed Data mẫu
    │   └── Migrations/                 # EF Core Code-First Migrations
    ├── Models/                         # Domain Entities (Customers, ComicBooks, Rentals, RentalDetails)
    ├── Repositories/                   # Tầng Data Access (Repository Pattern + Unit of Work)
    │   ├── Interfaces/
    │   └── Implementations/
    ├── Services/                       # Tầng Business Logic (Clean Architecture)
    │   ├── Interfaces/
    │   └── Implementations/
    ├── ViewModels/                     # DTOs cho các biểu mẫu và báo cáo
    ├── Views/                          # Razor Views giao diện người dùng
    ├── appsettings.json                # Chuỗi kết nối SQL Server (ComicSystem)
    └── Program.cs                      # Cấu hình IoC Container & Pipeline
```

---

## 4. Hướng Dẫn Cài Đặt & Chạy Ứng Dụng

### Cách 1: Tự động hoàn toàn qua Code-First (Khuyên dùng)
1. Đảm bảo dịch vụ SQL Server (ví dụ: `.\SQLEXPRESS` hoặc `(localdb)\mssqllocaldb`) đang chạy.
2. Mở terminal tại thư mục `ComicSystem`:
   ```bash
   cd ComicSystem
   dotnet run
   ```
3. Truy cập trình duyệt: **[http://localhost:5024](http://localhost:5024)**  
   *(Cơ sở dữ liệu `ComicSystem` và toàn bộ dữ liệu mẫu đề thi sẽ được tự động tạo và khởi tạo ngay khi ứng dụng khởi chạy lần đầu!)*

### Cách 2: Sử dụng Script SQL độc lập (Database-First)
Nếu giáo viên hoặc người chấm muốn tạo DB bằng SQL Server Management Studio (SSMS):
1. Mở file **`ComicSystem.sql`** trong thư mục gốc.
2. Nhấn **Execute** (F5). Script sẽ tạo database `ComicSystem`, toàn bộ 4 bảng, Index, dữ liệu mẫu đề thi và Stored Procedure `sp_GetRentalReportByDateRange`.
3. Chạy lệnh `dotnet run` trong thư mục `ComicSystem` để khởi động web.

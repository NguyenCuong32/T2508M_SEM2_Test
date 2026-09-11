# FPT Aptech - Developing ASP.NET Core MVC Applications - SET01

- **Học viên**: Đỗ Khắc Gia Khoa
- **Branch**: `DoKhacGiaKhoa_ACMF`
- **Đề bài**: Developing ASP.NET Core MVC Applications - SET01 (Duration: 60 minutes | Marks: 15)
- **Website**: `comicsys.com`
- **Database**: `ComicSystem` (SQL Server / LocalDB)

---

## 1. Nội dung hoàn thành theo barem chấm điểm (15/15 Marks)

| Câu hỏi | Nội dung | Điểm barem | Trạng thái |
| :--- | :--- | :---: | :---: |
| **Câu 1** | **CRUD Comic Books**: Quản lý sách truyện (Thêm, Xem danh sách, Chi tiết, Chỉnh sửa, Xóa an toàn) vào bảng `ComicBooks` | 3 | **Hoàn thành** |
| **Câu 2** | **Customer Register**: Đăng ký thông tin khách hàng (Họ tên, SĐT, Ngày đăng ký) vào bảng `Customers` | 3 | **Hoàn thành** |
| **Câu 3** | **Rental Book Page**: Lập phiếu thuê sách, chọn truyện, tính tiền, lưu đồng thời vào cả 2 bảng `Rentals` và `RentalDetails` | 5 | **Hoàn thành** |
| **Câu 4** | **Report All Book**: Báo cáo thuê truyện theo khoảng thời gian (`StartDate` đến `EndDate`) hiển thị chuẩn theo bảng mẫu đề thi | 3 | **Hoàn thành** |
| **Bonus** | **UI/UX & Clean Code**: Giao diện Bootstrap 5 hiện đại, Code-First EF Core, Seed data mẫu tự động, Stored Procedure SQL | 1 | **Hoàn thành** |
| **TỔNG** | | **15/15** | |

---

## 2. Cấu trúc Database & Script SQL
File script độc lập tạo toàn bộ CSDL, bảng, khóa ngoại, seed data và Stored Procedure:
- [`ComicSystem.sql`](ComicSystem.sql) (nằm ngay tại thư mục gốc)
- [`Database/ComicSystem.sql`](Database/ComicSystem.sql)

Gồm 4 bảng:
1. `Customers`: `CustomerID`, `FullName`, `PhoneNumber`, `RegistrationDate`
2. `ComicBooks`: `ComicBookID`, `Title`, `Author`, `PricePerDay`
3. `Rentals`: `RentalID`, `CustomerID`, `RentalDate`, `ReturnDate`, `Status`
4. `RentalDetails`: `RentalDetailID`, `RentalID`, `ComicBookID`, `Quantity`, `PricePerDay`

---

## 3. Hướng dẫn chạy đồ án

### Cách 1: Chạy trực tiếp với .NET CLI (Tự động khởi tạo DB & Seed Data mẫu)
```bash
dotnet restore
dotnet build
dotnet run --project ComicSystem.Web/ComicSystem.Web.csproj
```
Truy cập: `http://localhost:5241` hoặc cổng hiển thị trên terminal.
*Lưu ý: Hệ thống sử dụng EF Core tự động tạo DB `ComicSystem` trên `(localdb)\mssqllocaldb` và nạp sẵn dữ liệu mẫu Conan, Doraemon, khách hàng Nguyen Hung đúng theo đề thi.*

### Cách 2: Chạy Database First
1. Mở file `ComicSystem.sql` trong SQL Server Management Studio (SSMS) và thực thi (Execute).
2. Chạy ứng dụng web kết nối đến database `ComicSystem`.

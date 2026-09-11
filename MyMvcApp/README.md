# ComicSystem — bài thuê truyện ASP.NET Core MVC

Code trực tiếp trên project `MyMvcApp` có sẵn, giữ target `net11.0`.
Giao diện Bootstrap có sẵn trong project, không cần cài Node.js.
Database MySQL, dùng Entity Framework Core 9 + Pomelo 9.

## Chạy bài

1. Bật MySQL và Apache trong MAMP/XAMPP.
2. Mở phpMyAdmin, chọn **Import**, chọn file `Database/ComicSystem.sql` rồi chạy.
   Script tạo database `ComicSystem`, 4 bảng và 3 truyện mẫu; không xóa dữ liệu cũ.
   Với MAMP trên máy này: <http://localhost:8888/phpMyAdmin5/>.
3. Sửa `ConnectionStrings:DefaultConnection` trong `appsettings.json` theo MySQL của bạn:

   ```text
   Server=127.0.0.1;Port=3306;Database=ComicSystem;User=root;Password=;
   ```

   Trên máy hiện tại, kết nối MAMP đã được lưu bằng .NET User Secrets để chạy ngay ở
   môi trường Development, không đưa mật khẩu vào source. User Secrets ưu tiên hơn
   `appsettings.json`; nếu đổi kết nối, có thể cập nhật bằng lệnh:

   ```powershell
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=127.0.0.1;Port=3306;Database=ComicSystem;User=root;Password=MAT_KHAU_CUA_BAN;"
   ```

4. Trong thư mục `MyMvcApp`, chạy:

   ```powershell
   dotnet restore
   dotnet run --launch-profile http
   ```

5. Mở <http://localhost:5050>. Máy khác cần .NET SDK hỗ trợ `net11.0` như project gốc.

## Cấu trúc để review

```text
MyMvcApp/
├── Controllers/       Nhận request, kiểm tra ModelState, gọi Service
├── Data/              AppDbContext: ánh xạ 4 bảng và khóa ngoại
├── Database/          ComicSystem.sql: import bằng phpMyAdmin
├── Migrations/        Migration ban đầu của Entity Framework
├── Models/            ComicBook, Customer, Rental, RentalDetail
├── Repositories/      ComicRepository: đọc/ghi MySQL bằng EF Core
├── Services/          ComicService: xử lý nghiệp vụ
├── ViewModels/        Dữ liệu form thuê và báo cáo
├── Views/             Razor + Bootstrap
├── wwwroot/           CSS, JavaScript, thư viện có sẵn
├── appsettings.json   Cấu hình kết nối
└── Program.cs         Đăng ký DbContext, Repository, Service
```

Luồng xử lý: **View → Controller → Service → Repository → AppDbContext → MySQL**.
Một Repository và một Service xử lý 4 bảng để bài ngắn, dễ theo dõi.

## Các yêu cầu trong đề

| Yêu cầu | Trang | File chính |
|---|---|---|
| Thêm, xem, sửa, xóa truyện | `/ComicBooks` | `ComicBooksController.cs` |
| Đăng ký khách: họ tên, điện thoại, ngày đăng ký | `/Customers` | `CustomersController.cs` |
| Thuê một hoặc nhiều truyện: ngày thuê/trả, số lượng, giá/ngày | `/Rentals` | `RentalsController.cs` |
| Báo cáo STT, tên truyện, ngày thuê/trả, khách, số lượng | `/Rentals/Report` | `RentalsController.cs` |

- Chọn khách đã đăng ký, tích truyện và nhập số lượng khi lập phiếu.
- Giá/ngày lấy từ bảng truyện và lưu riêng vào `RentalDetails`; sửa giá truyện sau này
  không đổi giá trên phiếu đã lập. Cả phiếu và các dòng chi tiết lưu trong một transaction.
- Ngày trả phải từ ngày thuê trở đi, số lượng và giá phải dương.
- Báo cáo lọc **ngày thuê**, bao gồm cả ngày bắt đầu và ngày kết thúc.
- Chỉ xóa được truyện chưa có trong phiếu thuê để giữ lịch sử.
- Không cần đăng nhập; đăng ký khách là ghi thông tin khách theo đề.

## Về migrations

Cách chạy đơn giản là import SQL như trên. Thư mục `Migrations` lưu bản tạo schema
tương ứng để tham khảo. Nếu muốn dùng `dotnet ef database update` thay SQL, hãy dùng
database mới, trống và công cụ `dotnet-ef` phiên bản 9.0.18. Không chạy migration tạo
bảng trên database đã import SQL. Dữ liệu truyện mẫu chỉ có trong file SQL.

Tài liệu provider: <https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql>.

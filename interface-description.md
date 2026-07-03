# UI/UX Interface Description: Tree Shop

Tài liệu này mô tả chi tiết giao diện người dùng (UI/UX) của ứng dụng **Tree Shop** dựa trên file đề bài thực hành `03_07_2026___28492af4-44fe-47b3-b780-a9f6ad3a731e.pdf`. Tài liệu này đóng vai trò làm đặc tả thiết kế giúp lập trình viên phát triển giao diện chính xác và đồng bộ.

---

## 1. Bố cục Tổng thể (Layout Structure)

Giao diện ứng dụng được thiết kế trên một trang web duy nhất (Single Page) với bố cục chia thành 3 phần chính từ trên xuống dưới:
- **Header**: Thanh điều hướng dùng chung (Navigation Bar).
- **Body**: Khu vực hiển thị nội dung chính bao gồm:
  - Tiêu đề trang (Page Title).
  - Phần nhập liệu và xem trước ảnh (Preview & Form Input) chia làm 2 cột.
  - Phần hiển thị danh sách cây (Tree List Table).
- **Footer**: Thanh chân trang dùng chung chứa thông tin liên hệ.

---

## 2. Chi tiết các Thành phần Giao diện (UI Components)

### 2.1. Header (Thanh điều hướng)
- **Kiểu dáng**: Dạng thanh ngang chiếm toàn bộ chiều rộng màn hình (Full-width container).
- **Màu nền**: Màu xanh rêu xám đặc trưng (Mã màu gợi ý: `#7b9787` hoặc `#8A9A86`).
- **Nội dung điều hướng**:
  - Bên trái: Chữ **"Tree Shop"** màu trắng, in nghiêng (`italic`). Liên kết tới trang chủ (`/`).
  - Bên phải: Chữ **"About me"** màu xám sáng hoặc trắng nhạt. Liên kết tới trang giới thiệu bản thân (`/about`).
- **Khoảng đệm (Padding)**: Khoảng `10px` đến `15px` ở cạnh trên và dưới.

### 2.2. Component Quản lý Cây (Tree Management Card - Glassmorphism)
Toàn bộ phần tiêu đề trang, khung xem trước hình ảnh (Preview) và form nhập liệu được đưa vào **một khối component duy nhất** (Form Card). 

- **Kiểu dáng & Nền mờ (Glassmorphism)**:
  - Nền của component này sử dụng hiệu ứng kính mờ (Glassmorphism) để đồng bộ với hiệu ứng của bảng danh sách bên dưới, làm lộ hình nền nhà kính ẩn hiện phía sau.
  - CSS gợi ý cho nền mờ:
    ```css
    .form-card {
      background: rgba(255, 255, 255, 0.45); /* Nền trắng bán trong suốt */
      backdrop-filter: blur(12px); /* Hiệu ứng làm mờ nền phía sau */
      -webkit-backdrop-filter: blur(12px);
      border: 1px solid rgba(255, 255, 255, 0.25); /* Viền kính mờ */
      border-radius: 12px;
      padding: 24px 32px;
      box-shadow: 0 8px 32px 0 rgba(31, 38, 135, 0.08);
      margin-bottom: 32px;
    }
    ```
- **Bố cục bên trong (Inner Layout)**:
  - Phía trên cùng: Tiêu đề phụ **"Tree Shop"** (màu xanh rêu đậm, in nghiêng, cỡ chữ trung bình) được căn giữa.
  - Phía dưới tiêu đề là bố cục lưới chia làm 2 cột:
    - **Cột trái (Khung xem trước ảnh - Image Preview)**:
      - Khung hình chữ nhật bo góc, nền mờ đục hơn (`rgba(255, 255, 255, 0.3)` kèm `backdrop-filter: blur(8px)`) để làm nổi bật thông báo.
      - Chứa dòng chữ thông báo căn giữa: *"Chưa có ảnh xem trước"* (màu chữ xám đậm `#7a6f5d`).
    - **Cột phải (Form nhập liệu - Add Tree Form)**:
      1. **Tree Name** (Tên cây):
         - Label gồm 2 phần nằm ngang: `Tree Name` (bên trái) và bộ đếm độ dài `0/100` (bên phải, màu xám nhạt).
         - Ô nhập (Input Text) có nền màu trắng tinh, viền bo góc nhẹ.
      2. **Description** (Mô tả):
         - Label gồm 2 phần nằm ngang: `Description` (bên trái) và bộ đếm độ dài `0/500` (bên phải).
         - Ô nhập văn bản nhiều dòng (Textarea) có nền trắng, viền bo góc nhẹ.
      3. **Image** (Đường dẫn ảnh):
         - Label `Image`. Dưới label là dòng mô tả nhỏ màu xám nhạt: *"Chọn ảnh sẽ mở công cụ cắt ảnh theo tỷ lệ 1:1, 2:3 hoặc 3:2"*.
         - Cấu trúc: Một ô nhập text hiển thị đường dẫn ảnh (ví dụ: `D:\image\phonglan.png`) nằm cạnh nút **"Browser"**.
         - Nút **"Browser"**: Nền màu gỗ nhạt `#f5efe6` hoặc `#e8dfd1`, chữ màu nâu gỗ đậm, viền bo góc nhẹ.
         - **Thanh đo kích thước ảnh (Size Meter)**: Nằm ngay dưới ô nhập Image, hiển thị thanh tiến trình dạng thanh ngang (chỉ báo dung lượng/chất lượng ảnh) kèm theo nhãn góc phải hiển thị trạng thái (ví dụ: *"Chưa có ảnh"*, *"Quá dung lượng"*, v.v.).
      4. **Các nút hành động (Form Actions)**:
         - Căn chỉnh: Căn lề phải dưới form.
         - Nút **"Add"**: Nền màu xanh lục/rêu đậm (`#4a7c59`), chữ trắng, bo góc tròn.
         - Nút **"Reset"**: Nền màu đỏ gạch/hồng cam (`#a44a3f`), chữ trắng, bo góc tròn.

---

### 2.4. Bảng Danh sách Cây (Tree List Table)
Nằm phía dưới khu vực Form nhập liệu, hiển thị toàn bộ danh sách các loại cây trong cơ sở dữ liệu.

- **Tiêu đề bảng (Table Header)**:
  - Màu nền: Trùng màu xanh rêu của Header (`#7b9787`).
  - Chữ tiêu đề: Màu trắng, căn lề trái hoặc căn giữa tùy cột.
  - Các cột tiêu đề: `Id`, `Name`, `Image`, `Description`.
- **Nội dung bảng (Table Body)**:
  - **Id**: Số thứ tự tăng dần từ 1 (ví dụ: 1, 2, 3, 4...).
  - **Name**: Tên cây (ví dụ: `PhongLan`).
  - **Image**: Ảnh thu nhỏ (Thumbnail) của cây, kích thước nhỏ gọn (khoảng `50px` x `50px` đến `80px` x `80px`), căn giữa cột.
  - **Description**: Đoạn văn bản mô tả đầy đủ của cây.
  - **Cột thao tác (Actions)**: Không có tiêu đề trên Header bảng, chứa hai nút chức năng:
    - Biểu tượng **Bút chì (Edit)**: Màu xanh lam (ví dụ: `#6495ED`), dùng để chỉnh sửa thông tin cây.
    - Biểu tượng **Thùng rác (Delete)**: Màu xanh lam (hoặc đỏ nhạt để cảnh báo), dùng để xóa bản ghi cây đó.
- **Đường kẻ bảng (Table Borders)**: Sử dụng các đường kẻ ngang mảnh màu xám nhạt để phân tách các hàng, không dùng đường kẻ dọc để tạo sự thông thoáng.

---

### 2.5. Footer (Chân trang)
- **Kiểu dáng**: Dạng thanh ngang chiếm toàn bộ chiều rộng màn hình.
- **Màu nền**: Màu xanh rêu trùng khớp với Header (`#7b9787`).
- **Nội dung chữ**: Chữ màu trắng, căn lề trái, hiển thị địa chỉ:
  - **"Số 8, Tôn Thất Thuyết, Cầu Giấy, Hà Nội"**
- **Khoảng đệm (Padding)**: Khoảng `10px` đến `15px` ở cạnh trên và dưới.

---

## 3. Màu sắc và Kiểu dáng (Color Palette & Styling)

Dưới đây là bảng mã màu gợi ý dựa trên giao diện mẫu trong PDF nhằm đạt điểm tối đa cho phần **Bonus: good UI/UX (2 điểm)**:

| Thành phần | Mã màu gợi ý | Tên màu / Ý nghĩa |
| :--- | :--- | :--- |
| **Primary Theme (Header/Footer/Table Header)** | `#7b9787` | Sage Green (Xanh rêu xám dịu nhẹ, tạo cảm giác thiên nhiên) |
| **Secondary (Nút Add)** | `#6e8979` | Darker Sage Green (Xanh rêu đậm cho hành động tích cực) |
| **Danger (Nút Reset)** | `#b05f63` | Brick Red (Đỏ gạch, dùng cho hành động xóa dữ liệu) |
| **Background (Nền trang)** | `#fdfdfd` hoặc `#f5f6f5` | Off-white (Màu trắng ngà hoặc xám siêu nhạt để giảm độ chói) |
| **Borders & Grid lines** | `#cccccc` hoặc `#e0e0e0` | Light Gray (Màu xám nhạt cho viền ô nhập và đường kẻ bảng) |
| **Icons (Edit / Delete)** | `#3b82f6` hoặc `#6495ed` | Cornflower Blue (Xanh lam dịu cho các tác vụ phụ trợ) |

### Gợi ý font chữ:
Nên sử dụng font chữ sans-serif hiện đại như **'Inter'**, **'Roboto'**, hoặc **'Segoe UI'** thay cho font mặc định của trình duyệt để nâng cao trải nghiệm giao diện người dùng.

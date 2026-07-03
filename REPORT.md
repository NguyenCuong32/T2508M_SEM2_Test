# Báo cáo bài làm: Tree Shop - Practice Essentials of NodeJS - SET01

Họ tên: Đỗ Khắc Gia Khoa
Đề bài: [REQUIREMENTS.md](./REQUIREMENTS.md)
Kế hoạch triển khai: [PLAN.md](./PLAN.md)

## 1. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Backend | Node.js, Express.js |
| Template engine | EJS |
| Database | MongoDB (Mongoose ODM) |
| Style | CSS thuần (public/css/style.css) |

## 2. Cấu trúc dự án

```
app.js                      # Entry point: cấu hình Express, EJS, kết nối MongoDB
models/Tree.js               # Schema Mongoose: treename, description, image
routes/treeRoutes.js         # Định nghĩa route: /, /add, /reset, /about
views/
  partials/header.ejs        # Header dùng chung (menu TreeShop / About me)
  partials/footer.ejs        # Footer dùng chung (địa chỉ)
  index.ejs                  # Trang TreeShop: form thêm cây + danh sách
  about.ejs                  # Trang About me
public/css/style.css         # Giao diện (card, gradient, responsive)
.env                         # Cấu hình MONGO_URI, PORT (mặc định local)
```

## 3. Database

- Database: `TreeShop`
- Collection: `TreeCollection`
- Schema:
  - `treename: String` (bắt buộc)
  - `description: String` (bắt buộc)
  - `image: String` (không bắt buộc)

## 4. Đối chiếu với thang điểm

| # | Yêu cầu | Điểm | Trạng thái | Vị trí code |
|---|---|---|---|---|
| Q1 | Thêm mới cây (Create) | 5đ | ✅ Hoàn thành | `routes/treeRoutes.js` (POST `/add`) |
| Q2 | Header dùng chung (include) | 2đ | ✅ Hoàn thành | `views/partials/header.ejs`, include trong `index.ejs`, `about.ejs` |
| Q3 | Footer dùng chung (include) | 1đ | ✅ Hoàn thành | `views/partials/footer.ejs` |
| Q4 | Hiển thị toàn bộ danh sách cây | 3đ | ✅ Hoàn thành | `routes/treeRoutes.js` (GET `/`), `views/index.ejs` |
| Q5 | Reset - xóa toàn bộ dữ liệu | 1đ | ✅ Hoàn thành | `routes/treeRoutes.js` (POST `/reset`, `Tree.deleteMany({})`) |
| Q6 | Validate dữ liệu bắt buộc | 1đ | ✅ Hoàn thành | `routes/treeRoutes.js` (kiểm tra `treename`, `description` trước khi lưu) |
| Bonus | UI/UX | 2đ | ✅ Đã đầu tư | `public/css/style.css`: card layout, gradient header/footer, form focus state, bảng responsive |

**Tổng:** 15/15 điểm theo thang điểm + bonus UI/UX.

## 5. Mô tả chức năng chi tiết

1. **Trang TreeShop (`/`)**: hiển thị form thêm cây (Tree Name, Description, Image kèm nút Browse chọn file) và bảng danh sách toàn bộ cây (Id, Image, Name, Description, nút Sửa/Xóa — hiện là placeholder UI, không nằm trong thang điểm bắt buộc).
2. **Thêm cây (`POST /add`)**: kiểm tra `treename` và `description` không được rỗng; nếu thiếu, render lại trang kèm thông báo lỗi và giữ nguyên dữ liệu đã nhập; nếu hợp lệ, lưu vào MongoDB và redirect về `/`.
3. **Reset (`POST /reset`)**: xóa toàn bộ document trong `TreeCollection`, có xác nhận (`confirm`) trước khi thực hiện trên giao diện.
4. **Trang About me (`/about`)**: giới thiệu thông tin tác giả và công nghệ sử dụng, dùng chung header/footer với trang TreeShop.
5. **Header/Footer dùng chung**: sử dụng `<%- include('partials/...') %>` của EJS để tái sử dụng giữa 2 trang.

## 6. Hướng dẫn chạy dự án

Xem chi tiết tại [README.md](./README.md). Tóm tắt:

```bash
npm install
npm start
```

Yêu cầu MongoDB chạy sẵn tại `mongodb://127.0.0.1:27017` (mặc định, không cần cấu hình thêm — `.env` đã commit sẵn giá trị local).

## 7. Chức năng bổ sung (ngoài thang điểm)

- **Sửa (Edit) cây**: đã cài đặt đầy đủ logic. Nút "Sửa" trong bảng danh sách dẫn tới `GET /edit/:id` (`views/edit.ejs`, có validate giống form thêm mới); submit form gọi `POST /edit/:id` để cập nhật document trong `TreeCollection` rồi redirect về `/`.
- **Xóa (Delete) từng cây**: nút "Xóa" trong bảng danh sách nằm trong form riêng, submit `POST /delete/:id` (có `confirm` trước khi xóa) gọi `Tree.findByIdAndDelete` rồi redirect về `/`.

Xem chi tiết tại `routes/treeRoutes.js`.

- **Crop ảnh khi thêm/sửa cây**: khi chọn file ảnh qua nút "Browser", một modal cắt ảnh (`views/partials/crop-modal.ejs`, logic tại `public/js/image-crop.js`) hiện ra cho phép chọn 1 trong 3 tỉ lệ **1:1**, **2:3**, **3:2**, kéo để dịch chuyển và zoom để chỉnh vùng cắt, sau đó xuất ảnh đã crop bằng `<canvas>` thành data URL lưu vào trường `image`; tỉ lệ đã chọn lưu kèm trong trường `imageRatio` (`models/Tree.js`). Khung xem trước (preview) và cột Image trong bảng danh sách đọc `imageRatio` để hiển thị đúng tỉ lệ ảnh đã cắt (`aspect-ratio` CSS động theo từng cây).

## 8. Giới hạn / Ghi chú

- Trường **Image** lưu dưới dạng đường dẫn text (nếu gõ tay) hoặc data URL ảnh đã crop (nếu chọn file qua công cụ cắt ảnh) — không upload file thật lên server/ổ đĩa, ảnh crop được nhúng trực tiếp trong document MongoDB.

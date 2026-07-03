# Plan: Tree Shop (Practice Essentials of NodeJS - SET01)

Nguồn: REQUIREMENTS.md
Công nghệ: Express, EJS, MongoDB (Mongoose)

## 1. Cấu trúc thư mục

```
/
├── app.js                      # Entry point, kết nối MongoDB, cấu hình Express + EJS
├── package.json
├── .env                        # MONGO_URI, PORT
├── models/
│   └── Tree.js                 # Schema: treename, description, image
├── routes/
│   └── treeRoutes.js           # GET /, POST /add, POST /reset
├── views/
│   ├── partials/
│   │   ├── header.ejs          # Menu: TreeShop | About me
│   │   └── footer.ejs          # Địa chỉ dùng chung
│   ├── index.ejs                # Trang TreeShop: form + danh sách
│   └── about.ejs                # Trang About me
└── public/
    └── css/
        └── style.css            # UI/UX (bonus)
```

## 2. Database

- Database: `TreeShop`
- Collection: `TreeCollection`
- Schema (models/Tree.js):
  - `treename: { type: String, required: true }`
  - `description: { type: String, required: true }`
  - `image: { type: String }`

## 3. Routes / Chức năng

| Route | Method | Chức năng | Điểm |
|---|---|---|---|
| `/` | GET | Lấy toàn bộ cây từ DB, render `index.ejs` với danh sách | Q4 (3đ) |
| `/add` | POST | Validate `treename` & `description` bắt buộc → lưu cây mới vào MongoDB → redirect `/` | Q1 (5đ), Q6 (1đ) |
| `/reset` | POST | Xóa toàn bộ document trong `TreeCollection` → redirect `/` | Q5 (1đ) |
| `/about` | GET | Render `about.ejs` (dùng chung header/footer) | Q2, Q3 |

Validation (Q6): nếu thiếu `treename` hoặc `description` → render lại `index.ejs` kèm thông báo lỗi, không lưu DB.

## 4. Views

- **header.ejs**: nav gồm 2 link `/` (TreeShop) và `/about` (About me) — include vào cả 2 trang qua `<%- include('partials/header') %>`.
- **footer.ejs**: hiển thị "Số 8, Tôn Thất Thuyết, Cầu Giấy, Hà Nội" — include vào cả 2 trang.
- **index.ejs**:
  - Form thêm cây: input Tree Name, textarea Description, input text Image (kèm nút chọn file — dùng `<input type="file">` hiển thị, lưu đường dẫn/tên file vào ô text), nút Add (submit) và Reset (nút xóa toàn bộ dữ liệu DB, gọi POST `/reset`).
  - Danh sách cây dạng bảng: cột Id, Name, Image, Description, nút Sửa/Xóa (Sửa/Xóa để phục vụ bonus UI, không bắt buộc theo thang điểm — có thể để nút disabled/placeholder nếu không đủ thời gian).
- **about.ejs**: nội dung giới thiệu ngắn, có header + footer.

## 5. Việc cần làm (thứ tự triển khai)

1. Khởi tạo `package.json`, cài `express`, `mongoose`, `ejs`, `dotenv`, `method-override` (nếu cần PUT/DELETE cho Sửa/Xóa).
2. Viết `app.js`: cấu hình view engine EJS, static `public/`, body-parser (express.urlencoded), kết nối Mongoose tới `TreeShop`.
3. Viết `models/Tree.js`.
4. Viết `routes/treeRoutes.js` với các route ở mục 3.
5. Viết `views/partials/header.ejs`, `footer.ejs`.
6. Viết `views/index.ejs` (form + bảng danh sách + hiển thị lỗi validate).
7. Viết `views/about.ejs`.
8. Viết `public/css/style.css` cho UI/UX (bonus 2đ).
9. Test thủ công: thêm cây, kiểm tra validate khi bỏ trống field, xem danh sách, reset xóa toàn bộ, chuyển trang About me.

## 6. Ghi chú

- Ưu tiên hoàn thành đúng thứ tự thang điểm: Add (5đ) → Show all (3đ) → Header (2đ) → Footer/Reset/Validate (1đ mỗi mục) → UI/UX bonus.
- Giới hạn thời gian 60 phút nên giữ code tối giản, không thêm tính năng ngoài yêu cầu (Sửa cây có thể bỏ qua nếu thiếu thời gian vì không có trong thang điểm).

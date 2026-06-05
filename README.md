# Hệ Thống Quản Lý Thư Viện Sách Song Ngữ
### Software Requirements Specification (SRS)
**Môn học:** Phát triển Ứng dụng Web với Python/Django  
**Lớp:** T2508M — Học kỳ 2  
**Sinh viên:** Đỗ Khắc Gia Khoa  
**Ngày:** 05/06/2026

---

## ⚡ Review nhanh cơ sở dữ liệu (Database Quick Review)

> Dự án đã bao gồm sẵn file `db.sqlite3` với đầy đủ **12 sách mẫu** và **tài khoản quản trị viên** phục vụ mục đích kiểm thử và review nhanh dữ liệu.

### Bước 1: Khởi chạy dự án

```bash
# Tạo và kích hoạt virtual environment
python -m venv venv
.\venv\Scripts\activate        # Windows
# source venv/bin/activate     # macOS/Linux

# Cài đặt thư viện
pip install django pillow

# Khởi chạy local server
python manage.py runserver
```

Truy cập hệ thống tại: **http://127.0.0.1:8000**

### Tài khoản thử nghiệm có sẵn

| Tài khoản | Username | Password | Vai trò |
|---|---|---|---|
| Quản trị viên | `admin` | `Admin@1234` | Superuser |

> Có thể đăng ký thêm tài khoản mới trực tiếp trên giao diện tại `/accounts/register/`

### Dữ liệu mẫu đã nạp

- **12 cuốn sách** đầy đủ tiêu đề, tác giả và ảnh bìa.
- **3 cuốn sách có giá trên 100 USD** (phù hợp với các điều kiện lọc trong yêu cầu nghiệp vụ).
- Dữ liệu hoàn toàn sạch, phục vụ việc review nhanh các tính năng và database.
- **4 sách giá dưới 100.000 ₫** (đáp ứng yêu cầu bộ lọc VND)
- Tất cả sách có thể xem tại **http://127.0.0.1:8000**

---



## 1. Giới thiệu tổng quan

### 1.1 Mục đích
Tài liệu này mô tả các yêu cầu chức năng và phi chức năng của hệ thống **Quản Lý Thư Viện Sách Song Ngữ** — một ứng dụng web được xây dựng bằng Django Framework, cho phép người dùng quản lý danh sách sách với thông tin hiển thị song ngữ (Tiếng Anh và Tiếng Việt) cùng hỗ trợ hiển thị giá theo hai đơn vị tiền tệ USD và VND.

### 1.2 Phạm vi hệ thống
Hệ thống bao gồm hai module chính:
- **Module Xác thực (Accounts):** Đăng ký, đăng nhập, quên mật khẩu, dashboard cá nhân.
- **Module Quản lý Sách (Books):** Hiển thị, thêm mới, sắp xếp danh sách sách.

### 1.3 Công nghệ sử dụng
| Thành phần | Công nghệ |
|---|---|
| Backend Framework | Django 6.0.6 |
| Database | SQLite3 |
| Frontend | HTML5, CSS3, Vanilla JavaScript |
| Font | Google Fonts — Inter |
| Authentication | Django Built-in Auth |
| File Upload | Django ImageField / Pillow |

---

## 2. Yêu cầu chức năng

### 2.1 Module Xác thực (Accounts)

#### UC-01: Đăng ký tài khoản
- Người dùng nhập **Username**, **Password** (≥ 8 ký tự, phải có ít nhất 1 chữ số), **Confirm Password**.
- Người dùng chọn **Security Question** và nhập **Security Answer** phục vụ chức năng quên mật khẩu.
- Hệ thống kiểm tra username không trùng lặp trong cơ sở dữ liệu.
- Sau khi đăng ký thành công, redirect về trang đăng nhập.

#### UC-02: Đăng nhập
- Người dùng nhập Username và Password.
- Hệ thống xác thực qua Django Authentication.
- Đăng nhập thành công → redirect về `/accounts/dashboard/`.
- Đăng nhập thất bại → hiển thị thông báo lỗi, ở lại trang login.

#### UC-03: Quên mật khẩu
- **Bước 1:** Người dùng nhập username.
- **Bước 2:** Hệ thống hiển thị câu hỏi bảo mật đã đăng ký.
- **Bước 3:** Người dùng nhập đúng câu trả lời và mật khẩu mới.
- Sau khi đặt lại thành công → redirect về trang đăng nhập.

#### UC-04: Dashboard cá nhân
- Chỉ truy cập được khi **đã đăng nhập** (`@login_required`).
- Hiển thị thông tin: badge vai trò (Superuser / Staff / Member), ngày tham gia.
- Hiển thị thống kê thư viện: tổng số sách, số tác giả, giá trung bình, sách > $100, sách < 100.000 ₫.
- Hiển thị 3 sách mới nhất (Recent Additions) với ảnh bìa và giá.
- Hiển thị Price Highlights: sách đắt nhất và rẻ nhất.
- Hiển thị Quick Actions: các liên kết điều hướng nhanh.

#### UC-05: Đăng xuất
- Người dùng click "Logout" → hệ thống xóa session → redirect về trang đăng nhập.

---

### 2.2 Module Quản lý Sách (Books)

#### UC-06: Xem danh sách sách
- Hiển thị toàn bộ sách trong cơ sở dữ liệu dưới dạng card grid.
- Mỗi card sách hiển thị: ảnh bìa, tiêu đề tiếng Anh, tiêu đề tiếng Việt, tác giả, giá, ngày xuất bản.
- Hiển thị 3 stat cards: Tổng số sách, Số sách > $100, Số sách > 100.000 ₫.
- **Không yêu cầu đăng nhập** để xem danh sách.

#### UC-07: Chuyển đổi tiền tệ (USD ↔ VND)
- Toggle button "USD ($)" và "VND (₫)" cho phép chuyển đổi hiển thị giá.
- Lựa chọn được lưu vào `localStorage` để duy trì giữa các lần truy cập.
- Giá USD hiển thị dạng `$XX.XX` (luôn có 2 chữ số thập phân).
- Giá VND hiển thị dạng `X,XXX,XXX ₫` (dùng `intcomma` filter, ký hiệu Unicode ₫).

#### UC-08: Sắp xếp sách
Người dùng chọn từ dropdown, URL thay đổi query string `?sort=`:

| Tùy chọn | Query | Logic |
|---|---|---|
| Newest (mặc định) | `default` | `order_by('-created_at')` |
| Title (A–Z) | `alphabetical` | `order_by('title_en')` |
| Price (Low to High) | `price_asc` | `order_by('price_usd')` |
| Price (High to Low) | `price_desc` | `order_by('-price_usd')` |

#### UC-09: Thêm sách mới
- **Yêu cầu đăng nhập.** Nếu chưa đăng nhập → hiển thị nút "Sign In to Add Book".
- Form nhập: Tiêu đề tiếng Anh, Tiêu đề tiếng Việt, Tác giả, Giá USD, Giá VND, Ảnh bìa (tùy chọn), Ngày xuất bản.
- Sau khi thêm thành công → redirect và hiển thị thông báo xanh.

---

## 3. Yêu cầu phi chức năng

### 3.1 Bảo mật
- Tất cả form sử dụng **Django CSRF token**.
- Password được lưu dưới dạng **hash** (Django built-in `create_user`).
- Trang Dashboard và chức năng thêm sách được bảo vệ bằng `@login_required`.
- Câu trả lời bảo mật lưu dạng **lowercase** (case-insensitive).

### 3.2 Giao diện
- Thiết kế **Dark Mode** với theme thư viện (màu mahogany, amber, forest green).
- Hiệu ứng **Glassmorphism** cho card components.
- Font chữ: **Inter** (Google Fonts).
- Responsive layout với CSS Grid.
- Hover animations và micro-interactions cho book cards.

### 3.3 Dữ liệu mẫu
Hệ thống được khởi tạo với **12 sách** gồm:
- **3 sách giá trên $100 USD** (sách học thuật thực tế):
  - *The Art of Computer Programming* — Donald E. Knuth ($189.99)
  - *Computer Networks* — Andrew S. Tanenbaum ($109.99)
  - *Artificial Intelligence: A Modern Approach* — Russell & Norvig ($104.99)
- **4 sách giá dưới 100.000 ₫** (kiểm tra bộ lọc VND):
  - To Kill a Mockingbird, The Hobbit, Soft Skills, Don't Make Me Think
- **Các sách còn lại** có giá thực tế từ $10 – $90.

---

## 4. Cấu trúc dự án

```
django_assignment/
├── accounts/               # Module xác thực người dùng
│   ├── models.py           # UserProfile (security question)
│   ├── views.py            # Login, Register, Dashboard, Forgot Password
│   ├── forms.py            # UserRegisterForm, ForgotPasswordForm
│   └── templates/accounts/ # HTML templates
├── books/                  # Module quản lý sách
│   ├── models.py           # Book model (song ngữ, dual price)
│   ├── views.py            # Danh sách + thêm sách
│   ├── forms.py            # BookForm
│   └── templates/books/    # index.html
├── static/css/             # style.css (global stylesheet)
├── templates/              # base.html (layout chung)
├── media/covers/           # Ảnh bìa sách được upload
└── manage.py
```

---

## 5. Hướng dẫn cài đặt và chạy

```bash
# 1. Tạo và kích hoạt môi trường ảo
python -m venv venv
.\venv\Scripts\activate        # Windows

# 2. Cài đặt thư viện
pip install django pillow

# 3. Chạy migration
python manage.py migrate

# 4. Tạo superuser (tùy chọn)
python manage.py createsuperuser

# 5. Khởi động server
python manage.py runserver
# Truy cập: http://127.0.0.1:8000
```

---

## 6. Mô hình dữ liệu (ERD tóm tắt)

```
User (Django built-in)
 └── UserProfile (1-1)
       ├── security_question: CharField (choices)
       └── security_answer:   CharField

Book
 ├── title_en:    CharField
 ├── title_vi:    CharField
 ├── author:      CharField
 ├── price_usd:   DecimalField (10,2)
 ├── price_vnd:   DecimalField (12,0)
 ├── cover_image: ImageField
 ├── info_link:   URLField
 └── created_at:  DateField
```

# Giải Đề Thi Thực Hành SEM 2 - Lớp T2508M

Repository này chứa lời giải hoàn chỉnh và hệ thống kiểm thử tự động (Unit Tests) chất lượng cao cho hai bài tập trong đề thi thực hành học kỳ 2 lớp T2508M:
1. **Bài tập 1**: Chương trình quản lý điểm sinh viên (Hướng đối tượng - OOP).
2. **Bài tập 2**: Chương trình quản lý kho hàng sản phẩm (Thao tác file CSV sử dụng thư viện `pandas`).

Dự án được xây dựng với tư duy thiết kế tối ưu, có khả năng chống crash cao (Anti-Crash input validation) và định dạng kết xuất dữ liệu trực quan chuyên nghiệp.

---

## 📁 Cấu Trúc Dự Án

* 📄 [exercise-1.py](file:///d:/Github/T2508M_SEM2_PP/exercise-1.py) - File mã nguồn Bài tập 1. Đóng gói dữ liệu trong class `StudentScoreManager`.
* 📄 [exercise-2.py](file:///d:/Github/T2508M_SEM2_PP/exercise-2.py) - File mã nguồn Bài tập 2. Xử lý ghi/đọc dữ liệu CSV và phân tích bằng Pandas.
* 📄 [test-exercise-1.py](file:///d:/Github/T2508M_SEM2_PP/test-exercise-1.py) - Bộ kiểm thử tự động cho Bài 1 (**106 Test Cases** bao gồm mock đầu vào bàn phím và kiểm tra biên).
* 📄 [test-exercise-2.py](file:///d:/Github/T2508M_SEM2_PP/test-exercise-2.py) - Bộ kiểm thử tự động cho Bài 2 (**104 Test Cases** bao gồm kiểm tra biên giá, lượng tồn kho bằng 0 và DataFrame rỗng).
* 📊 `products.csv` - Tệp tin CSV tự động sinh ra trong quá trình chạy Bài 2.
* 📄 `README.md` - Tài liệu hướng dẫn sử dụng này.

---

## ⚡ Điểm Sáng Kỹ Thuật (Developer Highlights)

### 1. Kiến trúc Hướng đối tượng & Chống Crash (Bài tập 1)
* **OOP Đích thực**: Khởi tạo class `StudentScoreManager` quản lý danh sách sinh viên tập trung bằng danh sách các từ điển (`list` of `dict`) theo đúng chuẩn yêu cầu.
* **Xử lý ngoại lệ cực kỳ chặt chẽ (Validation Helpers)**: Bẫy toàn bộ các lỗi nhập liệu biên từ bàn phím như nhập chuỗi rỗng, chữ thay vì số, điểm số âm hoặc vượt quá giới hạn [0, 10], và chặn trùng ID sinh viên. Chương trình tự phục hồi yêu cầu nhập lại thay vì bị crash.
* **Xử lý đồng thủ khoa**: Thuật toán quét điểm cao nhất và lấy ra toàn bộ các sinh viên có cùng mức điểm thủ khoa thay vì chỉ lấy một người ngẫu nhiên.

### 2. Định dạng báo cáo Pandas nâng cao (Bài tập 2)
* **Định dạng hiển thị tiền tệ ($)**: Áp dụng định dạng tiền tệ chuyên nghiệp (`$1,200.00`) cho cột giá và cột tổng cộng trước khi in ra màn hình.
* **Bảo toàn dữ liệu gốc**: Hàm định dạng chỉ hoạt động trên một bản sao hiển thị (`df.copy()`), giúp giữ nguyên kiểu dữ liệu số thực (`float`) của DataFrame gốc phục vụ cho các phép tính toán chính xác cao ở các bước tiếp theo.

### 3. Hệ thống Unit Tests tự động & Dynamic Test Generation
* **100+ Test Cases mỗi bài**: Sử dụng vòng lặp định nghĩa động để sinh ra hơn 100 ca kiểm thử độc lập cho mỗi file test mà không làm phình dung lượng mã nguồn.
* **Mock Console Input**: Sử dụng `unittest.mock.patch` mô phỏng luồng nhập liệu lỗi của người dùng để tự động xác minh các hàm chống crash của hệ thống.
* **Xử lý Kebab-case Imports**: Sử dụng module `importlib` để import động các tệp tin chứa dấu gạch ngang (`exercise-1` và `exercise-2`) mà không gây lỗi cú pháp trình biên dịch Python.

---

## 🚀 Hướng Dẫn Chạy Chương Trình

### Yêu Cầu Hệ Thống
* Đã cài đặt Python 3.x trên máy.
* Cài đặt thư viện `pandas` (nếu chưa có):
  ```bash
  pip install pandas
  ```

### Chạy Chương Trình Chính

1. **Chạy Bài tập 1 (Quản lý điểm sinh viên)**:
   ```bash
   python exercise-1.py
   ```
2. **Chạy Bài tập 2 (Quản lý sản phẩm qua CSV & Pandas)**:
   ```bash
   python exercise-2.py
   ```

### Chạy Hệ Thống Kiểm Thử Tự Động (Unit Tests)

1. **Chạy bộ 106 test cases cho Bài tập 1**:
   ```bash
   python test-exercise-1.py
   ```
2. **Chạy bộ 104 test cases cho Bài tập 2**:
   ```bash
   python test-exercise-2.py
   ```

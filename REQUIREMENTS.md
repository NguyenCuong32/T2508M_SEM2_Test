# Đề bài: Practice Essentials of NodeJS - SET01

Thời gian làm bài: 60 phút
Công nghệ yêu cầu: Express, EJS, MongoDB

## Giao diện mẫu
Ứng dụng có giao diện Tree Shop gồm form nhập liệu và danh sách hiển thị:
1. Header: Chứa menu điều hướng với 2 trang: TreeShop và About me.
2. Form nhập thông tin cây mới:
   - Tree Name (Input text)
   - Description (Textarea)
   - Image (Input text kèm nút Browser/Chọn file)
   - Nút hành động: Add và Reset.
3. Danh sách các loại cây: Hiển thị dưới dạng bảng hoặc danh sách gồm các cột: Id, Name, Image, Description, cùng các nút chức năng Sửa, Xóa.
4. Footer: Hiển thị thông tin địa chỉ (ví dụ: Số 8, Tôn Thất Thuyết, Cầu Giấy, Hà Nội) được dùng chung (include) cho cả 2 trang.

## Các yêu cầu chức năng (Requirements)

1. Thêm mới cây (Add Tree): Viết chức năng lưu thông tin cây vào MongoDB bao gồm: name, description, image.
2. Header chung (Header Include): Tạo phần Header dùng chung với liên kết đến 2 trang: TreeShop và About me.
3. Footer chung (Footer Include): Tạo phần Footer dùng chung cho cả 2 trang TreeShop và About me.
4. Hiển thị danh sách (Show all trees): Hiển thị toàn bộ dữ liệu cây có trong cơ sở dữ liệu lên trang web.
5. Xóa toàn bộ (Reset function): Viết chức năng dọn dẹp/xóa toàn bộ dữ liệu cây trong cơ sở dữ liệu.
6. Kiểm tra dữ liệu đầu vào (Validation): Kiểm tra dữ liệu bắt buộc nhập cho các trường Tree Name và Description.

## Thông tin Cơ sở dữ liệu (Database Information)

- Database Name: TreeShop
- Collection Name: TreeCollection
- Schema Fields:
  - treename: String
  - description: String
  - image: String

## Thang điểm chi tiết (Marking Schema)

- Question 1 (5 điểm): Viết chức năng thêm mới cây (Create add function)
- Question 2 (2 điểm): Tạo header dùng chung (Create header with include)
- Question 3 (1 điểm): Tạo footer dùng chung (Create footer with include)
- Question 4 (3 điểm): Hiển thị tất cả danh sách cây (Show all trees)
- Question 5 (1 điểm): Chức năng reset xóa toàn bộ dữ liệu (Reset function)
- Question 6 (1 điểm): Xác thực dữ liệu đầu vào bắt buộc (Validate input)
- Bonus (2 điểm): Giao diện UI/UX tốt
- Tổng cộng: 15 điểm

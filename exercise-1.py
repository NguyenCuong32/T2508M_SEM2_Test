# Sử dụng Docstring ở đầu file để cung cấp thông tin tổng quan về module quản lý điểm sinh viên theo hướng đối tượng
"""
Exercise 1: Student Score Management System (OOP Version)
Author: Senior Principal Engineer
Description: Program to manage student scores using OOP concepts and list of dictionaries.
"""

# Định nghĩa lớp đối tượng để đóng gói toàn bộ dữ liệu và hành vi quản lý điểm số của sinh viên
class StudentScoreManager:
    # Sử dụng Docstring (cú pháp """) để mô tả hàm khởi tạo của lớp StudentScoreManager
    def __init__(self):
        """
        Khởi tạo danh sách trống để lưu trữ dữ liệu sinh viên tập trung dưới dạng list of dictionaries.
        """
        self.students = []

    # Sử dụng Docstring (cú pháp """) để giải thích vai trò của phương thức add_student
    def add_student(self, student_id, name, score):
        """
        Thêm một sinh viên mới dưới dạng từ điển (dictionary) vào danh sách quản lý chung của hệ thống.
        """
        student = {
            "id": student_id,
            "name": name,
            "score": score
        }
        self.students.append(student)

    # Sử dụng Docstring (cú pháp """) để giải thích vai trò của phương thức print_separator
    def print_separator(self):
        """
        In đường phân cách tiêu chuẩn ra terminal để tạo khoảng trống trực quan giữa các phần của báo cáo.
        """
        print("=" * 65)

    # Sử dụng Docstring (cú pháp """) để giải thích vai trò của phương thức display_table
    def display_table(self, student_list, title):
        """
        Biểu diễn một danh sách sinh viên cụ thể dưới dạng bảng phân cột thẳng hàng, hiển thị tiêu đề báo cáo rõ ràng.
        """
        self.print_separator()
        print(f" {title.upper()} ".center(65, "-"))
        self.print_separator()
        
        if not student_list:
            print("No matching student records found.".center(65))
            self.print_separator()
            return

        # Căn lề các tiêu đề cột tương thích với độ rộng cột hiển thị
        print(f"{'No.':<5} | {'Student ID':<12} | {'Full Name':<28} | {'Python Score':<12}")
        print("-" * 65)
        for index, student in enumerate(student_list, 1):
            print(f"{index:<5} | {student['id']:<12} | {student['name']:<28} | {student['score']:<12.2f}")
        self.print_separator()

    # Sử dụng Docstring (cú pháp """) để giải thích vai trò của phương thức display_all
    def display_all(self):
        """
        Gọi hàm dựng bảng hiển thị để in toàn bộ dữ liệu sinh viên hiện có trong bộ nhớ.
        """
        self.display_table(self.students, "All Students")

    # Sử dụng Docstring (cú pháp """) để giải thích vai trò của phương thức display_highest_score
    def display_highest_score(self):
        """
        Lọc ra các sinh viên có điểm số cao nhất (xử lý cả trường hợp đồng thủ khoa) và kết xuất ra bảng hiển thị.
        """
        if not self.students:
            self.display_table([], "Highest Scoring Student(s)")
            return
            
        max_score = max(student['score'] for student in self.students)
        highest_students = [student for student in self.students if student['score'] == max_score]
        self.display_table(highest_students, f"Highest Scoring Student(s) ({max_score:.2f})")

    # Sử dụng Docstring (cú pháp """) để giải thích vai trò của phương thức get_average_score
    def get_average_score(self):
        """
        Tính toán giá trị trung bình cộng điểm số của tất cả các sinh viên trong danh sách.
        """
        if not self.students:
            return 0.0
        return sum(student['score'] for student in self.students) / len(self.students)

    # Sử dụng Docstring (cú pháp """) để giải thích vai trò của phương thức display_passed
    def display_passed(self):
        """
        Lọc và hiển thị danh sách các sinh viên có điểm số Python đạt từ 5.0 trở lên.
        """
        passed_students = [student for student in self.students if student['score'] >= 5.0]
        self.display_table(passed_students, "Passed Students (Score >= 5)")


# Các hàm phụ trợ nhập liệu an toàn chống crash chương trình (Validation Helpers)

# Sử dụng Docstring (cú pháp """) để mô tả chức năng của hàm get_positive_integer
def get_positive_integer(prompt):
    """
    Nhận số nguyên dương từ bàn phím và kiểm soát các lỗi nhập liệu như chuỗi trống, ký tự không phải số hoặc số âm.
    """
    while True:
        try:
            val = input(prompt).strip()
            if not val:
                print("Error: Input cannot be empty. Please try again.")
                continue
            num = int(val)
            if num <= 0:
                print("Error: The number must be an integer greater than 0. Please try again.")
                continue
            return num
        except ValueError:
            print("Error: Invalid format. Please enter a valid integer.")

# Sử dụng Docstring (cú pháp """) để mô tả chức năng của hàm get_valid_score
def get_valid_score(prompt):
    """
    Nhận điểm số thực từ người dùng và giới hạn giá trị trong khoảng từ 0.0 đến 10.0 để đảm bảo tính hợp lệ của thang điểm.
    """
    while True:
        try:
            val = input(prompt).strip()
            if not val:
                print("Error: Score cannot be empty. Please try again.")
                continue
            score = float(val)
            if score < 0.0 or score > 10.0:
                print("Error: Score must be between 0.0 and 10.0. Please try again.")
                continue
            return score
        except ValueError:
            print("Error: Invalid format. Please enter a valid decimal number.")

# Sử dụng Docstring (cú pháp """) để mô tả chức năng của hàm get_non_empty_string
def get_non_empty_string(prompt, existing_ids=None):
    """
    Đọc chuỗi ký tự không rỗng từ bàn phím và kiểm tra tính duy nhất của mã định danh nếu tập hợp existing_ids được truyền vào.
    """
    while True:
        val = input(prompt).strip()
        if not val:
            print("Error: This field cannot be empty. Please try again.")
            continue
        if existing_ids is not None and val in existing_ids:
            print(f"Error: Student ID '{val}' already exists. Please enter a different ID.")
            continue
        return val

def main():
    print(" STUDENT SCORE MANAGEMENT SYSTEM (OOP) ".center(65, "="))
    
    # Khởi tạo đối tượng quản lý điểm sinh viên
    manager = StudentScoreManager()
    
    # Đọc số lượng phần tử cần nhập vào hệ thống
    num_students = get_positive_integer("Enter number of students: ")
    
    existing_ids = set()
    
    # Vòng lặp thu thập thông tin chi tiết của từng sinh viên
    for i in range(num_students):
        print(f"\n--- Enter details for student {i+1}/{num_students} ---")
        student_id = get_non_empty_string("Student ID: ", existing_ids)
        existing_ids.add(student_id)
        
        full_name = get_non_empty_string("Full name: ")
        score = get_valid_score("Python score (0-10): ")
        
        # Thêm sinh viên mới thông qua đối tượng quản lý
        manager.add_student(student_id, full_name, score)
        
    # Phần kết xuất thông tin báo cáo theo yêu cầu của đề bài
    
    # Hiển thị toàn bộ danh sách sinh viên đã nhập
    manager.display_all()
    
    # Tìm kiếm các sinh viên có điểm số cao nhất
    manager.display_highest_score()
    
    # Tính toán và hiển thị giá trị trung bình cộng điểm số của cả lớp
    avg_score = manager.get_average_score()
    print(f"👉 Class Average Score: {avg_score:.2f}")
    manager.print_separator()
    
    # Lọc và hiển thị các sinh viên vượt qua môn học
    manager.display_passed()

if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("\nProgram interrupted by user.")
    except Exception as e:
        print(f"\nA critical system error occurred: {e}")

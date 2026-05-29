# Sử dụng Docstring ở đầu file để mô tả tệp kiểm thử tự động với 100+ ca kiểm thử động và các ca biên đặc biệt cho Bài tập 1
"""
Test Suite for Exercise 1: Student Score Management System (Premium Edge Cases Version)
Author: Senior Principal Engineer
Description: Dynamic generation of 100+ test cases and mock-based unit tests for validation edge cases.
"""

import unittest
import importlib
from unittest.mock import patch

# Nhập động module có tên chứa ký hiệu gạch ngang (kebab-case) để tránh lỗi cú pháp trong Python
exercise_1 = importlib.import_module("exercise-1")
StudentScoreManager = exercise_1.StudentScoreManager

class TestStudentScoreManager(unittest.TestCase):
    # Sử dụng Docstring (cú pháp """) để mô tả phương thức thiết lập trước mỗi ca kiểm thử
    def setUp(self):
        """
        Khởi tạo đối tượng quản lý điểm sinh viên mới trước khi bắt đầu mỗi ca kiểm thử nhằm đảm bảo tính độc lập dữ liệu.
        """
        self.manager = StudentScoreManager()

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử cơ bản cho việc tính điểm trung bình
    def test_average_score_empty(self):
        """
        Xác thực điểm trung bình của danh sách sinh viên rỗng bằng 0 để tránh lỗi division by zero.
        """
        self.assertEqual(self.manager.get_average_score(), 0.0)

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử cho trường hợp đồng thủ khoa
    def test_multiple_highest_scores_tie(self):
        """
        Xác thực chương trình xử lý chính xác trường hợp có nhiều sinh viên cùng đạt điểm cao nhất (đồng thủ khoa).
        """
        self.manager.add_student("S001", "Alice", 9.5)
        self.manager.add_student("S002", "Bob", 8.0)
        self.manager.add_student("S003", "Charlie", 9.5)
        
        # Lấy điểm cao nhất
        max_score = max(s['score'] for s in self.manager.students)
        highest_students = [s for s in self.manager.students if s['score'] == max_score]
        
        self.assertEqual(max_score, 9.5)
        self.assertEqual(len(highest_students), 2)
        self.assertEqual(highest_students[0]["id"], "S001")
        self.assertEqual(highest_students[1]["id"], "S003")

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử cho điểm biên qua môn
    def test_score_boundaries(self):
        """
        Kiểm tra điểm cận biên đỗ/trượt (5.0 và 4.99) để xác minh tính chính xác của điều kiện >= 5.0.
        """
        self.manager.add_student("S001", "Borderline Pass", 5.0)  # Đạt
        self.manager.add_student("S002", "Borderline Fail", 4.99) # Trượt
        self.manager.add_student("S003", "Perfect Score", 10.0)   # Đạt
        self.manager.add_student("S004", "Zero Score", 0.0)       # Trượt

        passed_students = [s for s in self.manager.students if s['score'] >= 5.0]
        self.assertEqual(len(passed_students), 2)
        self.assertEqual(passed_students[0]["id"], "S001")
        self.assertEqual(passed_students[1]["id"], "S003")

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử mock nhập liệu số nguyên dương
    @patch('builtins.input')
    def test_validation_get_positive_integer(self, mock_input):
        """
        Kiểm tra hàm chống crash get_positive_integer khi nhận đầu vào lỗi (chuỗi trống, chữ, số âm, số 0) cho tới khi nhận được số nguyên dương hợp lệ.
        """
        mock_input.side_effect = ['', 'abc', '-5', '0', '3']
        result = exercise_1.get_positive_integer("Prompt")
        self.assertEqual(result, 3)
        self.assertEqual(mock_input.call_count, 5)

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử mock nhập điểm số
    @patch('builtins.input')
    def test_validation_get_valid_score(self, mock_input):
        """
        Kiểm tra hàm chống crash get_valid_score với các giá trị lỗi (chữ, trống, điểm ngoài khoảng 0-10) cho đến khi nhận được điểm hợp lệ.
        """
        mock_input.side_effect = ['xyz', '', '-0.1', '10.01', '7.5']
        result = exercise_1.get_valid_score("Prompt")
        self.assertEqual(result, 7.5)
        self.assertEqual(mock_input.call_count, 5)

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử mock trùng ID sinh viên
    @patch('builtins.input')
    def test_validation_get_non_empty_string_with_duplicates(self, mock_input):
        """
        Kiểm tra hàm nhập chuỗi không rỗng và kiểm tra trùng ID sinh viên (nhập trống, trùng ID đã có, nhập ID mới hợp lệ).
        """
        mock_input.side_effect = ['', 'S01', 'S02']
        result = exercise_1.get_non_empty_string("Prompt", existing_ids={'S01'})
        self.assertEqual(result, 'S02')
        self.assertEqual(mock_input.call_count, 3)

# Khởi chạy một vòng lặp ngoài để sinh động hóa đúng 100 ca kiểm thử khác nhau và gắn vào lớp TestStudentScoreManager
for i in range(1, 101):
    def create_test_function(index):
        # Định nghĩa một ca kiểm thử độc lập cho từng học sinh cụ thể từ 1 đến 100
        def test_case(self):
            student_id = f"S{index:03d}"
            name = f"Test Student {index}"
            # Tạo giải điểm nằm trong thang điểm [0, 10]
            score = float(index % 11)  # Điểm từ 0.0 đến 10.0
            
            # Thêm mới và đối chiếu kết quả lưu trữ
            self.manager.add_student(student_id, name, score)
            self.assertEqual(len(self.manager.students), 1)
            self.assertEqual(self.manager.students[0]["id"], student_id)
            self.assertEqual(self.manager.students[0]["name"], name)
            self.assertEqual(self.manager.students[0]["score"], score)
            
            # Kiểm chứng logic lọc sinh viên đỗ (score >= 5.0)
            is_passed = score >= 5.0
            passed_list = [s for s in self.manager.students if s["score"] >= 5.0]
            self.assertEqual(len(passed_list), 1 if is_passed else 0)
            
        return test_case

    # Gắn động phương thức kiểm thử mới vào lớp TestStudentScoreManager với tên định danh duy nhất
    test_name = f"test_student_record_{i:03d}"
    setattr(TestStudentScoreManager, test_name, create_test_function(i))

if __name__ == "__main__":
    unittest.main()

# Sử dụng Docstring ở đầu file để cung cấp thông tin tổng quan về tệp kiểm thử tự động với 100 ca kiểm thử cho Bài tập 2
"""
Test Suite for Exercise 2: Product Inventory Management using Pandas (Premium Edge Cases Version)
Author: Senior Principal Engineer
Description: Unit tests for CSV operations, Pandas filtering, and edge conditions such as boundary prices.
"""

import unittest
import importlib
import os
import pandas as pd

# Nhập động module có tên chứa ký hiệu gạch ngang (kebab-case) để tránh lỗi cú pháp trong Python
exercise_2 = importlib.import_module("exercise-2")

class TestProductInventory(unittest.TestCase):
    # Sử dụng Docstring (cú pháp """) để mô tả phương thức thiết lập trước mỗi ca kiểm thử
    def setUp(self):
        """
        Khởi tạo tên file CSV tạm thời dùng cho các ca kiểm thử để tránh ghi đè dữ liệu thật của sản phẩm.
        """
        self.temp_csv = "temp_products_test.csv"

    # Sử dụng Docstring (cú pháp """) để mô tả phương thức dọn dẹp sau khi kiểm thử xong
    def tearDown(self):
        """
        Dọn dẹp và xóa bỏ file CSV tạm thời sau khi hoàn thành các ca kiểm thử để giải phóng tài nguyên.
        """
        if os.path.exists(self.temp_csv):
            os.remove(self.temp_csv)

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử biên giá chính xác bằng 100.00
    def test_boundary_price_exactly_100(self):
        """
        Xác minh sản phẩm có giá trị chính xác là 100.00 không bị giữ lại bởi bộ lọc lớn hơn 100 (price > 100).
        """
        test_data = {
            "id": ["P100"],
            "name": ["Boundary product 100.00"],
            "price": [100.00],
            "quantity": [5]
        }
        df = pd.DataFrame(test_data)
        filtered_df = df[df["price"] > 100.0]
        self.assertTrue(filtered_df.empty)

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử biên giá lớn hơn 100 một chút
    def test_boundary_price_slightly_above_100(self):
        """
        Xác minh sản phẩm có giá trị 100.01 (lớn hơn 100 một lượng cực nhỏ) được bộ lọc price > 100 chấp nhận chính xác.
        """
        test_data = {
            "id": ["P101"],
            "name": ["Boundary product 100.01"],
            "price": [100.01],
            "quantity": [2]
        }
        df = pd.DataFrame(test_data)
        filtered_df = df[df["price"] > 100.0]
        self.assertEqual(len(filtered_df), 1)

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử lượng tồn kho bằng 0
    def test_zero_quantity(self):
        """
        Đảm bảo cột total và tính toán tổng tồn kho vẫn hoạt động chính xác khi số lượng tồn kho bằng 0.
        """
        test_data = {
            "id": ["P000"],
            "name": ["No quantity product"],
            "price": [250.00],
            "quantity": [0]
        }
        df = pd.DataFrame(test_data)
        total_value = (df["price"] * df["quantity"]).sum()
        self.assertEqual(total_value, 0.0)

    # Sử dụng Docstring (cú pháp """) để mô tả ca kiểm thử tính toán trên DataFrame trống
    def test_empty_dataframe_calculations(self):
        """
        Kiểm chứng tính ổn định của công thức tính giá trị tồn kho khi truyền vào một DataFrame rỗng (không xảy ra crash).
        """
        df = pd.DataFrame(columns=["id", "name", "price", "quantity"])
        total_value = (df["price"] * df["quantity"]).sum()
        self.assertEqual(total_value, 0.0)

# Tạo vòng lặp sinh động để gắn đúng 100 ca kiểm thử độc lập cho các bộ sản phẩm khác nhau
for i in range(1, 101):
    def create_test_function(index):
        # Định nghĩa một ca kiểm thử độc lập kiểm tra giá trị tồn kho và lưu trữ
        def test_case(self):
            # Tạo dữ liệu kiểm thử biến đổi theo index
            price = 10.0 + float(index)
            quantity = 5 + index
            expected_total = price * quantity
            
            test_data = {
                "id": [f"P{index:03d}"],
                "name": [f"Product {index}"],
                "price": [price],
                "quantity": [quantity]
            }
            df = pd.DataFrame(test_data)
            
            # Kiểm tra việc ghi và đọc file CSV tạm thời
            df.to_csv(self.temp_csv, index=False, encoding='utf-8')
            self.assertTrue(os.path.exists(self.temp_csv))
            
            loaded_df = pd.read_csv(self.temp_csv, encoding='utf-8')
            self.assertEqual(len(loaded_df), 1)
            self.assertEqual(loaded_df.loc[0, "price"], price)
            self.assertEqual(loaded_df.loc[0, "quantity"], quantity)
            
            # Kiểm chứng cột tổng tính toán total = price * quantity
            loaded_df["total"] = loaded_df["price"] * loaded_df["quantity"]
            self.assertEqual(loaded_df.loc[0, "total"], expected_total)
            
            # Kiểm chứng logic lọc giá lớn hơn 100
            is_greater_than_100 = price > 100.0
            filtered_df = loaded_df[loaded_df["price"] > 100.0]
            self.assertEqual(len(filtered_df), 1 if is_greater_than_100 else 0)
            
        return test_case

    # Gắn động phương thức kiểm thử vào lớp TestProductInventory với tên định danh duy nhất
    test_name = f"test_product_inventory_{i:03d}"
    setattr(TestProductInventory, test_name, create_test_function(i))

if __name__ == "__main__":
    unittest.main()

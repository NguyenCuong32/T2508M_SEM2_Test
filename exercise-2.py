# Sử dụng Docstring ở đầu file để cung cấp thông tin tổng quan về module quản lý sản phẩm bằng Pandas
"""
Exercise 2: Product Inventory Management using Pandas (Formatted Reporting Version)
Author: Senior Principal Engineer
Description: Program using pandas to create, save, read, filter, and calculate values from a CSV file.
"""

import os
import pandas as pd

def print_separator():
    print("=" * 75)

# Định nghĩa hàm phụ trợ để biểu diễn dữ liệu bảng trực quan hơn mà không làm thay đổi kiểu dữ liệu số gốc
def display_formatted_df(df, title):
    """
    Hàm bổ trợ giúp định dạng dữ liệu số thành dạng tiền tệ ($) đẹp mắt trước khi hiển thị ra console,
    không làm ảnh hướng đến dữ liệu gốc trong DataFrame phục vụ tính toán.
    """
    print_separator()
    print(f" {title.upper()} ".center(75, "-"))
    print_separator()
    
    # Tạo bản sao tạm thời để định dạng chuỗi hiển thị
    display_df = df.copy()
    
    # Áp dụng định dạng tiền tệ chuyên sâu cho cột price nếu tồn tại
    if "price" in display_df.columns:
        display_df["price"] = display_df["price"].map("${:,.2f}".format)
        
    # Áp dụng định dạng tiền tệ chuyên sâu cho cột total nếu tồn tại
    if "total" in display_df.columns:
        display_df["total"] = display_df["total"].map("${:,.2f}".format)
        
    # In DataFrame thẳng hàng căn chỉnh theo độ rộng cột và loại bỏ chỉ mục tự động
    print(display_df.to_string(index=False))
    print_separator()

def main():
    print_separator()
    print(" PRODUCT & INVENTORY MANAGEMENT SYSTEM USING PANDAS ".center(75, "="))
    print_separator()

    csv_filename = "products.csv"

    try:
        # Định nghĩa nguồn dữ liệu sản phẩm mẫu bao gồm 6 mục để khởi tạo DataFrame
        products_data = {
            "id": ["P001", "P002", "P003", "P004", "P005", "P006"],
            "name": ["Smart TV 4K", "Wireless Mouse", "Mechanical Keyboard", "Gaming Laptop", "USB-C Hub", "Bluetooth Earbuds"],
            "price": [450.00, 25.50, 89.99, 1200.00, 45.00, 110.00],
            "quantity": [15, 120, 45, 8, 75, 50]
        }
        
        df = pd.DataFrame(products_data)
        # Sử dụng hàm định dạng nâng cao để hiển thị bảng sản phẩm ban đầu chuyên nghiệp
        display_formatted_df(df, "1. Initial DataFrame created successfully")

        # Ghi dữ liệu DataFrame ra file CSV và loại bỏ chỉ mục mặc định (index=False) để giữ dữ liệu sạch
        df.to_csv(csv_filename, index=False, encoding='utf-8')
        print(f"2. DataFrame successfully saved to: '{os.path.abspath(csv_filename)}'")
        print_separator()

        # Kiểm tra sự tồn tại của file trước khi thực hiện thao tác đọc nhằm ngăn ngừa lỗi ngắt quãng chương trình
        if not os.path.exists(csv_filename):
            raise FileNotFoundError(f"File {csv_filename} was not found for reading.")
            
        loaded_df = pd.read_csv(csv_filename, encoding='utf-8')
        # Hiển thị tất cả sản phẩm đọc được từ file CSV với định dạng đẹp mắt
        display_formatted_df(loaded_df, "3. Loaded data from CSV file")

        # Sử dụng tính năng lọc có điều kiện (boolean indexing) của pandas để lấy danh sách sản phẩm giá cao
        filtered_df = loaded_df[loaded_df["price"] > 100]
        display_formatted_df(filtered_df, "4. Filtered products with price > 100")

        # Tính toán tổng giá tồn kho bằng cách nhân hai cột tương ứng theo dạng vector rồi cộng tổng tất cả các dòng
        total_inventory_value = (loaded_df["price"] * loaded_df["quantity"]).sum()
        print(f"5. 👉 Total inventory value of all products: ${total_inventory_value:,.2f}")
        print_separator()

        # Bổ sung một cột tính toán mới trực tiếp vào DataFrame hiện tại để quản lý tổng giá trị của từng dòng sản phẩm
        loaded_df["total"] = loaded_df["price"] * loaded_df["quantity"]
        # In toàn bộ DataFrame bao gồm cột mới đã được định dạng sang tiền tệ sang trọng
        display_formatted_df(loaded_df, "6. Added 'total' column (price * quantity) to DataFrame")

    except FileNotFoundError as fnf_error:
        print(f"Error: {fnf_error}")
    except PermissionError:
        print(f"Error: Access denied. Cannot write to file '{csv_filename}'. Please close the file if it is open.")
    except Exception as e:
        print(f"An unexpected error occurred: {e}")

if __name__ == "__main__":
    main()

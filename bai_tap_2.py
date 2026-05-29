# -*- coding: utf-8 -*-
"""
Bài tập 2: Chương trình Python sử dụng thư viện pandas để làm việc với tệp tin CSV.
Lớp: T2508M - Học kỳ 2
"""
import sys
import pandas as pd
import os

def main():
    # Cấu hình stdout hiển thị tiếng Việt trên Windows terminal không bị lỗi Unicode
    if hasattr(sys.stdout, 'reconfigure'):
        sys.stdout.reconfigure(encoding='utf-8')

    print("=========================================================")
    print("      CHƯƠNG TRÌNH QUẢN LÝ SẢN PHẨM VỚI PANDAS & CSV     ")
    print("=========================================================")

    # 1. Tạo một DataFrame với các cột: id, name, price, quantity
    # 2. Thêm ít nhất 5 sản phẩm vào DataFrame
    data = {
        'id': ['SP001', 'SP002', 'SP003', 'SP004', 'SP005', 'SP006'],
        'name': ['Bàn phím cơ Logitech G213', 'Chuột Logitech G502', 'Màn hình Dell UltraSharp 24"', 'Tai nghe HyperX Cloud II', 'Lót chuột Razer Goliathus', 'Loa Bluetooth JBL Flip 6'],
        'price': [120.0, 79.9, 280.0, 99.5, 25.0, 115.0],
        'quantity': [15, 30, 8, 20, 50, 12]
    }
    
    df = pd.DataFrame(data)
    print("\n1. Khởi tạo DataFrame ban đầu:")
    print(df.to_string(index=False))
    
    # 3. Lưu dữ liệu vào tệp tin products.csv
    # Sử dụng utf-8-sig để Excel có thể hiển thị đúng tiếng Việt có dấu
    file_name = 'products.csv'
    df.to_csv(file_name, index=False, encoding='utf-8-sig')
    print(f"\n2. Đã lưu dữ liệu vào tệp tin '{file_name}' thành công.")
    
    # 4. Đọc tệp tin CSV vừa lưu và thực hiện các yêu cầu
    print(f"\n3. Tiến hành đọc dữ liệu từ tệp tin '{file_name}':")
    df_read = pd.read_csv(file_name)
    
    # - Hiển thị tất cả sản phẩm
    print("\n   Hiển thị tất cả sản phẩm:")
    print("-" * 80)
    print(df_read.to_string(index=False))
    print("-" * 80)
    
    # - Hiển thị các sản phẩm có giá lớn hơn 100 (price > 100)
    print("\n   Các sản phẩm có giá lớn hơn 100 (price > 100):")
    print("-" * 80)
    df_expensive = df_read[df_read['price'] > 100]
    print(df_expensive.to_string(index=False))
    print("-" * 80)
    
    # - Tính tổng giá trị hàng tồn kho (Tổng của tất cả mặt hàng)
    # Giá trị tồn kho của mỗi mặt hàng = price * quantity
    # Tổng giá trị hàng tồn kho = Tổng tất cả (price * quantity)
    tong_gia_tri = (df_read['price'] * df_read['quantity']).sum()
    print(f"\n  Tổng giá trị hàng tồn kho của tất cả sản phẩm: {tong_gia_tri:,.2f} USD")
    
    # - Thêm một cột mới: total = price * quantity (Tổng tiền = Giá * Số lượng)
    df_read['total'] = df_read['price'] * df_read['quantity']
    print("\n   DataFrame sau khi thêm cột mới 'total' (total = price * quantity):")
    print("-" * 90)
    print(df_read.to_string(index=False))
    print("-" * 90)
    
    # Lưu lại DataFrame mới có cột total vào file products.csv
    df_read.to_csv(file_name, index=False, encoding='utf-8-sig')
    print(f"\n💡 Đã lưu cập nhật DataFrame có cột 'total' vào tệp tin '{file_name}'.")
    print("\n" + "="*57)

if __name__ == "__main__":
    main()

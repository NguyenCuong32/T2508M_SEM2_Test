import pandas as pd
data = {
    'id': [101, 102, 103, 104, 105],
    'name': ['Laptop', 'Chuột máy tính', 'Bàn phím cơ', 'Màn hình 4K', 'Tai nghe Bluetooth'],
    'price': [1200, 25, 80, 350, 95],
    'quantity': [10, 50, 30, 15, 40]
}

df = pd.DataFrame(data)
print("--- Đã tạo xong DataFrame ban đầu ---")
print(df)

df.to_csv('products.csv', index=False)
print("\n=> Đã lưu dữ liệu thành công vào file 'products.csv'!\n")

df_read = pd.read_csv('products.csv')

print("="*50)
print("a. Hiển thị tất cả sản phẩm đọc được từ file CSV:")
print(df_read)

print("\n" + "="*50)
print("b. Các sản phẩm có giá lớn hơn 100:")
expensive_products = df_read[df_read['price'] > 100]
print(expensive_products)

print("\n" + "="*50)
total_inventory_value = (df_read['price'] * df_read['quantity']).sum()
print(f"c. Tổng giá trị kho hàng là: ${total_inventory_value:,}")
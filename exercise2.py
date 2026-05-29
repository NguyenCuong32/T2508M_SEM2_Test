import pandas as pd

# Tạo dữ liệu sản phẩm
products = {
    "id": [1, 2, 3, 4, 5],
    "name": ["Laptop", "Mouse", "Keyboard", "Monitor", "USB"],
    "price": [1200, 50, 80, 300, 20],
    "quantity": [5, 20, 10, 7, 50]
}

# Tạo DataFrame
df = pd.DataFrame(products)

# Thêm cột total
df["total"] = df["price"] * df["quantity"]

# Lưu CSV
df.to_csv("products.csv", index=False)

print("Saved products.csv successfully!")

# Đọc CSV
data = pd.read_csv("products.csv")

# Hiển thị tất cả sản phẩm
print("\n=== ALL PRODUCTS ===")
print(data)

# Giá > 100
print("\n=== PRODUCTS PRICE > 100 ===")
print(data[data["price"] > 100])

# Tổng giá trị kho
inventory_value = data["total"].sum()

print("\nTotal inventory value:", inventory_value)

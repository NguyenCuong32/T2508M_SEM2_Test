import pandas as pd

# 1. Create DataFrame
data = {
    "id": [1, 2, 3, 4, 5],
    "name": ["Laptop", "Mouse", "Monitor", "Keyboard", "Headphones"],
    "price": [999.99, 29.99, 349.99, 89.99, 149.99],
    "quantity": [10, 50, 15, 30, 25],
}
df = pd.DataFrame(data)

# 2. Save → CSV
df.to_csv("products.csv", index=False)
print("=== Saved: products.csv ===\n")

# 3. Read CSV
df = pd.read_csv("products.csv")

# 4a. All products
print("=== All Products ===")
print(df.to_string(index=False))

# 4b. Price > 100
print("\n=== Price > 100 ===")
print(df[df["price"] > 100].to_string(index=False))

# 4c. Total inventory value
total_value = (df["price"] * df["quantity"]).sum()
print(f"\n=== Total Inventory Value: ${total_value:,.2f} ===")

# 5. New column: total = price * quantity
df["total"] = df["price"] * df["quantity"]
print("\n=== With 'total' Column ===")
print(df.to_string(index=False))

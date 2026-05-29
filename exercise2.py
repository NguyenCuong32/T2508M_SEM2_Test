import pandas as pd

products = {
    "id": [1,2,3,4,5,6,7,8,9,10],
    "name": [
        "Laptop", "Mouse", "Keyboard", "Monitor", "Headphone",
        "Printer", "Speaker", "Webcam", "SSD", "Microphone"
    ],
    "price": [1200,25,80,300,150,200,100,90,250,130],
    "quantity": [5,20,10,7,15,4,12,8,6,9]
}

df = pd.DataFrame(products)

df["total"] = df["price"] * df["quantity"]

df.to_csv("products.csv", index=False)

print("\nData saved to products.csv")

new_df = pd.read_csv("products.csv")

print("\n===== ALL PRODUCTS =====")
print(new_df.to_string(index=False))

print("\n===== PRODUCTS WITH PRICE > 100 =====")
print(new_df[new_df["price"] > 100].to_string(index=False))

inventory_value = new_df["total"].sum()

print("\n===== TOTAL INVENTORY VALUE =====")
print(f"Total Inventory Value: {inventory_value:,} $")
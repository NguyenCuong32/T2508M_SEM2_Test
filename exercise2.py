"""Exercise 2: Use pandas to work with product data and CSV files."""

from pathlib import Path

import pandas as pd


CSV_FILE = Path(__file__).with_name("products.csv")


def create_product_dataframe():
    """Create product data with the required columns."""
    products = [
        {"id": "P001", "name": "Keyboard", "price": 120.00, "quantity": 15},
        {"id": "P002", "name": "Mouse", "price": 35.50, "quantity": 40},
        {"id": "P003", "name": "Monitor", "price": 250.00, "quantity": 8},
        {"id": "P004", "name": "USB Cable", "price": 8.75, "quantity": 100},
        {"id": "P005", "name": "Headset", "price": 150.00, "quantity": 12},
        {"id": "P006", "name": "Laptop Stand", "price": 85.00, "quantity": 20},
    ]
    return pd.DataFrame(products, columns=["id", "name", "price", "quantity"])


def save_to_csv(dataframe, csv_file):
    """Save product data to a CSV file."""
    dataframe.to_csv(csv_file, index=False)


def read_from_csv(csv_file):
    """Read product data from a CSV file."""
    return pd.read_csv(csv_file)


def add_total_column(dataframe):
    """Add total = price * quantity."""
    result = dataframe.copy()
    result["total"] = result["price"] * result["quantity"]
    return result


def display_dataframe(title, dataframe):
    """Display a DataFrame with a title."""
    print(f"\n{title}")
    print("-" * 70)
    print(dataframe.to_string(index=False))


def main():
    products = create_product_dataframe()
    save_to_csv(products, CSV_FILE)

    products_from_csv = read_from_csv(CSV_FILE)
    products_with_total = add_total_column(products_from_csv)
    save_to_csv(products_with_total, CSV_FILE)

    display_dataframe("All products", products_with_total)

    expensive_products = products_with_total[products_with_total["price"] > 100]
    display_dataframe("Products with price > 100", expensive_products)

    total_inventory_value = products_with_total["total"].sum()
    print(f"\nTotal inventory value: {total_inventory_value:.2f}")
    print(f"CSV file saved at: {CSV_FILE}")


if __name__ == "__main__":
    main()

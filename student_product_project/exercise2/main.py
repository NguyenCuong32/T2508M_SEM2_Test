from product_manager import (
    create_dataframe,
    save_to_csv,
    read_from_csv,
    filter_expensive_products,
    add_total_column,
    calculate_inventory_value
)


def main():
    file_path = "data/products.csv"

    df = create_dataframe()

    save_to_csv(df, file_path)

    print("=== ALL PRODUCTS ===")
    df = read_from_csv(file_path)
    print(df)

    print("\n=== PRODUCTS WITH PRICE > 100 ===")
    expensive_products = filter_expensive_products(df)
    print(expensive_products)

    df = add_total_column(df)

    print("\n=== PRODUCTS WITH TOTAL COLUMN ===")
    print(df)

    inventory_value = calculate_inventory_value(df)

    print("\n=== TOTAL INVENTORY VALUE ===")
    print(inventory_value)


if __name__ == "__main__":
    main()
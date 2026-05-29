import product_manager

def main():
    file_path = 'products.csv'
    
    # 1. Create DataFrame with columns (id, name, price, quantity)
    # 2. Add at least 5 products
    # 3. Save data to products.csv
    product_manager.create_and_save_products(file_path)
    
    # 4. Read the CSV file
    #   - Display all products
    df = product_manager.read_and_display_all_products(file_path)
    
    #   - Display products with price > 100
    product_manager.display_expensive_products(df, threshold=100)
    
    #   - Calculate the total inventory value
    product_manager.calculate_total_inventory_value(df)
    
    # 5. Add a new column: total = price * quantity
    df_with_total = product_manager.add_total_column(df)

if __name__ == "__main__":
    main()

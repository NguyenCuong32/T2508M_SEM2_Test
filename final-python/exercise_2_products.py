import pandas as pd
import os

def main():
    # 1. Create a DataFrame with columns: id, name, price, quantity
    # 2. Add at least 5 products
    data = {
        'id': [101, 102, 103, 104, 105],
        'name': ['Laptop', 'Mouse', 'Keyboard', 'Monitor', 'Headset'],
        'price': [1200, 25, 45, 150, 80],
        'quantity': [5, 50, 30, 10, 20]
    }
    
    df = pd.DataFrame(data)
    
    # 3. Save data to products.csv
    csv_file = 'products.csv'
    df.to_csv(csv_file, index=False)
    print(f"Data saved to {csv_file}")
    
    # 4. Read the CSV file
    print("\nReading CSV file...")
    loaded_df = pd.read_csv(csv_file)
    
    # Display all products
    print("\n" + "="*30)
    print("ALL PRODUCTS")
    print("="*30)
    print(loaded_df)
    
    # Display products with price > 100
    print("\n" + "="*30)
    print("PRODUCTS WITH PRICE > 100")
    print("="*30)
    expensive_products = loaded_df[loaded_df['price'] > 100]
    print(expensive_products)
    
    # Calculate the total inventory value
    total_value = (loaded_df['price'] * loaded_df['quantity']).sum()
    print("\n" + "="*30)
    print(f"TOTAL INVENTORY VALUE: {total_value}")
    
    # 5. Add a new column: total = price * quantity
    print("\nAdding 'total' column...")
    loaded_df['total'] = loaded_df['price'] * loaded_df['quantity']
    
    print("\n" + "="*30)
    print("FINAL DATAFRAME")
    print("="*30)
    print(loaded_df)

if __name__ == "__main__":
    # Check if pandas is installed
    try:
        import pandas
    except ImportError:
        print("Error: pandas library is not installed. Please install it using 'pip install pandas'.")
    else:
        main()

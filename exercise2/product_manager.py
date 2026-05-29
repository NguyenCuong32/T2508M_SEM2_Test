import pandas as pd

def create_and_save_products(file_path):
    """Creates a DataFrame with 5 products and saves it to a CSV file."""
    data = {
        'id': [1, 2, 3, 4, 5],
        'name': ['Laptop', 'Mouse', 'Keyboard', 'Monitor', 'Headphones'],
        'price': [1200.0, 25.5, 75.0, 300.0, 150.0],
        'quantity': [10, 50, 30, 20, 40]
    }
    df = pd.DataFrame(data)
    df.to_csv(file_path, index=False)
    print(f"Data saved successfully to {file_path}\n")

def read_and_display_all_products(file_path):
    """Reads the CSV file and displays all products."""
    df = pd.read_csv(file_path)
    print("="*40)
    print("--- All Products ---")
    print(df)
    print("="*40 + "\n")
    return df

def display_expensive_products(df, threshold=100):
    """Displays products with a price greater than the given threshold."""
    print("="*40)
    print(f"--- Products with price > {threshold} ---")
    expensive_products = df[df['price'] > threshold]
    print(expensive_products)
    print("="*40 + "\n")

def calculate_total_inventory_value(df):
    """Calculates and displays the total inventory value (sum of price * quantity)."""
    print("="*40)
    print("--- Total Inventory Value ---")
    total_value = (df['price'] * df['quantity']).sum()
    print(f"Total value: {total_value:.2f}")
    print("="*40 + "\n")

def add_total_column(df):
    """Adds a new column 'total' = price * quantity and displays the updated DataFrame."""
    print("="*40)
    print("--- DataFrame with new 'total' column ---")
    df['total'] = df['price'] * df['quantity']
    print(df)
    print("="*40 + "\n")
    return df

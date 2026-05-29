# Exercise 2: Work with CSV Files using Pandas

import pandas as pd

def manage_products():
    # Create a DataFrame with sample products
    data = {
        'id': [1, 2, 3, 4, 5, 6],
        'name': ['Laptop', 'Mouse', 'Keyboard', 'Monitor', 'Headphones', 'Webcam'],
        'price': [800, 25, 75, 250, 150, 120],
        'quantity': [5, 50, 30, 10, 20, 15]
    }
    
    df = pd.DataFrame(data)
    
    # Add new column: total = price * quantity
    df['total'] = df['price'] * df['quantity']
    
    # Save data to products.csv
    df.to_csv('products.csv', index=False)
    print("✓ Data saved to products.csv")
    
    # Read the CSV file
    df = pd.read_csv('products.csv')
    
    # Display all products
    print("\n" + "="*80)
    print("ALL PRODUCTS:")
    print("="*80)
    print(df.to_string(index=False))
    
    # Display products with price > 100
    print("\n" + "="*80)
    print("PRODUCTS WITH PRICE > 100:")
    print("="*80)
    expensive_products = df[df['price'] > 100]
    print(expensive_products.to_string(index=False))
    
    # Calculate total inventory value
    print("\n" + "="*80)
    total_inventory_value = df['total'].sum()
    print(f"TOTAL INVENTORY VALUE: ${total_inventory_value:,.2f}")
    print("="*80)
    
    # Display summary statistics
    print("\n" + "="*80)
    print("SUMMARY STATISTICS:")
    print("="*80)
    print(f"Total Products: {len(df)}")
    print(f"Total Quantity in Stock: {df['quantity'].sum()} units")
    print(f"Average Product Price: ${df['price'].mean():.2f}")
    print(f"Most Expensive Product: {df.loc[df['price'].idxmax(), 'name']} (${df['price'].max()})")
    print(f"Cheapest Product: {df.loc[df['price'].idxmin(), 'name']} (${df['price'].min()})")

if __name__ == "__main__":
    manage_products()

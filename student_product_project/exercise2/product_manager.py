import pandas as pd


def create_dataframe():
    data = {
        "id": [1, 2, 3, 4, 5],
        "name": [
            "Mouse",
            "Keyboard",
            "Monitor",
            "SSD",
            "Webcam"
        ],
        "price": [150, 200, 500, 300, 120],
        "quantity": [10, 5, 2, 8, 6]
    }

    return pd.DataFrame(data)


def save_to_csv(df, file_path):
    df.to_csv(file_path, index=False)


def read_from_csv(file_path):
    return pd.read_csv(file_path)


def filter_expensive_products(df):
    return df[df["price"] > 100]


def add_total_column(df):
    df["total"] = df["price"] * df["quantity"]
    return df


def calculate_inventory_value(df):
    return df["total"].sum()
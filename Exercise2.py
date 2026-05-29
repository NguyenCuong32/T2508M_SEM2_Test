# BÀI 2: THAO TÁC FILE CSV BẰNG THƯ VIỆN PANDAS
# ================================================

import pandas as pd
import sys, io
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding="utf-8")

# ── 1. TẠO DATAFRAME VỚI 5+ SẢN PHẨM ─────────────────────────────────────────
du_lieu = {
    "id":       [1,          2,          3,          4,          5,          6],
    "name":     ["Laptop",   "Chuột",    "Bàn phím", "Màn hình", "Tai nghe", "Ổ cứng SSD"],
    "price":    [15000000,   250000,     850000,     3500000,    450000,     1200000],
    "quantity": [10,         50,         30,         15,         40,         25],
}

df = pd.DataFrame(du_lieu)

print("=" * 60)
print("  BÀI 2: THAO TÁC FILE CSV BẰNG THƯ VIỆN PANDAS")
print("=" * 60)

print("\n[1] DataFrame vừa tạo:")
print(df.to_string(index=False))

# ── 2. LƯU DATAFRAME THÀNH FILE products.csv ──────────────────────────────────
ten_file = "products.csv"
df.to_csv(ten_file, index=False, encoding="utf-8-sig")
print(f"\n[2] Đã lưu DataFrame vào file '{ten_file}' thành công.")

# ── 3. ĐỌC LẠI FILE products.csv ──────────────────────────────────────────────
df_doc = pd.read_csv(ten_file, encoding="utf-8-sig")

print("\n[3] Toàn bộ sản phẩm đọc từ file CSV:")
print(df_doc.to_string(index=False))

# ── 4. LỌC SẢN PHẨM CÓ price > 100 ───────────────────────────────────────────
df_loc = df_doc[df_doc["price"] > 100]
print(f"\n[4] Các sản phẩm có price > 100 ({len(df_loc)} sản phẩm):")
print(df_loc.to_string(index=False))

# ── 5. TÍNH TỔNG GIÁ TRỊ HÀNG TỒN KHO ────────────────────────────────────────
tong_gia_tri = (df_doc["price"] * df_doc["quantity"]).sum()
print(f"\n[5] Tổng giá trị hàng tồn kho: {tong_gia_tri:,.0f} VNĐ")

# ── 6. THÊM CỘT total = price * quantity ──────────────────────────────────────
df_doc["total"] = df_doc["price"] * df_doc["quantity"]
print("\n[6] DataFrame sau khi thêm cột 'total':")
print(df_doc.to_string(index=False))

# Lưu lại file CSV đã cập nhật cột total
df_doc.to_csv(ten_file, index=False, encoding="utf-8-sig")
print(f"\n    Đã cập nhật file '{ten_file}' với cột 'total'.")

print("\n" + "=" * 60)
print("  Hoàn thành Bài 2!")
print("=" * 60)
# -*- coding: utf-8 -*-
"""
Bài tập 1: Chương trình quản lý điểm số của học sinh sử dụng danh sách (list) và từ điển (dictionary).
Lớp: T2508M - Học kỳ 2
"""
import sys

def main():
    # Cấu hình stdout hiển thị tiếng Việt trên Windows terminal không bị lỗi Unicode
    if hasattr(sys.stdout, 'reconfigure'):
        sys.stdout.reconfigure(encoding='utf-8')
        
    print("=========================================================")
    print("       CHƯƠNG TRÌNH QUẢN LÝ ĐIỂM SỐ HỌC SINH             ")
    print("=========================================================")
    
    # 1. Nhập số lượng học sinh
    while True:
        try:
            n = int(input("Nhập số lượng học sinh: "))
            if n > 0:
                break
            else:
                print("⚠️ Số lượng học sinh phải lớn hơn 0. Vui lòng nhập lại!")
        except ValueError:
            print("⚠️ Vui lòng nhập một số nguyên hợp lệ!")
            
    # Danh sách để lưu trữ tất cả học sinh (list of dictionaries)
    danh_sach_hoc_sinh = []
    
    # 2. Nhập thông tin của từng học sinh
    for i in range(n):
        print(f"\n--- Nhập thông tin cho học sinh thứ {i + 1} ---")
        
        # Nhập Mã số học sinh (Student ID) và kiểm tra trùng lặp
        while True:
            ma_hs = input("Nhập mã số học sinh (Student ID): ").strip()
            if not ma_hs:
                print("⚠️ Mã số học sinh không được để trống!")
                continue
            
            # Kiểm tra xem ID đã tồn tại trong danh sách chưa
            trung_lap = False
            for hs in danh_sach_hoc_sinh:
                if hs['id'] == ma_hs:
                    trung_lap = True
                    break
            
            if trung_lap:
                print("⚠️ Mã số học sinh này đã tồn tại! Vui lòng nhập mã khác.")
            else:
                break
                
        # Nhập Họ và tên (Full name)
        while True:
            ho_ten = input("Nhập họ và tên (Full name): ").strip()
            if ho_ten:
                # Chuẩn hóa tên (viết hoa chữ cái đầu của mỗi từ)
                ho_ten = ho_ten.title()
                break
            print("⚠️ Họ và tên không được để trống!")
            
        # Nhập Điểm môn Python (Python score)
        while True:
            try:
                diem_python = float(input("Nhập điểm môn Python (Python score) [0 - 10]: "))
                if 0 <= diem_python <= 10:
                    break
                else:
                    print("⚠️ Điểm số phải nằm trong khoảng từ 0.0 đến 10.0!")
            except ValueError:
                print("⚠️ Vui lòng nhập một số thực hợp lệ!")
                
        # Lưu trữ thông tin của học sinh vào một từ điển (dictionary)
        hoc_sinh = {
            'id': ma_hs,
            'name': ho_ten,
            'score': diem_python
        }
        
        # Lưu học sinh vào danh sách (list)
        danh_sach_hoc_sinh.append(hoc_sinh)
        
    # --- HIỂN THỊ KẾT QUẢ ---
    print("\n" + "="*60)
    print("                      KẾT QUẢ THỐNG KÊ                      ")
    print("="*60)
    
    # A. Hiển thị danh sách tất cả học sinh
    print("\n1. DANH SÁCH TẤT CẢ HỌC SINH:")
    print(f"{'STT':<5} | {'Mã số HS':<12} | {'Họ và tên':<25} | {'Điểm Python':<12}")
    print("-" * 65)
    for idx, hs in enumerate(danh_sach_hoc_sinh, 1):
        print(f"{idx:<5} | {hs['id']:<12} | {hs['name']:<25} | {hs['score']:<12.2f}")
        
    # B. Tìm học sinh có điểm số cao nhất (xử lý cả trường hợp đồng thủ khoa)
    max_score = max(hs['score'] for hs in danh_sach_hoc_sinh)
    danh_sach_max = [hs for hs in danh_sach_hoc_sinh if hs['score'] == max_score]
    
    print(f"\n2. HỌC SINH CÓ ĐIỂM SỐ CAO NHẤT (Điểm: {max_score:.2f}):")
    for hs in danh_sach_max:
        print(f"   ⭐ Mã số: {hs['id']} | Họ tên: {hs['name']}")
        
    # C. Tính điểm số trung bình của cả lớp
    tong_diem = sum(hs['score'] for hs in danh_sach_hoc_sinh)
    diem_trung_binh = tong_diem / n
    print(f"\n3. ĐIỂM SỐ TRUNG BÌNH CỦA CẢ LỚP: {diem_trung_binh:.2f}")
    
    # D. Danh sách các học sinh đã đạt/đỗ (điểm >= 5)
    danh_sach_dat = [hs for hs in danh_sach_hoc_sinh if hs['score'] >= 5]
    print(f"\n4. DANH SÁCH HỌC SINH ĐÃ ĐẠT/ĐỖ (Điểm >= 5.0) - Sĩ số đỗ: {len(danh_sach_dat)}/{n}:")
    if danh_sach_dat:
        print(f"{'Mã số HS':<12} | {'Họ và tên':<25} | {'Điểm Python':<12}")
        print("-" * 60)
        for hs in danh_sach_dat:
            print(f"{hs['id']:<12} | {hs['name']:<25} | {hs['score']:<12.2f}")
    else:
        print("   ❌ Không có học sinh nào đạt điểm đỗ (>= 5.0).")
    print("\n" + "="*60)

if __name__ == "__main__":
    main()

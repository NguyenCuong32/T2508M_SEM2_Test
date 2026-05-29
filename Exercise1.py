# BÀI 1: QUẢN LÝ ĐIỂM SINH VIÊN
# ================================

import sys, io
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding="utf-8")

def nhap_sinh_vien():
    """Nhập thông tin danh sách sinh viên."""
    danh_sach = []
    ma_da_dung = set()

    while True:
        try:
            so_luong = int(input("Nhập số lượng sinh viên: "))
            if so_luong <= 0:
                print("  Số lượng phải lớn hơn 0. Vui lòng nhập lại.")
                continue
            break
        except ValueError:
            print("  Vui lòng nhập một số nguyên hợp lệ.")

    for i in range(so_luong):
        print(f"\n--- Sinh viên {i + 1} ---")

        # Nhập mã sinh viên (không trùng)
        while True:
            ma_sv = input("  Mã sinh viên: ").strip()
            if not ma_sv:
                print("  Mã sinh viên không được để trống.")
            elif ma_sv in ma_da_dung:
                print("  Mã sinh viên đã tồn tại. Vui lòng nhập mã khác.")
            else:
                ma_da_dung.add(ma_sv)
                break

        # Nhập họ tên
        while True:
            ho_ten = input("  Họ và tên: ").strip()
            if not ho_ten:
                print("  Họ tên không được để trống.")
            else:
                break

        # Nhập điểm Python (0 - 10)
        while True:
            try:
                diem = float(input("  Điểm Python (0 - 10): "))
                if diem < 0 or diem > 10:
                    print("  Điểm phải nằm trong khoảng 0 đến 10.")
                else:
                    break
            except ValueError:
                print("  Vui lòng nhập một số hợp lệ.")

        danh_sach.append({
            "ma_sv": ma_sv,
            "ho_ten": ho_ten,
            "diem": diem
        })

    return danh_sach


def hien_thi_tat_ca(danh_sach):
    """Hiển thị toàn bộ danh sách sinh viên."""
    print("\n" + "=" * 55)
    print("       DANH SÁCH TOÀN BỘ SINH VIÊN")
    print("=" * 55)
    print(f"{'STT':<5} {'Mã SV':<12} {'Họ và Tên':<25} {'Điểm':>6}")
    print("-" * 55)
    for idx, sv in enumerate(danh_sach, 1):
        print(f"{idx:<5} {sv['ma_sv']:<12} {sv['ho_ten']:<25} {sv['diem']:>6.2f}")
    print("=" * 55)


def hien_thi_diem_cao_nhat(danh_sach):
    """Hiển thị sinh viên có điểm cao nhất (xử lý nhiều SV cùng điểm)."""
    diem_cao_nhat = max(sv["diem"] for sv in danh_sach)
    sv_cao_nhat = [sv for sv in danh_sach if sv["diem"] == diem_cao_nhat]

    print(f"\n{'=' * 55}")
    print(f"  SINH VIÊN ĐIỂM CAO NHẤT: {diem_cao_nhat:.2f}")
    print("=" * 55)
    for sv in sv_cao_nhat:
        print(f"  Mã SV  : {sv['ma_sv']}")
        print(f"  Họ tên : {sv['ho_ten']}")
        print(f"  Điểm   : {sv['diem']:.2f}")
        if len(sv_cao_nhat) > 1:
            print("  " + "-" * 30)


def hien_thi_diem_trung_binh(danh_sach):
    """Tính và hiển thị điểm trung bình của cả lớp."""
    trung_binh = sum(sv["diem"] for sv in danh_sach) / len(danh_sach)
    print(f"\n  Điểm trung bình cả lớp: {trung_binh:.2f}")


def hien_thi_sinh_vien_dat(danh_sach):
    """Hiển thị danh sách sinh viên thi đạt (điểm >= 5)."""
    sv_dat = [sv for sv in danh_sach if sv["diem"] >= 5]

    print(f"\n{'=' * 55}")
    print(f"  DANH SÁCH SINH VIÊN ĐẠT (Điểm >= 5)  —  {len(sv_dat)}/{len(danh_sach)} SV")
    print("=" * 55)

    if not sv_dat:
        print("  Không có sinh viên nào đạt.")
    else:
        print(f"{'STT':<5} {'Mã SV':<12} {'Họ và Tên':<25} {'Điểm':>6}")
        print("-" * 55)
        for idx, sv in enumerate(sv_dat, 1):
            print(f"{idx:<5} {sv['ma_sv']:<12} {sv['ho_ten']:<25} {sv['diem']:>6.2f}")
    print("=" * 55)


def main():
    print("=" * 55)
    print("     BÀI 1: QUẢN LÝ ĐIỂM SINH VIÊN")
    print("=" * 55)

    danh_sach = nhap_sinh_vien()

    hien_thi_tat_ca(danh_sach)
    hien_thi_diem_cao_nhat(danh_sach)
    hien_thi_diem_trung_binh(danh_sach)
    hien_thi_sinh_vien_dat(danh_sach)


if __name__ == "__main__":
    main()
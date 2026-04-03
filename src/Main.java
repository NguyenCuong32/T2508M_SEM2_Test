import java.util.List;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        PlayerDAO dao = new PlayerDAO();
        Scanner sc = new Scanner(System.in);

        while (true) {
            System.out.println("\n===== QUẢN LÝ NGƯỜI CHƠI (HERO GAME) =====");
            System.out.println("1. Hiển thị danh sách (Table 1)");
            System.out.println("2. Thêm người chơi mới");
            System.out.println("3. Sửa thông tin người chơi");
            System.out.println("4. Xóa người chơi");
            System.out.println("0. Thoát");
            System.out.print("Mời bạn chọn: ");
            int chon = sc.nextInt();
            sc.nextLine(); // Chống trôi lệnh

            try {
                switch (chon) {
                    case 1: // HIỂN THỊ BẢNG GIỐNG ĐỀ BÀI
                        List<Player> list = dao.getAllPlayers();
                        System.out.println("\n-------------------------------------------------------------------------");
                        System.out.printf("| %-10s | %-15s | %-10s | %-10s | %-10s |\n",
                                "Player Id", "Player name", "High Score", "Level", "National");
                        System.out.println("-------------------------------------------------------------------------");
                        for (Player p : list) {
                            System.out.printf("| %-10d | %-15s | %-10d | %-10d | %-10s |\n",
                                    p.getPlayerId(), p.getPlayerName(), p.getHighScore(), p.getLevel(), p.getNationalName());
                        }
                        System.out.println("-------------------------------------------------------------------------");
                        break;

                    case 2: // THÊM
                        System.out.print("Nhập tên: "); String ten = sc.nextLine();
                        System.out.print("Nhập điểm: "); int diem = sc.nextInt();
                        System.out.print("Nhập level: "); int lv = sc.nextInt();
                        System.out.print("Nhập ID quốc gia (1:VN, 2:USA, 3:Japan): "); int idQG = sc.nextInt();
                        dao.insertPlayer(ten, diem, lv, idQG);
                        System.out.println("=> Thêm thành công!");
                        break;

                    case 3: // SỬA (Cập nhật điểm và Level)
                        System.out.print("Nhập ID người chơi cần sửa: "); int idSua = sc.nextInt();
                        System.out.print("Nhập điểm mới: "); int diemMoi = sc.nextInt();
                        System.out.print("Nhập level mới: "); int lvMoi = sc.nextInt();
                        dao.updatePlayer(idSua, diemMoi, lvMoi);
                        System.out.println("=> Cập nhật thành công!");
                        break;

                    case 4: // XÓA
                        System.out.print("Nhập ID người chơi cần xóa: "); int idXoa = sc.nextInt();
                        dao.deletePlayer(idXoa);
                        System.out.println("=> Đã xóa người chơi!");
                        break;

                    case 0:
                        System.out.println("Tạm biệt!");
                        System.exit(0);
                }
            } catch (Exception e) {
                System.out.println("Có lỗi xảy ra: " + e.getMessage());
            }
        }
    }
}
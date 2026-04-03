package herogame;

import herogameservice.PlayerService;
import herogameentity.Player;
import java.util.Scanner;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        PlayerService service = new PlayerService();
        Scanner sc = new Scanner(System.in);
        int chon;

        do {
            System.out.println("\n===== MENU QUẢN LÝ NGƯỜI CHƠI =====");
            System.out.println("1. Hiển thị danh sách");
            System.out.println("2. Thêm người chơi mới");
            System.out.println("3. Xóa người chơi theo ID");
            System.out.println("4. Xem Top 10");
            System.out.println("0. Thoát");
            System.out.print("Mời bạn chọn: ");
            chon = sc.nextInt();
            sc.nextLine(); // Chống trôi lệnh

            switch (chon) {
                case 1:
                    List<Player> list = service.getAll();
                    System.out.println("ID | Tên | Điểm | Level | Quốc gia");
                    for (Player p : list) {
                        System.out.println(p.getPlayerId() + " | " + p.getPlayerName() + " | " + p.getHighScore() + " | " + p.getLevel() + " | " + p.getNationalName());
                    }
                    break;

                case 2:
                    System.out.print("Nhập tên: "); String name = sc.nextLine();
                    System.out.print("Nhập điểm: "); int score = sc.nextInt();
                    System.out.print("Nhập Level: "); int lv = sc.nextInt();
                    System.out.print("Nhập ID Quốc gia (1-VN, 2-USA): "); int nid = sc.nextInt();

                    Player newP = new Player(0, name, score, lv, nid, "");
                    service.add(newP);
                    System.out.println("Đã thêm thành công!");
                    break;

                case 3:
                    System.out.print("Nhập ID người chơi cần xóa: ");
                    int idXoa = sc.nextInt();
                    service.delete(idXoa);
                    System.out.println("Đã xóa xong!");
                    break;

                case 4:
                    service.getTop10().forEach(p -> System.out.println(p.getPlayerName() + " - " + p.getHighScore()));
                    break;
            }
        } while (chon != 0);
    }
}
package herogamedatabase;

import java.sql.Connection;
import java.sql.DriverManager;

public class DBContext {
    /* THAY ĐỔI CÁC THÔNG SỐ DƯỚI ĐÂY CHO PHÙ HỢP VỚI MÁY BẠN */
    private final String serverName = "localhost";
    private final String dbName = "HeroGame";
    private final String portNumber = "8889"; // 3306 cho MySQL, 1433 cho SQL Server
    private final String userID = "root";
    private final String password = "root";

    public Connection getConnection() throws Exception {
        String url = "jdbc:mysql://" + serverName + ":" + portNumber + "/" + dbName;
        Class.forName("com.mysql.cj.jdbc.Driver");
        return DriverManager.getConnection(url, userID, password);
    }

    // Test thử kết nối
    public static void main(String[] args) {
        try {
            if (new DBContext().getConnection() != null) {
                System.out.println("Kết nối Database thành công!");
            }
        } catch (Exception e) {
            System.out.println("Kết nối thất bại: " + e.getMessage());
        }
    }
}
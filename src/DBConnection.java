import java.sql.Connection;
import java.sql.DriverManager;

public class DBConnection {
    private final String serverName = "localhost";
    private final String dbName = "HeroGame";
    private final String portNumber = "3306";
    private final String userID = "root";
    private final String password = ""; // Điền mật khẩu MySQL nếu có

    public Connection getConnection() throws Exception {
        String url = "jdbc:mysql://" + serverName + ":" + portNumber + "/" + dbName;
        Class.forName("com.mysql.cj.jdbc.Driver");
        return DriverManager.getConnection(url, userID, password);
    }

    // Thêm hàm main này vào để chuột phải chạy thử (Run) xem có báo lỗi gì không
    public static void main(String[] args) {
        try {
            DBConnection db = new DBConnection();
            if (db.getConnection() != null) {
                System.out.println("Kết nối thành công!");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
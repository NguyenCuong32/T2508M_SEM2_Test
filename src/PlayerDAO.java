import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PlayerDAO {
    // Kết nối database từ class DBConnection
    DBConnection db = new DBConnection();

    // 1. HIỂN THỊ DANH SÁCH (JOIN để lấy tên nước)
    public List<Player> getAllPlayers() throws Exception {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT p.PlayerId, p.PlayerName, p.HighScore, p.Level, n.NationalName " +
                "FROM Player p JOIN National n ON p.NationalId = n.NationalId";

        try (Connection conn = db.getConnection();
             Statement st = conn.createStatement();
             ResultSet rs = st.executeQuery(sql)) {

            while (rs.next()) {
                list.add(new Player(
                        rs.getInt("PlayerId"),
                        rs.getString("PlayerName"),
                        rs.getInt("HighScore"),
                        rs.getInt("Level"),
                        rs.getString("NationalName")
                ));
            }
        }
        return list;
    }

    // 2. THÊM NGƯỜI CHƠI MỚI
    public void insertPlayer(String name, int score, int level, int nationId) throws Exception {
        String sql = "INSERT INTO Player (PlayerName, HighScore, Level, NationalId) VALUES (?, ?, ?, ?)";
        try (Connection conn = db.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setString(1, name);
            ps.setInt(2, score);
            ps.setInt(3, level);
            ps.setInt(4, nationId);
            ps.executeUpdate();
        }
    }

    // 3. SỬA (FIX LỖI: Chỉ dùng 3 tham số để khớp với file Main của Kiên)
    public void updatePlayer(int id, int score, int level) throws Exception {
        String sql = "UPDATE Player SET HighScore = ?, Level = ? WHERE PlayerId = ?";
        try (Connection conn = db.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setInt(1, score);
            ps.setInt(2, level);
            ps.setInt(3, id);
            ps.executeUpdate();
        }
    }

    // 4. XÓA NGƯỜI CHƠI
    public void deletePlayer(int id) throws Exception {
        String sql = "DELETE FROM Player WHERE PlayerId = ?";
        try (Connection conn = db.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setInt(1, id);
            ps.executeUpdate();
        }
    }

    // 5. TÌM KIẾM THEO TÊN
    public List<Player> findByName(String name) throws Exception {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId WHERE p.PlayerName LIKE ?";
        try (Connection conn = db.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setString(1, "%" + name + "%");
            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(new Player(rs.getInt("PlayerId"), rs.getString("PlayerName"),
                        rs.getInt("HighScore"), rs.getInt("Level"), rs.getString("NationalName")));
            }
        }
        return list;
    }
}
package herogamedao;

import herogamedatabase.DBContext;
import herogameentity.National;
import herogameentity.Player;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PlayerDAO extends DBContext {

    // Câu 2: Hiển thị tất cả (Table 1 trong đề)
    public List<Player> displayAll() {
        List<Player> list = new ArrayList<>();
        // Dùng JOIN để lấy tên quốc gia từ bảng National
        String sql = "SELECT p.*, n.NationalName FROM Player p " +
                "JOIN National n ON p.NationalId = n.NationalId";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql);
             ResultSet rs = ps.executeQuery()) {

            while (rs.next()) {
                list.add(new Player(
                        rs.getInt("PlayerId"),
                        rs.getString("PlayerName"),
                        rs.getInt("HighScore"),
                        rs.getInt("Level"),
                        rs.getInt("NationalId"),
                        rs.getString("NationalName")
                ));
            }
        } catch (Exception e) { e.printStackTrace(); }
        return list;
    }

    // Câu 1: Thêm Player mới (insertPlayer)
    public void insertPlayer(Player p) {
        String sql = "INSERT INTO Player (NationalId, PlayerName, HighScore, Level) VALUES (?, ?, ?, ?)";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, p.getNationalId());
            ps.setString(2, p.getPlayerName());
            ps.setInt(3, p.getHighScore());
            ps.setInt(4, p.getLevel());
            ps.executeUpdate();
        } catch (Exception e) { e.printStackTrace(); }
    }

    // Câu 3: Tìm kiếm theo tên (displayAllByPlayerName)
    public List<Player> displayAllByPlayerName(String name) {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT p.*, n.NationalName FROM Player p " +
                "JOIN National n ON p.NationalId = n.NationalId " +
                "WHERE p.PlayerName LIKE ?";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setString(1, "%" + name + "%");
            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(new Player(rs.getInt(1), rs.getString(3), rs.getInt(4), rs.getInt(5), rs.getInt(2), rs.getString(6)));
            }
        } catch (Exception e) { e.printStackTrace(); }
        return list;
    }

    // Câu 4: Hiển thị Top 10 (displayTop10)
    public List<Player> displayTop10() {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT p.*, n.NationalName FROM Player p " +
                "JOIN National n ON p.NationalId = n.NationalId " +
                "ORDER BY p.HighScore DESC LIMIT 10";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql);
             ResultSet rs = ps.executeQuery()) {
            while (rs.next()) {
                list.add(new Player(rs.getInt(1), rs.getString(3), rs.getInt(4), rs.getInt(5), rs.getInt(2), rs.getString(6)));
            }
        } catch (Exception e) { e.printStackTrace(); }
        return list;
    }
    // Thêm vào PlayerDAO.java
    public void deletePlayer(int id) {
        String sql = "DELETE FROM Player WHERE PlayerId = ?";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (Exception e) { e.printStackTrace(); }
    }

    public void updatePlayer(Player p) {
        String sql = "UPDATE Player SET PlayerName=?, HighScore=?, Level=?, NationalId=? WHERE PlayerId=?";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setString(1, p.getPlayerName());
            ps.setInt(2, p.getHighScore());
            ps.setInt(3, p.getLevel());
            ps.setInt(4, p.getNationalId());
            ps.setInt(5, p.getPlayerId());
            ps.executeUpdate();
        } catch (Exception e) { e.printStackTrace(); }
    }
    // Thêm vào PlayerDAO.java
    public List<National> getAllNationals() {
        List<National> list = new ArrayList<>();
        String sql = "SELECT * FROM National";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql);
             ResultSet rs = ps.executeQuery()) {
            while (rs.next()) {
                list.add(new National(rs.getInt("NationalId"), rs.getString("NationalName")));
            }
        } catch (Exception e) { e.printStackTrace(); }
        return list;
    }
}

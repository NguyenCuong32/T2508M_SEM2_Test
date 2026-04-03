package org.example.letriphuong.daos;

import org.example.letriphuong.database.DBConnection;
import org.example.letriphuong.models.Player;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PlayerDAO {
    // 1. insertPlayer
    public boolean insertPlayer(Player p) {
        String sql = "INSERT INTO Player (NationalId, PlayerName, HighScore, Level) VALUES (?, ?, ?, ?)";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, p.getNationalId());
            ps.setString(2, p.getPlayerName());
            ps.setInt(3, p.getHighScore());
            ps.setInt(4, p.getLevel());
            return ps.executeUpdate() > 0;
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }

    // 1. deletePlayer
    public boolean deletePlayer(int id) {
        String sql = "DELETE FROM Player WHERE PlayerId = ?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            return ps.executeUpdate() > 0;
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }

    // 2. displayAll
    public List<Player> displayAll() {
        return getPlayersBySQL("SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId");
    }

    // 3. displayAllByPlayerName
    public List<Player> displayAllByPlayerName(String name) {
        String sql = "SELECT p.*, n.NationalName FROM Player p " +
                     "JOIN National n ON p.NationalId = n.NationalId " +
                     "WHERE p.PlayerName LIKE ?";
        List<Player> list = new ArrayList<>();
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setString(1, "%" + name + "%");
            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(extractPlayer(rs));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return list;
    }

    // 4. displayTop10
    public List<Player> displayTop10() {
        return getPlayersBySQL("SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId " +
                               "ORDER BY p.HighScore DESC LIMIT 10");
    }

    private List<Player> getPlayersBySQL(String sql) {
        List<Player> list = new ArrayList<>();
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(extractPlayer(rs));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return list;
    }

    private Player extractPlayer(ResultSet rs) throws SQLException {
        return new Player(
                rs.getInt("PlayerId"),
                rs.getInt("NationalId"),
                rs.getString("PlayerName"),
                rs.getInt("HighScore"),
                rs.getInt("Level"),
                rs.getString("NationalName")
        );
    }
}

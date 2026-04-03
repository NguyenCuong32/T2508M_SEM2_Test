package com.final_test.dao;

import com.final_test.entity.National;
import com.final_test.entity.Player;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PlayerDAO {
    private static final String URL = readConfig("db.url", "DB_URL", "jdbc:mysql://localhost:3306/HeroGame");
    private static final String USER = readConfig("db.user", "DB_USER", "root");
    private static final String PASS = readConfig("db.pass", "DB_PASS", "123456");

    public static Connection getConnection() throws SQLException {
        return DriverManager.getConnection(URL, USER, PASS);
    }

    public boolean insertPlayer(Player p) {
        String sql = "INSERT INTO Player (NationalId, PlayerName, HighScore, Level) VALUES (?, ?, ?, ?)";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, p.getNationalId());
            ps.setString(2, p.getPlayerName());
            ps.setInt(3, p.getHighScore());
            ps.setInt(4, p.getLevel());
            return ps.executeUpdate() > 0;
        } catch (SQLException e) {
            logSqlError("insert player", e);
        }
        return false;
    }

    public boolean deletePlayer(int playerId) {
        String sql = "DELETE FROM Player WHERE PlayerId = ?";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, playerId);
            return ps.executeUpdate() > 0;
        } catch (SQLException e) {
            logSqlError("delete player", e);
        }
        return false;
    }

    public List<Player> displayAll() {
        return getPlayersByQuery("SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId");
    }

    public List<Player> displayAllByPlayerName(String name) {
        String sql = "SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId WHERE p.PlayerName LIKE ?";
        List<Player> list = new ArrayList<>();
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setString(1, "%" + name + "%");
            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(extractPlayer(rs));
            }
        } catch (SQLException e) {
            logSqlError("search players by name", e);
        }
        return list;
    }

    public List<Player> displayTop10() {
        return getPlayersByQuery("SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId ORDER BY p.HighScore DESC LIMIT 10");
    }

    private List<Player> getPlayersByQuery(String sql) {
        List<Player> list = new ArrayList<>();
        try (Connection conn = getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                list.add(extractPlayer(rs));
            }
        } catch (SQLException e) {
            logSqlError("load players", e);
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

    public List<National> getAllNationals() {
        List<National> list = new ArrayList<>();
        String sql = "SELECT * FROM National";
        try (Connection conn = getConnection(); Statement stmt = conn.createStatement(); ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                list.add(new National(rs.getInt("NationalId"), rs.getString("NationalName")));
            }
        } catch (SQLException e) {
            logSqlError("load national list", e);
        }
        return list;
    }

    private static String readConfig(String systemPropertyKey, String envKey, String defaultValue) {
        String propertyValue = System.getProperty(systemPropertyKey);
        if (propertyValue != null && !propertyValue.isBlank()) {
            return propertyValue;
        }

        String envValue = System.getenv(envKey);
        if (envValue != null && !envValue.isBlank()) {
            return envValue;
        }

        return defaultValue;
    }

    private static void logSqlError(String action, SQLException e) {
        System.err.println("Failed to " + action + ": " + e.getMessage());
    }
}

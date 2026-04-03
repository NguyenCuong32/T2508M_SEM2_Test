package com.example.demo.repository;

import com.example.demo.connect.DBConnection;
import com.example.demo.entity.Player;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PlayerRepository {

    public void insertPlayer(Player p) {
        String sql = "INSERT INTO player(NationalId, PlayerName, HighScore, Level) VALUES (?, ?, ?, ?)";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, p.getNationalId());
            ps.setString(2, p.getPlayerName());
            ps.setInt(3, p.getHighScore());
            ps.setInt(4, p.getLevel());
            ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void deletePlayer(int id) {
        String sql = "DELETE FROM player WHERE PlayerId = ?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public List<Player> getAll() {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT p.*, n.NationalName FROM player p " +
                "JOIN national n ON p.NationalId = n.NationalId";
        try (Connection conn = DBConnection.getConnection();
             Statement st = conn.createStatement();
             ResultSet rs = st.executeQuery(sql)) {
            while (rs.next()) {
                list.add(new Player(
                        rs.getInt("PlayerId"),
                        rs.getInt("NationalId"),
                        rs.getString("PlayerName"),
                        rs.getInt("HighScore"),
                        rs.getInt("Level"),
                        rs.getString("NationalName")
                ));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
    }

    public List<Player> findByName(String name) {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT p.*, n.NationalName FROM player p " +
                "JOIN national n ON p.NationalId = n.NationalId " +
                "WHERE p.PlayerName LIKE ?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setString(1, "%" + name + "%");
            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(new Player(
                        rs.getInt("PlayerId"),
                        rs.getInt("NationalId"),
                        rs.getString("PlayerName"),
                        rs.getInt("HighScore"),
                        rs.getInt("Level"),
                        rs.getString("NationalName")
                ));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
    }

    public List<Player> top10() {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT p.*, n.NationalName FROM player p " +
                "JOIN national n ON p.NationalId = n.NationalId " +
                "ORDER BY p.HighScore DESC LIMIT 10";
        try (Connection conn = DBConnection.getConnection();
             Statement st = conn.createStatement();
             ResultSet rs = st.executeQuery(sql)) {
            while (rs.next()) {
                list.add(new Player(
                        rs.getInt("PlayerId"),
                        rs.getInt("NationalId"),
                        rs.getString("PlayerName"),
                        rs.getInt("HighScore"),
                        rs.getInt("Level"),
                        rs.getString("NationalName")
                ));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
    }
}
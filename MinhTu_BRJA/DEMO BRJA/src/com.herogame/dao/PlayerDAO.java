package com.herogame.dao;

import com.herogame.model.Player;
import com.herogame.utils.DBConnection;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PlayerDAO {

    public List<Player> getAll() {
        List<Player> list = new ArrayList<>();
        try {
            Connection conn = DBConnection.getConnection();
            String sql = "SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId";
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);

            while (rs.next()) {
                list.add(new Player(
                        rs.getInt("PlayerId"),
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

    public void add(String name, int score, int level, int nationalId) {
        try {
            Connection conn = DBConnection.getConnection();
            String sql = "INSERT INTO Player(PlayerName, HighScore, Level, NationalId) VALUES (?, ?, ?, ?)";
            PreparedStatement ps = conn.prepareStatement(sql);
            ps.setString(1, name);
            ps.setInt(2, score);
            ps.setInt(3, level);
            ps.setInt(4, nationalId);
            ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void delete(int id) {
        try {
            Connection conn = DBConnection.getConnection();
            String sql = "DELETE FROM Player WHERE PlayerId=?";
            PreparedStatement ps = conn.prepareStatement(sql);
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public List<Player> search(String keyword) {
        List<Player> list = new ArrayList<>();
        try {
            Connection conn = DBConnection.getConnection();
            String sql = "SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId WHERE PlayerName LIKE ?";
            PreparedStatement ps = conn.prepareStatement(sql);
            ps.setString(1, "%" + keyword + "%");

            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(new Player(
                        rs.getInt("PlayerId"),
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
        try {
            Connection conn = DBConnection.getConnection();
            String sql = "SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId ORDER BY HighScore DESC LIMIT 10";
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);

            while (rs.next()) {
                list.add(new Player(
                        rs.getInt("PlayerId"),
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
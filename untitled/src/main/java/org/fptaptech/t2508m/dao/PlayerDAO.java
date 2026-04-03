package org.fptaptech.t2508m.dao;

import org.fptaptech.t2508m.model.National;
import org.fptaptech.t2508m.model.Player;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class PlayerDAO {

    public boolean insert(Player p) {
        String sql = "INSERT INTO Player (PlayerName, HighScore, Level, NationalId) VALUES (?, ?, ?, ?)";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setString(1, p.getPlayerName());
            ps.setInt(2, p.getHighScore());
            ps.setInt(3, p.getLevel());
            ps.setInt(4, p.getNational().getNationalId());

            return ps.executeUpdate() > 0;

        } catch (Exception e) {
            return false;
        }
    }

    public boolean update(Player p) {
        String sql = "UPDATE Player SET PlayerName=?, HighScore=?, Level=?, NationalId=? WHERE PlayerId=?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setString(1, p.getPlayerName());
            ps.setInt(2, p.getHighScore());
            ps.setInt(3, p.getLevel());
            ps.setInt(4, p.getNational().getNationalId());
            ps.setInt(5, p.getPlayerId());

            return ps.executeUpdate() > 0;

        } catch (Exception e) {
            return false;
        }
    }

    public boolean delete(int id) {
        String sql = "DELETE FROM Player WHERE PlayerId=?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setInt(1, id);
            return ps.executeUpdate() > 0;

        } catch (Exception e) {
            return false;
        }
    }


    public List<Player> findAll() {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT p.*, n.NationalName FROM Player p JOIN National n ON p.NationalId = n.NationalId";

        try (Connection conn = DBConnection.getConnection();
             Statement st = conn.createStatement();
             ResultSet rs = st.executeQuery(sql)) {

            while (rs.next()) {
                National n = new National(
                        rs.getInt("NationalId"),
                        rs.getString("NationalName")
                );

                Player p = new Player();
                p.setPlayerId(rs.getInt("PlayerId"));
                p.setPlayerName(rs.getString("PlayerName"));
                p.setHighScore(rs.getInt("HighScore"));
                p.setLevel(rs.getInt("Level"));
                p.setNational(n);

                list.add(p);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

        return list;
    }

    public List<Player> findByName(String name) {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT * FROM Player WHERE PlayerName LIKE ?";

        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setString(1, "%" + name + "%");
            ResultSet rs = ps.executeQuery();

            while (rs.next()) {
                Player p = new Player();
                p.setPlayerId(rs.getInt("PlayerId"));
                p.setPlayerName(rs.getString("PlayerName"));
                p.setHighScore(rs.getInt("HighScore"));
                p.setLevel(rs.getInt("Level"));

                list.add(p);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

        return list;
    }

    public List<Player> getTop10() {
        List<Player> list = new ArrayList<>();
        String sql = "SELECT * FROM Player ORDER BY HighScore DESC LIMIT 10";

        try (Connection conn = DBConnection.getConnection();
             Statement st = conn.createStatement();
             ResultSet rs = st.executeQuery(sql)) {

            while (rs.next()) {
                Player p = new Player();
                p.setPlayerId(rs.getInt("PlayerId"));
                p.setPlayerName(rs.getString("PlayerName"));
                p.setHighScore(rs.getInt("HighScore"));
                p.setLevel(rs.getInt("Level"));

                list.add(p);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

        return list;
    }

}

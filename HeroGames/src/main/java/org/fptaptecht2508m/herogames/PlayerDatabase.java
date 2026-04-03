package org.fptaptecht2508m.herogames;

import org.fptaptecht2508m.herogames.entity.Player;

import java.sql.*;

public class PlayerDatabase {
    public void insertPlayer(Player p) throws Exception {
        Connection conn = DBConnection.getConnection();
        String sql = "Insert into Player(PlayerName, highScore, Level, NationalId) Values (?, ?, ?, ?)";

        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setString(1, p.getPlayerName());
        ps.setInt(2, p.getHighScore());
        ps.setInt(3, p.getLevel());
        ps.setInt(4, p.getNationalId());

        ps.executeUpdate();
        conn.close();
    }

    public void deletePlayer(int id) throws Exception {
        Connection conn = DBConnection.getConnection();
        String sql = "Delete From player Where PlayerId=?";

        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setInt(1, id);

        ps.executeUpdate();
        conn.close();
    }

    public void displayAll() throws Exception{
        Connection conn = DBConnection.getConnection();
        String sql = "SELECT p.PlayerId, p.PlayerName, p.highScore, p.Level, n.NationalName" + "FROM Player p JOIN National n ON p.NationalId = n.NationalId";

        Statement st = conn.createStatement();
        ResultSet rs = st.executeQuery(sql);

        while (rs.next()) {
            System.out.println(
                    rs.getInt("PlayerId") + "|" +
                    rs.getInt("PlayerName") + "|" +
                    rs.getInt("highScore") + "|" +
                    rs.getInt("Level") + "|" +
                    rs.getString("NationName") + "|"
            );
        }
        conn.close();
    }

    public void displayAllByPlayerName(String name) throws Exception{
        Connection conn = DBConnection.getConnection();

        String sql = "SELECT * FROM Player WHERE PlayerName LIKE ?";
        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setString(1, "%" + name + "%");
        ResultSet rs = ps.executeQuery();

        while (rs.next()){
            System.out.println(rs.getString("PlayerName"));
        }
        conn.close();
    }

    public void displayTop10() throws Exception{
        Connection conn = DBConnection.getConnection();

        String sql = "SELECT * FROM Player ORDER BY highScore DESC LIMIT 10";
        Statement st = conn.createStatement();
        ResultSet rs = st.executeQuery(sql);

        while (rs.next()){
            System.out.println(
                    rs.getString("PlayerName") + "-" +
                    rs.getInt("highScore")
            );
        }
        conn.close();
    }
}

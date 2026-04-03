package org.example.letriphuong.daos;

import org.example.letriphuong.database.DBConnection;
import org.example.letriphuong.models.National;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class NationalDAO {
    public List<National> getAllNationals() {
        List<National> list = new ArrayList<>();
        String sql = "SELECT * FROM National";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(new National(
                        rs.getInt("NationalId"),
                        rs.getString("NationalName")
                ));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return list;
    }

    public void insertNational(String name) {
        String sql = "INSERT INTO National (NationalName) VALUES (?)";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setString(1, name);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void deleteNational(int id) {
        String sql = "DELETE FROM National WHERE NationalId = ?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}

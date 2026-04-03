package com.example.demo.repository;

import com.example.demo.connect.DBConnection;
import com.example.demo.entity.National;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class NationalRepository {

    public List<National> getAll() {
        List<National> list = new ArrayList<>();
        try (Connection conn = DBConnection.getConnection();
             Statement st = conn.createStatement();
             ResultSet rs = st.executeQuery("SELECT * FROM national")) {
            while (rs.next()) {
                list.add(new National(rs.getInt("NationalId"), rs.getString("NationalName")));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
    }

    public void insertNational(National n) {
        String sql = "INSERT INTO national(NationalId, NationalName) VALUES (?, ?)";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, n.getNationalId());
            ps.setString(2, n.getNationalName());
            ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void deleteNational(int id) {
        String sql = "DELETE FROM national WHERE NationalId = ?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
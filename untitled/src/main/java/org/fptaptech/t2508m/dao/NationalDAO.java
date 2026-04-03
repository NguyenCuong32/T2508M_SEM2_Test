package org.fptaptech.t2508m.dao;

import org.fptaptech.t2508m.model.National;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class NationalDAO {
    public boolean insert(National n) {
        String sql = "INSERT INTO National (NationalName) VALUES (?)";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setString(1, n.getNationalName());
            return ps.executeUpdate() > 0;

        } catch (Exception e) {
            return false;
        }
    }

    public boolean update(National n) {
        String sql = "UPDATE National SET NationalName=? WHERE NationalId=?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setString(1, n.getNationalName());
            ps.setInt(2, n.getNationalId());
            return ps.executeUpdate() > 0;

        } catch (Exception e) {
            return false;
        }
    }

    public boolean delete(int id) {
        String sql = "DELETE FROM National WHERE NationalId=?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {

            ps.setInt(1, id);
            return ps.executeUpdate() > 0;

        } catch (Exception e) {
            return false;
        }
    }

    public List<National> findAll() {
        List<National> list = new ArrayList<>();
        String sql = "SELECT * FROM National";

        try (Connection conn = DBConnection.getConnection();
             Statement st = conn.createStatement();
             ResultSet rs = st.executeQuery(sql)) {

            while (rs.next()) {
                National n = new National();
                n.setNationalId(rs.getInt("NationalId"));
                n.setNationalName(rs.getString("NationalName"));
                list.add(n);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

        return list;
    }
}

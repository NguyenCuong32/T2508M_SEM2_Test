package com.herogame.dao;

import com.herogame.utils.DBConnection;

import java.sql.Connection;
import java.sql.PreparedStatement;

public class NationalDAO {

    public void add(String name) {
        try {
            Connection conn = DBConnection.getConnection();
            String sql = "INSERT INTO National(NationalName) VALUES (?)";
            PreparedStatement ps = conn.prepareStatement(sql);
            ps.setString(1, name);
            ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void delete(int id) {
        try {
            Connection conn = DBConnection.getConnection();
            String sql = "DELETE FROM National WHERE NationalId=?";
            PreparedStatement ps = conn.prepareStatement(sql);
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
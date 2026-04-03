package org.example.letriphuong.database;

import java.sql.Connection;
import java.sql.SQLException;

public class TestConnection {
    public static void main(String[] args) {
        try {
            Connection conn = DBConnection.getConnection();
            if (conn != null) {
                System.out.println("--- CONNECTION SUCCESSFUL! ---");
                conn.close();
            }
        } catch (SQLException e) {
            System.err.println("--- CONNECTION FAILED! ---");
            System.err.println("Reason: " + e.getMessage());
        }
    }
}

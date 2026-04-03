package org.fptaptech.t2508m.dao;
import java.sql.Connection;
import java.sql.DriverManager;
public class DBConnection {
    public static Connection getConnection() {
        try {
            return DriverManager.getConnection(
                    "jdbc:mysql://localhost:3306/herogame   ",
                    "hungdev",
                    "hung404*"
            );
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
}

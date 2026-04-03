package com.herogame.utils;

import java.sql.Connection;
import java.sql.DriverManager;

public class DBConnection {
    public static Connection getConnection() {
        try {
            return DriverManager.getConnection(
                    "jdbc:mysql://localhost:3306/HeroGame",
                    "root",
                    ""
            );
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
}
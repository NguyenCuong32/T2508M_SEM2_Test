package org.fptaptecht2508m.herogames;

import java.sql.Connection;
import java.sql.DriverManager;

public class DBConnection {
    public static Connection getConnection() throws Exception{
        String url = "jdbc:mysql://localhost:3306/HeroGames";
        String user = "root";
        String password = "HuyBuia123";

        return DriverManager.getConnection(url, user, password);
    }
}

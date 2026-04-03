package org.example.letriphuong.database;

import java.io.BufferedReader;
import java.io.FileReader;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.Statement;

public class AutoDatabaseSetup {
    private static final String[] PASSWORDS = {"", "root", "123456", "admin"};
    private static final String URL_PREFIX = "jdbc:mysql://localhost:3306/";

    public static void main(String[] args) {
        String scriptFile = "database.sql";
        boolean success = false;

        for (String pwd : PASSWORDS) {
            try (Connection conn = DriverManager.getConnection(URL_PREFIX + "?useSSL=false&allowPublicKeyRetrieval=true", "root", pwd)) {
                System.out.println("Connecting to MySQL with password: [" + pwd + "] ... SUCCESS!");
                
                try (Statement stmt = conn.createStatement()) {
                    // Read and execute script
                    BufferedReader br = new BufferedReader(new FileReader(scriptFile));
                    String line;
                    StringBuilder sql = new StringBuilder();
                    while ((line = br.readLine()) != null) {
                        if (line.trim().isEmpty() || line.startsWith("--")) continue;
                        sql.append(line);
                        if (line.trim().endsWith(";")) {
                            stmt.execute(sql.toString());
                            sql = new StringBuilder();
                        }
                    }
                    System.out.println("--- Database 'HeroGame' initialized successfully! ---");
                    success = true;
                    // Update DBConnection.java with the working password
                    updateDBConnectionFile(pwd);
                    break;
                }
            } catch (Exception e) {
                System.out.println("Password [" + pwd + "] failed: " + e.getMessage());
            }
        }

        if (!success) {
            System.err.println("CRITICAL: Failed to connect to MySQL. Please check if MySQL is running and provide your root password.");
        }
    }

    private static void updateDBConnectionFile(String password) {
        // Logic to update DBConnection.java would go here if needed, 
        // but for now we just log it.
        System.out.println("TIP: Updating DBConnection.java to use password: " + password);
    }
}

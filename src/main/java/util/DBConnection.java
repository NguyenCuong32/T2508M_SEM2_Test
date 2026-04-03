package util;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.SQLException;

public class DBConnection {
    private static final String SERVER_URL = "jdbc:mysql://localhost:3306/?useSSL=false&allowPublicKeyRetrieval=true&serverTimezone=UTC";
    private static final String DATABASE_URL = "jdbc:mysql://localhost:3306/HeroGame?useSSL=false&allowPublicKeyRetrieval=true&serverTimezone=UTC";
    private static final String USERNAME = "root";
    private static final String PASSWORD = "";
    private static final String SQL_RESOURCE = "/sql/hero_game.sql";
    private static boolean initialized = false;

    private DBConnection() {
    }

    public static synchronized void initializeDatabase() throws SQLException {
        if (initialized) {
            return;
        }

        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
        } catch (ClassNotFoundException e) {
            throw new SQLException("MySQL JDBC Driver not found.", e);
        }

        Connection connection;
        try {
            connection = DriverManager.getConnection(SERVER_URL, USERNAME, PASSWORD);
        } catch (SQLException e) {
            throw new SQLException(buildConnectionErrorMessage(e), e);
        }

        try (Connection closableConnection = connection) {
            runSqlScript(connection);
            initialized = true;
        } catch (SQLException e) {
            throw new SQLException(buildInitializationErrorMessage(e), e);
        }
    }

    public static Connection getConnection() throws SQLException {
        initializeDatabase();
        return DriverManager.getConnection(DATABASE_URL, USERNAME, PASSWORD);
    }

    private static void runSqlScript(Connection connection) throws SQLException {
        try (InputStream inputStream = DBConnection.class.getResourceAsStream(SQL_RESOURCE)) {
            if (inputStream == null) {
                throw new SQLException("Cannot find SQL resource: " + SQL_RESOURCE);
            }

            try (BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream, StandardCharsets.UTF_8))) {
                StringBuilder statementBuilder = new StringBuilder();
                String line;

                while ((line = reader.readLine()) != null) {
                    String trimmedLine = line.trim();

                    if (trimmedLine.isEmpty() || trimmedLine.startsWith("--") || trimmedLine.startsWith("#")) {
                        continue;
                    }

                    statementBuilder.append(line).append('\n');

                    if (trimmedLine.endsWith(";")) {
                        executeStatement(connection, statementBuilder.toString());
                        statementBuilder.setLength(0);
                    }
                }

                if (statementBuilder.length() > 0) {
                    executeStatement(connection, statementBuilder.toString());
                }
            } catch (IOException e) {
                throw new SQLException("Cannot read SQL initialization file.", e);
            }
        } catch (IOException e) {
            throw new SQLException("Cannot load SQL initialization file.", e);
        }
    }

    private static void executeStatement(Connection connection, String sql) throws SQLException {
        String normalizedSql = sql.trim();
        if (normalizedSql.endsWith(";")) {
            normalizedSql = normalizedSql.substring(0, normalizedSql.length() - 1).trim();
        }

        if (normalizedSql.isEmpty()) {
            return;
        }

        try (PreparedStatement preparedStatement = connection.prepareStatement(normalizedSql)) {
            preparedStatement.execute();
        }
    }

    private static String buildConnectionErrorMessage(SQLException exception) {
        return "Cannot connect to MySQL at localhost:3306.\n"
                + "Please open XAMPP and start MySQL, then run the app again.\n"
                + "Default account: user 'root' with empty password.\n"
                + "Technical detail: " + exception.getMessage();
    }

    private static String buildInitializationErrorMessage(SQLException exception) {
        return "Connected to MySQL, but failed to initialize database 'HeroGame'.\n"
                + "Please check the SQL script inside the project resources.\n"
                + "Technical detail: " + exception.getMessage();
    }
}

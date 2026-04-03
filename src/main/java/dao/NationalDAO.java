package dao;

import model.National;
import util.DBConnection;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class NationalDAO {

    public boolean insertNational(National national) {
        String sql = "INSERT INTO National (NationalName) VALUES (?)";

        try (Connection connection = DBConnection.getConnection();
             PreparedStatement preparedStatement = connection.prepareStatement(sql)) {

            preparedStatement.setString(1, national.getNationalName());
            return preparedStatement.executeUpdate() > 0;
        } catch (SQLException e) {
            System.out.println("Error inserting national: " + e.getMessage());
            return false;
        }
    }

    public boolean deleteNational(int nationalId) {
        String sql = "DELETE FROM National WHERE NationalId = ?";

        try (Connection connection = DBConnection.getConnection();
             PreparedStatement preparedStatement = connection.prepareStatement(sql)) {

            preparedStatement.setInt(1, nationalId);
            return preparedStatement.executeUpdate() > 0;
        } catch (SQLException e) {
            System.out.println("Error deleting national: " + e.getMessage());
            return false;
        }
    }

    public List<National> getAllNational() {
        List<National> nationals = new ArrayList<>();
        String sql = "SELECT NationalId, NationalName FROM National ORDER BY NationalId";

        try (Connection connection = DBConnection.getConnection();
             PreparedStatement preparedStatement = connection.prepareStatement(sql);
             ResultSet resultSet = preparedStatement.executeQuery()) {

            while (resultSet.next()) {
                nationals.add(new National(
                        resultSet.getInt("NationalId"),
                        resultSet.getString("NationalName")
                ));
            }
        } catch (SQLException e) {
            System.out.println("Error loading national list: " + e.getMessage());
        }
        return nationals;
    }
}

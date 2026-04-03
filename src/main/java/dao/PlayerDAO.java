package dao;

import model.Player;
import util.DBConnection;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class PlayerDAO {

    public boolean insertPlayer(Player player) {
        String sql = "INSERT INTO Player (NationalId, PlayerName, HighScore, Level) VALUES (?, ?, ?, ?)";

        try (Connection connection = DBConnection.getConnection();
             PreparedStatement preparedStatement = connection.prepareStatement(sql)) {

            preparedStatement.setInt(1, player.getNationalId());
            preparedStatement.setString(2, player.getPlayerName());
            preparedStatement.setInt(3, player.getHighScore());
            preparedStatement.setInt(4, player.getLevel());

            return preparedStatement.executeUpdate() > 0;
        } catch (SQLException e) {
            System.out.println("Error inserting player: " + e.getMessage());
            return false;
        }
    }

    public boolean deletePlayer(int playerId) {
        String sql = "DELETE FROM Player WHERE PlayerId = ?";

        try (Connection connection = DBConnection.getConnection();
             PreparedStatement preparedStatement = connection.prepareStatement(sql)) {

            preparedStatement.setInt(1, playerId);
            return preparedStatement.executeUpdate() > 0;
        } catch (SQLException e) {
            System.out.println("Error deleting player: " + e.getMessage());
            return false;
        }
    }

    public List<Player> displayAll() {
        List<Player> players = new ArrayList<>();
        String sql = "SELECT p.PlayerId, p.NationalId, p.PlayerName, p.HighScore, p.Level, n.NationalName " +
                "FROM Player p JOIN National n ON p.NationalId = n.NationalId " +
                "ORDER BY p.PlayerId";

        try (Connection connection = DBConnection.getConnection();
             PreparedStatement preparedStatement = connection.prepareStatement(sql);
             ResultSet resultSet = preparedStatement.executeQuery()) {

            while (resultSet.next()) {
                players.add(new Player(
                        resultSet.getInt("PlayerId"),
                        resultSet.getInt("NationalId"),
                        resultSet.getString("PlayerName"),
                        resultSet.getInt("HighScore"),
                        resultSet.getInt("Level"),
                        resultSet.getString("NationalName")
                ));
            }
        } catch (SQLException e) {
            System.out.println("Error displaying all players: " + e.getMessage());
        }
        return players;
    }

    public List<Player> displayAllByPlayerName(String playerName) {
        List<Player> players = new ArrayList<>();
        String sql = "SELECT p.PlayerId, p.NationalId, p.PlayerName, p.HighScore, p.Level, n.NationalName " +
                "FROM Player p JOIN National n ON p.NationalId = n.NationalId " +
                "WHERE p.PlayerName LIKE ? " +
                "ORDER BY p.PlayerId";

        try (Connection connection = DBConnection.getConnection();
             PreparedStatement preparedStatement = connection.prepareStatement(sql)) {

            preparedStatement.setString(1, "%" + playerName + "%");

            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    players.add(new Player(
                            resultSet.getInt("PlayerId"),
                            resultSet.getInt("NationalId"),
                            resultSet.getString("PlayerName"),
                            resultSet.getInt("HighScore"),
                            resultSet.getInt("Level"),
                            resultSet.getString("NationalName")
                    ));
                }
            }
        } catch (SQLException e) {
            System.out.println("Error searching players by name: " + e.getMessage());
        }
        return players;
    }

    public List<Player> displayTop10() {
        List<Player> players = new ArrayList<>();
        String sql = "SELECT p.PlayerId, p.NationalId, p.PlayerName, p.HighScore, p.Level, n.NationalName " +
                "FROM Player p JOIN National n ON p.NationalId = n.NationalId " +
                "ORDER BY p.HighScore DESC, p.PlayerId ASC " +
                "LIMIT 10";

        try (Connection connection = DBConnection.getConnection();
             PreparedStatement preparedStatement = connection.prepareStatement(sql);
             ResultSet resultSet = preparedStatement.executeQuery()) {

            while (resultSet.next()) {
                players.add(new Player(
                        resultSet.getInt("PlayerId"),
                        resultSet.getInt("NationalId"),
                        resultSet.getString("PlayerName"),
                        resultSet.getInt("HighScore"),
                        resultSet.getInt("Level"),
                        resultSet.getString("NationalName")
                ));
            }
        } catch (SQLException e) {
            System.out.println("Error displaying top 10 players: " + e.getMessage());
        }
        return players;
    }
}

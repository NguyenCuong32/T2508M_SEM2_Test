package controller;

import dao.NationalDAO;
import dao.PlayerDAO;
import javafx.collections.FXCollections;
import javafx.fxml.FXML;
import javafx.scene.control.Alert;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import model.National;
import model.Player;

import java.util.List;

public class MainController {

    @FXML
    private TextField txtPlayerName;

    @FXML
    private TextField txtHighScore;

    @FXML
    private TextField txtLevel;

    @FXML
    private ComboBox<National> cbNational;

    @FXML
    private TextField txtSearchPlayerName;

    @FXML
    private TextField txtDeletePlayerId;

    @FXML
    private TextField txtNationalName;

    @FXML
    private TextField txtDeleteNationalId;

    @FXML
    private TableView<Player> tblPlayers;

    @FXML
    private TableColumn<Player, Integer> colPlayerId;

    @FXML
    private TableColumn<Player, String> colPlayerName;

    @FXML
    private TableColumn<Player, Integer> colHighScore;

    @FXML
    private TableColumn<Player, Integer> colLevel;

    @FXML
    private TableColumn<Player, String> colNational;

    private final PlayerDAO playerDAO = new PlayerDAO();
    private final NationalDAO nationalDAO = new NationalDAO();

    @FXML
    public void initialize() {
        colPlayerId.setCellValueFactory(new PropertyValueFactory<>("playerId"));
        colPlayerName.setCellValueFactory(new PropertyValueFactory<>("playerName"));
        colHighScore.setCellValueFactory(new PropertyValueFactory<>("highScore"));
        colLevel.setCellValueFactory(new PropertyValueFactory<>("level"));
        colNational.setCellValueFactory(new PropertyValueFactory<>("nationalName"));
        tblPlayers.setPlaceholder(new Label("No player data available."));
        tblPlayers.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY);

        loadNationalComboBox();
        loadAllPlayers();
    }

    @FXML
    private void handleAddPlayer() {
        String playerName = txtPlayerName.getText().trim();
        String highScoreText = txtHighScore.getText().trim();
        String levelText = txtLevel.getText().trim();
        National national = cbNational.getValue();

        if (playerName.isEmpty()) {
            showAlert(Alert.AlertType.ERROR, "Validation Error", "Player name cannot be empty.");
            return;
        }

        int highScore;
        try {
            highScore = Integer.parseInt(highScoreText);
            if (highScore < 0) {
                showAlert(Alert.AlertType.ERROR, "Validation Error", "HighScore must be greater than or equal to 0.");
                return;
            }
        } catch (NumberFormatException e) {
            showAlert(Alert.AlertType.ERROR, "Validation Error", "HighScore must be a valid integer.");
            return;
        }

        int level;
        try {
            level = Integer.parseInt(levelText);
            if (level < 1) {
                showAlert(Alert.AlertType.ERROR, "Validation Error", "Level must be greater than or equal to 1.");
                return;
            }
        } catch (NumberFormatException e) {
            showAlert(Alert.AlertType.ERROR, "Validation Error", "Level must be a valid integer.");
            return;
        }

        if (national == null) {
            showAlert(Alert.AlertType.ERROR, "Validation Error", "Please select a national.");
            return;
        }

        Player player = new Player(0, national.getNationalId(), playerName, highScore, level, national.getNationalName());
        boolean result = playerDAO.insertPlayer(player);

        if (result) {
            showAlert(Alert.AlertType.INFORMATION, "Success", "Player added successfully.");
            clearPlayerInputs();
            loadAllPlayers();
        } else {
            showAlert(Alert.AlertType.ERROR, "Database Error", "Cannot add player. Please check database connection.");
        }
    }

    @FXML
    private void handleDeletePlayer() {
        String playerIdText = txtDeletePlayerId.getText().trim();

        int playerId;
        try {
            playerId = Integer.parseInt(playerIdText);
        } catch (NumberFormatException e) {
            showAlert(Alert.AlertType.ERROR, "Validation Error", "Delete Player Id must be a valid integer.");
            return;
        }

        boolean result = playerDAO.deletePlayer(playerId);

        if (result) {
            showAlert(Alert.AlertType.INFORMATION, "Success", "Player deleted successfully.");
            txtDeletePlayerId.clear();
            loadAllPlayers();
        } else {
            showAlert(Alert.AlertType.ERROR, "Database Error", "Cannot delete player. Please check Player Id.");
        }
    }

    @FXML
    private void handleAddNational() {
        String nationalName = txtNationalName.getText().trim();

        if (nationalName.isEmpty()) {
            showAlert(Alert.AlertType.ERROR, "Validation Error", "National name cannot be empty.");
            return;
        }

        boolean result = nationalDAO.insertNational(new National(0, nationalName));

        if (result) {
            showAlert(Alert.AlertType.INFORMATION, "Success", "National added successfully.");
            txtNationalName.clear();
            loadNationalComboBox();
        } else {
            showAlert(Alert.AlertType.ERROR, "Database Error", "Cannot add national. Please check database connection.");
        }
    }

    @FXML
    private void handleDeleteNational() {
        String nationalIdText = txtDeleteNationalId.getText().trim();

        int nationalId;
        try {
            nationalId = Integer.parseInt(nationalIdText);
        } catch (NumberFormatException e) {
            showAlert(Alert.AlertType.ERROR, "Validation Error", "Delete National Id must be a valid integer.");
            return;
        }

        boolean result = nationalDAO.deleteNational(nationalId);

        if (result) {
            showAlert(Alert.AlertType.INFORMATION, "Success", "National deleted successfully.");
            txtDeleteNationalId.clear();
            loadNationalComboBox();
            loadAllPlayers();
        } else {
            showAlert(
                    Alert.AlertType.ERROR,
                    "Database Error",
                    "Cannot delete national. It may not exist or it is still used by players."
            );
        }
    }

    @FXML
    private void handleShowAll() {
        loadAllPlayers();
    }

    @FXML
    private void handleSearchByName() {
        String playerName = txtSearchPlayerName.getText().trim();
        List<Player> players = playerDAO.displayAllByPlayerName(playerName);
        tblPlayers.setItems(FXCollections.observableArrayList(players));
    }

    @FXML
    private void handleTop10() {
        List<Player> players = playerDAO.displayTop10();
        tblPlayers.setItems(FXCollections.observableArrayList(players));
    }

    @FXML
    private void handleRefreshNationalComboBox() {
        loadNationalComboBox();
        showAlert(Alert.AlertType.INFORMATION, "Success", "National ComboBox refreshed.");
    }

    private void loadAllPlayers() {
        List<Player> players = playerDAO.displayAll();
        tblPlayers.setItems(FXCollections.observableArrayList(players));
    }

    private void loadNationalComboBox() {
        List<National> nationals = nationalDAO.getAllNational();
        cbNational.setItems(FXCollections.observableArrayList(nationals));

        if (!nationals.isEmpty()) {
            cbNational.getSelectionModel().selectFirst();
        }
    }

    private void clearPlayerInputs() {
        txtPlayerName.clear();
        txtHighScore.clear();
        txtLevel.clear();

        if (!cbNational.getItems().isEmpty()) {
            cbNational.getSelectionModel().selectFirst();
        }
    }

    private void showAlert(Alert.AlertType alertType, String title, String message) {
        Alert alert = new Alert(alertType);
        alert.setTitle(title);
        alert.setHeaderText(null);
        alert.setContentText(message);
        alert.showAndWait();
    }
}

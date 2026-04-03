package org.example.letriphuong;

import javafx.application.Application;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.geometry.Insets;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.*;
import javafx.stage.Stage;
import org.example.letriphuong.daos.NationalDAO;
import org.example.letriphuong.daos.PlayerDAO;
import org.example.letriphuong.models.National;
import org.example.letriphuong.models.Player;

import java.util.Optional;

public class MainApp extends Application {
    private PlayerDAO playerDAO = new PlayerDAO();
    private NationalDAO nationalDAO = new NationalDAO();
    
    private TableView<Player> table = new TableView<>();
    private ComboBox<National> comboNational = new ComboBox<>();
    private TextField txtName = new TextField();
    private TextField txtHighScore = new TextField();
    private TextField txtLevel = new TextField();
    private TextField txtSearch = new TextField();

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage primaryStage) {
        primaryStage.setTitle("HeroGame - Player Management");

        // 1. Table View Configuration
        TableColumn<Player, Integer> colId = new TableColumn<>("PlayerId");
        colId.setCellValueFactory(new PropertyValueFactory<>("playerId"));

        TableColumn<Player, String> colName = new TableColumn<>("PlayerName");
        colName.setCellValueFactory(new PropertyValueFactory<>("playerName"));

        TableColumn<Player, Integer> colScore = new TableColumn<>("HighScore");
        colScore.setCellValueFactory(new PropertyValueFactory<>("highScore"));

        TableColumn<Player, Integer> colLevel = new TableColumn<>("Level");
        colLevel.setCellValueFactory(new PropertyValueFactory<>("level"));

        TableColumn<Player, String> colNational = new TableColumn<>("National");
        colNational.setCellValueFactory(cellData -> new SimpleStringProperty(cellData.getValue().getNationalName()));

        @SuppressWarnings("unchecked")
        ObservableList<TableColumn<Player, ?>> columns = table.getColumns();
        columns.addAll(colId, colName, colScore, colLevel, colNational);
        table.setPrefHeight(400);

        // 2. Search Area
        HBox searchBox = new HBox(10);
        searchBox.setPadding(new Insets(10));
        Button btnSearch = new Button("Search Name");
        Button btnShowAll = new Button("Show All");
        Button btnTop10 = new Button("Top 10 Highscore");
        searchBox.getChildren().addAll(new Label("Search:"), txtSearch, btnSearch, btnShowAll, btnTop10);

        // 3. Form Area
        GridPane form = new GridPane();
        form.setHgap(10);
        form.setVgap(10);
        form.setPadding(new Insets(10));

        form.add(new Label("Player Name:"), 0, 0);
        form.add(txtName, 1, 0);
        form.add(new Label("High Score:"), 0, 1);
        form.add(txtHighScore, 1, 1);
        form.add(new Label("Level:"), 2, 0);
        form.add(txtLevel, 3, 0);
        form.add(new Label("National:"), 2, 1);
        form.add(comboNational, 3, 1);

        Button btnAdd = new Button("Add Player");
        Button btnDelete = new Button("Delete Player");
        Button btnAddNational = new Button("Manage National"); // Separate Action

        HBox actions = new HBox(10);
        actions.setPadding(new Insets(10));
        actions.getChildren().addAll(btnAdd, btnDelete, btnAddNational);

        // 4. Main Layout
        VBox root = new VBox(10);
        root.getChildren().addAll(searchBox, new Label("Table 1: Player information"), table, form, actions);

        // 5. Button Actions
        btnShowAll.setOnAction(e -> loadPlayers());
        btnSearch.setOnAction(e -> searchPlayer());
        btnTop10.setOnAction(e -> loadTop10());
        btnAdd.setOnAction(e -> addPlayer());
        btnDelete.setOnAction(e -> deletePlayer());
        btnAddNational.setOnAction(e -> showNationalDialog());

        // 6. Initial Load
        loadNationals();
        loadPlayers();

        Scene scene = new Scene(root, 800, 650);
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    private void loadPlayers() {
        ObservableList<Player> data = FXCollections.observableArrayList(playerDAO.displayAll());
        table.setItems(data);
    }

    private void loadTop10() {
        ObservableList<Player> data = FXCollections.observableArrayList(playerDAO.displayTop10());
        table.setItems(data);
    }

    private void searchPlayer() {
        String query = txtSearch.getText();
        ObservableList<Player> data = FXCollections.observableArrayList(playerDAO.displayAllByPlayerName(query));
        table.setItems(data);
    }

    private void loadNationals() {
        comboNational.setItems(FXCollections.observableArrayList(nationalDAO.getAllNationals()));
    }

    private void addPlayer() {
        try {
            String name = txtName.getText();
            int score = Integer.parseInt(txtHighScore.getText());
            int level = Integer.parseInt(txtLevel.getText());
            National selected = comboNational.getValue();

            if (selected != null && !name.isEmpty()) {
                Player p = new Player(selected.getNationalId(), name, score, level);
                if (playerDAO.insertPlayer(p)) {
                    loadPlayers();
                    clearFields();
                }
            } else {
                showAlert("Error", "Please fill all fields and select a National.");
            }
        } catch (NumberFormatException e) {
            showAlert("Error", "Score and Level must be numbers.");
        }
    }

    private void deletePlayer() {
        Player selected = table.getSelectionModel().getSelectedItem();
        if (selected != null) {
            if (playerDAO.deletePlayer(selected.getPlayerId())) {
                loadPlayers();
            }
        } else {
            showAlert("Warning", "Please select a player to delete.");
        }
    }

    private void clearFields() {
        txtName.clear();
        txtHighScore.clear();
        txtLevel.clear();
    }

    private void showNationalDialog() {
        // Simple dialog to add/remove National
        TextInputDialog dialog = new TextInputDialog();
        dialog.setTitle("National Management");
        dialog.setHeaderText("Add New National Name:");
        Optional<String> result = dialog.showAndWait();
        result.ifPresent(name -> {
            nationalDAO.insertNational(name);
            loadNationals();
        });
    }

    private void showAlert(String title, String content) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle(title);
        alert.setContentText(content);
        alert.showAndWait();
    }
}

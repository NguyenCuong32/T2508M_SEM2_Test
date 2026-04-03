package com.example.demo;

import com.example.demo.entity.National;
import com.example.demo.entity.Player;
import com.example.demo.service.NationalService;
import com.example.demo.service.PlayerService;
import javafx.application.Application;
import javafx.collections.FXCollections;
import javafx.geometry.Insets;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class MainApp extends Application {

    private PlayerService playerService = new PlayerService();
    private NationalService nationalService = new NationalService();

    private TableView<Player> playerTable = new TableView<>();
    private ComboBox<National> cbNational = new ComboBox<>();
    private TableView<National> nationalTable = new TableView<>();

    @Override
    public void start(Stage stage) {

        TableColumn<Player, Integer> idCol = new TableColumn<>("ID");
        idCol.setCellValueFactory(new PropertyValueFactory<>("playerId"));

        TableColumn<Player, String> nameCol = new TableColumn<>("Name");
        nameCol.setCellValueFactory(new PropertyValueFactory<>("playerName"));

        TableColumn<Player, Integer> scoreCol = new TableColumn<>("Score");
        scoreCol.setCellValueFactory(new PropertyValueFactory<>("highScore"));

        TableColumn<Player, Integer> levelCol = new TableColumn<>("Level");
        levelCol.setCellValueFactory(new PropertyValueFactory<>("level"));

        TableColumn<Player, String> nationCol = new TableColumn<>("National");
        nationCol.setCellValueFactory(new PropertyValueFactory<>("nationalName"));

        playerTable.getColumns().addAll(idCol, nameCol, scoreCol, levelCol, nationCol);

        TextField txtName = new TextField(); txtName.setPromptText("Player Name");
        TextField txtScore = new TextField(); txtScore.setPromptText("Score");
        TextField txtLevel = new TextField(); txtLevel.setPromptText("Level");

        Button btnAddPlayer = new Button("Add Player");
        Button btnDeletePlayer = new Button("Delete Player");
        TextField txtSearch = new TextField(); txtSearch.setPromptText("Search name...");
        Button btnSearch = new Button("Search");
        Button btnTop10 = new Button("Top 10 Score");
        Button btnRefresh = new Button("Refresh All");

        btnAddPlayer.setOnAction(e -> {
            try {
                National selected = cbNational.getSelectionModel().getSelectedItem();
                if (selected == null) { showAlert("Error", "Please select a National!"); return; }
                Player p = new Player(0, selected.getNationalId(), txtName.getText(),
                        Integer.parseInt(txtScore.getText()),
                        Integer.parseInt(txtLevel.getText()), "");
                playerService.add(p);
                loadData();
            } catch (Exception ex) {
                showAlert("Error", "Check input data (Score/Level must be number)");
            }
        });

        btnDeletePlayer.setOnAction(e -> {
            Player selected = playerTable.getSelectionModel().getSelectedItem();
            if (selected != null) { playerService.delete(selected.getPlayerId()); loadData(); }
        });

        btnSearch.setOnAction(e -> playerTable.setItems(FXCollections.observableArrayList(playerService.search(txtSearch.getText()))));
        btnTop10.setOnAction(e -> playerTable.setItems(FXCollections.observableArrayList(playerService.top10())));
        btnRefresh.setOnAction(e -> loadData());

        HBox playerInputs = new HBox(10, txtName, txtScore, txtLevel, cbNational, btnAddPlayer);
        HBox searchBox = new HBox(10, txtSearch, btnSearch, btnTop10, btnRefresh, btnDeletePlayer);

        VBox playerLayout = new VBox(15, new Label("PLAYER MANAGEMENT"), playerTable,
                new Label("Add Player:"), playerInputs, searchBox);
        playerLayout.setPadding(new Insets(10));

        Tab playerTab = new Tab("Players", playerLayout);

        TableColumn<National, Integer> natIdCol = new TableColumn<>("ID");
        natIdCol.setCellValueFactory(new PropertyValueFactory<>("nationalId"));

        TableColumn<National, String> natNameCol = new TableColumn<>("Name");
        natNameCol.setCellValueFactory(new PropertyValueFactory<>("nationalName"));

        nationalTable.getColumns().addAll(natIdCol, natNameCol);

        TextField txtNationId = new TextField(); txtNationId.setPromptText("New National ID");
        TextField txtNationName = new TextField(); txtNationName.setPromptText("New National Name");
        Button btnAddNation = new Button("Add National");
        Button btnDeleteNation = new Button("Delete Selected National");

        btnAddNation.setOnAction(e -> {
            try {
                National n = new National(Integer.parseInt(txtNationId.getText()), txtNationName.getText());
                nationalService.add(n);
                loadData();
            } catch (Exception ex) { showAlert("Error", "Check National ID/Name"); }
        });

        btnDeleteNation.setOnAction(e -> {
            National selected = nationalTable.getSelectionModel().getSelectedItem();
            if (selected != null) { nationalService.delete(selected.getNationalId()); loadData(); }
        });

        HBox nationInputs = new HBox(10, txtNationId, txtNationName, btnAddNation, btnDeleteNation);
        VBox nationalLayout = new VBox(15, new Label("NATIONAL MANAGEMENT"), nationalTable, nationInputs);
        nationalLayout.setPadding(new Insets(10));

        Tab nationalTab = new Tab("Nationals", nationalLayout);

        TabPane tabPane = new TabPane(playerTab, nationalTab);

        Scene scene = new Scene(tabPane, 850, 650);
        stage.setScene(scene);
        stage.setTitle("Hero Game - Player & National Manager");
        stage.show();

        loadData();
    }

    private void loadData() {
        playerTable.setItems(FXCollections.observableArrayList(playerService.getAll()));
        nationalTable.setItems(FXCollections.observableArrayList(nationalService.getAll()));
        cbNational.setItems(FXCollections.observableArrayList(nationalService.getAll()));
    }

    private void showAlert(String title, String content) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle(title);
        alert.setContentText(content);
        alert.showAndWait();
    }

    public static void main(String[] args) { launch(); }
}
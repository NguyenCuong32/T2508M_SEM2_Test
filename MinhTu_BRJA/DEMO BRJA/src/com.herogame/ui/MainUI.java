package com.herogame.ui;

import com.herogame.dao.PlayerDAO;
import com.herogame.model.Player;
import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class MainUI extends Application {

    TableView<Player> table = new TableView<>();
    PlayerDAO dao = new PlayerDAO();

    public void start(Stage stage) {

        TableColumn<Player, Integer> colId = new TableColumn<>("ID");
        colId.setCellValueFactory(data -> new javafx.beans.property.SimpleIntegerProperty(data.getValue().getId()).asObject());

        TableColumn<Player, String> colName = new TableColumn<>("Name");
        colName.setCellValueFactory(data -> new javafx.beans.property.SimpleStringProperty(data.getValue().getName()));

        TableColumn<Player, Integer> colScore = new TableColumn<>("Score");
        colScore.setCellValueFactory(data -> new javafx.beans.property.SimpleIntegerProperty(data.getValue().getScore()).asObject());

        TableColumn<Player, Integer> colLevel = new TableColumn<>("Level");
        colLevel.setCellValueFactory(data -> new javafx.beans.property.SimpleIntegerProperty(data.getValue().getLevel()).asObject());

        TableColumn<Player, String> colNational = new TableColumn<>("National");
        colNational.setCellValueFactory(data -> new javafx.beans.property.SimpleStringProperty(data.getValue().getNational()));

        table.getColumns().addAll(colId, colName, colScore, colLevel, colNational);

        TextField txtName = new TextField();
        txtName.setPromptText("Name");

        TextField txtScore = new TextField();
        txtScore.setPromptText("Score");

        TextField txtLevel = new TextField();
        txtLevel.setPromptText("Level");

        TextField txtSearch = new TextField();
        txtSearch.setPromptText("Search...");

        Button btnAdd = new Button("Add");
        Button btnDelete = new Button("Delete");
        Button btnSearch = new Button("Search");
        Button btnTop = new Button("Top 10");

        btnAdd.setOnAction(e -> {
            dao.add(
                    txtName.getText(),
                    Integer.parseInt(txtScore.getText()),
                    Integer.parseInt(txtLevel.getText()),
                    1
            );
            loadData();
        });

        btnDelete.setOnAction(e -> {
            Player p = table.getSelectionModel().getSelectedItem();
            if (p != null) {
                dao.delete(p.getId());
                loadData();
            }
        });

        btnSearch.setOnAction(e -> {
            table.getItems().setAll(dao.search(txtSearch.getText()));
        });

        btnTop.setOnAction(e -> {
            table.getItems().setAll(dao.top10());
        });

        loadData();

        VBox root = new VBox(
                txtName, txtScore, txtLevel,
                btnAdd, btnDelete,
                txtSearch, btnSearch,
                btnTop,
                table
        );

        stage.setScene(new Scene(root, 600, 500));
        stage.setTitle("Hero Game");
        stage.show();
    }

    private void loadData() {
        table.getItems().setAll(dao.getAll());
    }

    public static void main(String[] args) {
        launch();
    }
}
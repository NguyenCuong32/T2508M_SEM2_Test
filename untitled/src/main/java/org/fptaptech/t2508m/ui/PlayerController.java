package org.fptaptech.t2508m.ui;

import javafx.beans.property.SimpleStringProperty;
import javafx.collections.*;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import org.fptaptech.t2508m.model.Player;
import org.fptaptech.t2508m.model.National;
import org.fptaptech.t2508m.service.PlayerService;
import org.fptaptech.t2508m.service.NationalService;

public class PlayerController {

    // ===== TABLE =====
    @FXML private TableView<Player> table;
    @FXML private TableColumn<Player, String> colName;
    @FXML private TableColumn<Player, String> colScore;
    @FXML private TableColumn<Player, String> colLevel;
    @FXML private TableColumn<Player, String> colNational;

    // ===== FORM =====
    @FXML private TextField txtName;
    @FXML private TextField txtScore;
    @FXML private TextField txtLevel;
    @FXML private ComboBox<National> cbNational;

    private PlayerService playerService = new PlayerService();
    private NationalService nationalService = new NationalService();

    private ObservableList<Player> data = FXCollections.observableArrayList();

    // ===== INIT =====
    @FXML
    public void initialize() {

        // set column
        colName.setCellValueFactory(c ->
                new SimpleStringProperty(c.getValue().getPlayerName())
        );

        colScore.setCellValueFactory(c ->
                new SimpleStringProperty(String.valueOf(c.getValue().getHighScore()))
        );

        colLevel.setCellValueFactory(c ->
                new SimpleStringProperty(String.valueOf(c.getValue().getLevel()))
        );

        colNational.setCellValueFactory(c ->
                new SimpleStringProperty(
                        c.getValue().getNational().getNationalName()
                )
        );

        // load data table
        loadTable();

        // load combobox
        cbNational.setItems(
                FXCollections.observableArrayList(
                        nationalService.getAllNationals()
                )
        );

        // hiển thị tên trong combobox
        cbNational.setCellFactory(param -> new ListCell<>() {
            @Override
            protected void updateItem(National item, boolean empty) {
                super.updateItem(item, empty);
                setText(empty ? "" : item.getNationalName());
            }
        });

        cbNational.setButtonCell(new ListCell<>() {
            @Override
            protected void updateItem(National item, boolean empty) {
                super.updateItem(item, empty);
                setText(empty ? "" : item.getNationalName());
            }
        });
    }

    // ===== LOAD TABLE =====
    private void loadTable() {
        data.clear();
        data.addAll(playerService.getAllPlayers());
        table.setItems(data);
    }

    // ===== ADD =====
    @FXML
    public void handleAdd() {
        try {
            String name = txtName.getText();
            int score = Integer.parseInt(txtScore.getText());
            int level = Integer.parseInt(txtLevel.getText());
            National national = cbNational.getValue();

            if (name.isEmpty() || national == null) {
                System.out.println("Missing data!");
                return;
            }

            Player p = new Player(name, score, level, national);

            if (playerService.addPlayer(p)) {
                loadTable();
                clearForm();
            }

        } catch (Exception e) {
            System.out.println("Invalid input!");
        }
    }

    // ===== DELETE =====
    @FXML
    public void handleDelete() {
        Player selected = table.getSelectionModel().getSelectedItem();

        if (selected != null) {
            playerService.deletePlayer(selected.getPlayerId());
            loadTable();
        } else {
            System.out.println("Select a player first!");
        }
    }

    // ===== TOP 10 =====
    @FXML
    public void handleTop10() {
        data.clear();
        data.addAll(playerService.getTop10Players());
    }

    // ===== CLEAR FORM =====
    private void clearForm() {
        txtName.clear();
        txtScore.clear();
        txtLevel.clear();
        cbNational.setValue(null);
    }
}
package com.final_test.application;

import com.final_test.dao.PlayerDAO;
import com.final_test.entity.National;
import com.final_test.entity.Player;
import javafx.application.Application;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.geometry.Insets;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class MainApp extends Application {

    private final PlayerDAO dao = new PlayerDAO();
    private final TableView<Player> table = new TableView<>();
    private final ObservableList<Player> playerList = FXCollections.observableArrayList();

    @Override
    public void start(Stage primaryStage) {
        primaryStage.setTitle("Hero Game - Player Management");

        // 1. Cấu hình các cột cho Bảng
        TableColumn<Player, Integer> idCol = new TableColumn<>("Player Id");
        idCol.setCellValueFactory(new PropertyValueFactory<>("playerId"));

        TableColumn<Player, String> nameCol = new TableColumn<>("Player name");
        nameCol.setCellValueFactory(new PropertyValueFactory<>("playerName"));

        TableColumn<Player, Integer> scoreCol = new TableColumn<>("High Score");
        scoreCol.setCellValueFactory(new PropertyValueFactory<>("highScore"));

        TableColumn<Player, Integer> levelCol = new TableColumn<>("Level");
        levelCol.setCellValueFactory(new PropertyValueFactory<>("level"));

        TableColumn<Player, String> nationalCol = new TableColumn<>("National");
        nationalCol.setCellValueFactory(new PropertyValueFactory<>("nationalName"));

        table.getColumns().addAll(idCol, nameCol, scoreCol, levelCol, nationalCol);
        table.setItems(playerList);

        // 2. Form nhập liệu
        TextField txtName = new TextField(); txtName.setPromptText("Player Name");
        TextField txtScore = new TextField(); txtScore.setPromptText("High Score");
        TextField txtLevel = new TextField(); txtLevel.setPromptText("Level");

        ComboBox<National> cbNational = new ComboBox<>();
        cbNational.setItems(FXCollections.observableArrayList(dao.getAllNationals()));
        if (!cbNational.getItems().isEmpty()) cbNational.getSelectionModel().selectFirst();

        Button btnAdd = new Button("Add Player");
        Button btnDelete = new Button("Delete Selected");

        HBox formBox = new HBox(10, txtName, txtScore, txtLevel, cbNational, btnAdd, btnDelete);
        formBox.setPadding(new Insets(10, 0, 10, 0));

        // 3. Thanh công cụ tìm kiếm và lọc
        TextField txtSearch = new TextField(); txtSearch.setPromptText("Search by Name...");
        Button btnSearch = new Button("Search");
        Button btnTop10 = new Button("Top 10 High Score");
        Button btnLoadAll = new Button("Load All");

        HBox searchBox = new HBox(10, txtSearch, btnSearch, btnTop10, btnLoadAll);
        searchBox.setPadding(new Insets(0, 0, 10, 0));

        // 4. Xử lý các sự kiện click
        loadTableData(); // Tải dữ liệu lần đầu chạy ứng dụng

        btnLoadAll.setOnAction(e -> loadTableData());

        btnSearch.setOnAction(e -> {
            playerList.setAll(dao.displayAllByPlayerName(txtSearch.getText()));
        });

        btnTop10.setOnAction(e -> {
            playerList.setAll(dao.displayTop10());
        });

        btnAdd.setOnAction(e -> {
            try {
                National selectedNational = cbNational.getValue();
                if (selectedNational == null) {
                    showAlert("Thiếu dữ liệu", "Chưa có quốc gia nào để gán cho người chơi. Hãy kiểm tra bảng National.");
                    return;
                }

                if (txtName.getText().isBlank()) {
                    showAlert("Lỗi nhập liệu", "Player Name không được để trống.");
                    return;
                }

                Player p = new Player();
                p.setPlayerName(txtName.getText());
                p.setHighScore(Integer.parseInt(txtScore.getText()));
                p.setLevel(Integer.parseInt(txtLevel.getText()));
                p.setNationalId(selectedNational.getNationalId());

                if (dao.insertPlayer(p)) {
                    loadTableData();
                    txtName.clear(); txtScore.clear(); txtLevel.clear();
                } else {
                    showAlert("Lỗi", "Không thể thêm người chơi. Vui lòng thử lại.");
                }
            } catch (NumberFormatException ex) {
                showAlert("Lỗi nhập liệu", "Score và Level bắt buộc phải là số (Integer).");
            }
        });

        btnDelete.setOnAction(e -> {
            Player selected = table.getSelectionModel().getSelectedItem();
            if (selected != null) {
                if (dao.deletePlayer(selected.getPlayerId())) {
                    loadTableData();
                }
            } else {
                showAlert("Thông báo", "Vui lòng chọn 1 người chơi từ bảng phía trên để xóa.");
            }
        });

        // 5. Sắp xếp bố cục chính
        VBox mainLayout = new VBox(10, searchBox, table, formBox);
        mainLayout.setPadding(new Insets(15));

        Scene scene = new Scene(mainLayout, 800, 500);
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    private void loadTableData() {
        playerList.setAll(dao.displayAll());
    }

    private void showAlert(String title, String content) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle(title);
        alert.setHeaderText(null);
        alert.setContentText(content);
        alert.showAndWait();
    }
}

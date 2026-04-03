package main;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.stage.Stage;
import util.DBConnection;

public class MainApp extends Application {

    @Override
    public void start(Stage stage) throws Exception {
        try {
            DBConnection.initializeDatabase();
        } catch (Exception e) {
            showStartupError(e.getMessage());
            Platform.exit();
            return;
        }

        FXMLLoader fxmlLoader = new FXMLLoader(MainApp.class.getResource("/main-view.fxml"));
        Scene scene = new Scene(fxmlLoader.load(), 1180, 760);
        scene.getStylesheets().add(MainApp.class.getResource("/app.css").toExternalForm());
        stage.setTitle("Hero Game Management");
        stage.setMinWidth(1100);
        stage.setMinHeight(720);
        stage.setScene(scene);
        stage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }

    private void showStartupError(String message) {
        Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setTitle("Database Connection Error");
        alert.setHeaderText("MySQL is not ready");
        alert.setContentText(message);
        alert.showAndWait();
    }
}

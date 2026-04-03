package main;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class MainApp extends Application {

    @Override
    public void start(Stage stage) throws Exception {
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
}

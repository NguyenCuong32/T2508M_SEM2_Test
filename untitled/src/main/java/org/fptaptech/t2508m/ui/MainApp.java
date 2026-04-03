package org.fptaptech.t2508m.ui;

import javafx.application.Application;


import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class MainApp extends Application {

    @Override
    public void start(Stage stage) {
        try {
            FXMLLoader loader = new FXMLLoader(
                    getClass().getResource("/ui/player-view.fxml")
            );

            Scene scene = new Scene(loader.load());

            stage.setTitle("Hero Game Management");
            stage.setScene(scene);
            stage.setWidth(800);
            stage.setHeight(500);
            stage.show();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void main(String[] args) {
        launch(args);
    }
}

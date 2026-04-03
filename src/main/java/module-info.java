module HeroGame {
    requires java.sql;
    requires javafx.controls;
    requires javafx.fxml;

    exports main;
    exports model;

    opens controller to javafx.fxml;
    opens model to javafx.base;
}

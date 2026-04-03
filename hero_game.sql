DROP DATABASE IF EXISTS HeroGame;
CREATE DATABASE HeroGame;
USE HeroGame;

CREATE TABLE National (
    NationalId INT AUTO_INCREMENT PRIMARY KEY,
    NationalName VARCHAR(100) NOT NULL
);

CREATE TABLE Player (
    PlayerId INT AUTO_INCREMENT PRIMARY KEY,
    NationalId INT NOT NULL,
    PlayerName VARCHAR(100) NOT NULL,
    HighScore INT NOT NULL,
    Level INT NOT NULL,
    CONSTRAINT fk_player_national
        FOREIGN KEY (NationalId) REFERENCES National(NationalId)
);

INSERT INTO National (NationalName)
VALUES
    ('Vietnam'),
    ('USA'),
    ('Japan');

INSERT INTO Player (NationalId, PlayerName, HighScore, Level)
VALUES
    ((SELECT NationalId FROM National WHERE NationalName = 'Vietnam'), 'Player 1', 100, 2),
    ((SELECT NationalId FROM National WHERE NationalName = 'USA'), 'Player 2', 1050, 10),
    ((SELECT NationalId FROM National WHERE NationalName = 'Japan'), 'Player 3', 200, 5);

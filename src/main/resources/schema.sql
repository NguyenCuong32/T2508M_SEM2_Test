-- CREATE DATABASE HeroGame;

CREATE TABLE Player (
    PlayerId INT AUTO_INCREMENT,
    NationalId INT,
    PlayerName VARCHAR(100),
    HighScore INT,
    Level INT,
    PRIMARY KEY (PlayerId, NationalId)
);

CREATE TABLE National (
    NationalId INT AUTO_INCREMENT,
    NationalName VARCHAR(100),
    PRIMARY KEY (NationalId)
);
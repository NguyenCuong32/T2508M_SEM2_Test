-- Create Database
CREATE DATABASE IF NOT EXISTS HeroGame;
USE HeroGame;

-- Create National Table
CREATE TABLE IF NOT EXISTS National (
    NationalId INT AUTO_INCREMENT PRIMARY KEY,
    NationalName VARCHAR(255) NOT NULL
);

-- Create Player Table
CREATE TABLE IF NOT EXISTS Player (
    PlayerId INT AUTO_INCREMENT PRIMARY KEY,
    NationalId INT,
    PlayerName VARCHAR(255) NOT NULL,
    HighScore INT DEFAULT 0,
    Level INT DEFAULT 1,
    FOREIGN KEY (NationalId) REFERENCES National(NationalId)
);

-- Insert Sample Data for National
INSERT INTO National (NationalName) VALUES ('Vietnam'), ('USA'), ('Japan'), ('South Korea'), ('China');

-- Insert Sample Data for Player
INSERT INTO Player (NationalId, PlayerName, HighScore, Level) VALUES 
(1, 'Player1', 100, 2),
(2, 'Player2', 1050, 10),
(3, 'Player3', 200, 5),
(1, 'Nguyen Van A', 500, 4),
(4, 'Kim Jung Un', 1200, 15),
(5, 'Li Wei', 850, 8),
(2, 'John Smith', 600, 6),
(3, 'Satoshi', 950, 9),
(1, 'Le Tri Phuong', 1500, 20),
(4, 'Park Shin Hye', 400, 3),
(5, 'Zhang San', 750, 7),
(1, 'Tran Thi B', 300, 2);

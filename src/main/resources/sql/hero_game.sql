CREATE DATABASE IF NOT EXISTS HeroGame
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_general_ci;

USE HeroGame;

CREATE TABLE IF NOT EXISTS National (
    NationalId INT AUTO_INCREMENT PRIMARY KEY,
    NationalName VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS Player (
    PlayerId INT AUTO_INCREMENT PRIMARY KEY,
    NationalId INT NOT NULL,
    PlayerName VARCHAR(100) NOT NULL,
    HighScore INT NOT NULL,
    Level INT NOT NULL,
    CONSTRAINT fk_player_national
        FOREIGN KEY (NationalId) REFERENCES National(NationalId)
);

INSERT INTO National (NationalName)
SELECT 'Vietnam'
WHERE NOT EXISTS (
    SELECT 1
    FROM National
    WHERE NationalName = 'Vietnam'
);

INSERT INTO National (NationalName)
SELECT 'USA'
WHERE NOT EXISTS (
    SELECT 1
    FROM National
    WHERE NationalName = 'USA'
);

INSERT INTO National (NationalName)
SELECT 'Japan'
WHERE NOT EXISTS (
    SELECT 1
    FROM National
    WHERE NationalName = 'Japan'
);

INSERT INTO Player (NationalId, PlayerName, HighScore, Level)
SELECT n.NationalId, 'Player 1', 100, 2
FROM National n
WHERE n.NationalName = 'Vietnam'
  AND NOT EXISTS (
      SELECT 1
      FROM Player p
      WHERE p.PlayerName = 'Player 1'
        AND p.HighScore = 100
        AND p.Level = 2
  );

INSERT INTO Player (NationalId, PlayerName, HighScore, Level)
SELECT n.NationalId, 'Player 2', 1050, 10
FROM National n
WHERE n.NationalName = 'USA'
  AND NOT EXISTS (
      SELECT 1
      FROM Player p
      WHERE p.PlayerName = 'Player 2'
        AND p.HighScore = 1050
        AND p.Level = 10
  );

INSERT INTO Player (NationalId, PlayerName, HighScore, Level)
SELECT n.NationalId, 'Player 3', 200, 5
FROM National n
WHERE n.NationalName = 'Japan'
  AND NOT EXISTS (
      SELECT 1
      FROM Player p
      WHERE p.PlayerName = 'Player 3'
        AND p.HighScore = 200
        AND p.Level = 5
  );

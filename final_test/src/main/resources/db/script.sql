CREATE DATABASE IF NOT EXISTS HeroGame;
USE HeroGame;

CREATE TABLE IF NOT EXISTS National (
    NationalId INT AUTO_INCREMENT PRIMARY KEY,
    NationalName VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS Player (
    PlayerId INT AUTO_INCREMENT PRIMARY KEY,
    NationalId INT,
    PlayerName VARCHAR(255) NOT NULL,
    HighScore INT,
    Level INT,
    CONSTRAINT FK_Player_National
        FOREIGN KEY (NationalId) REFERENCES National (NationalId)
);

INSERT INTO National (NationalName)
SELECT *
FROM (
    SELECT 'Vietnam' AS NationalName
    UNION ALL
    SELECT 'USA'
    UNION ALL
    SELECT 'Japan'
) AS seed
WHERE NOT EXISTS (
    SELECT 1
    FROM National
    WHERE NationalName = seed.NationalName
);

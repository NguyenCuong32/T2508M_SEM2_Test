CREATE DATABASE IF NOT EXISTS playerdb;
USE playerdb;

INSERT INTO nationals (name)
SELECT 'Vietnam'
WHERE NOT EXISTS (
    SELECT 1 FROM nationals WHERE name = 'Vietnam'
);

INSERT INTO nationals (name)
SELECT 'Argentina'
WHERE NOT EXISTS (
    SELECT 1 FROM nationals WHERE name = 'Argentina'
);

INSERT INTO players (player_name, high_score, national_id)
SELECT 'Nguyen Van A', 1200, n.id
FROM nationals n
WHERE n.name = 'Vietnam'
  AND NOT EXISTS (
      SELECT 1 FROM players WHERE player_name = 'Nguyen Van A'
  );

INSERT INTO players (player_name, high_score, national_id)
SELECT 'Lionel Messi', 9800, n.id
FROM nationals n
WHERE n.name = 'Argentina'
  AND NOT EXISTS (
      SELECT 1 FROM players WHERE player_name = 'Lionel Messi'
  );

create table National(
	NationalId int AUTO_INCREMENT Primary key,
	NationalName VARCHAR(100)
);

create table Player(
	PlayerId int AUTO_INCREMENT Primary key,
	NationalId int,
	PlayerName VARCHAR(100),
	Highscore int,
	Level int,
	Foreign key(NationalId) References National(NationalId)
);

Insert into National(NationalName) Values ('Vietnam'), ('USA'), ('Japan');

Insert into Player (PlayerName, Highscore, Level, NationalId) Values ('Player 1', 100, 2, 1);
Insert into Player (PlayerName, Highscore, Level, NationalId) Values ('Player 2', 1050, 10, 2);
Insert into Player (PlayerName, Highscore, Level, NationalId) Values ('Player 3', 200, 5, 3);
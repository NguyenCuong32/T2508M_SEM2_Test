# HeroGame Report

## Tech Stack

- Java 17+
- Spring Boot MVC
- Spring Data JPA
- Thymeleaf
- H2 embedded database
- Gradle

## Implemented Features

1. Add new players.
2. Delete players.
3. Add new nationals.
4. Delete nationals.
5. Display all players.
6. Search players by name.
7. Display top 10 players by high score.

## Default Sample Data

| Player Id | Player Name | High Score | Level | National |
| --- | --- | ---: | ---: | --- |
| 1 | Player 1 | 100 | 2 | Vietnam |
| 2 | Player 2 | 1050 | 10 | USA |
| 3 | Player 3 | 200 | 5 | Japan |

Sample data is inserted automatically when the database is empty.

## Run Instructions

1. Install JDK 17 or newer.
2. Open a terminal in the project folder.
3. Run `run.bat` or `gradlew.bat bootRun`.
4. Open `http://localhost:2007/`.

## H2 Console

- URL: `http://localhost:2007/h2-console`
- JDBC URL: `jdbc:h2:file:./data/herogame`
- Username: `sa`
- Password: empty

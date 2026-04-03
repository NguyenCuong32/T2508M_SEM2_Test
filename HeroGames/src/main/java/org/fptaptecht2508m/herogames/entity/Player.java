package org.fptaptecht2508m.herogames.entity;

public class Player {
    private int playerId;
    private int NationalId;
    private String playerName;
    private int highScore;
    private int level;

    public Player() {}

    public Player(int NationalId, String playerName, int highScore, int level){
        this.NationalId = NationalId;
        this.playerName = playerName;
        this.highScore = highScore;
        this.level = level;
    }

    public int getPlayerId() {
        return playerId;
    }

    public void setPlayerId(int playerId) {
        this.playerId = playerId;
    }

    public int getNationalId() {
        return NationalId;
    }

    public void setNationalId(int nationalId) {
        this.NationalId = nationalId;
    }

    public String getPlayerName() {
        return playerName;
    }

    public void setPlayerName(String playerName) {
        this.playerName = playerName;
    }

    public int getHighScore() {
        return highScore;
    }

    public void setHighScore(int highScore) {
        this.highScore = highScore;
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }
}

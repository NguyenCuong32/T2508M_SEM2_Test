package org.fptaptech.t2508m.model;

public class Player {
    private int playerId;
    private String playerName;
    private int highScore;
    private int level;
    private National national;

    public Player(int playerId, String playerName, int highScore, int level, National national) {
        this.playerId = playerId;
        this.playerName = playerName;
        this.highScore = highScore;
        this.level = level;
        this.national = national;
    }
    public Player() {}

    public Player(String playerName, int highScore, int level, National national) {
        this.playerName = playerName;
        this.highScore = highScore;
        this.level = level;
        this.national = national;
    }

    public int getPlayerId() {
        return playerId;
    }

    public void setPlayerId(int playerId) {
        this.playerId = playerId;
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

    public National getNational() {
        return national;
    }

    public void setNational(National national) {
        this.national = national;
    }
}

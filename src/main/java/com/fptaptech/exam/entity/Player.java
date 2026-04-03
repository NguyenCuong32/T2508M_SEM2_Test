package com.fptaptech.exam.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "Player")
public class Player {
    @Id
    @GeneratedValue
    private Integer playerId;

    private Integer nationalId;

    private String playerName;

    private Integer highScore;

    private Integer level;

    public Integer getPlayerId() {
        return playerId;
    }
    public void setPlayerId(Integer newPlayerId) {
        this.playerId = newPlayerId;
    }

    public Integer getNationalId() {
        return nationalId;
    }
    public void setNationalId(Integer newNationalId) {
        this.nationalId = newNationalId;
    }

    public String getPlayerName() {
        return playerName;
    }
    public void setPlayerName(String newPlayerName) {
        this.playerName = newPlayerName;
    }

    public Integer getHighScore() {
        return highScore;
    }
    public void setHighScore(Integer newHighScore) {
        this.highScore = newHighScore;
    }

    public Integer getLevel() {
        return level;
    }
    public void setLevel(Integer newLevel) {
        this.level = newLevel;
    }
}

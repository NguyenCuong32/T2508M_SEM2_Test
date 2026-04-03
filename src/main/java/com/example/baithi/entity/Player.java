package com.example.baithi.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "Player")
public class Player {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "PlayerId")
    private Integer playerId;

    @Column(name = "PlayerName", nullable = false)
    private String playerName;

    @Column(name = "HighScore")
    private Integer highScore;

    @Column(name = "Level")
    private Integer level;

    @ManyToOne
    @JoinColumn(name = "NationalId", nullable = false)
    private National national;

    public Player() {}

    public Player(String playerName, Integer highScore, Integer level, National national) {
        this.playerName = playerName;
        this.highScore = highScore;
        this.level = level;
        this.national = national;
    }

    public Integer getPlayerId() {
        return playerId;
    }

    public void setPlayerId(Integer playerId) {
        this.playerId = playerId;
    }

    public String getPlayerName() {
        return playerName;
    }

    public void setPlayerName(String playerName) {
        this.playerName = playerName;
    }

    public Integer getHighScore() {
        return highScore;
    }

    public void setHighScore(Integer highScore) {
        this.highScore = highScore;
    }

    public Integer getLevel() {
        return level;
    }

    public void setLevel(Integer level) {
        this.level = level;
    }

    public National getNational() {
        return national;
    }

    public void setNational(National national) {
        this.national = national;
    }
}

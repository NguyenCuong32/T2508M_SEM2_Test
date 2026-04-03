package com.herogame.model;

public class Player {
    private int id;
    private String name;
    private int score;
    private int level;
    private String national;

    public Player(int id, String name, int score, int level, String national) {
        this.id = id;
        this.name = name;
        this.score = score;
        this.level = level;
        this.national = national;
    }

    public int getId() { return id; }
    public String getName() { return name; }
    public int getScore() { return score; }
    public int getLevel() { return level; }
    public String getNational() { return national; }
}
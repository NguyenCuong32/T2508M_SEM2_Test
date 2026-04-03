package org.example.letriphuong.models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class Player {
    private int playerId;
    private int nationalId;
    private String playerName;
    private int highScore;
    private int level;
    private String nationalName; // Field for joining results

    // Constructor without playerId (for inserting)
    public Player(int nationalId, String playerName, int highScore, int level) {
        this.nationalId = nationalId;
        this.playerName = playerName;
        this.highScore = highScore;
        this.level = level;
    }
}

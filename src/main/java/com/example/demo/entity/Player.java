package com.example.demo.entity;

import lombok.*;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Getter
@Setter
public class Player {
    private int playerId;
    private int nationalId;
    private String playerName;
    private int highScore;
    private int level;
    private String nationalName;
}
package com.example.demo.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "Player")
public class Player {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "PlayerId")
    private Long playerId;

    @Column(name = "PlayerName", nullable = false, length = 150)
    private String playerName;

    @Column(name = "HighScore", nullable = false)
    private Integer highScore;

    @Column(name = "Level", nullable = false)
    private Integer level;

    @ManyToOne(fetch = FetchType.EAGER, optional = false)
    @JoinColumn(name = "NationalId", nullable = false)
    private National national;
}

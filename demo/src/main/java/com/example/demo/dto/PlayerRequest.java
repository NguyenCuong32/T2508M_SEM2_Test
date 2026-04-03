package com.example.demo.dto;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.Data;

@Data
public class PlayerRequest {

    @NotBlank(message = "Player name is required")
    private String playerName;

    @NotNull(message = "High score is required")
    @Min(value = 0, message = "High score must be greater than or equal to 0")
    private Integer highScore;

    @NotNull(message = "Level is required")
    @Min(value = 1, message = "Level must be greater than or equal to 1")
    private Integer level;

    @NotNull(message = "National id is required")
    private Long nationalId;
}

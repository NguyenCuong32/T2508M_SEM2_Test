package com.example.demo.dto;

import jakarta.validation.constraints.NotBlank;
import lombok.Data;

@Data
public class NationalRequest {

    @NotBlank(message = "National name is required")
    private String name;
}

package com.example.demo.entity;

import lombok.*;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Getter
@Setter
public class National {
    private int nationalId;
    private String nationalName;
    @Override
    public String toString() {
        return nationalName;
    }
}
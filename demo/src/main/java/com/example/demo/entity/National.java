package com.example.demo.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "National")
public class National {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "NationalId")
    private Long nationalId;

    @Column(name = "NationalName", nullable = false, unique = true, length = 100)
    private String nationalName;
}

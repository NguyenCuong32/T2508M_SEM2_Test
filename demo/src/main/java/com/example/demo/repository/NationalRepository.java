package com.example.demo.repository;

import com.example.demo.entity.National;
import org.springframework.data.jpa.repository.JpaRepository;

public interface NationalRepository extends JpaRepository<National, Long> {
}

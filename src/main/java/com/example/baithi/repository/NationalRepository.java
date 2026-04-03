package com.example.baithi.repository;

import com.example.baithi.entity.National;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface NationalRepository extends JpaRepository<National, Integer> {
}

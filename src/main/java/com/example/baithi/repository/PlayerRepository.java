package com.example.baithi.repository;

import com.example.baithi.entity.Player;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface PlayerRepository extends JpaRepository<Player, Integer> {
    List<Player> findByPlayerNameContainingIgnoreCase(String name);
    
    @Query("SELECT p FROM Player p ORDER BY p.highScore DESC")
    List<Player> findTop10ByHighScore();
}

package com.example.demo.repository;

import com.example.demo.entity.Player;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PlayerRepository extends JpaRepository<Player, Long> {

    List<Player> findByPlayerNameContainingIgnoreCase(String name);

    List<Player> findTop10ByOrderByHighScoreDesc();
}

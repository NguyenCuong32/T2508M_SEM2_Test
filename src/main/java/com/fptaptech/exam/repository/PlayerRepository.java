package com.fptaptech.exam.repository;

import com.fptaptech.exam.entity.Player;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface PlayerRepository extends JpaRepository<Player, Integer> {
    List<Player> findByPlayerNameContaining(String playerName);

    List<Player> findTop10ByOrderByHighScoreDesc();
}

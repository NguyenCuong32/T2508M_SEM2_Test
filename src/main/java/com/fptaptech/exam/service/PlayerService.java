package com.fptaptech.exam.service;

import com.fptaptech.exam.entity.Player;
import com.fptaptech.exam.repository.PlayerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.GetMapping;

import java.util.List;

@Service
public class PlayerService {
    @Autowired
    private PlayerRepository playerRepository;

    public Player insertPlayer(Player player) {
        return playerRepository.save(player);
    }

    public void deletePlayer(Integer id) {
        playerRepository.deleteById(id);
    }

    public List<Player> getAllPlayer() {
        return playerRepository.findAll();
    }

    public List<Player> findPlayerByName(String name) {
        return playerRepository.findByPlayerNameContaining(name);
    }

    public List<Player> top10Players() {
        return playerRepository.findTop10ByOrderByHighScoreDesc();
    }

    @GetMapping("/test")
    public String test(){
        return "API working";
    }
}

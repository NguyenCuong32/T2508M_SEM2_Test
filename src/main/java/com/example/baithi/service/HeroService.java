package com.example.baithi.service;

import com.example.baithi.entity.National;
import com.example.baithi.entity.Player;
import com.example.baithi.repository.NationalRepository;
import com.example.baithi.repository.PlayerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class HeroService {
    @Autowired
    private PlayerRepository playerRepository;

    @Autowired
    private NationalRepository nationalRepository;

    public List<Player> getAllPlayers() {
        return playerRepository.findAll();
    }

    public List<National> getAllNationals() {
        return nationalRepository.findAll();
    }

    public Player savePlayer(Player player) {
        return playerRepository.save(player);
    }

    public void deletePlayer(Integer id) {
        playerRepository.deleteById(id);
    }

    public National saveNational(National national) {
        return nationalRepository.save(national);
    }

    public void deleteNational(Integer id) {
        nationalRepository.deleteById(id);
    }

    public List<Player> searchPlayersByName(String name) {
        return playerRepository.findByPlayerNameContainingIgnoreCase(name);
    }

    public List<Player> getTop10Players() {
        // Simple implementation using we'll just slice the list or use the query
        return playerRepository.findTop10ByHighScore().stream().limit(10).toList();
    }
    
    public Optional<National> getNationalById(Integer id) {
        return nationalRepository.findById(id);
    }
}

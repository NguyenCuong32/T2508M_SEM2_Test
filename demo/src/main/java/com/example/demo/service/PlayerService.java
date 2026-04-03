package com.example.demo.service;

import com.example.demo.dto.PlayerRequest;
import com.example.demo.entity.National;
import com.example.demo.entity.Player;
import com.example.demo.repository.NationalRepository;
import com.example.demo.repository.PlayerRepository;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class PlayerService {

    private final PlayerRepository playerRepository;
    private final NationalRepository nationalRepository;

    public PlayerService(PlayerRepository playerRepository, NationalRepository nationalRepository) {
        this.playerRepository = playerRepository;
        this.nationalRepository = nationalRepository;
    }

    public List<Player> getAllPlayers() {
        return playerRepository.findAll();
    }

    public Player createPlayer(PlayerRequest request) {
        National national = nationalRepository.findById(request.getNationalId())
                .orElseThrow(() -> new IllegalArgumentException("National not found with id: " + request.getNationalId()));

        Player player = new Player();
        player.setPlayerName(request.getPlayerName());
        player.setHighScore(request.getHighScore());
        player.setLevel(request.getLevel());
        player.setNational(national);

        return playerRepository.save(player);
    }

    public void deletePlayer(Long id) {
        if (!playerRepository.existsById(id)) {
            throw new IllegalArgumentException("Player not found with id: " + id);
        }
        playerRepository.deleteById(id);
    }

    public List<Player> searchPlayersByName(String name) {
        return playerRepository.findByPlayerNameContainingIgnoreCase(name);
    }

    public List<Player> getTop10Players() {
        return playerRepository.findTop10ByOrderByHighScoreDesc();
    }
}

package com.example.demo.service;

import com.example.demo.entity.Player;
import com.example.demo.repository.PlayerRepository;
import java.util.List;

public class PlayerService {

    private PlayerRepository repo = new PlayerRepository();

    public void add(Player p) {
        if (p.getPlayerName() != null && !p.getPlayerName().isEmpty()) {
            repo.insertPlayer(p);
        }
    }

    public void delete(int id) {
        repo.deletePlayer(id);
    }

    public List<Player> getAll() {
        return repo.getAll();
    }

    public List<Player> search(String name) {
        return repo.findByName(name);
    }

    public List<Player> top10() {
        return repo.top10();
    }
}
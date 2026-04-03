package org.fptaptech.t2508m.service;

import org.fptaptech.t2508m.dao.PlayerDAO;
import org.fptaptech.t2508m.model.Player;

import java.util.List;

public class PlayerService {
    private PlayerDAO playerDAO = new PlayerDAO();
    public boolean addPlayer(Player p) {
        if (p.getPlayerName() == null || p.getPlayerName().isEmpty()) {
            System.out.println("Player name is required");
            return false;
        }

        if (p.getHighScore() < 0 || p.getLevel() < 0) {
            System.out.println("Score/Level must be >= 0");
            return false;
        }

        if (p.getNational() == null) {
            System.out.println("National is required");
            return false;
        }

        return playerDAO.insert(p);
    }
    public List<Player> getAllPlayers() {
        return playerDAO.findAll();
    }

    public boolean updatePlayer(Player p) {
        if (p.getPlayerId() <= 0) {
            System.out.println("Invalid player ID");
            return false;
        }
        return playerDAO.update(p);
    }

    public boolean deletePlayer(int id) {
        if (id <= 0) {
            System.out.println("Invalid ID");
            return false;
        }
        return playerDAO.delete(id);
    }

    // SEARCH
    public List<Player> searchByName(String name) {
        if (name == null || name.trim().isEmpty()) {
            return playerDAO.findAll();
        }
        return playerDAO.findByName(name);
    }

    public List<Player> getTop10Players() {
        return playerDAO.getTop10();
    }
}

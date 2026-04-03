package org.fptaptech.t2508m.service;

import org.fptaptech.t2508m.dao.NationalDAO;
import org.fptaptech.t2508m.dao.PlayerDAO;
import org.fptaptech.t2508m.model.National;
import org.fptaptech.t2508m.model.Player;

import java.util.List;

public class NationalService {
    private NationalDAO nationalDAO = new NationalDAO();
    private PlayerDAO playerDAO = new PlayerDAO();

    public boolean addNational(National n) {
        if (n.getNationalName() == null || n.getNationalName().isEmpty()) {
            System.out.println("National name required");
            return false;
        }
        return nationalDAO.insert(n);
    }

    public List<National> getAllNationals() {
        return nationalDAO.findAll();
    }

    // UPDATE
    public boolean updateNational(National n) {
        if (n.getNationalId() <= 0) {
            System.out.println("Invalid ID");
            return false;
        }
        return nationalDAO.update(n);
    }

    public boolean deleteNational(int id) {

        // check có player dùng không
        List<Player> players = playerDAO.findAll();

        for (Player p : players) {
            if (p.getNational().getNationalId() == id) {
                System.out.println("Cannot delete! National is used by Player.");
                return false;
            }
        }

        return nationalDAO.delete(id);
    }

}

package herogameservice;

import herogamedao.PlayerDAO;
import herogameentity.Player;
import java.util.List;

public class PlayerService {
    private PlayerDAO dao = new PlayerDAO();

    public List<Player> getAll() {
        return dao.displayAll();
    }

    public void add(Player p) {
        dao.insertPlayer(p);
    }

    public List<Player> search(String name) {
        return dao.displayAllByPlayerName(name);
    }

    public List<Player> getTop10() {
        return dao.displayTop10();
    }
    public void update(Player p) { dao.updatePlayer(p); }
    public void delete(int id) { dao.deletePlayer(id); }
}
package aptech.t2508m.baithihero.service;

import aptech.t2508m.baithihero.entity.National;
import aptech.t2508m.baithihero.entity.Player;
import aptech.t2508m.baithihero.repository.NationalRepository;
import aptech.t2508m.baithihero.repository.PlayerRepository;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Service
@Transactional
public class HeroGameService {
    private final PlayerRepository playerRepository;
    private final NationalRepository nationalRepository;

    public HeroGameService(PlayerRepository playerRepository, NationalRepository nationalRepository) {
        this.playerRepository = playerRepository;
        this.nationalRepository = nationalRepository;
    }

    public Player insertPlayer(Player player) {
        validatePlayer(player);
        return playerRepository.save(player);
    }

    public void deletePlayer(Integer playerId) {
        playerRepository.deleteById(playerId);
    }

    public National insertNational(String nationalName) {
        if (nationalName == null || nationalName.isBlank()) {
            throw new IllegalArgumentException("National name cannot be empty.");
        }
        National national = new National();
        national.setNationalName(nationalName.trim());
        return nationalRepository.save(national);
    }

    public void deleteNational(Integer nationalId) {
        nationalRepository.deleteById(nationalId);
    }

    @Transactional(readOnly = true)
    public List<Player> displayAll() {
        return playerRepository.findAllByOrderByPlayerIdAsc();
    }

    @Transactional(readOnly = true)
    public List<Player> displayAllByPlayerName(String playerName) {
        if (playerName == null || playerName.isBlank()) {
            return displayAll();
        }
        return playerRepository.findByPlayerNameContainingIgnoreCaseOrderByPlayerIdAsc(playerName.trim());
    }

    @Transactional(readOnly = true)
    public List<Player> displayTop10() {
        return playerRepository.findTopPlayers(PageRequest.of(0, 10));
    }

    @Transactional(readOnly = true)
    public List<National> displayAllNational() {
        return nationalRepository.findAllByOrderByNationalNameAsc();
    }

    @Transactional(readOnly = true)
    public National findNationalById(Integer nationalId) {
        return nationalRepository.findById(nationalId)
                .orElseThrow(() -> new IllegalArgumentException("National not found."));
    }

    private void validatePlayer(Player player) {
        if (player.getPlayerName() == null || player.getPlayerName().isBlank()) {
            throw new IllegalArgumentException("Player name cannot be empty.");
        }
        if (player.getHighScore() == null || player.getHighScore() < 0) {
            throw new IllegalArgumentException("High score must be >= 0.");
        }
        if (player.getLevel() == null || player.getLevel() < 0) {
            throw new IllegalArgumentException("Level must be >= 0.");
        }
        if (player.getNational() == null) {
            throw new IllegalArgumentException("National is required.");
        }
    }
}

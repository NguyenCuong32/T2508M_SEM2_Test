package aptech.t2508m.baithihero.repository;

import aptech.t2508m.baithihero.entity.Player;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.EntityGraph;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface PlayerRepository extends JpaRepository<Player, Integer> {
    @EntityGraph(attributePaths = "national")
    List<Player> findAllByOrderByPlayerIdAsc();

    @EntityGraph(attributePaths = "national")
    List<Player> findByPlayerNameContainingIgnoreCaseOrderByPlayerIdAsc(String playerName);

    @EntityGraph(attributePaths = "national")
    @Query("select p from Player p order by p.highScore desc, p.level desc, p.playerId asc")
    List<Player> findTopPlayers(Pageable pageable);
}

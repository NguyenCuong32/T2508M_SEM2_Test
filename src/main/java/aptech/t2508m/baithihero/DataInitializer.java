package aptech.t2508m.baithihero;

import aptech.t2508m.baithihero.entity.National;
import aptech.t2508m.baithihero.entity.Player;
import aptech.t2508m.baithihero.repository.NationalRepository;
import aptech.t2508m.baithihero.repository.PlayerRepository;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Component
public class DataInitializer implements CommandLineRunner {
    private final NationalRepository nationalRepository;
    private final PlayerRepository playerRepository;

    public DataInitializer(NationalRepository nationalRepository, PlayerRepository playerRepository) {
        this.nationalRepository = nationalRepository;
        this.playerRepository = playerRepository;
    }

    @Override
    public void run(String... args) {
        if (nationalRepository.count() > 0 || playerRepository.count() > 0) {
            return;
        }

        National vietnam = nationalRepository.save(new National(null, "Vietnam"));
        National usa = nationalRepository.save(new National(null, "USA"));
        National japan = nationalRepository.save(new National(null, "Japan"));

        playerRepository.save(new Player(null, vietnam, "Player 1", 100, 2));
        playerRepository.save(new Player(null, usa, "Player 2", 1050, 10));
        playerRepository.save(new Player(null, japan, "Player 3", 200, 5));
    }
}

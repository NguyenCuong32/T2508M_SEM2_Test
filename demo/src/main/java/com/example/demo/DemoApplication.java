package com.example.demo;

import com.example.demo.entity.National;
import com.example.demo.entity.Player;
import com.example.demo.repository.NationalRepository;
import com.example.demo.repository.PlayerRepository;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class DemoApplication {

    public static void main(String[] args) {
        SpringApplication.run(DemoApplication.class, args);
    }

    @Bean
    public CommandLineRunner runner(PlayerRepository playerRepository, NationalRepository nationalRepository) {
        return args -> {
            if (nationalRepository.count() == 0) {
                National vietnam = nationalRepository.save(new National(null, "Vietnam"));
                National usa = nationalRepository.save(new National(null, "USA"));
                National japan = nationalRepository.save(new National(null, "Japan"));

                if (playerRepository.count() == 0) {
                    playerRepository.save(new Player(null, "Player 1", 100, 2, vietnam));
                    playerRepository.save(new Player(null, "Player 2", 1050, 10, usa));
                    playerRepository.save(new Player(null, "Player 3", 200, 5, japan));
                }
            }
        };
    }
}

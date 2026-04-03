package com.example.baithi;

import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class BaithiApplication {

    public static void main(String[] args) {
        SpringApplication.run(BaithiApplication.class, args);
    }

    @Bean
    public CommandLineRunner initData(com.example.baithi.repository.NationalRepository nationalRepo, 
                                      com.example.baithi.repository.PlayerRepository playerRepo) {
        return args -> {
            var vn = nationalRepo.save(new com.example.baithi.entity.National("Việt Nam"));
            var usa = nationalRepo.save(new com.example.baithi.entity.National("Nước Ngoài"));
        };
    }
}

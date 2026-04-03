package com.fptaptech.exam.controller;

import com.fptaptech.exam.entity.Player;
import com.fptaptech.exam.service.PlayerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/players")
public class PlayerController {

    @Autowired
    private PlayerService playerService;

    // GET ALL PLAYERS
    @GetMapping
    public List<Player> getAllPlayers() {
        return playerService.getAllPlayer();
    }

    // INSERT PLAYER
    @PostMapping
    public Player insertPlayer(@RequestBody Player player) {
        return playerService.insertPlayer(player);
    }

    // DELETE PLAYER
    @DeleteMapping("/{id}")
    public void deletePlayer(@PathVariable Integer id) {
        playerService.deletePlayer(id);
    }

    // TEST API
    @GetMapping("/test")
    public String test(){
        return "API working";
    }
}
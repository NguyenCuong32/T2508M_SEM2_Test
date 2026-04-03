package com.example.baithi.controller;

import com.example.baithi.entity.National;
import com.example.baithi.entity.Player;
import com.example.baithi.service.HeroService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Controller
@RequestMapping("/")
public class HeroController {

    @Autowired
    private HeroService heroService;

    @GetMapping
    public String index(Model model) {
        model.addAttribute("players", heroService.getAllPlayers());
        model.addAttribute("nationals", heroService.getAllNationals());
        model.addAttribute("newPlayer", new Player());
        model.addAttribute("newNational", new National());
        return "index";
    }

    @PostMapping("/add-player")
    public String addPlayer(@ModelAttribute Player player, @RequestParam("nationalId") Integer nationalId) {
        National national = heroService.getNationalById(nationalId).orElse(null);
        if (national != null) {
            player.setNational(national);
            heroService.savePlayer(player);
        }
        return "redirect:/";
    }

    @GetMapping("/delete-player/{id}")
    public String deletePlayer(@PathVariable Integer id) {
        heroService.deletePlayer(id);
        return "redirect:/";
    }

    @PostMapping("/add-national")
    public String addNational(@ModelAttribute National national) {
        heroService.saveNational(national);
        return "redirect:/";
    }

    @GetMapping("/delete-national/{id}")
    public String deleteNational(@PathVariable Integer id) {
        heroService.deleteNational(id);
        return "redirect:/";
    }

    @GetMapping("/search")
    public String search(@RequestParam("name") String name, Model model) {
        model.addAttribute("players", heroService.searchPlayersByName(name));
        model.addAttribute("nationals", heroService.getAllNationals());
        model.addAttribute("newPlayer", new Player());
        model.addAttribute("newNational", new National());
        model.addAttribute("searchName", name);
        return "index";
    }

    @GetMapping("/top10")
    public String top10(Model model) {
        model.addAttribute("players", heroService.getTop10Players());
        model.addAttribute("nationals", heroService.getAllNationals());
        model.addAttribute("newPlayer", new Player());
        model.addAttribute("newNational", new National());
        model.addAttribute("isTop10", true);
        return "index";
    }
}

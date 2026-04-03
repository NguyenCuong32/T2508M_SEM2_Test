package com.example.demo.controller;

import com.example.demo.dto.NationalRequest;
import com.example.demo.dto.PlayerRequest;
import com.example.demo.service.NationalService;
import com.example.demo.service.PlayerService;
import jakarta.validation.Valid;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class WebController {

    private final PlayerService playerService;
    private final NationalService nationalService;

    public WebController(PlayerService playerService, NationalService nationalService) {
        this.playerService = playerService;
        this.nationalService = nationalService;
    }

    @GetMapping("/")
    public String index(Model model, @RequestParam(name = "search", required = false) String search, 
                        @RequestParam(name = "top10", required = false) boolean top10) {
        if (top10) {
            model.addAttribute("players", playerService.getTop10Players());
        } else if (search != null && !search.isEmpty()) {
            model.addAttribute("players", playerService.searchPlayersByName(search));
        } else {
            model.addAttribute("players", playerService.getAllPlayers());
        }
        
        model.addAttribute("nationals", nationalService.getAllNationals());
        model.addAttribute("playerRequest", new PlayerRequest());
        model.addAttribute("nationalRequest", new NationalRequest());
        return "index";
    }

    @PostMapping("/players/add")
    public String addPlayer(@Valid @ModelAttribute("playerRequest") PlayerRequest request, BindingResult result, Model model) {
        if (result.hasErrors()) {
            model.addAttribute("players", playerService.getAllPlayers());
            model.addAttribute("nationals", nationalService.getAllNationals());
            model.addAttribute("nationalRequest", new NationalRequest());
            return "index";
        }
        playerService.createPlayer(request);
        return "redirect:/";
    }

    @PostMapping("/players/delete/{id}")
    public String deletePlayer(@PathVariable Long id) {
        playerService.deletePlayer(id);
        return "redirect:/";
    }

    @PostMapping("/nationals/add")
    public String addNational(@Valid @ModelAttribute("nationalRequest") NationalRequest request, BindingResult result, Model model) {
        if (result.hasErrors()) {
            model.addAttribute("players", playerService.getAllPlayers());
            model.addAttribute("nationals", nationalService.getAllNationals());
            model.addAttribute("playerRequest", new PlayerRequest());
            return "index";
        }
        nationalService.createNational(request);
        return "redirect:/";
    }

    @PostMapping("/nationals/delete/{id}")
    public String deleteNational(@PathVariable Long id) {
        nationalService.deleteNational(id);
        return "redirect:/";
    }
}

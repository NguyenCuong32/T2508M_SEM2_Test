package aptech.t2508m.baithihero.controller;

import aptech.t2508m.baithihero.entity.National;
import aptech.t2508m.baithihero.entity.Player;
import aptech.t2508m.baithihero.service.HeroGameService;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.mvc.support.RedirectAttributes;

import java.util.List;

@Controller
public class HeroGameController {
    private final HeroGameService heroGameService;

    public HeroGameController(HeroGameService heroGameService) {
        this.heroGameService = heroGameService;
    }

    @GetMapping("/")
    public String index(@RequestParam(required = false) String keyword,
                        @RequestParam(defaultValue = "all") String view,
                        Model model) {
        List<Player> players = "top10".equalsIgnoreCase(view)
                ? heroGameService.displayTop10()
                : heroGameService.displayAllByPlayerName(keyword);

        model.addAttribute("players", players);
        model.addAttribute("nationals", heroGameService.displayAllNational());
        model.addAttribute("playerForm", new PlayerForm());
        model.addAttribute("nationalForm", new NationalForm());
        model.addAttribute("keyword", keyword == null ? "" : keyword);
        model.addAttribute("view", view);
        return "index";
    }

    @PostMapping("/players")
    public String createPlayer(@ModelAttribute PlayerForm playerForm, RedirectAttributes redirectAttributes) {
        try {
            National national = heroGameService.findNationalById(playerForm.getNationalId());
            Player player = new Player(null, national, playerForm.getPlayerName(), playerForm.getHighScore(), playerForm.getLevel());
            heroGameService.insertPlayer(player);
            redirectAttributes.addFlashAttribute("successMessage", "Added player successfully.");
        } catch (Exception e) {
            redirectAttributes.addFlashAttribute("errorMessage", e.getMessage());
        }
        return "redirect:/";
    }

    @PostMapping("/players/{playerId}/delete")
    public String deletePlayer(@PathVariable Integer playerId, RedirectAttributes redirectAttributes) {
        try {
            heroGameService.deletePlayer(playerId);
            redirectAttributes.addFlashAttribute("successMessage", "Deleted player successfully.");
        } catch (Exception e) {
            redirectAttributes.addFlashAttribute("errorMessage", e.getMessage());
        }
        return "redirect:/";
    }

    @PostMapping("/nationals")
    public String createNational(@ModelAttribute NationalForm nationalForm, RedirectAttributes redirectAttributes) {
        try {
            heroGameService.insertNational(nationalForm.getNationalName());
            redirectAttributes.addFlashAttribute("successMessage", "Added national successfully.");
        } catch (Exception e) {
            redirectAttributes.addFlashAttribute("errorMessage", e.getMessage());
        }
        return "redirect:/";
    }

    @PostMapping("/nationals/{nationalId}/delete")
    public String deleteNational(@PathVariable Integer nationalId, RedirectAttributes redirectAttributes) {
        try {
            heroGameService.deleteNational(nationalId);
            redirectAttributes.addFlashAttribute("successMessage", "Deleted national successfully.");
        } catch (Exception e) {
            redirectAttributes.addFlashAttribute("errorMessage", "Cannot delete national that is still used by players.");
        }
        return "redirect:/";
    }

    public static class PlayerForm {
        private String playerName;
        private Integer highScore;
        private Integer level;
        private Integer nationalId;

        public String getPlayerName() {
            return playerName;
        }

        public void setPlayerName(String playerName) {
            this.playerName = playerName;
        }

        public Integer getHighScore() {
            return highScore;
        }

        public void setHighScore(Integer highScore) {
            this.highScore = highScore;
        }

        public Integer getLevel() {
            return level;
        }

        public void setLevel(Integer level) {
            this.level = level;
        }

        public Integer getNationalId() {
            return nationalId;
        }

        public void setNationalId(Integer nationalId) {
            this.nationalId = nationalId;
        }
    }

    public static class NationalForm {
        private String nationalName;

        public String getNationalName() {
            return nationalName;
        }

        public void setNationalName(String nationalName) {
            this.nationalName = nationalName;
        }
    }
}

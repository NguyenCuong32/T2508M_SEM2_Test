package com.example.demo;

import com.example.demo.entity.National;
import com.example.demo.repository.NationalRepository;
import com.example.demo.repository.PlayerRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@AutoConfigureMockMvc
@SpringBootTest
class DemoApplicationTests {

    @Autowired
    private MockMvc mockMvc;
    
    @Autowired
    private NationalRepository nationalRepository;
    
    @Autowired
    private PlayerRepository playerRepository;
    
    private Long nationalId;

    @BeforeEach
    void setUp() {
        playerRepository.deleteAll();
        nationalRepository.deleteAll();
        National national = nationalRepository.save(new National(null, "Vietnam"));
        nationalId = national.getNationalId();
    }

    @Test
    void contextLoads() {
    }

    @Test
    void shouldCreatePlayerAndSearchAndGetTop10() throws Exception {
        String requestBody = """
                {
                  "playerName": "An",
                  "highScore": 1500,
                  "level": 10,
                  "nationalId": %d
                }
                """.formatted(nationalId);

        mockMvc.perform(post("/players")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(requestBody))
                .andExpect(status().isCreated())
                .andExpect(jsonPath("$.playerName").value("An"))
                .andExpect(jsonPath("$.highScore").value(1500))
                .andExpect(jsonPath("$.level").value(10))
                .andExpect(jsonPath("$.national.nationalName").value("Vietnam"));

        mockMvc.perform(get("/players/search").param("name", "An"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$[0].playerName").value("An"));

        mockMvc.perform(get("/players/top10"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$[0].highScore").value(1500));
    }
}

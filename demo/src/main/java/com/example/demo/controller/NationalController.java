package com.example.demo.controller;

import com.example.demo.dto.NationalRequest;
import com.example.demo.entity.National;
import com.example.demo.service.NationalService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/nationals")
public class NationalController {

    private final NationalService nationalService;

    public NationalController(NationalService nationalService) {
        this.nationalService = nationalService;
    }

    @GetMapping
    public List<National> getAllNationals() {
        return nationalService.getAllNationals();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public National createNational(@Valid @RequestBody NationalRequest request) {
        return nationalService.createNational(request);
    }
}

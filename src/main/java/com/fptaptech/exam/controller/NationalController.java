package com.fptaptech.exam.controller;

import com.fptaptech.exam.entity.National;
import com.fptaptech.exam.service.NationalService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/nationals")
public class NationalController {

    @Autowired
    private NationalService nationalService;

    @PostMapping
    public National insertNational(@RequestBody National national) {
        return nationalService.insertNational(national);
    }

    @DeleteMapping
    public void deleteNational(@PathVariable Integer id) {
        nationalService.deleteNational(id);
    }
}

package com.example.demo.service;

import com.example.demo.entity.National;
import com.example.demo.repository.NationalRepository;
import java.util.List;

public class NationalService {

    private NationalRepository repo = new NationalRepository();

    public List<National> getAll() {
        return repo.getAll();
    }

    public void add(National n) {
        repo.insertNational(n);
    }

    public void delete(int id) {
        repo.deleteNational(id);
    }
}
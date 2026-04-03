package com.example.demo.service;

import com.example.demo.dto.NationalRequest;
import com.example.demo.entity.National;
import com.example.demo.repository.NationalRepository;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class NationalService {

    private final NationalRepository nationalRepository;

    public NationalService(NationalRepository nationalRepository) {
        this.nationalRepository = nationalRepository;
    }

    public List<National> getAllNationals() {
        return nationalRepository.findAll();
    }

    public National createNational(NationalRequest request) {
        National national = new National();
        national.setNationalName(request.getName());
        return nationalRepository.save(national);
    }

    public void deleteNational(Long id) {
        if (!nationalRepository.existsById(id)) {
            throw new IllegalArgumentException("National not found with id: " + id);
        }
        nationalRepository.deleteById(id);
    }
}

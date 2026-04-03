package com.fptaptech.exam.service;

import com.fptaptech.exam.entity.National;
import com.fptaptech.exam.repository.NationalRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class NationalService {
    @Autowired
    private NationalRepository nationalRepository;

    public National insertNational(National national) {
        return nationalRepository.save(national);
    }

    public void deleteNational(Integer id) {
        nationalRepository.deleteById(id);
    }
}

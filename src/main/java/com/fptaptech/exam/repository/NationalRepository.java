package com.fptaptech.exam.repository;

import com.fptaptech.exam.entity.National;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface NationalRepository extends JpaRepository<National, Integer> {

}

package com.fptaptech.exam.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "National")
public class National {
    @Id
    @GeneratedValue
    private Integer nationalId;

    private String nationalName;

    public Integer getNationalId() {
        return nationalId;
    }
    public void setNationalId(Integer newNationalId) {
        this.nationalId = newNationalId;
    }

    public String getNationalName() {
        return nationalName;
    }
    public void setNationalName(String newNationalName) {
        this.nationalName = newNationalName;
    }
}

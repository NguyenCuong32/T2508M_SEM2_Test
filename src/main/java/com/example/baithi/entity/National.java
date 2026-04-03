package com.example.baithi.entity;

import jakarta.persistence.*;
import java.util.List;

@Entity
@Table(name = "National")
public class National {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "NationalId")
    private Integer nationalId;

    @Column(name = "NationalName", nullable = false)
    private String nationalName;

    @OneToMany(mappedBy = "national", cascade = CascadeType.ALL)
    private List<Player> players;

    public National() {}

    public National(String nationalName) {
        this.nationalName = nationalName;
    }

    public Integer getNationalId() {
        return nationalId;
    }

    public void setNationalId(Integer nationalId) {
        this.nationalId = nationalId;
    }

    public String getNationalName() {
        return nationalName;
    }

    public void setNationalName(String nationalName) {
        this.nationalName = nationalName;
    }

    public List<Player> getPlayers() {
        return players;
    }

    public void setPlayers(List<Player> players) {
        this.players = players;
    }
}

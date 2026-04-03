package aptech.t2508m.baithihero.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.OneToMany;
import jakarta.persistence.Table;

import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "National")
public class National {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "NationalId")
    private Integer nationalId;

    @Column(name = "NationalName", nullable = false, unique = true, length = 100)
    private String nationalName;

    @OneToMany(mappedBy = "national")
    private List<Player> players = new ArrayList<>();

    public National() {
    }

    public National(Integer nationalId, String nationalName) {
        this.nationalId = nationalId;
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

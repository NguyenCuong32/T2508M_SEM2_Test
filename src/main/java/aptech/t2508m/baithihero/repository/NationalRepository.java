package aptech.t2508m.baithihero.repository;

import aptech.t2508m.baithihero.entity.National;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface NationalRepository extends JpaRepository<National, Integer> {
    List<National> findAllByOrderByNationalNameAsc();
}

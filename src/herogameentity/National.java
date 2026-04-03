package herogameentity;

public class National {
    private int nationalId;
    private String nationalName;

    public National() {
    }

    public National(int nationalId, String nationalName) {
        this.nationalId = nationalId;
        this.nationalName = nationalName;
    }

    // Getter và Setter
    public int getNationalId() { return nationalId; }
    public void setNationalId(int nationalId) { this.nationalId = nationalId; }

    public String getNationalName() { return nationalName; }
    public void setNationalName(String nationalName) { this.nationalName = nationalName; }

    // Ghi đè toString để khi đưa vào ComboBox nó hiện tên nước thay vì mã hash
    @Override
    public String toString() {
        return nationalName;
    }
}
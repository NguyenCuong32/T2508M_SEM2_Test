package herogameentity;
public class Player {
    private int playerId;
    private String playerName;
    private int highScore;
    private int level;
    private int nationalId;
    private String nationalName; // Thuộc tính này rất quan trọng để hiện tên nước

    public int getPlayerId() {
        return playerId;
    }

    public void setPlayerId(int playerId) {
        this.playerId = playerId;
    }

    public String getPlayerName() {
        return playerName;
    }

    public void setPlayerName(String playerName) {
        this.playerName = playerName;
    }

    public int getHighScore() {
        return highScore;
    }

    public void setHighScore(int highScore) {
        this.highScore = highScore;
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public int getNationalId() {
        return nationalId;
    }

    public void setNationalId(int nationalId) {
        this.nationalId = nationalId;
    }

    public String getNationalName() {
        return nationalName;
    }

    public void setNationalName(String nationalName) {
        this.nationalName = nationalName;
    }

    // Constructor không đối số (nên có)
    public Player() {}

    // Constructor đầy đủ 6 đối số (BẮT BUỘC ĐỂ HẾT LỖI Ở DAO)
    // Đảm bảo thứ tự: Id, Name, Score, Level, NationalId, NationalName
    public Player(int playerId, String playerName, int highScore, int level, int nationalId, String nationalName) {
        this.playerId = playerId;
        this.playerName = playerName;
        this.highScore = highScore;
        this.level = level;
        this.nationalId = nationalId;
        this.nationalName = nationalName;
    }

    // Đừng quên tạo Getter/Setter cho tất cả các biến trên nhé!
}
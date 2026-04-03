public class Player {
    private int playerId;
    private String playerName;
    private int highScore;
    private int level;
    private String nationalName; // Dùng cái này để hiển thị tên nước lên bảng cho dễ

    public Player(int playerId, String playerName, int highScore, int level, String nationalName) {
        this.playerId = playerId;
        this.playerName = playerName;
        this.highScore = highScore;
        this.level = level;
        this.nationalName = nationalName;
    }

    // Getters để TableView của JavaFX có thể lấy dữ liệu
    public int getPlayerId() { return playerId; }
    public String getPlayerName() { return playerName; }
    public int getHighScore() { return highScore; }
    public int getLevel() { return level; }
    public String getNationalName() { return nationalName; }
}

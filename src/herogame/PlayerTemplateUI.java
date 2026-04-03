package herogame;

import herogameentity.Player;
import herogameservice.PlayerService;
import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.awt.*;
import java.util.List;

public class PlayerTemplateUI extends JFrame {
    private PlayerService service = new PlayerService();
    private JTable table;
    private DefaultTableModel model;

    // Các thành phần Template Input
    private JTextField txtName, txtScore, txtLevel, txtNationalId;
    private JButton btnAdd, btnDelete, btnSearch, btnTop10, btnReset;

    public PlayerTemplateUI() {

        setTitle("HERO GAME - PLAYER MANAGEMENT SYSTEM");
        setSize(1000, 600);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLocationRelativeTo(null);
        setLayout(new BorderLayout(10, 10));

        // --- 2. HEADER (Tiêu đề) ---
        JLabel lblTitle = new JLabel("PLAYER INFORMATION DASHBOARD", JLabel.CENTER);
        lblTitle.setFont(new Font("Arial", Font.BOLD, 24));
        lblTitle.setForeground(new Color(41, 128, 185));
        add(lblTitle, BorderLayout.NORTH);

        // --- 3. CENTER (Bảng hiển thị dữ liệu - Table 1) ---
        String[] columns = {"Player Id", "Player Name", "High Score", "Level", "National Name"};
        model = new DefaultTableModel(columns, 0) {
            @Override
            public boolean isCellEditable(int row, int column) {
                return false; // Không cho sửa trực tiếp trên bảng
            }
        };
        table = new JTable(model);
        table.setRowHeight(25);
        table.setSelectionMode(ListSelectionModel.SINGLE_SELECTION); // Chỉ cho chọn 1 dòng để xóa
        add(new JScrollPane(table), BorderLayout.CENTER);

        // --- 4. WEST (Form nhập liệu) ---
        JPanel formPanel = new JPanel(new GridLayout(5, 2, 5, 5));
        formPanel.setBorder(BorderFactory.createTitledBorder("Player Details"));

        formPanel.add(new JLabel("Player Name:")); txtName = new JTextField(); formPanel.add(txtName);
        formPanel.add(new JLabel("High Score:")); txtScore = new JTextField(); formPanel.add(txtScore);
        formPanel.add(new JLabel("Level:")); txtLevel = new JTextField(); formPanel.add(txtLevel);
        formPanel.add(new JLabel("National ID:")); txtNationalId = new JTextField(); formPanel.add(txtNationalId);

        // --- 5. EAST (Nút bấm điều khiển) ---
        JPanel actionPanel = new JPanel(new GridLayout(5, 1, 10, 10));
        actionPanel.setBorder(BorderFactory.createEmptyBorder(0, 10, 0, 10));

        btnAdd = new JButton("INSERT NEW");
        btnDelete = new JButton("DELETE SELECTED");
        btnSearch = new JButton("SEARCH BY NAME");
        btnTop10 = new JButton("SHOW TOP 10");
        btnReset = new JButton("REFRESH LIST");

        actionPanel.add(btnAdd);
        actionPanel.add(btnDelete);
        actionPanel.add(btnSearch);
        actionPanel.add(btnTop10);
        actionPanel.add(btnReset);

        add(formPanel, BorderLayout.WEST);
        add(actionPanel, BorderLayout.EAST);

        // --- 6. XỬ LÝ LOGIC SỰ KIỆN ---

        // Load lại danh sách
        btnReset.addActionListener(e -> loadData(service.getAll()));

        // Xem Top 10
        btnTop10.addActionListener(e -> loadData(service.getTop10()));

        // Tìm kiếm theo tên
        btnSearch.addActionListener(e -> {
            String name = JOptionPane.showInputDialog(this, "Enter Player Name to Search:");
            if (name != null && !name.trim().isEmpty()) {
                loadData(service.search(name));
            }
        });

        // Thêm người chơi mới
        btnAdd.addActionListener(e -> {
            try {
                String name = txtName.getText();
                int score = Integer.parseInt(txtScore.getText());
                int lv = Integer.parseInt(txtLevel.getText());
                int nId = Integer.parseInt(txtNationalId.getText());

                Player p = new Player(0, name, score, lv, nId, "");
                service.add(p);
                loadData(service.getAll()); // Refresh bảng
                JOptionPane.showMessageDialog(this, "Added Player Successfully!");

                // Xóa trống form sau khi thêm
                txtName.setText(""); txtScore.setText(""); txtLevel.setText(""); txtNationalId.setText("");
            } catch (NumberFormatException ex) {
                JOptionPane.showMessageDialog(this, "Score, Level, and National ID must be numbers!");
            } catch (Exception ex) {
                JOptionPane.showMessageDialog(this, "Error: Check if National ID exists!");
            }
        });

        // XÓA NGƯỜI CHƠI (PHẦN BẠN ĐANG THIẾU)
        btnDelete.addActionListener(e -> {
            int selectedRow = table.getSelectedRow();
            if (selectedRow != -1) {
                // Lấy ID từ cột đầu tiên (index 0)
                int id = Integer.parseInt(table.getValueAt(selectedRow, 0).toString());

                int confirm = JOptionPane.showConfirmDialog(this,
                        "Are you sure you want to delete Player ID: " + id + "?",
                        "Confirm Delete", JOptionPane.YES_NO_OPTION);

                if (confirm == JOptionPane.YES_OPTION) {
                    service.delete(id);          // Gọi xuống Service xóa trong DB
                    loadData(service.getAll());  // Load lại bảng
                    JOptionPane.showMessageDialog(this, "Deleted Successfully!");
                }
            } else {
                JOptionPane.showMessageDialog(this, "Please select a player from the table to delete!");
            }
        });

        // Tự động load dữ liệu khi mở app
        loadData(service.getAll());
    }

    private void loadData(List<Player> list) {
        model.setRowCount(0);
        if (list != null) {
            for (Player p : list) {
                model.addRow(new Object[]{
                        p.getPlayerId(),
                        p.getPlayerName(),
                        p.getHighScore(),
                        p.getLevel(),
                        p.getNationalName()
                });
            }
        }
    }

    public static void main(String[] args) {
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (Exception e) {}

        SwingUtilities.invokeLater(() -> {
            new PlayerTemplateUI().setVisible(true);
        });
    }
}
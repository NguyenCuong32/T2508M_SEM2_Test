public class Product {
    private int id;
    private String name;
    private String thumbnail;
    private double price;
    private int qty;
    private String description;

    // Constructor mặc định
    public Product() {
        this.id = 0;
        this.name = "";
        this.thumbnail = "";
        this.price = 0;
        this.qty = 0;
        this.description = "";
    }

    // Getter & Setter
    public int getId() {
        return id;
    }

    public void setId(int id) {
        if (id >= 0) this.id = id;
        else System.out.println("ID không hợp lệ!");
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getThumbnail() {
        return thumbnail;
    }

    public void setThumbnail(String thumbnail) {
        this.thumbnail = thumbnail;
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        if (price >= 0) this.price = price;
        else System.out.println("Price không được âm!");
    }

    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        if (qty >= 0) this.qty = qty;
        else System.out.println("Quantity không được âm!");
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    // Hiển thị
    public void displayInfo() {
        System.out.println("\n=== PRODUCT INFO ===");
        System.out.println("ID: " + id);
        System.out.println("Name: " + name);
        System.out.println("Price: " + price);
        System.out.println("Quantity: " + qty);
        System.out.println("Description: " + description);
    }

    // Kiểm tra tồn kho
    public boolean checkAvailability(int orderQty) {
        return orderQty > 0 && orderQty <= qty;
    }

    // Đặt hàng
    public double placeOrder(int orderQty) {
        if (checkAvailability(orderQty)) {
            qty -= orderQty;
            return orderQty * price;
        } else {
            System.out.println("Không đủ hàng!");
            return 0;
        }
    }
}

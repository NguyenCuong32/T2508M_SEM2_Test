public class Product {
    private int id;
    private String name;
    private String thumbnail;
    private double price;
    private int qty;
    private String description;


    public Product() {
    }

    // 1c. Getters và Setters (Kèm kiểm tra dữ liệu không âm)
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
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
        if (price >= 0) {
            this.price = price;
        } else {
            System.out.println("Lỗi: Giá không được là số âm!");
        }
    }

    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        if (qty >= 0) {
            this.qty = qty;
        } else {
            System.out.println("Lỗi: Số lượng không được là số âm!");
        }
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }


    public void displayInfo() {
        System.out.println("\n--- THÔNG TIN SẢN PHẨM ---");
        System.out.println("ID: " + id);
        System.out.println("Tên SP: " + name);
        System.out.println("Ảnh: " + thumbnail);
        System.out.println("Giá: " + price);
        System.out.println("Số lượng kho: " + qty);
        System.out.println("Mô tả: " + description);
    }


    public boolean checkAvailability(int orderQty) {
        return orderQty > 0 && orderQty <= this.qty;
    }


    public double placeOrder(int orderQty) {
        if (checkAvailability(orderQty)) {
            this.qty -= orderQty;
            return orderQty * this.price;
        } else {
            return 0;
        }
    }
}
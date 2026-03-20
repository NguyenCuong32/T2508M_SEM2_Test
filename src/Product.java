public class Product {
    private int id;
    private String name;
    private String thumbnail;
    private double price;
    private int qty;
    private String description;

    public Product() {
        this.id = 0;
        this.name = "";
        this.thumbnail = "";
        this.price = 0.0;
        this.qty = 0;
        this.description = "";
    }

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
            System.out.println("Gia san pham khong duoc am.");
        }
    }

    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        if (qty >= 0) {
            this.qty = qty;
        } else {
            System.out.println("So luong san pham khong duoc am.");
        }
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void displayInfo() {
        System.out.println("ID: " + id);
        System.out.println("Ten: " + name);
        System.out.println("Anh: " + thumbnail);
        System.out.println("Gia: " + price);
        System.out.println("Ton kho: " + qty);
        System.out.println("Mo ta: " + description);
    }

    public boolean checkAvailability(int orderQty) {

        return orderQty > 0 && orderQty <= qty;
    }

    public double placeOrder(int orderQty) {
        if (checkAvailability(orderQty)) {
            double totalPrice = orderQty * price;
            qty -= orderQty;
            return totalPrice;
        } else {
            return -1;
        }
    }
}
public class Product {
    private int id;
    private String name;
    private String thumbnail;
    private double price;
    private int qty;
    private String description;

    public Product() {
    }

    public Product(int id, String name, String thumbnail, double price, int qty, String description) {
        this.id = id;
        this.name = name;
        this.thumbnail = thumbnail;
        setPrice(price);
        setQty(qty);
        this.description = description;
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
        if (name != null && !name.trim().isEmpty()) {
            this.name = name;
        } else {
            this.name = "Unknown";
        }
    }

    public String getThumbnail() {
        return thumbnail;
    }

    public void setThumbnail(String thumbnail) {
        if (thumbnail != null) {
            this.thumbnail = thumbnail;
        } else {
            this.thumbnail = "";
        }
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        if (price >= 0) {
            this.price = price;
        } else {
            System.out.println("Invalid price. Set to 0.");
            this.price = 0;
        }
    }

    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        if (qty >= 0) {
            this.qty = qty;
        } else {
            System.out.println("Invalid quantity. Set to 0.");
            this.qty = 0;
        }
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        if (description != null) {
            this.description = description;
        } else {
            this.description = "";
        }
    }

    public void displayInfo() {
        System.out.println("\n===== PRODUCT INFORMATION =====");
        System.out.println("ID: " + id);
        System.out.println("Product name: " + name);
        System.out.println("Thumbnail path: " + thumbnail);
        System.out.println("Price: " + price);
        System.out.println("Quantity in stock: " + qty);
        System.out.println("Description: " + description);
    }

    public boolean checkAvailability(int expectedQty) {
        return expectedQty > 0 && expectedQty <= qty;
    }

    public double placeOrder(int orderQty) {
        if (checkAvailability(orderQty)) {
            qty -= orderQty;
            return orderQty * price;
        }
        return 0;
    }
}
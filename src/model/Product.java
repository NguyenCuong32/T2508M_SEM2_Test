package model;

public class Product {
    private int id;
    private String name;
    private String thumbnail;
    private double price;
    private int qty;
    private String description;

    public Product() {
        id = 0;
        name = "";
        thumbnail = "";
        price = 0;
        qty = 0;
        description = "";
    }

    public int getId() { return id; }
    public void setId(int id) {
        if (id > 0) this.id = id;
    }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getThumbnail() { return thumbnail; }
    public void setThumbnail(String thumbnail) { this.thumbnail = thumbnail; }

    public double getPrice() { return price; }
    public void setPrice(double price) {
        if (price >= 0) this.price = price;
    }

    public int getQty() { return qty; }
    public void setQty(int qty) {
        if (qty >= 0) this.qty = qty;
    }

    public String getDescription() { return description; }
    public void setDescription(String description) {
        this.description = description;
    }

    public void displayInfo() {
        System.out.println("ID: " + id);
        System.out.println("Name: " + name);
        System.out.println("Price: " + price);
        System.out.println("Qty: " + qty);
        System.out.println("Description: " + description);
    }

    public boolean checkAvailability(int orderQty) {
        return orderQty > 0 && orderQty <= qty;
    }

    public double placeOrder(int orderQty) {
        if (checkAvailability(orderQty)) {
            qty -= orderQty;
            return orderQty * price;
        }
        return 0;
    }
}
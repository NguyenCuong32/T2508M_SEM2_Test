package org.aptech.t2508m;

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
        this.price = 0;
        this.qty = 0;
        this.description = "";
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = Math.max(id, 0);
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name != null ? name.trim() : "";
    }

    public String getThumbnail() {
        return thumbnail;
    }

    public void setThumbnail(String thumbnail) {
        this.thumbnail = thumbnail != null ? thumbnail.trim() : "";
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = Math.max(price, 0);
    }

    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        this.qty = Math.max(qty, 0);
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description != null ? description.trim() : "";
    }

    public void displayInfo() {
        System.out.println("----- PRODUCT INFO -----");
        System.out.println("ID          : " + id);
        System.out.println("Name        : " + name);
        System.out.println("Price       : $" + price);
        System.out.println("Quantity    : " + qty);
        System.out.println("Description : " + description);
        System.out.println("------------------------");
    }

    public boolean checkAvailability(int orderQty) {
        return orderQty > 0 && orderQty <= qty;
    }

    public double placeOrder(int orderQty) {
        if (!checkAvailability(orderQty)) {
            System.out.println("Order failed: invalid quantity or not enough stock.");
            return 0;
        }
        qty -= orderQty;
        return orderQty * price;
    }
}
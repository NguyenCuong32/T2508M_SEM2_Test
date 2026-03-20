package org.fptaptech.t2508m;

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
        this.name = name == null ? "" : name.trim();
    }

    public String getThumbnail() {
        return thumbnail;
    }

    public void setThumbnail(String thumbnail) {
        this.thumbnail = thumbnail == null ? "" : thumbnail.trim();
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        if (price < 0) {
            throw new IllegalArgumentException("Gia khong duoc am.");
        }
        this.price = price;
    }

    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        if (qty < 0) {
            throw new IllegalArgumentException("So luong khong duoc am.");
        }
        this.qty = qty;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description == null ? "" : description.trim();
    }

    public void displayInfo() {
        System.out.println("Ma san pham: " + id);
        System.out.println("Ten san pham: " + name);
        System.out.println("Gia: " + price);
        System.out.println("So luong trong kho: " + qty);
        System.out.println("Mo ta: " + description);
    }

    public boolean checkAvailability(int orderQty) {
        return orderQty > 0 && orderQty <= qty;
    }

    public double placeOrder(int orderQty) {
        if (!checkAvailability(orderQty)) {
            throw new IllegalArgumentException(
                "So luong dat hang phai lon hon 0 va nho hon hoac bang so luong trong kho."
            );
        }

        qty -= orderQty;
        return orderQty * price;
    }
}

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
        if (id >= 0)
            this.id = id;
        else
            System.out.println("ID không hợp lệ!");
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
        if (price >= 0)
            this.price = price;
        else
            System.out.println("Giá không được âm!");
    }

    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        if (qty >= 0)
            this.qty = qty;
        else
            System.out.println("Số lượng không được âm!");
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void displayInfo() {
        System.out.println("ID: " + id);
        System.out.println("Name: " + name);
        System.out.println("Price: " + price);
        System.out.println("Quantity: " + qty);
        System.out.println("Description: " + description);
    }

    public boolean checkAvailability(int orderQty) {
        return orderQty > 0 && orderQty <= qty;
    }

    public double placeOrder(int orderQty) {
        if (checkAvailability(orderQty)) {
            qty -= orderQty;
            return orderQty * price;
        } else {
            System.out.println("Không đủ hàng để đặt!");
            return 0;
        }
    }

    public String toFileString() {
        return id + "|" + name + "|" + thumbnail + "|" + price + "|" + qty + "|" + description;
    }

    public static Product fromFileString(String line) {
        String[] parts = line.split("\\|");
        Product p = new Product();
        if (parts.length == 6) {
            p.setId(Integer.parseInt(parts[0]));
            p.setName(parts[1]);
            p.setThumbnail(parts[2]);
            p.setPrice(Double.parseDouble(parts[3]));
            p.setQty(Integer.parseInt(parts[4]));
            p.setDescription(parts[5]);
        }
        return p;
    }
}
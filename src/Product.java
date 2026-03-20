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
            System.out.println("Giá sản phẩm không được âm.");
        }
    }


    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        if (qty >= 0) {
            this.qty = qty;
        } else {
            System.out.println("Số lượng sản phẩm không được âm.");
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
        System.out.println("Tên: " + name);
        System.out.println("Ảnh: " + thumbnail);
        System.out.println("Giá: " + price);
        System.out.println("Tồn kho: " + qty);
        System.out.println("Mô tả: " + description);
    }

    public boolean checkAvailability(int orderQty) {
        if (orderQty > 0 && orderQty <= qty) {
            return true;
        } else {
            return false;
        }
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
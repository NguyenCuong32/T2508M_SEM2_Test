public class Product {
    private int id;
    private String name;
    private String thumbnail;
    private double price;
    private int qty;
    private String description;
}

public Product(){
    this.id = 0;
    this.name = "Unknown";
    this.thumbnail = "";
    this.price = 0.0;
    this.qty = 0;
    this.description = "";
}

public int getId(){
    return id;
}

public void setId(int id){
    if (id >= 0)
        this.id = id;
    else
        System.out.println("ID cannot be negative");
}

public String getName(){
    return name;
}

public void setName(String name){
    if (name != null && !name.trim().isEmpty())
        this.name = name;
    else
        System.out.println("Name cannot be empty");
}

public String getThumbnail(){
    return thumbnail;
}

public void setThumbnail(String thumbnail){
    this.thumbnail = thumbnail;
}

public double getPrice(){
    return price;
}

public void setPrice(double price){
    if (price >=0)
        this.price = price;
    else
        System.out.println("Price cannot be negative");
}

public String getDescription(){
    return description;
}

public void setDescription(String description){
    this.description = description;
}

public int getQty(){
    return qty;
}

public void setQty(int qty){
    if(qty >= 0)
        this.qty = qty;
    else
        System.out.println("Quantity cannot be negative");
}

public void displayInfo(){
    System.out.println("=====Product Info=====");
    System.out.println("ID:" + id);
    System.out.println("Name:" + name);
    System.out.println("Price:" + price);
    System.out.println("Quantity:" + quantity);
    System.out.println("Description:" + description);
}

public boolean checkAvailablity(int orderQty) {
    return orderQty > 0 && orderQty <= qty;
}

public double placeOrder(int orderQty) {
    if (checkAvailablity(orderQty)) {
        qty -= orderQty;
        return orderQty * price;
    } else {
        System.out.println("Not enough stock or invalid quantity");
        return 0;
    }
}
}

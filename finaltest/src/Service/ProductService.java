package Service;

import Model.Product;

import java.util.ArrayList;
import java.util.List;

public class ProductService {
    private List<Product> productList = new ArrayList<>();

    public void addProduct(Product product) {
        productList.add(product);
    }
    public void displayInfo() {
        if(productList.isEmpty()){
            System.out.println("No products found");
            return;
        }
        for (Product p : productList) {
            System.out.println("========== PRODUCT INFO ==========");
            System.out.println("ID          : " + p.getId());
            System.out.println("Name        : " + p.getName());
            System.out.println("Price       : " + p.getPrice());
            System.out.println("Quantity    : " + p.getQuantity());
            System.out.println("Description : " + p.getDescription());
            System.out.println("==================================");
        }

    }

    public boolean checkAvailability(Product product , int quantity) {
         return quantity > 0 && product.getQuantity() >= quantity;
    }
    public double placeOrder(Product product , int orderQty) {
        if(checkAvailability(product, orderQty)) {
             product.setQuantity(product.getQuantity() - orderQty);
             return product.getPrice() * orderQty;
        } else {
            System.out.println("Sold out");
            return 0;
        }
    }
    public Product findbyID(int id) {
        for (Product p : productList) {
            if (p.getId() == id) {
                return p;
            }
        }
        return null;
    }

}

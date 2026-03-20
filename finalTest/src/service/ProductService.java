package service;

import model.Product;

import java.util.ArrayList;
import java.util.List;

public class ProductService {

    private List<Product> productList = new ArrayList<>();

    public void addProduct(Product p) {
        productList.add(p);
    }

    public void displayAll() {
        if (productList.isEmpty()) {
            System.out.println("No products available!");
            return;
        }

        for (Product p : productList) {
            System.out.println("-----");
            System.out.println("ID: " + p.getId());
            System.out.println("Name: " + p.getProductName());
            System.out.println("Price: $" + p.getPrice());
            System.out.println("Qty: " + p.getQuantity());
            System.out.println("Description: " + p.getDescription());
        }
    }

    public Product findById(int id) {
        for (Product p : productList) {
            if (p.getId() == id) {
                return p;
            }
        }
        return null;
    }

    public boolean checkAvailability(Product p, int overQty) {
        return overQty > 0 && p.getQuantity() >= overQty;
    }

    public double placeOrder(Product p, int overQty) {
        if (checkAvailability(p, overQty)) {
            p.setQuantity(p.getQuantity() - overQty);
            return p.getPrice() *  overQty;
        }
        else {
            System.out.println("Product not available");
            return 0;
        }
    }
}

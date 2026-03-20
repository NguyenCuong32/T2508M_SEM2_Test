package Controller;

import Model.Product;
import Service.ProductService;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Menu {
    List<Product> products = new ArrayList<>();
    static ProductService productService = new ProductService();
    public void run() {
        Scanner sc = new Scanner(System.in);

        while(true) {
            System.out.println("1. Add Product");
            System.out.println("2 Display Products");
            System.out.println("3. Order");
            int choice  = sc.nextInt();
            switch(choice) {
                case 1:
                    Product p = addProduct();
                     productService.addProduct(p);
                    System.out.println("Added successfully!");
                    break;
                case 2:
                    displayProduct();
                    break;
                    case 3:
                        orderProduct();
                        break;
                        default:
                            return;

            }
        }
    }
    public Product addProduct() {
         Product p = new Product();
         Scanner sc = new Scanner(System.in);
        System.out.println("Enter Product id: ");
        p.setId(sc.nextInt());
        System.out.println("Enter Product name: ");
        p.setName(sc.next());
        System.out.println("Enter thumbnail");
        p.setThumbnail(sc.next());
        System.out.println("Enter price: ");
        p.setPrice(sc.nextDouble());
        System.out.println("Enter quantity: ");
        p.setQuantity(sc.nextInt());
        System.out.println("Enter product dep: ");
        p.setDescription(sc.nextLine());
        return p;

    }
    public void displayProduct() {
            productService.displayInfo();
    }
    public void orderProduct() {
        Scanner sc = new Scanner(System.in);
        System.out.println("Enter Product Id: ");
        int productId = sc.nextInt();
        Product product = productService.findbyID(productId);
        if(product == null) {
            System.out.println("Product not found!");
            return;
        }
        System.out.println("Enter Product Quantity: ");
        int productQuantity = sc.nextInt();
        boolean check = productService.checkAvailability(product , productQuantity);
        if(check) {
            double total = productService.placeOrder(product, productQuantity);
            System.out.println("Order Successful!" + total);
        } else {
            System.out.println("Order Not Available!");
        }
    }

}

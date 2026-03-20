package controller;

import model.Product;
import service.ProductService;

import java.util.Scanner;

public class ProductController {
    private ProductService service = new ProductService();
    private Scanner sc = new Scanner(System.in);

    public void menu() {
        while (true) {
            System.out.println("\n===== MENU =====");
            System.out.println("1. Add Product");
            System.out.println("2. Display All Products");
            System.out.println("3. Order Product");
            System.out.println("0. Exit");
            System.out.print("Choose: ");

            int choice = sc.nextInt();

            switch (choice) {
                case 1:
                    Product p = inputProduct();
                    service.addProduct(p);
                    break;

                case 2:
                    service.displayAll();
                    break;

                case 3:
                    orderProduct();
                    break;

                case 0:
                    System.out.println("Exit...");
                    return;

                default:
                    System.out.println("Invalid choice!");
            }
        }
    }

    private Product inputProduct() {
        Product p = new Product();

        System.out.print("Enter ID: ");
        p.setId(sc.nextInt());
        sc.nextLine();

        System.out.print("Enter Name: ");
        p.setProductName(sc.nextLine());

        System.out.print("Enter Thumbnail: ");
        p.setThumbnail(sc.nextLine());

        System.out.print("Enter Price: ");
        p.setPrice(sc.nextDouble());

        System.out.print("Enter Quantity: ");
        p.setQuantity(sc.nextInt());
        sc.nextLine();

        System.out.print("Enter Description: ");
        p.setDescription(sc.nextLine());

        return p;
    }

    private void orderProduct() {
        System.out.print("Enter product ID: ");
        int id = sc.nextInt();

        Product p = service.findById(id);

        if (p == null) {
            System.out.println("Product not found!");
            return;
        }

        System.out.print("Enter order quantity: ");
        int qty = sc.nextInt();

        if (service.checkAvailability(p, qty)) {
            double total = service.placeOrder(p, qty);
            System.out.println("Order success! Total: $" + total);
        } else {
            System.out.println("Not enough stock!");
        }
    }
}
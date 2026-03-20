package org.aptech.t2508m;

import java.util.Scanner;

public class Main {

    static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        Product p = new Product();

        p.setId(inputInt("Enter ID: ", 0));
        p.setName(inputString("Enter Name: "));
        p.setThumbnail(inputString("Enter Thumbnail: "));
        p.setPrice(inputDouble("Enter Price: ", 0));
        p.setQty(inputInt("Enter Quantity: ", 0));
        p.setDescription(inputString("Enter Description: "));

        p.displayInfo();

        int orderQty = inputInt("\nEnter Order Quantity: ", 1);

        double total = p.placeOrder(orderQty);
        if (total > 0) {
            System.out.println("Order successful!");
            System.out.println("Total price: " + total + "$");
            System.out.println("Remaining stock: " + p.getQty());
        }
    }

    static int inputInt(String msg, int min) {
        int value;
        while (true) {
            System.out.print(msg);
            if (sc.hasNextInt()) {
                value = sc.nextInt();
                sc.nextLine();
                if (value >= min) return value;
            } else {
                sc.nextLine();
            }
            System.out.println("Invalid input. Try again.");
        }
    }

    static double inputDouble(String msg, double min) {
        double value;
        while (true) {
            System.out.print(msg);
            if (sc.hasNextDouble()) {
                value = sc.nextDouble();
                sc.nextLine();
                if (value >= min) return value;
            } else {
                sc.nextLine();
            }
            System.out.println("Invalid input. Try again.");
        }
    }

    static String inputString(String msg) {
        System.out.print(msg);
        return sc.nextLine().trim();
    }
}
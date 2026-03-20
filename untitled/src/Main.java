import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Product product = new Product();

        System.out.print("Enter product ID: ");
        product.setId(readInt(sc));

        System.out.print("Enter product name: ");
        product.setName(sc.nextLine());

        System.out.print("Enter thumbnail path: ");
        product.setThumbnail(sc.nextLine());

        System.out.print("Enter product price: ");
        product.setPrice(readDouble(sc));

        System.out.print("Enter quantity in stock: ");
        product.setQty(readInt(sc));

        System.out.print("Enter product description: ");
        product.setDescription(sc.nextLine());

        product.displayInfo();

        System.out.print("\nEnter quantity to order: ");
        int orderQty = readInt(sc);

        if (product.checkAvailability(orderQty)) {
            double total = product.placeOrder(orderQty);
            System.out.println("Order placed successfully!");
            System.out.println("Total amount: " + total);
            System.out.println("Remaining quantity in stock: " + product.getQty());
        } else {
            System.out.println("Order failed!");
            System.out.println("Order quantity must be greater than 0 and less than or equal to stock quantity.");
        }

        sc.close();
    }

    public static int readInt(Scanner sc) {
        while (true) {
            try {
                return Integer.parseInt(sc.nextLine());
            } catch (NumberFormatException e) {
                System.out.print("Please enter a valid integer: ");
            }
        }
    }

    public static double readDouble(Scanner sc) {
        while (true) {
            try {
                return Double.parseDouble(sc.nextLine());
            } catch (NumberFormatException e) {
                System.out.print("Please enter a valid double number: ");
            }
        }
    }
}
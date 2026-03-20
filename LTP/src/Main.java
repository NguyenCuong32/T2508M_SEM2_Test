import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Product product = new Product();

        System.out.print("Enter product ID: ");
        product.setId(readInt(scanner));

        System.out.print("Enter product name: ");
        product.setName(scanner.nextLine());

        System.out.print("Enter thumbnail path: ");
        product.setThumbnail(scanner.nextLine());

        System.out.print("Enter price: ");
        product.setPrice(readNonNegativeDouble(scanner));

        System.out.print("Enter quantity in stock: ");
        product.setQty(readNonNegativeInt(scanner));

        System.out.print("Enter product description: ");
        product.setDescription(scanner.nextLine());

        System.out.println();
        product.displayInfo();

        System.out.print("\nEnter order quantity: ");
        int orderQty = readInt(scanner);

        if (product.checkAvailability(orderQty)) {
            try {
                double total = product.placeOrder(orderQty);
                System.out.println("Order placed successfully.");
                System.out.println("Total price: " + total);
                System.out.println("Remaining quantity: " + product.getQty());
            } catch (IllegalArgumentException | IllegalStateException e) {
                System.out.println("Order failed: " + e.getMessage());
            }
        } else {
            System.out.println("Not enough stock or invalid order quantity.");
        }

        scanner.close();
    }

    private static int readInt(Scanner scanner) {
        while (true) {
            String line = scanner.nextLine();
            try {
                return Integer.parseInt(line.trim());
            } catch (NumberFormatException e) {
                System.out.print("Invalid integer. Please enter again: ");
            }
        }
    }

    private static int readNonNegativeInt(Scanner scanner) {
        while (true) {
            int value = readInt(scanner);
            if (value >= 0) {
                return value;
            }
            System.out.print("Value cannot be negative. Please enter again: ");
        }
    }

    private static double readNonNegativeDouble(Scanner scanner) {
        while (true) {
            String line = scanner.nextLine();
            try {
                double value = Double.parseDouble(line.trim());
                if (value >= 0) {
                    return value;
                }
                System.out.print("Value cannot be negative. Please enter again: ");
            } catch (NumberFormatException e) {
                System.out.print("Invalid number. Please enter again: ");
            }
        }
    }
}
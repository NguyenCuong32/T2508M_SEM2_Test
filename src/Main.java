import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Product p = new Product();

        // Nhập thông tin
        System.out.print("Enter ID: ");
        p.setId(sc.nextInt());
        sc.nextLine();

        System.out.print("Enter Name: ");
        p.setName(sc.nextLine());

        System.out.print("Enter Thumbnail: ");
        p.setThumbnail(sc.nextLine());

        System.out.print("Enter Price: ");
        p.setPrice(sc.nextDouble());

        System.out.print("Enter Quantity: ");
        p.setQty(sc.nextInt());
        sc.nextLine();

        System.out.print("Enter Description: ");
        p.setDescription(sc.nextLine());

        // Hiển thị
        p.displayInfo();

        // Order
        System.out.print("\nEnter quantity to order: ");
        int orderQty = sc.nextInt();

        if (p.checkAvailability(orderQty)) {
            double total = p.placeOrder(orderQty);
            System.out.println("Order successful!");
            System.out.println("Total price: " + total);
            System.out.println("Remaining quantity: " + p.getQty());
        } else {
            System.out.println("Order failed!");
        }

        sc.close();
    }
}

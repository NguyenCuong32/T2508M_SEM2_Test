import java.util.Scanner;

public class Main {
    public static void main(String[] args){
        Scanner sc = new Scanner(System.in);
        Product p = new Product();

        System.out.print("Enter product ID:");
        p.setId(sc.nextInt());
        sc.nextLine();

        System.out.print("Enter product name:");
        p.setName(sc.nextLine());

        System.out.print("Enter thumbnail path");
        p.setThumbnail(sc.nextLine());

        System.out.print("Enter price:");
        p.setPrice(sc.nextDouble());

        System.out.print("Enter quantity:");
        p.setQty(sc.nextInt());
        sc.nextLine();

        System.out.print("Enter description:");
        p.setDescription(sc.nextLine());

        p.displayInfo();

        System.out.print("\nEnter order quantity:");
        int orderQty = sc.nextInt();

        if (p.checkAvailability(orderQty)){
            double total = p.placeOrder(orderQty);
            System.out.println("Order successful!");
            System.out.println("Total price:" + total);
        } else {
            System.out.println("Order failed! Not enough stock");
        }

        System.out.println("\nAfter order:");
        p.displayInfo();
        sc.close();
    }
}

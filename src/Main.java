import model.Product;
import java.util.Scanner;

public class Main {
  public static void main(String[] args) {
    Scanner sc = new Scanner(System.in);
    Product p = new Product();

    System.out.print("Enter ID: ");
    p.setId(sc.nextInt());
    sc.nextLine();

    System.out.print("Enter name: ");
    p.setName(sc.nextLine());

    System.out.print("Enter thumbnail: ");
    p.setThumbnail(sc.nextLine());

    System.out.print("Enter price: ");
    p.setPrice(sc.nextDouble());

    System.out.print("Enter quantity: ");
    p.setQty(sc.nextInt());
    sc.nextLine();

    System.out.print("Enter description: ");
    p.setDescription(sc.nextLine());

    System.out.println("\n=== PRODUCT INFO ===");
    p.displayInfo();

    System.out.print("\nEnter order quantity: ");
    int orderQty = sc.nextInt();

    if (p.checkAvailability(orderQty)) {
      double total = p.placeOrder(orderQty);
      System.out.println("Order success!");
      System.out.println("Total: " + total);
    } else {
      System.out.println("Not enough stock!");
    }
  }
}
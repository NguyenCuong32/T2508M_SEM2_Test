import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        Product p = new Product();

        System.out.print("Nhập ID: ");
        p.setId(sc.nextInt());
        sc.nextLine();

        System.out.print("Nhập tên sản phẩm: ");
        p.setName(sc.nextLine());

        System.out.print("Nhập thumbnail: ");
        p.setThumbnail(sc.nextLine());

        System.out.print("Nhập giá: ");
        p.setPrice(sc.nextDouble());

        System.out.print("Nhập số lượng: ");
        p.setQty(sc.nextInt());
        sc.nextLine();

        System.out.print("Nhập mô tả: ");
        p.setDescription(sc.nextLine());

        System.out.println("\n--- Thông tin sản phẩm ---");
        p.displayInfo();

        System.out.print("\nNhập số lượng muốn mua: ");
        int orderQty = sc.nextInt();

        if (p.checkAvailability(orderQty)) {
            double total = p.placeOrder(orderQty);
            System.out.println("Đặt hàng thành công!");
            System.out.println("Tổng tiền: " + total);
        } else {
            System.out.println("Không đủ hàng để đặt!");
        }

        sc.close();
    }
}

import java.util.Scanner;
import java.text.DecimalFormat;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        DecimalFormat df = new DecimalFormat("#,### VNĐ"); // Định dạng tiền tệ
        Product prod = new Product();

        System.out.println("===== THIẾT LẬP KHO HÀNG =====");

        // Sử dụng hàm nhập an toàn (đã viết ở dưới) để tránh lỗi crash
        prod.setId(readInt(sc, "Nhập ID: "));

        System.out.print("Nhập tên sản phẩm: ");
        prod.setName(sc.nextLine());

        System.out.print("Nhập giá sản phẩm: ");
        prod.setPrice(readDouble(sc));

        prod.setQty(readInt(sc, "Nhập số lượng trong kho: "));

        System.out.print("Nhập mô tả sản phẩm: ");
        prod.setDescription(sc.nextLine());


        prod.displayInfo();


        while (prod.getQty() > 0) {
            System.out.println("\n--- THỰC HIỆN GIAO DỊCH ---");
            System.out.println("Kho hiện còn: " + prod.getQty());
            System.out.print("Nhập số lượng muốn mua (Nhập 0 để thoát): ");
            int buyQty = sc.nextInt();

            if (buyQty == 0) break;

            if (prod.checkAvailability(buyQty)) {
                double total = prod.placeOrder(buyQty);
                System.out.println("=> THÀNH CÔNG! Tổng tiền: " + df.format(total));
            } else {
                System.out.println("=> THẤT BẠI: Kho không đủ hoặc số lượng không hợp lệ!");
            }
        }

        System.out.println("Cảm ơn bạn đã sử dụng hệ thống!");
        sc.close();
    }


    public static int readInt(Scanner sc, String prompt) {
        while (true) {
            try {
                System.out.print(prompt);
                int val = Integer.parseInt(sc.nextLine());
                return val;
            } catch (NumberFormatException e) {
                System.out.println("Lỗi: Vui lòng nhập số nguyên!");
            }
        }
    }


    public static double readDouble(Scanner sc) {
        while (true) {
            try {
                double val = Double.parseDouble(sc.nextLine());
                return val;
            } catch (NumberFormatException e) {
                System.out.println("Lỗi: Vui lòng nhập số thập phân (ví dụ: 1500.5)!");
            }
        }
    }
}
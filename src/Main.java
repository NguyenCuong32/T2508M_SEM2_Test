import java.util.Scanner;

public class Main {
    public static void main(String[] args) { // Chữ m viết thường mới chạy được
        Scanner scanner = new Scanner(System.in);
        Product product = new Product();
        System.out.println("=== NHẬP THÔNG TIN SẢN PHẨM ===");

        System.out.print("Nhập ID: ");
        product.setId(scanner.nextInt());
        scanner.nextLine();

        System.out.print("Nhập tên sản phẩm: ");
        product.setName(scanner.nextLine());

        System.out.print("Nhập ảnh (thumbnail): ");
        product.setThumbnail(scanner.nextLine());

        System.out.print("Nhập giá sản phẩm: ");
        product.setPrice(scanner.nextDouble());

        System.out.print("Nhập số lượng tồn kho: ");
        product.setQty(scanner.nextInt());
        scanner.nextLine();

        System.out.print("Nhập mô tả sản phẩm: ");
        product.setDescription(scanner.nextLine());


        System.out.println("\n=== CHI TIẾT SẢN PHẨM VỪA NHẬP ===");
        product.displayInfo();


        System.out.println("\n=== ĐẶT HÀNG ===");
        System.out.print("Nhập số lượng bạn muốn mua: ");
        int orderQty = scanner.nextInt();


        if (product.checkAvailability(orderQty)) {
            double total = product.placeOrder(orderQty);
            System.out.println("=> Đặt hàng thành công!");
            System.out.println("=> Tổng số tiền thanh toán: " + total);
            System.out.println("=> Số lượng còn lại trong kho: " + product.getQty());
        } else {
            System.out.println("=> Đặt hàng thất bại: Không đủ hàng trong kho hoặc số lượng không hợp lệ!");
        }

        scanner.close();
    }
}
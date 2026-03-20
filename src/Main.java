import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Product product = new Product();

        System.out.println("=== NHAP THONG TIN SAN PHAM ===");

        System.out.print("Nhap ID: ");
        product.setId(scanner.nextInt());
        scanner.nextLine(); // Doc bo dong trong sau khi nhap so

        System.out.print("Nhap ten san pham: ");
        product.setName(scanner.nextLine());

        System.out.print("Nhap anh (thumbnail): ");
        product.setThumbnail(scanner.nextLine());

        System.out.print("Nhap gia san pham: ");
        product.setPrice(scanner.nextDouble());

        System.out.print("Nhap so luong ton kho: ");
        product.setQty(scanner.nextInt());
        scanner.nextLine(); // Doc bo dong trong

        System.out.print("Nhap mo ta san pham: ");
        product.setDescription(scanner.nextLine());


        System.out.println("\n=== CHI TIET SAN PHAM VUA NHAP ===");
        product.displayInfo();


        System.out.println("\n=== DAT HANG ===");
        System.out.print("Nhap so luong ban muon mua: ");
        int orderQty = scanner.nextInt();


        if (product.checkAvailability(orderQty)) {
            double total = product.placeOrder(orderQty);
            System.out.println("=> Dat hang thanh cong!");
            System.out.println("=> Tong so tien thanh toan: " + total);
            System.out.println("=> So luong con lai trong kho: " + product.getQty());
        } else {
            System.out.println("=> Dat hang that bai: Khong du hang trong kho hoac so luong khong hop le!");
        }

        scanner.close();
    }
}
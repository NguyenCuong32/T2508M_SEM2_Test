import java.util.*;

public class Main {
    static Scanner sc = new Scanner(System.in);
    static List<Product> list = new ArrayList<>();
    static String FILE = "products.txt";

    public static void main(String[] args) {
        list = FileHelper.readProducts(FILE);

        int choice;
        do {
            System.out.println("\n===== MENU =====");
            System.out.println("1. Thêm sản phẩm");
            System.out.println("2. Hiển thị danh sách");
            System.out.println("3. Tìm sản phẩm theo tên");
            System.out.println("4. Sửa sản phẩm theo ID");
            System.out.println("5. Xóa sản phẩm theo ID");
            System.out.println("6. Mua sản phẩm theo ID");
            System.out.println("0. Thoát");
            System.out.print("Chọn: ");
            choice = sc.nextInt();
            sc.nextLine();

            switch (choice) {
                case 1:
                    addProduct();
                    break;
                case 2:
                    showList();
                    break;
                case 3:
                    searchProduct();
                    break;
                case 4:
                    updateProduct();
                    break;
                case 5:
                    deleteProduct();
                    break;
                case 6:
                    orderProduct();
                    break;
            }

        } while (choice != 0);

        System.out.println("Thoát chương trình!");
    }

    static void addProduct() {
        Product p = new Product();

        System.out.print("ID: ");
        p.setId(sc.nextInt());
        sc.nextLine();

        System.out.print("Tên: ");
        p.setName(sc.nextLine());

        System.out.print("Thumbnail: ");
        p.setThumbnail(sc.nextLine());

        System.out.print("Giá: ");
        p.setPrice(sc.nextDouble());

        System.out.print("Số lượng: ");
        p.setQty(sc.nextInt());
        sc.nextLine();

        System.out.print("Mô tả: ");
        p.setDescription(sc.nextLine());

        list.add(p);
        saveAll();

        System.out.println("✔ Đã thêm!");
    }

    static void showList() {
        if (list.isEmpty()) {
            System.out.println("Danh sách trống!");
            return;
        }

        for (Product p : list) {
            p.displayInfo();
            System.out.println("----------------");
        }
    }

    static void searchProduct() {
        System.out.print("Nhập tên cần tìm: ");
        String keyword = sc.nextLine();

        for (Product p : list) {
            if (p.getName().toLowerCase().contains(keyword.toLowerCase())) {
                p.displayInfo();
                System.out.println("----------------");
            }
        }
    }

    static void updateProduct() {
        System.out.print("Nhập ID cần sửa: ");
        int id = sc.nextInt();
        sc.nextLine();

        for (Product p : list) {
            if (p.getId() == id) {
                System.out.print("Tên mới: ");
                p.setName(sc.nextLine());

                System.out.print("Giá mới: ");
                p.setPrice(sc.nextDouble());

                System.out.print("Số lượng mới: ");
                p.setQty(sc.nextInt());
                sc.nextLine();

                System.out.print("Mô tả mới: ");
                p.setDescription(sc.nextLine());

                saveAll();
                System.out.println("✔ Đã cập nhật!");
                return;
            }
        }

        System.out.println("Không tìm thấy!");
    }

    static void deleteProduct() {
        System.out.print("Nhập ID cần xóa: ");
        int id = sc.nextInt();

        Iterator<Product> it = list.iterator();

        while (it.hasNext()) {
            if (it.next().getId() == id) {
                it.remove();
                saveAll();
                System.out.println("✔ Đã xóa!");
                return;
            }
        }

        System.out.println("Không tìm thấy!");
    }

    static void saveAll() {
        FileHelper.writeAll(list, FILE);
    }

    static void orderProduct() {
        System.out.print("Nhập ID sản phẩm: ");
        int id = sc.nextInt();

        for (Product p : list) {
            if (p.getId() == id) {
                System.out.print("Nhập số lượng muốn mua: ");
                int qty = sc.nextInt();

                if (p.checkAvailability(qty)) {
                    double total = p.placeOrder(qty);
                    saveAll();
                    System.out.println("✔ Mua thành công!");
                    System.out.println("Tổng tiền: " + total);
                    System.out.println("Còn lại: " + p.getQty());
                } else {
                    System.out.println("Không đủ hàng!");
                }
                return;
            }
        }

        System.out.println("Không tìm thấy sản phẩm!");
    }
}
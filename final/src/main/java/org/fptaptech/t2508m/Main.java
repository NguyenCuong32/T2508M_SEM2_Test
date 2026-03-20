package org.fptaptech.t2508m;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Product product = new Product();

        System.out.print("Nhap ma san pham: ");
        product.setId(readInt(scanner));

        System.out.print("Nhap ten san pham: ");
        product.setName(readNonEmptyLine(scanner));

        System.out.print("Nhap duong dan hinh anh: ");
        product.setThumbnail(readNonEmptyLine(scanner));

        System.out.print("Nhap gia san pham: ");
        product.setPrice(readNonNegativeDouble(scanner));

        System.out.print("Nhap so luong trong kho: ");
        product.setQty(readNonNegativeInt(scanner));

        System.out.print("Nhap mo ta san pham: ");
        product.setDescription(readNonEmptyLine(scanner));

        System.out.println();
        System.out.println("Thong tin san pham:");
        product.displayInfo();

        System.out.println();
        System.out.print("Nhap so luong muon dat: ");
        int orderQty = readPositiveInt(scanner);

        if (product.checkAvailability(orderQty)) {
            double totalPrice = product.placeOrder(orderQty);
            System.out.println("Dat hang thanh cong.");
            System.out.println("Tong tien: " + totalPrice);
            System.out.println("So luong con lai trong kho: " + product.getQty());
        } else {
            System.out.println("Khong du so luong trong kho de dat hang.");
        }
    }

    private static int readInt(Scanner scanner) {
        while (!scanner.hasNextInt()) {
            System.out.print("Du lieu khong hop le. Vui long nhap so nguyen: ");
            scanner.next();
        }
        int value = scanner.nextInt();
        scanner.nextLine();
        return value;
    }

    private static int readNonNegativeInt(Scanner scanner) {
        while (true) {
            int value = readInt(scanner);
            if (value >= 0) {
                return value;
            }
            System.out.print("Gia tri khong duoc am. Nhap lai: ");
        }
    }

    private static int readPositiveInt(Scanner scanner) {
        while (true) {
            int value = readInt(scanner);
            if (value > 0) {
                return value;
            }
            System.out.print("Gia tri phai lon hon 0. Nhap lai: ");
        }
    }

    private static double readNonNegativeDouble(Scanner scanner) {
        while (!scanner.hasNextDouble()) {
            System.out.print("Du lieu khong hop le. Vui long nhap so: ");
            scanner.next();
        }

        double value = scanner.nextDouble();
        scanner.nextLine();

        while (value < 0) {
            System.out.print("Gia tri khong duoc am. Nhap lai: ");
            while (!scanner.hasNextDouble()) {
                System.out.print("Du lieu khong hop le. Vui long nhap so: ");
                scanner.next();
            }
            value = scanner.nextDouble();
            scanner.nextLine();
        }

        return value;
    }

    private static String readNonEmptyLine(Scanner scanner) {
        while (true) {
            String value = scanner.nextLine().trim();
            if (!value.isEmpty()) {
                return value;
            }
            System.out.print("Khong duoc de trong. Nhap lai: ");
        }
    }
}
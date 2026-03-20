package org.fptaptech.t2508m;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        boolean continueProgram;
        do {
            Product product = new Product();

            System.out.println("Nhap thong tin san pham");
            product.setId(readInt(scanner, "Ma san pham: "));
            product.setName(readNonEmptyString(scanner, "Ten san pham: "));
            product.setThumbnail(readNonEmptyString(scanner, "Duong dan hinh anh: "));
            product.setPrice(readNonNegativeDouble(scanner, "Gia: "));
            product.setQty(readNonNegativeInt(scanner, "So luong trong kho: "));
            product.setDescription(readNonEmptyString(scanner, "Mo ta: "));

            System.out.println();
            System.out.println("Thong tin san pham");
            product.displayInfo();
            System.out.println("Duong dan hinh anh: " + product.getThumbnail());

            int orderQty = readPositiveInt(scanner, "Nhap so luong muon dat: ");
            if (product.checkAvailability(orderQty)) {
                double totalPrice = product.placeOrder(orderQty);
                System.out.println("Dat hang thanh cong.");
                System.out.println("Tong tien: " + totalPrice);
                System.out.println("So luong con lai: " + product.getQty());
            } else {
                System.out.println("Khong du so luong trong kho de dat hang.");
            }

            System.out.println();
            continueProgram = askToContinue(scanner);
            System.out.println();
        } while (continueProgram);

        System.out.println("Chuong trinh ket thuc.");

        scanner.close();
    }

    private static int readInt(Scanner scanner, String message) {
        while (true) {
            System.out.print(message);
            if (scanner.hasNextInt()) {
                int value = scanner.nextInt();
                scanner.nextLine();
                return value;
            }

            System.out.println("So nguyen khong hop le. Vui long thu lai.");
            scanner.nextLine();
        }
    }

    private static int readNonNegativeInt(Scanner scanner, String message) {
        while (true) {
            int value = readInt(scanner, message);
            if (value >= 0) {
                return value;
            }

            System.out.println("Gia tri khong duoc am.");
        }
    }

    private static int readPositiveInt(Scanner scanner, String message) {
        while (true) {
            int value = readInt(scanner, message);
            if (value > 0) {
                return value;
            }

            System.out.println("Gia tri phai lon hon 0.");
        }
    }

    private static double readNonNegativeDouble(Scanner scanner, String message) {
        while (true) {
            System.out.print(message);
            if (scanner.hasNextDouble()) {
                double value = scanner.nextDouble();
                scanner.nextLine();
                if (value >= 0) {
                    return value;
                }

                System.out.println("Gia tri khong duoc am.");
            } else {
                System.out.println("So khong hop le. Vui long thu lai.");
                scanner.nextLine();
            }
        }
    }

    private static String readNonEmptyString(Scanner scanner, String message) {
        while (true) {
            System.out.print(message);
            String value = scanner.nextLine().trim();
            if (!value.isEmpty()) {
                return value;
            }

            System.out.println("Gia tri khong duoc de trong.");
        }
    }

    private static boolean askToContinue(Scanner scanner) {
        while (true) {
            System.out.print("Ban co muon tiep tuc khong? (y/n): ");
            String answer = scanner.nextLine().trim().toLowerCase();
            if (answer.equals("y")) {
                return true;
            }
            if (answer.equals("n")) {
                return false;
            }

            System.out.println("Vui long nhap y hoac n.");
        }
    }
}

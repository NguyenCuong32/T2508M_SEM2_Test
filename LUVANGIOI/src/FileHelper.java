import java.io.*;
import java.util.*;

public class FileHelper {
    public static void saveProduct(Product p, String fileName) {
        try (FileWriter fw = new FileWriter(fileName, true);
             BufferedWriter bw = new BufferedWriter(fw)) {

            bw.write(p.toFileString());
            bw.newLine();

        } catch (IOException e) {
            System.out.println("Lỗi ghi file!");
        }
    }

    public static List<Product> readProducts(String fileName) {
        List<Product> list = new ArrayList<>();

        try (BufferedReader br = new BufferedReader(new FileReader(fileName))) {
            String line;

            while ((line = br.readLine()) != null) {
                list.add(Product.fromFileString(line));
            }

        } catch (IOException e) {
            System.out.println("Lỗi đọc file!");
        }

        return list;
    }
}
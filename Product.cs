using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AssignmentCS
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
    }

    public class ProductService
    {
        public static void DisplayProducts()
        {
            string filePath = "product.json";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Không tìm thấy file product.json!");
                return;
            }
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                List<Product>? products = JsonConvert.DeserializeObject<List<Product>>(jsonContent);

                if (products == null || products.Count == 0)
                {
                    Console.WriteLine("Danh sách sản phẩm trống hoặc file JSON không hợp lệ.");
                    return;
                }
                Console.WriteLine("\n=== GIẢ LẬP GIAO DIỆN HIỂN THỊ SẢN PHẨM ===");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine(string.Format("| {0,-5} | {1,-20} | {2,-12} |", "ID", "Tên Sản Phẩm", "Giá ($)"));
                Console.WriteLine("-------------------------------------------------");
                
                foreach (var prod in products)
                {
                    Console.WriteLine(string.Format("| {0,-5} | {1,-20} | {2,-12:F2} |", prod.Id, prod.ProductName ?? "", prod.Price));
                }
                Console.WriteLine("-------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi: {ex.Message}");
            }
        }
    }
}
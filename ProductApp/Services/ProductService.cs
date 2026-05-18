using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using ProductApp.Models;

namespace ProductApp.Services
{
    public class ProductService
    {
        private readonly string _filePath;

        public ProductService(string filePath)
        {
            _filePath = filePath;
        }

        public List<Product> LoadProducts()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException($"Không tìm thấy file: {_filePath}");

            string json = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Product>>(json, options)
                   ?? new List<Product>();
        }
    }
}

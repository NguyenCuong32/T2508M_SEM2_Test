using System.Text.Json;
using ProductApp.Models;

namespace ProductApp.Services
{
    class ProductService
    {
        private string GetJsonPath()
        {
            string projectRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
            return Path.Combine(projectRoot, "Data", "product.json");
        }

        public List<Product> GetProducts()
        {
            try
            {
                string jsonPath = GetJsonPath();
                string json = File.ReadAllText(jsonPath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var products = JsonSerializer.Deserialize<List<Product>>(json, options) ?? new List<Product>();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading products: {ex.Message}");
            }
        }

        public void AddProduct(Product product)
        {
            var products = GetProducts();
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            SaveProducts(products);
        }

        public void UpdateProduct(Product product)
        {
            var products = GetProducts();
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                SaveProducts(products);
            }
        }

        public void DeleteProduct(int id)
        {
            var products = GetProducts();
            products.RemoveAll(p => p.Id == id);
            SaveProducts(products);
        }

        private void SaveProducts(List<Product> products)
        {
            try
            {
                string jsonPath = GetJsonPath();
                var options = new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                };
                string json = JsonSerializer.Serialize(products, options);
                File.WriteAllText(jsonPath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving products: {ex.Message}");
            }
        }
    }
}

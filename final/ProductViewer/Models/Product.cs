using System.Text.Json.Serialization;

namespace ProductViewer.Models
{
    /// <summary>
    /// Represents a Product item from the product.json database.
    /// Includes helper properties to power a beautiful, high-fidelity UI.
    /// </summary>
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        // Presentation helpers for a premium feel
        public string FormattedPrice => $"${Price:N2}";
        
        public string Icon => ProductName.ToLower() switch
        {
            var name when name.Contains("iphone") => "📱",
            var name when name.Contains("macbook") => "💻",
            var name when name.Contains("ipod") => "🎧",
            _ => "📦"
        };

        public String Description => ProductName.ToLower() switch
        {
            var name when name.Contains("iphone") => "Experience the latest in mobile technology with stunning design and powerful performance.",
            var name when name.Contains("macbook") => "Unleash your creativity with a sleek laptop that combines power and portability.",
            var name when name.Contains("ipod") => "Enjoy your music on the go with a compact device that delivers rich sound quality.",
            _ => "Discover our premium electronics, crafted for those who demand excellence."
        };

        public string Category => ProductName.ToLower() switch
        {
            var name when name.Contains("iphone") => "Mobile Devices",
            var name when name.Contains("macbook") => "Laptops & Computers",
            var name when name.Contains("ipod") => "Portable Audio",
            _ => "Premium Electronics"
        };

        public string Brand => "Apple Inc.";
        
        public string RatingStars => "⭐⭐⭐⭐★ (4.8/5)";
    }
}

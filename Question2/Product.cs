using System.Text.Json.Serialization;

namespace Question2
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("productName")]
        public string? ProductName { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}

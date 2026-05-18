using System.Text.Json.Serialization;

namespace ProductViewerWinForms.Models;

public sealed class Product
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("productName")]
    public string ProductName { get; init; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; init; }
}


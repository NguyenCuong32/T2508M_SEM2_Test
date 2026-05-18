using System.Text.Json;
using ProductViewerWinForms.Models;

namespace ProductViewerWinForms.Data;

public sealed class ProductRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly string _filePath;

    public ProductRepository(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Cannot find product data file.", _filePath);
        }

        await using FileStream jsonStream = File.OpenRead(_filePath);
        List<Product>? products = await JsonSerializer.DeserializeAsync<List<Product>>(
            jsonStream,
            JsonOptions,
            cancellationToken);

        return products ?? new List<Product>();
    }
}


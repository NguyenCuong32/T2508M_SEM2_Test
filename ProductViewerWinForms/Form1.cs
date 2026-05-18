using System.Text.Json;
using ProductViewerWinForms.Data;
using ProductViewerWinForms.Models;

namespace ProductViewerWinForms;

public partial class Form1 : Form
{
    private readonly BindingSource _productBindingSource = new();
    private readonly ProductRepository _productRepository;

    public Form1()
    {
        InitializeComponent();
        productGrid.DataSource = _productBindingSource;
        _productRepository = new ProductRepository(Path.Combine(AppContext.BaseDirectory, "product.json"));
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadProductsAsync();
    }

    private async void reloadButton_Click(object? sender, EventArgs e)
    {
        await LoadProductsAsync();
    }

    private async Task LoadProductsAsync()
    {
        SetLoadingState(true, "Loading product.json...");

        try
        {
            List<Product> products = await _productRepository.GetAllAsync();
            _productBindingSource.DataSource = products;
            SetLoadingState(false, $"{products.Count} products loaded");
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
        {
            _productBindingSource.DataSource = null;
            SetLoadingState(false, ex.Message);
        }
    }

    private void SetLoadingState(bool isLoading, string message)
    {
        reloadButton.Enabled = !isLoading;
        statusLabel.Text = message;
    }
}


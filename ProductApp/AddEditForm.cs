using ProductApp.Models;
using ProductApp.Services;

namespace ProductApp;

public partial class AddEditForm : Form
{
    private Product? _product;
    private ProductService _productService;

    public AddEditForm(Product? product)
    {
        _product = product;
        _productService = new ProductService();
        InitializeComponent();

        if (product != null)
        {
            this.Text = "Edit Product";
            txtId.Text = product.Id.ToString();
            txtName.Text = product.ProductName ?? "";
            txtPrice.Text = product.Price.ToString();
        }
        else
        {
            this.Text = "Add New Product";
            txtId.Text = "Auto-generated";
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Product name is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Price must be a valid number", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_product == null)
            {
                // Add new product
                var newProduct = new Product
                {
                    ProductName = txtName.Text,
                    Price = price
                };
                _productService.AddProduct(newProduct);
            }
            else
            {
                // Update existing product
                _product.ProductName = txtName.Text;
                _product.Price = price;
                _productService.UpdateProduct(_product);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}

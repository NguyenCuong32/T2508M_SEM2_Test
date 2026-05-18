using ProductApp.Services;
using ProductApp.Models;

namespace ProductApp
{
    public partial class Form1 : Form
    {
        private ProductService _productService;
        private List<Product> _allProducts = new List<Product>();

        public Form1()
        {
            InitializeComponent();
            _productService = new ProductService();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                _allProducts = _productService.GetProducts();
                dgvProducts.DataSource = _allProducts;
                lblStatus.Text = $"Loaded {_allProducts.Count} products";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error loading products";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Open form to add new product
            AddEditForm form = new AddEditForm(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
                lblStatus.Text = "Product added successfully";
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to edit", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            if (selectedRow.DataBoundItem is Product selectedProduct)
            {
                AddEditForm form = new AddEditForm(selectedProduct);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                    lblStatus.Text = "Product updated successfully";
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var selectedRow = dgvProducts.SelectedRows[0];
                if (selectedRow.DataBoundItem is Product selectedProduct)
                {
                    _productService.DeleteProduct(selectedProduct.Id);
                    LoadProducts();
                    lblStatus.Text = "Product deleted successfully";
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadProducts();
            lblStatus.Text = "Refreshed";
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                dgvProducts.DataSource = _allProducts;
                lblStatus.Text = $"Showing {_allProducts.Count} products";
            }
            else
            {
                var filtered = _allProducts
                    .Where(p => p.ProductName?.ToLower().Contains(searchTerm) ?? false)
                    .ToList();
                dgvProducts.DataSource = filtered;
                lblStatus.Text = $"Found {filtered.Count} products";
            }
        }
    }
}
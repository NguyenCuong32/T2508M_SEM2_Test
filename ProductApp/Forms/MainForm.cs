using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using ProductApp.Models;
using ProductApp.Services;

namespace ProductApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly ProductService _productService;

        public MainForm()
        {
            InitializeComponent();

            // Tìm file product.json ở thư mục chạy ứng dụng
            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "product.json"
            );

            _productService = new ProductService(filePath);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                List<Product> products = _productService.LoadProducts();
                BindDataToGrid(products);
                lblStatus.Text = $"Đã tải {products.Count} sản phẩm thành công.";
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Lỗi: không tìm thấy file.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataToGrid(List<Product> products)
        {
            // Dùng BindingSource để bind data sạch
            var bindingSource = new BindingSource { DataSource = products };
            dataGridView.DataSource = bindingSource;

            // Đặt tiêu đề cột thân thiện
            dataGridView.Columns["Id"].HeaderText          = "ID";
            dataGridView.Columns["ProductName"].HeaderText = "Tên Sản Phẩm";
            dataGridView.Columns["Price"].HeaderText       = "Giá (USD)";
        }
    }
}

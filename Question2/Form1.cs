using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Question2;

public partial class Form1 : Form
{
    private DataGridView dataGridView1;

    public Form1()
    {
        InitializeComponent();
        InitializeUI();
        LoadData();
    }

    private void InitializeUI()
    {
        this.Text = "Thông tin sản phẩm";
        this.Size = new System.Drawing.Size(600, 400);

        dataGridView1 = new DataGridView();
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.ReadOnly = true;

        this.Controls.Add(dataGridView1);
    }

    private void LoadData()
    {
        try
        {
            string jsonString = File.ReadAllText("product.json");
            var products = JsonSerializer.Deserialize<List<Product>>(jsonString);
            
            if (products != null)
            {
                dataGridView1.DataSource = products;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi đọc file JSON: " + ex.Message);
        }
    }
}

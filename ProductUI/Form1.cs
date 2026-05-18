using System.Text.Json;

namespace ProductUI;

public partial class Form1 : Form
{
    private DataGridView dataGridView1;

    public Form1()
    {
        InitializeComponent();
        SetupUI();
        LoadData();
    }

    private void SetupUI()
    {
        this.Text = "Products";
        this.Size = new Size(400, 300);

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
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Product>? products = JsonSerializer.Deserialize<List<Product>>(jsonString, options);
            if (products != null)
            {
                dataGridView1.DataSource = products;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading data: " + ex.Message);
        }
    }
}

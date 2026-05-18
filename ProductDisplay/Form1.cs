using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace ProductDisplay;

public class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = "";
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        
        DataGridView grid = new DataGridView { Dock = DockStyle.Fill };
        this.Controls.Add(grid);

        try 
        {
            string json = File.ReadAllText("product.json");
            grid.DataSource = JsonSerializer.Deserialize<List<Product>>(json);
        }
        catch 
        {
            MessageBox.Show("Error reading file.");
        }
    }
}

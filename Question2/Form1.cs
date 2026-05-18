using Newtonsoft.Json;

namespace Question2;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        LoadProducts();
    }

    private void LoadProducts()
    {
        try
        {
            string path = Path.Combine(Application.StartupPath, "product.json");

            string json = File.ReadAllText(path);

            List<Product>? products =
                JsonConvert.DeserializeObject<List<Product>>(json);

            dataGridView1.DataSource = products;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
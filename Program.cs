using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace ProductViewer;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");
        Product[] products = Array.Empty<Product>();
        if (File.Exists(jsonPath))
        {
            var txt = File.ReadAllText(jsonPath);
            products = JsonSerializer.Deserialize<Product[]>(txt) ?? Array.Empty<Product>();
        }

        Application.Run(new MainForm(products));
    }
}

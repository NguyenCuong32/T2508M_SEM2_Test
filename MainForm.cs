using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProductViewer;

public partial class MainForm : Form
{
    private readonly List<Product> _products;

    public MainForm(IEnumerable<Product> products)
    {
        _products = new List<Product>(products);
        InitializeComponent();
        dataGridViewProducts.AutoGenerateColumns = true;
        dataGridViewProducts.DataSource = _products;
    }

    private void buttonRefresh_Click(object sender, EventArgs e)
    {
        dataGridViewProducts.Refresh();
    }
}

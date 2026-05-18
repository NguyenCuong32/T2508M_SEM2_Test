using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace ProductApp
{
    public class Product
    {
        public int id { get; set; }
        public string productName { get; set; }
        public double price { get; set; }
    }

    public class MainForm : Form
    {
        private Button btnLoad;
        private DataGridView dgv;

        public MainForm()
        {
            this.Text = "Product Management";
            this.Size = new System.Drawing.Size(550, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnLoad = new Button();
            btnLoad.Text = "Load Products";
            btnLoad.Location = new System.Drawing.Point(20, 20);
            btnLoad.Size = new System.Drawing.Size(120, 30);
            btnLoad.Click += BtnLoad_Click;

            dgv = new DataGridView();
            dgv.Location = new System.Drawing.Point(20, 70);
            dgv.Size = new System.Drawing.Size(490, 260);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.Controls.Add(btnLoad);
            this.Controls.Add(dgv);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            string path = "product.json";

            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string json = File.ReadAllText(path);
                List<Product> list = JsonSerializer.Deserialize<List<Product>>(json);
                
                dgv.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
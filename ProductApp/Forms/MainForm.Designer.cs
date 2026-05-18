using System;
using System.Windows.Forms;

namespace ProductApp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView;
        private Label lblTitle;
        private Button btnLoad;
        private Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle     = new Label();
            this.btnLoad      = new Button();
            this.dataGridView = new DataGridView();
            this.lblStatus    = new Label();

            // Form
            this.Text          = "Product Manager";
            this.Size          = new System.Drawing.Size(700, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            // lblTitle
            lblTitle.Text      = "📦 Danh Sách Sản Phẩm";
            lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            lblTitle.Location  = new System.Drawing.Point(20, 15);
            lblTitle.AutoSize  = true;

            // btnLoad
            btnLoad.Text      = "Tải dữ liệu";
            btnLoad.Location  = new System.Drawing.Point(560, 12);
            btnLoad.Size      = new System.Drawing.Size(100, 32);
            btnLoad.Click     += new EventHandler(btnLoad_Click);

            // dataGridView
            dataGridView.Location          = new System.Drawing.Point(20, 60);
            dataGridView.Size              = new System.Drawing.Size(640, 340);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ReadOnly          = true;
            dataGridView.AllowUserToAddRows = false;

            // lblStatus
            lblStatus.Location = new System.Drawing.Point(20, 415);
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = System.Drawing.Color.Gray;

            // Add controls
            this.Controls.AddRange(new Control[]
            {
                lblTitle, btnLoad, dataGridView, lblStatus
            });
        }
    }
}

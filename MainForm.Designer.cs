namespace ProductViewer;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.DataGridView dataGridViewProducts;
    private System.Windows.Forms.Button buttonRefresh;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
        this.buttonRefresh = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridViewProducts
        // 
        this.dataGridViewProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewProducts.Location = new System.Drawing.Point(12, 12);
        this.dataGridViewProducts.Name = "dataGridViewProducts";
        this.dataGridViewProducts.RowTemplate.Height = 25;
        this.dataGridViewProducts.Size = new System.Drawing.Size(560, 300);
        this.dataGridViewProducts.TabIndex = 0;
        // 
        // buttonRefresh
        // 
        this.buttonRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.buttonRefresh.Location = new System.Drawing.Point(249, 325);
        this.buttonRefresh.Name = "buttonRefresh";
        this.buttonRefresh.Size = new System.Drawing.Size(75, 30);
        this.buttonRefresh.TabIndex = 1;
        this.buttonRefresh.Text = "Refresh";
        this.buttonRefresh.UseVisualStyleBackColor = true;
        this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(584, 371);
        this.Controls.Add(this.buttonRefresh);
        this.Controls.Add(this.dataGridViewProducts);
        this.Name = "MainForm";
        this.Text = "Product Viewer";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
        this.ResumeLayout(false);
    }
}

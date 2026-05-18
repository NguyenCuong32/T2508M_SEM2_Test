namespace ProductViewerWinForms;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

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
        components = new System.ComponentModel.Container();
        rootLayout = new TableLayoutPanel();
        titleLabel = new Label();
        actionPanel = new FlowLayoutPanel();
        reloadButton = new Button();
        statusLabel = new Label();
        productGrid = new DataGridView();
        idColumn = new DataGridViewTextBoxColumn();
        productNameColumn = new DataGridViewTextBoxColumn();
        priceColumn = new DataGridViewTextBoxColumn();
        rootLayout.SuspendLayout();
        actionPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)productGrid).BeginInit();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(actionPanel, 0, 1);
        rootLayout.Controls.Add(productGrid, 0, 2);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(16);
        rootLayout.RowCount = 3;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Size = new Size(840, 520);
        rootLayout.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        titleLabel.Location = new Point(19, 16);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(802, 48);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Product List";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // actionPanel
        // 
        actionPanel.Controls.Add(reloadButton);
        actionPanel.Controls.Add(statusLabel);
        actionPanel.Dock = DockStyle.Fill;
        actionPanel.Location = new Point(19, 67);
        actionPanel.Name = "actionPanel";
        actionPanel.Size = new Size(802, 38);
        actionPanel.TabIndex = 1;
        // 
        // reloadButton
        // 
        reloadButton.AutoSize = true;
        reloadButton.Location = new Point(0, 3);
        reloadButton.Margin = new Padding(0, 3, 12, 3);
        reloadButton.Name = "reloadButton";
        reloadButton.Size = new Size(75, 32);
        reloadButton.TabIndex = 0;
        reloadButton.Text = "Reload";
        reloadButton.UseVisualStyleBackColor = true;
        reloadButton.Click += reloadButton_Click;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(90, 8);
        statusLabel.Margin = new Padding(3, 8, 3, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(44, 20);
        statusLabel.TabIndex = 1;
        statusLabel.Text = "Ready";
        // 
        // productGrid
        // 
        productGrid.AllowUserToAddRows = false;
        productGrid.AllowUserToDeleteRows = false;
        productGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        productGrid.BackgroundColor = SystemColors.Window;
        productGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        productGrid.Columns.AddRange(new DataGridViewColumn[] { idColumn, productNameColumn, priceColumn });
        productGrid.Dock = DockStyle.Fill;
        productGrid.Location = new Point(19, 111);
        productGrid.MultiSelect = false;
        productGrid.Name = "productGrid";
        productGrid.ReadOnly = true;
        productGrid.RowHeadersVisible = false;
        productGrid.RowHeadersWidth = 51;
        productGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        productGrid.Size = new Size(802, 390);
        productGrid.TabIndex = 2;
        // 
        // idColumn
        // 
        idColumn.DataPropertyName = "Id";
        idColumn.FillWeight = 30F;
        idColumn.HeaderText = "ID";
        idColumn.MinimumWidth = 64;
        idColumn.Name = "idColumn";
        idColumn.ReadOnly = true;
        // 
        // productNameColumn
        // 
        productNameColumn.DataPropertyName = "ProductName";
        productNameColumn.HeaderText = "Product Name";
        productNameColumn.MinimumWidth = 160;
        productNameColumn.Name = "productNameColumn";
        productNameColumn.ReadOnly = true;
        // 
        // priceColumn
        // 
        priceColumn.DataPropertyName = "Price";
        priceColumn.DefaultCellStyle.Format = "N2";
        priceColumn.FillWeight = 50F;
        priceColumn.HeaderText = "Price";
        priceColumn.MinimumWidth = 120;
        priceColumn.Name = "priceColumn";
        priceColumn.ReadOnly = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(840, 520);
        Controls.Add(rootLayout);
        MinimumSize = new Size(640, 420);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Product Viewer";
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        actionPanel.ResumeLayout(false);
        actionPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)productGrid).EndInit();
        ResumeLayout(false);
    }

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private FlowLayoutPanel actionPanel;
    private Button reloadButton;
    private Label statusLabel;
    private DataGridView productGrid;
    private DataGridViewTextBoxColumn idColumn;
    private DataGridViewTextBoxColumn productNameColumn;
    private DataGridViewTextBoxColumn priceColumn;
}


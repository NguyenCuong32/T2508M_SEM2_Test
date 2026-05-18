namespace ProductApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method by the code editor.
    /// </summary>
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnAdd;
    private System.Windows.Forms.ToolStripButton btnEdit;
    private System.Windows.Forms.ToolStripButton btnDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnRefresh;
    private System.Windows.Forms.Panel pnlSearch;
    private System.Windows.Forms.Label lblSearch;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.DataGridView dgvProducts;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        
        // ToolStrip
        toolStrip1 = new System.Windows.Forms.ToolStrip();
        btnAdd = new System.Windows.Forms.ToolStripButton();
        btnEdit = new System.Windows.Forms.ToolStripButton();
        btnDelete = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        btnRefresh = new System.Windows.Forms.ToolStripButton();
        
        // Search Panel
        pnlSearch = new System.Windows.Forms.Panel();
        lblSearch = new System.Windows.Forms.Label();
        txtSearch = new System.Windows.Forms.TextBox();
        btnSearch = new System.Windows.Forms.Button();
        
        // DataGridView
        dgvProducts = new System.Windows.Forms.DataGridView();
        
        // StatusStrip
        statusStrip1 = new System.Windows.Forms.StatusStrip();
        lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
        
        ((System.ComponentModel.ISupportInitialize)(dgvProducts)).BeginInit();
        toolStrip1.SuspendLayout();
        pnlSearch.SuspendLayout();
        statusStrip1.SuspendLayout();
        this.SuspendLayout();

        // toolStrip1
        toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnAdd, btnEdit, btnDelete, toolStripSeparator1, btnRefresh });
        toolStrip1.Location = new System.Drawing.Point(0, 0);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new System.Drawing.Size(1000, 25);
        toolStrip1.TabIndex = 0;

        // btnAdd
        btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        btnAdd.Text = "➕ Add";
        btnAdd.Click += BtnAdd_Click;

        // btnEdit
        btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        btnEdit.Text = "✏️ Edit";
        btnEdit.Click += BtnEdit_Click;

        // btnDelete
        btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        btnDelete.Text = "🗑️ Delete";
        btnDelete.Click += BtnDelete_Click;

        // btnRefresh
        btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        btnRefresh.Text = "🔄 Refresh";
        btnRefresh.Click += BtnRefresh_Click;

        // pnlSearch
        pnlSearch.Controls.Add(lblSearch);
        pnlSearch.Controls.Add(txtSearch);
        pnlSearch.Controls.Add(btnSearch);
        pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
        pnlSearch.Location = new System.Drawing.Point(0, 25);
        pnlSearch.Name = "pnlSearch";
        pnlSearch.Size = new System.Drawing.Size(1000, 50);
        pnlSearch.TabIndex = 1;
        pnlSearch.Padding = new System.Windows.Forms.Padding(10);

        // lblSearch
        lblSearch.AutoSize = true;
        lblSearch.Location = new System.Drawing.Point(10, 15);
        lblSearch.Name = "lblSearch";
        lblSearch.Text = "Search by name:";
        lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;

        // txtSearch
        txtSearch.Location = new System.Drawing.Point(120, 12);
        txtSearch.Name = "txtSearch";
        txtSearch.Size = new System.Drawing.Size(300, 23);
        txtSearch.TabIndex = 1;
        txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

        // btnSearch
        btnSearch.Location = new System.Drawing.Point(430, 12);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new System.Drawing.Size(75, 23);
        btnSearch.TabIndex = 2;
        btnSearch.Text = "Search";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += BtnSearch_Click;
        btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;

        // dgvProducts
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
        dgvProducts.Location = new System.Drawing.Point(0, 75);
        dgvProducts.Name = "dgvProducts";
        dgvProducts.ReadOnly = true;
        dgvProducts.RowHeadersWidth = 51;
        dgvProducts.Size = new System.Drawing.Size(1000, 400);
        dgvProducts.TabIndex = 2;
        dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.MultiSelect = false;

        // statusStrip1
        statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { lblStatus });
        statusStrip1.Location = new System.Drawing.Point(0, 455);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new System.Drawing.Size(1000, 25);
        statusStrip1.TabIndex = 3;

        // lblStatus
        lblStatus.Name = "lblStatus";
        lblStatus.Text = "Ready";

        // Form1
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1000, 480);
        Controls.Add(dgvProducts);
        Controls.Add(pnlSearch);
        Controls.Add(toolStrip1);
        Controls.Add(statusStrip1);
        Name = "Form1";
        Text = "Product Management System";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        ((System.ComponentModel.ISupportInitialize)(dgvProducts)).EndInit();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }



    #endregion
}

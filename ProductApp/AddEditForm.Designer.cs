namespace ProductApp;

partial class AddEditForm
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
        
        var lblId = new System.Windows.Forms.Label();
        var lblName = new System.Windows.Forms.Label();
        var lblPrice = new System.Windows.Forms.Label();
        var btnSave = new System.Windows.Forms.Button();
        var btnCancel = new System.Windows.Forms.Button();
        
        txtId = new System.Windows.Forms.TextBox();
        txtName = new System.Windows.Forms.TextBox();
        txtPrice = new System.Windows.Forms.TextBox();

        this.SuspendLayout();

        // lblId
        lblId.AutoSize = true;
        lblId.Location = new System.Drawing.Point(20, 20);
        lblId.Text = "ID:";

        // txtId
        txtId.Location = new System.Drawing.Point(100, 17);
        txtId.Name = "txtId";
        txtId.Size = new System.Drawing.Size(200, 23);
        txtId.ReadOnly = true;

        // lblName
        lblName.AutoSize = true;
        lblName.Location = new System.Drawing.Point(20, 60);
        lblName.Text = "Product Name:";

        // txtName
        txtName.Location = new System.Drawing.Point(100, 57);
        txtName.Name = "txtName";
        txtName.Size = new System.Drawing.Size(200, 23);

        // lblPrice
        lblPrice.AutoSize = true;
        lblPrice.Location = new System.Drawing.Point(20, 100);
        lblPrice.Text = "Price:";

        // txtPrice
        txtPrice.Location = new System.Drawing.Point(100, 97);
        txtPrice.Name = "txtPrice";
        txtPrice.Size = new System.Drawing.Size(200, 23);

        // btnSave
        btnSave.Location = new System.Drawing.Point(100, 140);
        btnSave.Text = "Save";
        btnSave.Size = new System.Drawing.Size(85, 25);
        btnSave.Click += BtnSave_Click;

        // btnCancel
        btnCancel.Location = new System.Drawing.Point(200, 140);
        btnCancel.Text = "Cancel";
        btnCancel.Size = new System.Drawing.Size(85, 25);
        btnCancel.Click += BtnCancel_Click;

        // AddEditForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(350, 200);
        this.Controls.Add(lblId);
        this.Controls.Add(txtId);
        this.Controls.Add(lblName);
        this.Controls.Add(txtName);
        this.Controls.Add(lblPrice);
        this.Controls.Add(txtPrice);
        this.Controls.Add(btnSave);
        this.Controls.Add(btnCancel);
        this.Name = "AddEditForm";
        this.Text = "Add/Edit Product";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    public System.Windows.Forms.TextBox txtId;
    public System.Windows.Forms.TextBox txtName;
    public System.Windows.Forms.TextBox txtPrice;
}

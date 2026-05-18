namespace Question2;

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
    ///  the contents of this method with the code editor.
    /// </summary>
    private DataGridView dataGridView1;

    private void InitializeComponent()
    {
        dataGridView1 = new DataGridView();

        SuspendLayout();

        dataGridView1.ColumnHeadersHeightSizeMode =
            DataGridViewColumnHeadersHeightSizeMode.AutoSize;

        dataGridView1.Dock = DockStyle.Fill;

        dataGridView1.Location = new Point(0, 0);

        dataGridView1.Name = "dataGridView1";

        dataGridView1.RowHeadersWidth = 51;

        dataGridView1.RowTemplate.Height = 29;

        dataGridView1.Size = new Size(800, 450);

        AutoScaleDimensions = new SizeF(8F, 20F);

        AutoScaleMode = AutoScaleMode.Font;

        ClientSize = new Size(800, 450);

        Controls.Add(dataGridView1);

        Name = "Form1";

        Text = "Product Management";

        ResumeLayout(false);
    }

    #endregion
}

namespace ToDo_App_M324.WinClient;

partial class MainForm
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
    private void InitializeComponent()
    {
        dataGridView1 = new DataGridView();
        btnAdd = new Button();
        btnDelete = new Button();
        btnDuplicate = new Button();
        btnExport = new Button();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.AllowUserToOrderColumns = true;
        dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new Point(12, 12);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersWidth = 62;
        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new Size(875, 363);
        dataGridView1.TabIndex = 0;
        dataGridView1.CellMouseDoubleClick += DataGridView1_CellMouseDoubleClick;
        // 
        // btnAdd
        // 
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Location = new Point(893, 12);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(207, 34);
        btnAdd.TabIndex = 1;
        btnAdd.Text = "Neues Todo erstellen";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += BtnAdd_Click;
        // 
        // btnDelete
        // 
        btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDelete.Location = new Point(893, 52);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(207, 34);
        btnDelete.TabIndex = 2;
        btnDelete.Text = "Todo löschen";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += BtnDelete_Click;
        // 
        // btnDuplicate
        // 
        btnDuplicate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDuplicate.Location = new Point(893, 92);
        btnDuplicate.Name = "btnDuplicate";
        btnDuplicate.Size = new Size(207, 34);
        btnDuplicate.TabIndex = 3;
        btnDuplicate.Text = "Todo duplizieren";
        btnDuplicate.UseVisualStyleBackColor = true;
        btnDuplicate.Click += BtnDuplicate_Click;
        // 
        // btnExport
        // 
        btnExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnExport.Location = new Point(893, 341);
        btnExport.Name = "btnExport";
        btnExport.Size = new Size(207, 34);
        btnExport.TabIndex = 4;
        btnExport.Text = "JSON Export";
        btnExport.UseVisualStyleBackColor = true;
        btnExport.Click += BtnExport_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1112, 387);
        Controls.Add(btnExport);
        Controls.Add(btnDuplicate);
        Controls.Add(btnDelete);
        Controls.Add(btnAdd);
        Controls.Add(dataGridView1);
        Name = "Form1";
        Text = "Form1";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DataGridView dataGridView1;
    private Button btnAdd;
    private Button btnDelete;
    private Button btnDuplicate;
    private Button btnExport;
}

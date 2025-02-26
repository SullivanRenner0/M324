namespace ToDo_App_M324.WinClient;

partial class TodoForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        txtHeader = new TextBox();
        txtDescription = new TextBox();
        cmbStatus = new ComboBox();
        cmbPriority = new ComboBox();
        dtpDeadline = new DateTimePicker();
        SuspendLayout();
        // 
        // txtHeader
        // 
        txtHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtHeader.Location = new Point(12, 12);
        txtHeader.Name = "txtHeader";
        txtHeader.Size = new Size(776, 31);
        txtHeader.TabIndex = 0;
        // 
        // txtDescription
        // 
        txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtDescription.Location = new Point(12, 69);
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.Size = new Size(485, 369);
        txtDescription.TabIndex = 1;
        // 
        // cmbStatus
        // 
        cmbStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.FormattingEnabled = true;
        cmbStatus.Location = new Point(512, 69);
        cmbStatus.Name = "cmbStatus";
        cmbStatus.Size = new Size(276, 33);
        cmbStatus.TabIndex = 2;
        // 
        // cmbPriority
        // 
        cmbPriority.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cmbPriority.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPriority.FormattingEnabled = true;
        cmbPriority.Location = new Point(512, 148);
        cmbPriority.Name = "cmbPriority";
        cmbPriority.Size = new Size(276, 33);
        cmbPriority.TabIndex = 3;
        // 
        // dtpDeadline
        // 
        dtpDeadline.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        dtpDeadline.CustomFormat = "dd.MM.yyyy HH:mm";
        dtpDeadline.Format = DateTimePickerFormat.Custom;
        dtpDeadline.Location = new Point(512, 227);
        dtpDeadline.Name = "dtpDeadline";
        dtpDeadline.ShowCheckBox = true;
        dtpDeadline.Size = new Size(276, 31);
        dtpDeadline.TabIndex = 4;
        // 
        // TodoForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(dtpDeadline);
        Controls.Add(cmbPriority);
        Controls.Add(cmbStatus);
        Controls.Add(txtDescription);
        Controls.Add(txtHeader);
        Name = "TodoForm";
        Text = "TodoForm";
        FormClosing += TodoForm_FormClosing;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox txtHeader;
    private TextBox txtDescription;
    private ComboBox cmbStatus;
    private ComboBox cmbPriority;
    private DateTimePicker dtpDeadline;
}
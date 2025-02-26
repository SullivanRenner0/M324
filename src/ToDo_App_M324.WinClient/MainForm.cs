using System.Diagnostics;
using ToDo_App_M324.Logic;

namespace ToDo_App_M324.WinClient;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        dataGridView1.DataBindings.Add(nameof(DataGridView.BackgroundColor), this, nameof(BackColor));
    }
    private void Form1_Load(object sender, EventArgs e)
    {
        LoadTodos();
    }

    private void LoadTodos()
    {
        dataGridView1.DataSource = null;
        Application.DoEvents();

        dataGridView1.SuspendLayout();

        var todos = TodoManager.LoadTodos();
        dataGridView1.DataSource = todos;

        dataGridView1.Columns[nameof(Todo.Id)].Visible = false;
        dataGridView1.Columns[nameof(Todo.CreatedAt)].Visible = false;
        dataGridView1.ResumeLayout();
    }

    private void DeleteTodo()
    {
        if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows[0].DataBoundItem is not Todo item)
        {
            return;
        }

        var answer = MessageBox.Show($"Soll das Todo: '{item.Header}' wirklich gelöscht werden?", "Todo löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (answer != DialogResult.Yes)
        {
            return;
        }

        TodoManager.RemoveTodo(item.Id);
        LoadTodos();
    }
    private void AddTodo()
    {
        var frm = new TodoForm();
        frm.ShowDialog();
        LoadTodos();
    }

    public void EditTodo()
    {
        if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows[0].DataBoundItem is not Todo item)
        {
            return;
        }

        var frm = new TodoForm();
        frm.LoadTodo(item.Id);
        frm.ShowDialog();
        LoadTodos();
    }
    private void DuplicateTodo()
    {
        if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows[0].DataBoundItem is not Todo item)
        {
            return;
        }

        var frm = new TodoForm();
        frm.LoadTodo(item.Id, true);
        frm.ShowDialog();
        LoadTodos();
    }

    private void ExportTodos()
    {
        var ofd = new SaveFileDialog
        {
            Filter = "Json Datei|*.json",
            Title = "Todos exportieren"
        };
        if (ofd.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        TodoManager.ExportTodos(ofd.FileName);
        Process.Start("explorer.exe", "/select, " + ofd.FileName);
    }


    private void BtnDelete_Click(object sender, EventArgs e)
    {
        DeleteTodo();
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        AddTodo();
    }

    private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        EditTodo();
    }

    private void BtnDuplicate_Click(object sender, EventArgs e)
    {
        DuplicateTodo();
    }

    private void BtnExport_Click(object sender, EventArgs e)
    {
        ExportTodos();
    }
}

using ToDo_App_M324.Logic;

namespace ToDo_App_M324.WinClient;
public partial class TodoForm : Form
{
    private const long NO_ID = -1;

    private long _id = NO_ID;
    public TodoForm()
    {
        InitializeComponent();

        cmbPriority.DataSource = Enum.GetNames<TodoPriority>().Select(n => n.Replace("_", " ")).ToArray();
        cmbStatus.DataSource = Enum.GetNames<TodoStatus>().Select(n => n.Replace("_", " ")).ToArray();
    }

    private void TodoForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        var text = _id == NO_ID
            ? "Soll das Todo erstellt werden?"
            : "Soll das Todo gespeichert werden?";

        var answer = MessageBox.Show(this, text, "Todo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (answer == DialogResult.Cancel)
        {
            e.Cancel = true;
            return;
        }
        if (answer != DialogResult.Yes)
        {
            return;
        }

        SaveTodo();
    }

    public void LoadTodo(long id, bool saveAsNew = false)
    {
        var todo = Program.TodoManager.GetTodo(id);
        _id = saveAsNew ? NO_ID : todo.Id;

        txtHeader.Text = todo.Header;
        txtDescription.Text = todo.Description;
        cmbPriority.SelectedItem = todo.Priority.ToString().Replace(" ", "_");
        cmbStatus.SelectedItem = todo.Status.ToString().Replace(" ", "_"); ;

        dtpDeadline.Checked = todo.Deadline.HasValue;
        if (todo.Deadline.HasValue)
            dtpDeadline.Value = todo.Deadline.Value;
        else
            dtpDeadline.Value = DateTime.Now;
    }

    private void SaveTodo()
    {
        Todo todo;
        if (_id == NO_ID)
            todo = new Todo();
        else
            todo = Program.TodoManager.GetTodo(_id);

        todo.Header = txtHeader.Text;
        todo.Description = txtDescription.Text;
        todo.Deadline = dtpDeadline.Checked ? dtpDeadline.Value : null;

        var priorityText = cmbPriority.SelectedItem!.ToString()!.Replace(" ", "_");
        var statusText = cmbStatus.SelectedItem!.ToString()!.Replace(" ", "_");
        todo.Priority = Enum.Parse<TodoPriority>(priorityText);
        todo.Status = Enum.Parse<TodoStatus>(statusText);


        if (_id == NO_ID)
            Program.TodoManager.AddTodo(todo);
        else
            Program.TodoManager.UpdateTodo(todo);
    }
}

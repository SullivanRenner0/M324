using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using ToDo_App_M324.Logic;

namespace ToDo_App_M324.Tests;

[TestFixture]
[NonParallelizable]
public class TodoManagerTests
{
    private string file = null!; // must be the same as in TodoManager
    private string dbDateFormat = null!; // must be the same as in TodoManager
    private JsonSerializerOptions options = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        var fileProp = typeof(TodoManager).GetField("file", BindingFlags.Static | BindingFlags.NonPublic)!;
        file = (string)fileProp.GetValue(null)!;

        var dbDateFormatProp = typeof(TodoManager).GetField("dbDateFormat", BindingFlags.Static | BindingFlags.NonPublic)!;
        dbDateFormat = (string)dbDateFormatProp.GetValue(null)!;

        // Trigger static constructor
        TodoManager.RemoveTodo(-1);
    }

    private void SetupDatasource(int count)
    {
        var todos = Enumerable.Range(0, count).Select(i => new Todo
        {
            Header = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Status = TodoStatus.Open,
            Priority = TodoPriority.Medium,
            Deadline = DateTime.Now
        }).ToArray();

        using var connection = new SQLiteConnection($"Data Source={file}");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "Delete FROM Todos";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO Todos (Header, Description, Status, Priority, Deadline, Created) VALUES (@Header, @Description, @Status, @Priority, @Deadline, @Created)";
        command.Connection = connection;
        command.Parameters.Add("@Header", DbType.String);
        command.Parameters.Add("@Description", DbType.String);
        command.Parameters.Add("@Status", DbType.String);
        command.Parameters.Add("@Priority", DbType.String);
        command.Parameters.Add("@Deadline", DbType.String);
        command.Parameters.Add("@Created", DbType.String);

        foreach (var todo in todos)
        {
            command.Parameters["@Header"].Value = todo.Header;
            command.Parameters["@Description"].Value = todo.Description;
            command.Parameters["@Status"].Value = todo.Status.ToString();
            command.Parameters["@Priority"].Value = todo.Priority.ToString();
            command.Parameters["@Deadline"].Value = todo.Deadline?.ToString(dbDateFormat) ?? null;
            command.Parameters["@Created"].Value = todo.CreatedAt.ToString(dbDateFormat);
            command.ExecuteNonQuery();
        }
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(99)]
    [TestCase(100)]
    [TestCase(101)]
    public void LoadTodos_Test(int count)
    {
        SetupDatasource(count);

        var loadedTodos = TodoManager.LoadTodos();

        Assert.That(loadedTodos, Has.Length.EqualTo(count));
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(5, 4)]
    public void RemoveTodos_Test(int before, int expectedCount)
    {
        SetupDatasource(before);

        var removed = TodoManager.RemoveTodo(1);

        var loadedTodos = TodoManager.LoadTodos();

        Assert.That(removed, Is.EqualTo(before != expectedCount)); // if before == expectedCount, then removed should be false
        Assert.That(loadedTodos, Has.Length.EqualTo(expectedCount));
    }

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(5, 6)]
    public void AddTodos_Test(int before, int expected)
    {
        SetupDatasource(before);

        var todo = new Todo
        {
            Header = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Status = TodoStatus.Open,
            Priority = TodoPriority.Medium,
            Deadline = DateTime.Now
        };

        var added = TodoManager.AddTodo(todo);

        var loadedTodos = TodoManager.LoadTodos();

        Assert.That(added, Is.True);
        Assert.That(loadedTodos, Has.Length.EqualTo(expected));
        Assert.That(loadedTodos.Last().Id, Is.Not.EqualTo(-1));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(99)]
    [TestCase(100)]
    [TestCase(101)]
    public void ExportTodos_Test(int count)
    {
        SetupDatasource(count);

        var jsonPath = "test.json";

        var exported = TodoManager.ExportTodos(jsonPath);

        TestDelegate jsonValidation = () =>
        {
            var reader = new Utf8JsonReader(File.ReadAllBytes(jsonPath));
            reader.Read();
            reader.Skip();
        };

        Assert.That(exported, Is.True);
        Assert.DoesNotThrow(jsonValidation, "Json is not valid");
    }
}
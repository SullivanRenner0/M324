using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using ToDo_App_M324.Logic;

namespace ToDo_App_M324.Tests;

[TestFixture]
[NonParallelizable]
public class TodoManagerTests
{
    private const string file = "todo_list.json"; // must be the same as in TodoManager
    private JsonSerializerOptions options = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        options.Converters.Add(new JsonStringEnumConverter());
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(999)]
    [TestCase(1000)]
    [TestCase(1001)]
    public void LoadTodos_Test(int count)
    {
        var todos = Enumerable.Range(0, count).Select(i => new Todo
        {
            Id = i,
            Header = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Status = TodoStatus.Open,
            Priority = TodoPriority.Medium,
            Deadline = DateTime.Now
        }).ToArray();
        var json = JsonSerializer.Serialize(todos, options);
        File.WriteAllText(file, json);

        var loadedTodos = TodoManager.LoadTodos();

        Assert.That(loadedTodos, Has.Length.EqualTo(count));
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(5, 4)]
    public void RemoveTodos_Test(int before, int expected)
    {
        var todos = Enumerable.Range(0, before).Select(i => new Todo
        {
            Id = i,
            Header = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Status = TodoStatus.Open,
            Priority = TodoPriority.Medium,
            Deadline = DateTime.Now
        }).ToArray();

        var json = JsonSerializer.Serialize(todos, options);
        File.WriteAllText(file, json);

        TodoManager.RemoveTodo(0);

        var loadedTodos = TodoManager.LoadTodos();

        Assert.That(loadedTodos, Has.Length.EqualTo(expected));
    }

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(5, 6)]
    public void AddTodos_Test(int before, int expected)
    {
        var todos = Enumerable.Range(0, before).Select(i => new Todo
        {
            Id = i,
            Header = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Status = TodoStatus.Open,
            Priority = TodoPriority.Medium,
            Deadline = DateTime.Now
        }).ToArray();

        var json = JsonSerializer.Serialize(todos, options);
        File.WriteAllText(file, json);

        var todo = new Todo
        {
            Id = -1,
            Header = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Status = TodoStatus.Open,
            Priority = TodoPriority.Medium,
            Deadline = DateTime.Now
        };
        TodoManager.AddTodo(todo);

        var loadedTodos = TodoManager.LoadTodos();

        Assert.That(loadedTodos, Has.Length.EqualTo(expected));
        Assert.That(loadedTodos.Last().Id, Is.Not.EqualTo(-1));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(999)]
    [TestCase(1000)]
    [TestCase(1001)]
    public void ExportTodos_Test(int count)
    {
        var todos = Enumerable.Range(0, count).Select(i => new Todo
        {
            Id = i,
            Header = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Status = TodoStatus.Open,
            Priority = TodoPriority.Medium,
            Deadline = DateTime.Now
        }).ToArray();

        var json = JsonSerializer.Serialize(todos, options);
        File.WriteAllText(file, json);

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
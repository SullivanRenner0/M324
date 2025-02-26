using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDo_App_M324.Logic;

public static class TodoManager
{
    private const string file = "todo_list.json";
    private static readonly JsonSerializerOptions options;
    static TodoManager()
    {
        options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        options.Converters.Add(new JsonStringEnumConverter());
    }

    public static Todo[] LoadTodos()
    {
        if (File.Exists(file) == false)
        {
            return [];
        }

        try
        {
            var json = File.ReadAllText(file);
            return JsonSerializer.Deserialize<Todo[]>(json, options) ?? [];
        }
        catch
        {
            return [];
        }
    }
    public static Todo GetTodo(long id)
    {
        return LoadTodos().Single(t => t.Id == id);
    }

    public static bool RemoveTodo(long id)
    {
        var todos = LoadTodos();
        var todo = todos.Where(t => t.Id != id).ToArray();
        return SaveTodos(todo);
    }

    public static void AddTodo(Todo todo)
    {
        var todos = LoadTodos();
        todo.Id = todos.Length == 0 ? 1 : todos.Max(t => t.Id) + 1;
        SaveTodos([.. todos, todo]);
    }

    public static void UpdateTodo(Todo todo)
    {
        var todos = LoadTodos();
        var index = Array.FindIndex(todos, t => t.Id == todo.Id);
        if (index < 0)
            return;

        todos[index] = todo;
        SaveTodos(todos);
    }

    private static bool SaveTodos(Todo[] todos)
    {
        try
        {
            var json = JsonSerializer.Serialize(todos, options);
            File.WriteAllText(file, json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool ExportTodos(string jsonPath)
    {
        var json = JsonSerializer.Serialize(LoadTodos(), options);

        var dir = new FileInfo(jsonPath).Directory?.FullName;
        if (string.IsNullOrWhiteSpace(dir) == false)
            Directory.CreateDirectory(dir);

        try
        {
            File.WriteAllText(jsonPath, json);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

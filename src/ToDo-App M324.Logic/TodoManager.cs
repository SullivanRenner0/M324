using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDo_App_M324.Logic;

/// <summary>
/// Verwaltet eine Liste von To-Do-Aufgaben.
/// </summary>
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

    /// <summary>
    /// Lädt alle gespeicherten To-Do-Aufgaben.
    /// </summary>
    /// <returns>Ein Array von To-Do-Aufgaben.</returns>
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

    /// <summary>
    /// Ruft eine spezifische To-Do-Aufgabe anhand der ID ab.
    /// </summary>
    /// <param name="id">Die ID der gesuchten Aufgabe.</param>
    /// <returns>Die gefundene To-Do-Aufgabe.</returns>
    public static Todo GetTodo(long id)
    {
        return LoadTodos().Single(t => t.Id == id);
    }

    /// <summary>
    /// Entfernt eine To-Do-Aufgabe anhand der ID.
    /// </summary>
    /// <param name="id">Die ID der zu löschenden Aufgabe.</param>
    /// <returns>Gibt <see langword="true"/> zurück, wenn die Aufgabe erfolgreich entfernt wurde.</returns>
    public static bool RemoveTodo(long id)
    {
        var todos = LoadTodos();
        var todo = todos.Where(t => t.Id != id).ToArray();
        return SaveTodos(todo);
    }

    /// <summary>
    /// Fügt eine neue To-Do-Aufgabe hinzu.
    /// </summary>
    /// <param name="todo">Die hinzuzufügende To-Do-Aufgabe.</param>
    public static void AddTodo(Todo todo)
    {
        var todos = LoadTodos();
        todo.Id = todos.Length == 0 ? 1 : todos.Max(t => t.Id) + 1;
        SaveTodos([.. todos, todo]);
    }

    /// <summary>
    /// Aktualisiert eine bestehende To-Do-Aufgabe.
    /// </summary>
    /// <param name="todo">Die zu aktualisierende To-Do-Aufgabe.</param>
    public static void UpdateTodo(Todo todo)
    {
        var todos = LoadTodos();
        var index = Array.FindIndex(todos, t => t.Id == todo.Id);
        if (index < 0)
            return;

        todos[index] = todo;
        SaveTodos(todos);
    }

    /// <summary>
    /// Speichert die To-Do-Liste in einer Datei.
    /// </summary>
    /// <param name="todos">Das zu speichernde To-Do-Array.</param>
    /// <returns>Gibt <see langword="true"/> zurück, wenn das Speichern erfolgreich war.</returns>
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

    /// <summary>
    /// Exportiert die To-Do-Liste als JSON-Datei an einen angegebenen Pfad.
    /// </summary>
    /// <param name="jsonPath">Der Speicherpfad der exportierten JSON-Datei.</param>
    /// <returns>Gibt <see langword="true"/> zurück, wenn der Export erfolgreich war.</returns>
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

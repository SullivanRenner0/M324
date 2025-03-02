using System.Data.SQLite;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDo_App_M324.Logic;

/// <summary>
/// Verwaltet eine Liste von To-Do-Aufgaben.
/// </summary>
public static class TodoManager
{
    private const string file = "todo_list.sqlite";
    private const string dbDateFormat = "yyyy-MM-dd HH:mm:ss";
    private static readonly JsonSerializerOptions options;

    static TodoManager()
    {
        CreateDatabase();

        options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        options.Converters.Add(new JsonStringEnumConverter());
    }

    /// <summary>
    /// Erstellt die SQLite-Datenbank, falls sie nicht existiert.
    /// </summary>
    private static void CreateDatabase()
    {
        var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Todos (Id INTEGER PRIMARY KEY, Header TEXT NOT NULL, Description TEXT NOT NULL, Status TEXT NOT NULL, Priority TEXT NOT NULL, Deadline TEXT, Created TEXT NOT NULL)");
        ExecuteNonQuery(command);
    }


    /// <summary>
    /// Erstellt eine neue SQLite-Verbindung.
    /// </summary>
    /// <returns></returns>
    private static SQLiteConnection CreateConnection()
    {
        var connection = new SQLiteConnection($"Data Source={file}");
        connection.Open();
        return connection;
    }

    /// <summary>
    /// Gibt an, dass eine SQL-Abfrage fehlgeschlagen ist.
    /// </summary>
    private const int QUERY_FAILED = -1;

    /// <summary>
    /// Führt eine SQL-Abfrage aus, die die Anzahl betroffenen Zeilen zückgibt.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    private static int ExecuteNonQuery(SQLiteCommand command)
    {
        try
        {
            using var connection = CreateConnection();
            command.Connection = connection;
            return command.ExecuteNonQuery();
        }
        catch
        {
            return QUERY_FAILED;
        }
    }

    private static Todo LoadTodo(SQLiteDataReader reader)
    {
        return new Todo
        {
            Id = reader.GetInt64(0),
            Header = reader.GetString(1),
            Description = reader.GetString(2),
            Status = Enum.Parse<TodoStatus>(reader.GetString(3)),
            Priority = Enum.Parse<TodoPriority>(reader.GetString(4)),
            Deadline = reader.IsDBNull(5) ? null : DateTime.ParseExact(reader.GetString(5), dbDateFormat, CultureInfo.InvariantCulture),
            CreatedAt = DateTime.ParseExact(reader.GetString(6), dbDateFormat, CultureInfo.InvariantCulture),
        };
    }

    /// <summary>
    /// Lädt alle gespeicherten To-Do-Aufgaben.
    /// </summary>
    /// <returns>Ein Array von To-Do-Aufgaben.</returns>
    public static Todo[] LoadTodos()
    {
        try
        {
            using var connection = CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Header, Description, Status, Priority, Deadline, Created FROM Todos";
            using var reader = command.ExecuteReader();
            var todos = new List<Todo>();
            while (reader.Read())
            {
                var todo = LoadTodo(reader);
                todos.Add(todo);
            }
            return [.. todos];
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
    /// <exception cref="Exception"></exception>
    public static Todo GetTodo(long id)
    {
        using var connection = CreateConnection();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Header, Description, Status, Priority, Deadline, Created FROM Todos WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        using var reader = command.ExecuteReader();

        return reader.Read()
            ? LoadTodo(reader)
            : throw new Exception($"Todo mit ID {id} nicht gefunden");
    }

    /// <summary>
    /// Entfernt eine To-Do-Aufgabe anhand der ID.
    /// </summary>
    /// <param name="id">Die ID der zu löschenden Aufgabe.</param>
    /// <returns>Gibt <see langword="true"/> zurück, wenn die Aufgabe erfolgreich entfernt wurde.</returns>
    public static bool RemoveTodo(long id)
    {
        var command = new SQLiteCommand("DELETE FROM Todos WHERE Id = @Id");
        command.Parameters.AddWithValue("@Id", id);
        return ExecuteNonQuery(command) > 0;
    }

    /// <summary>
    /// Fügt eine neue To-Do-Aufgabe hinzu.
    /// </summary>
    /// <param name="todo">Die hinzuzufügende To-Do-Aufgabe.</param>
    public static bool AddTodo(Todo todo)
    {
        var command = new SQLiteCommand("INSERT INTO Todos (Header, Description, Status, Priority, Deadline, Created) VALUES (@Header, @Description, @Status, @Priority, @Deadline, @Created)");
        command.Parameters.AddWithValue("@Header", todo.Header);
        command.Parameters.AddWithValue("@Description", todo.Description);
        command.Parameters.AddWithValue("@Status", todo.Status.ToString());
        command.Parameters.AddWithValue("@Priority", todo.Priority.ToString());
        command.Parameters.AddWithValue("@Deadline", todo.Deadline?.ToString(dbDateFormat) ?? null);
        command.Parameters.AddWithValue("@Created", todo.CreatedAt.ToString(dbDateFormat));

        return ExecuteNonQuery(command) > 0;
    }

    /// <summary>
    /// Aktualisiert eine bestehende To-Do-Aufgabe.
    /// </summary>
    /// <param name="todo">Die zu aktualisierende To-Do-Aufgabe.</param>
    public static bool UpdateTodo(Todo todo)
    {
        var command = new SQLiteCommand("UPDATE Todos SET Header = @Header, Description = @Description, Status = @Status, Priority = @Priority, Deadline = @Deadline WHERE Id = @Id");
        command.Parameters.AddWithValue("@Id", todo.Id);
        command.Parameters.AddWithValue("@Header", todo.Header);
        command.Parameters.AddWithValue("@Description", todo.Description);
        command.Parameters.AddWithValue("@Status", todo.Status.ToString());
        command.Parameters.AddWithValue("@Priority", todo.Priority.ToString());
        command.Parameters.AddWithValue("@Deadline", todo.Deadline?.ToString(dbDateFormat) ?? "");
        return ExecuteNonQuery(command) > 0;
    }

    /// <summary>
    /// Exportiert die To-Do-Liste als JSON-Datei an einen angegebenen Pfad.
    /// </summary>
    /// <param name="jsonPath">Der Speicherpfad der exportierten JSON-Datei.</param>
    /// <returns>Gibt <see langword="true"/> zurück, wenn der Export erfolgreich war.</returns>
    public static bool ExportTodos(string jsonPath)
    {
        var todos = LoadTodos();
        var json = JsonSerializer.Serialize(todos, options);

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

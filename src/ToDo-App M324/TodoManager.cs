using System.Text.Json;

namespace ToDo_App_M324;

public class TodoManager(string file)
{
    private List<string> _tasks = [];
    public string[] Tasks => _tasks.ToArray(); // copy

    public void LoadTasks()
    {
        if (File.Exists(file) == false)
        {
            _tasks = [];
            return;
        }

        try
        {
            _tasks = File.ReadAllLines(file).ToList();
        }
        catch
        {
            _tasks = [];
        }
    }
    public bool RemoveTask(int index)
    {
        if (_tasks.Count > index && index >= 0)
        {
            _tasks.RemoveAt(index);
            return true;
        }
        return false;
    }
    public void AddTask(string task)
    {
        _tasks.Add(task);
    }
    public bool SaveTasks()
    {
        try
        {
            File.WriteAllLines(file, _tasks);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool ExportTasks(string jsonPath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(_tasks, options);

        var dir = new FileInfo(jsonPath).Directory?.FullName;
        if (dir != null)
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

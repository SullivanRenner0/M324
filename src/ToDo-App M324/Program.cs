using System.Text.Json;

class Program
{
    const string FILE_PATH = "todo_list.csv";
    static List<string> tasks = [];

    static void Main()
    {
        LoadTasks();

        while (true)
        {
            Console.WriteLine("\nToDo-Liste: ");
            Console.WriteLine("1. Aufgabe hinzufügen");
            Console.WriteLine("2. Aufgabe entfernen");
            Console.WriteLine("3. Aufgaben anzeigen");
            Console.WriteLine("4. Aufgaben speichern");
            Console.WriteLine("5. Aufgaben exportieren (JSON)");
            Console.WriteLine("6. Beenden");
            Console.Write("Auswahl: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    RemoveTask();
                    break;
                case "3":
                    ShowTasks();
                    break;
                case "4":
                    SaveTasks();
                    Console.WriteLine("Aufgaben gespeichert!");
                    break;
                case "5":
                    ExportTasks();
                    break;
                case "6":
                    SaveTasks();
                    return;
                default:
                    Console.WriteLine("Ungültige Auswahl!");
                    break;
            }
        }
    }

    static void LoadTasks()
    {
        if (File.Exists(FILE_PATH))
        {
            tasks = new List<string>(File.ReadAllLines(FILE_PATH));
        }
    }

    static void SaveTasks()
    {
        File.WriteAllLines(FILE_PATH, tasks);
    }

    static void AddTask()
    {
        Console.Write("Neue Aufgabe: ");
        var task = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(task))
        {
            tasks.Add(task);
            Console.WriteLine("Aufgabe hinzugefügt!");
        }
    }

    static void RemoveTask()
    {
        ShowTasks();
        Console.Write("Nummer der zu löschenden Aufgabe: ");
        if (int.TryParse(Console.ReadLine(), out var index) && index > 0 && index <= tasks.Count)
        {
            tasks.RemoveAt(index - 1);
            Console.WriteLine("Aufgabe entfernt!");
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe!");
        }
    }

    static void ExportTasks()
    {
        Console.Write("Dateiname (JSON): ");
        var fileName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(fileName))
        {
            if (!fileName.EndsWith(".json"))
            {
                fileName += ".json";
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(tasks, options);

            File.WriteAllText(fileName, json);
            Console.WriteLine("Aufgaben exportiert!");
        }
    }

    static void ShowTasks()
    {
        Console.WriteLine("\nAktuelle Aufgaben:");
        if (tasks.Count == 0)
        {
            Console.WriteLine("Keine Aufgaben vorhanden.");
        }
        else
        {
            for (var i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
        }
    }
}

using System.Text.Json;

namespace ToDo_App_M324;

class Program
{
    private static readonly TodoManager _manager = new("todo_list.csv");

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
        _manager.LoadTasks();
    }

    static void SaveTasks()
    {
        if (_manager.SaveTasks())
            Console.WriteLine("Aufgaben gespeichert!");
        else
            Console.WriteLine("Beim speichern der Aufgaben ist ein Fehler aufgetreten!");
    }

    static void AddTask()
    {
        Console.Write("Neue Aufgabe: ");
        var task = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(task))
        {
            _manager.AddTask(task);
            Console.WriteLine("Aufgabe hinzugefügt!");
        }
    }

    static void RemoveTask()
    {
        ShowTasks();
        Console.Write("Nummer der zu löschenden Aufgabe: ");

        if (int.TryParse(Console.ReadLine(), out var nr) && _manager.RemoveTask(nr - 1))
        {
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
                fileName += ".json";

            if (_manager.ExportTasks(fileName))
                Console.WriteLine("Aufgaben exportiert!");
            else
                Console.WriteLine("Beim exportieren ist ein Fehler aufgetreten!");
        }
    }

    static void ShowTasks()
    {
        Console.WriteLine("\nAktuelle Aufgaben:");
        if (_manager.Tasks.Length == 0)
            Console.WriteLine("Keine Aufgaben vorhanden.");
        else
        {
            for (var i = 0; i < _manager.Tasks.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {_manager.Tasks[i]}");
            }
        }
    }
}
namespace ToDo_App_M324.Logic;

/// <summary>
/// Repräsentiert eine To-Do-Aufgabe.
/// </summary>
public class Todo
{
    /// <summary>
    /// Eindeutige Identifikationsnummer der To-Do-Aufgabe.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Überschrift der To-Do-Aufgabe.
    /// </summary>
    public string Header { get; set; } = "";

    /// <summary>
    /// Beschreibung der To-Do-Aufgabe.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Status der To-Do-Aufgabe.
    /// </summary>
    public TodoStatus Status { get; set; }

    /// <summary>
    /// Priorität der To-Do-Aufgabe.
    /// </summary>
    public TodoPriority Priority { get; set; }

    /// <summary>
    /// Erstellungsdatum der To-Do-Aufgabe.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Fälligkeitsdatum der To-Do-Aufgabe (optional).
    /// </summary>
    public DateTime? Deadline { get; set; }
}

using ToDo_App_M324.Logic;

namespace ToDo_App_M324.WinClient;

internal static class Program
{
    public static readonly TodoManager TodoManager = new("todos.db");

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
using System.Text.Json;

namespace ToDo_App_M324.Tests;

[TestClass()]
public class TodoManagerTests
{
    private string myTempFolder = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        myTempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(myTempFolder);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        try
        {
            Directory.Delete(myTempFolder, true);
        }
        catch
        {
            foreach (var subDir in Directory.EnumerateDirectories(myTempFolder))
            {
                try
                {
                    Directory.Delete(subDir, true);
                }
                catch
                {

                }
            }

            foreach (var file in Directory.EnumerateFiles(myTempFolder))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {

                }
            }
        }
    }


    private TodoManager CreateSUT(int entriesCount, bool load, out string filepath)
    {
        var folder = Path.Combine(myTempFolder, Guid.NewGuid().ToString());
        Directory.CreateDirectory(folder);
        filepath = Path.Combine(folder, "items.csv");

        var lines = Enumerable.Range(0, entriesCount).Select(_ => Guid.NewGuid().ToString());
        File.WriteAllLines(filepath, lines);

        var manager = new TodoManager(filepath);
        if (load)
            manager.LoadTasks();

        return manager;
    }


    [TestMethod()]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(999)]
    [DataRow(1000)]
    [DataRow(1001)]
    public void LoadTasks_Test(int count)
    {
        var sut = CreateSUT(count, false, out var _);

        sut.LoadTasks();

        Assert.AreEqual(count, sut.Tasks.Length);
    }

    [TestMethod()]
    [DataRow(0, 0)]
    [DataRow(1, 0)]
    [DataRow(5, 4)]
    public void RemoveTask_Test(int before, int expected)
    {
        var sut = CreateSUT(before, true, out var _);

        sut.RemoveTask(0);

        Assert.AreEqual(expected, sut.Tasks.Length);

    }

    [TestMethod()]
    [DataRow(0, 1)]
    [DataRow(1, 2)]
    [DataRow(5, 6)]
    public void AddTask_Test(int before, int expected)
    {
        var sut = CreateSUT(before, true, out var _);

        sut.AddTask(Guid.NewGuid().ToString());

        Assert.AreEqual(expected, sut.Tasks.Length);
    }

    [TestMethod()]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(999)]
    [DataRow(1000)]
    [DataRow(1001)]
    public void SaveTasks_Test(int count)
    {
        var sut = CreateSUT(count, true, out var filePath);

        File.Delete(filePath);
        Assert.AreEqual(false, File.Exists(filePath)); // Manager is not allowed to lock file

        var saved = sut.SaveTasks();
        var lines = File.ReadAllLines(filePath);

        Assert.AreEqual(true, saved);
        Assert.AreEqual(count, lines.Length);
    }

    [TestMethod()]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(999)]
    [DataRow(1000)]
    [DataRow(1001)]
    public void ExportTasks_Test(int count)
    {
        var sut = CreateSUT(count, true, out var filePath);

        var dir = new FileInfo(filePath).Directory!.FullName;
        var jsonPath = Path.Combine(dir, "test.json");

        var exported = sut.ExportTasks(jsonPath);
        var json = File.ReadAllText(jsonPath);
        using var jsonObj = JsonDocument.Parse(json);

        Assert.AreEqual(true, exported);
        Assert.AreEqual(true, jsonObj != null);
    }
}
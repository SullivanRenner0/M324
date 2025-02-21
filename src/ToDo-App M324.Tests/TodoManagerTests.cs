using System.Text.Json;
using NUnit.Framework;

namespace ToDo_App_M324.Tests;

[TestFixture]
public class TodoManagerTests
{
    private string myTempFolder = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        myTempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(myTempFolder);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
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


    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(999)]
    [TestCase(1000)]
    [TestCase(1001)]
    public void LoadTasks_Test(int count)
    {
        var sut = CreateSUT(count, false, out var _);

        sut.LoadTasks();

        Assert.That(sut.Tasks, Has.Length.EqualTo(count));
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(5, 4)]
    public void RemoveTask_Test(int before, int expected)
    {
        var sut = CreateSUT(before, true, out var _);

        sut.RemoveTask(0);

        Assert.That(sut.Tasks, Has.Length.EqualTo(expected));
    }

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(5, 6)]
    public void AddTask_Test(int before, int expected)
    {
        var sut = CreateSUT(before, true, out var _);

        sut.AddTask(Guid.NewGuid().ToString());

        Assert.That(sut.Tasks, Has.Length.EqualTo(expected));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(999)]
    [TestCase(1000)]
    [TestCase(1001)]
    public void SaveTasks_Test(int count)
    {
        var sut = CreateSUT(count, true, out var filePath);

        File.Delete(filePath);
        Assert.That(File.Exists(filePath), Is.False); // Manager is not allowed to lock file

        var saved = sut.SaveTasks();
        var lines = File.ReadAllLines(filePath);

        Assert.That(saved, Is.True);
        Assert.That(lines, Has.Length.EqualTo(count));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(999)]
    [TestCase(1000)]
    [TestCase(1001)]
    public void ExportTasks_Test(int count)
    {
        var sut = CreateSUT(count, true, out var filePath);

        var dir = new FileInfo(filePath).Directory!.FullName;
        var jsonPath = Path.Combine(dir, "test.json");

        var exported = sut.ExportTasks(jsonPath);

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
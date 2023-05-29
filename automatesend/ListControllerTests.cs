using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using automatesend;
using Microsoft.VisualStudio.TestTools.UnitTesting;


[TestClass]
public class ListControllerTests
{
    private const string TestFilePath = "C:\\Users\\mahna\\OneDrive\\Skrivebord\\OutputData.csv";
    private ListController listCont = new ListController();

    private const string TestTemplateFilePath = "template.odt";
    private const string TestDesktopPath = "C:\\Users\\Username\\Desktop"; // Replace with your actual desktop path

    List<Person> people = new List<Person>();

    Person person1 = new Person("John", "mahnaaz2021@gmail.com");
    Person person2 = new Person("Emily", "minexemple@gmail.com");

   

    [TestInitialize]
    public void TestInitialize()
    {
        people.Add(person1);
        people.Add(person2);

        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

    [TestCleanup]
    public void TestCleanup()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

    [TestMethod]
    public void WriteCSV_ShouldWriteDataToFile()
    {        

        // Act
        listCont.WriteCSV(people, TestFilePath);

        // Assert
        Assert.IsTrue(File.Exists(TestFilePath));

        var lines = File.ReadAllLines(TestFilePath);
        Assert.AreEqual(3, lines.Length); // 2 data rows + 1 header row

        Assert.AreEqual("Name,Email", lines[0]);
        Assert.AreEqual("John Doe,john@example.com", lines[1]);
        Assert.AreEqual("Jane Smith,jane@example.com", lines[2]);
    }
    [TestMethod]
    public void ReadCSV_ShouldReadDataFromFile()
    {
        // Arrange
        var csvContent = "Name,Email\nJohn Doe,john@example.com\nJane Smith,jane@example.com";
        File.WriteAllText(TestFilePath, csvContent);

        // Act
        List<Person> people = listCont.ReadCSV(TestFilePath);

        // Assert
        Assert.IsNotNull(people);
        Assert.AreEqual(2, people.Count);

        Assert.AreEqual("John Doe", people[0].Name);
        Assert.AreEqual("", people[0].Email);

        Assert.AreEqual("Jane Smith", people[1].Name);
        Assert.AreEqual("", people[1].Email);
    }
    [TestMethod]
    public void GenerateMergeFiles_ShouldGenerateFilesWithMergedContent()
    {

        // Act
        listCont.GenerateMergeFiles(TestTemplateFilePath, people);

        // Assert
        foreach (Person person in people)
        {
            string mergeFileName = $"{person.Name}_merge_file.odt";
            string mergeFilePath = Path.Combine(TestDesktopPath, "MergeFiles", mergeFileName);

            Assert.IsTrue(File.Exists(mergeFilePath));

            string fileContent = File.ReadAllText(mergeFilePath);
            string expectedContent = $"Dear {person.Name},\n\nWe are pleased to inform you that your email address has been added to our mailing list.\n\nThank you for your interest!\n\nBest regards,\nYour Company";

            Assert.AreEqual(expectedContent, fileContent);
        }
    }
}

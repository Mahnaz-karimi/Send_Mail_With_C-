using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;


namespace automatesend
{
    internal class ListController
    {
       

        public void PerformMergeFromCSV()
        {

            string templatePath = "C:\\Users\\mahna\\OneDrive\\Skrivebord\\mergeFile.odt";
            string outputPath = "C:\\Users\\mahna\\OneDrive\\Skrivebord\\OutputDocument.odt";
            string csvFilePath = "C:\\Users\\mahna\\OneDrive\\Skrivebord\\OutputData.csv";


            string libreOfficePath = "C:\\Program Files\\LibreOffice\\program\\soffice.bin"; // Update this path to the actual installation directory of LibreOffice

            // Generate the CSV file from the list of Person objects
            CreateCSVFromPersonList(getListOfPersons(), csvFilePath);

            // Prepare the command to perform the merge
            string command = $"\"{libreOfficePath}\" --headless --convert-to odt:\"writer8\" --outdir \"{outputPath}\" \"{templatePath}\" \"{csvFilePath}\"";

            // Start the process
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/c {command}";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.WaitForExit();
            }


        }
     

        public string randomText()
        {
            List<string> stringList = new List<string>()
        {
            "prodoct 1",
            "prodoct 2",
            "prodoct 3",
            "prodoct 4",
            "prodoct 5"
        };

            Random random = new Random();
            int randomIndex = random.Next(0, stringList.Count);
            string randomString = stringList[randomIndex];
            return randomString;
        }

        public List<Person> getListOfPersons()
        {
            List<Person> people = new List<Person>();

            Person person1 = new Person("John", "mahnaaz2021@gmail.com");
            Person person2 = new Person("Emily", "minexemple@gmail.com");


            people.Add(person1);
            people.Add(person2);

            return people;
        }


        public void CreateCSVFromPersonList<T>(List<T> dataList, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // Get the properties of the class using reflection
            PropertyInfo[] properties = typeof(T).GetProperties();

            // Write the header row with property names
            foreach (PropertyInfo property in properties)
            {
                sb.Append(property.Name);
                sb.Append(",");
            }
            sb.AppendLine();

            // Write the data rows
            foreach (T item in dataList)
            {
                foreach (PropertyInfo property in properties)
                {
                    // Get the value of each property dynamically using reflection
                    object value = property.GetValue(item);

                    // Enclose the value in double quotes to handle values with commas or special characters
                    sb.Append("\"" + value?.ToString().Replace("\"", "\"\"") + "\"");
                    sb.Append(",");
                }
                sb.AppendLine();
            }

            // Write the CSV content to a file
            File.WriteAllText(filePath, sb.ToString());
        }
        public void ReadDatafromCSV()
        {

            // Specify the path to your CSV file
            string csvFilePath = "C:\\Users\\mahna\\OneDrive\\Skrivebord\\OutputData.csv";

            // Create an instance of TextFieldParser
            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                // Set the delimiter (e.g., comma)
                parser.Delimiters = new string[] { "," };

                // Set whether the fields can be enclosed in quotes
                parser.HasFieldsEnclosedInQuotes = true;

                // Read the header line (optional)
                if (!parser.EndOfData)
                {
                    string[] headers = parser.ReadFields();
                    // Process the headers if needed
                }

                // Read the remaining data lines
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    // Process the fields of each data line
                    foreach (string field in fields)
                    {
                        // Access each field value and perform desired operations
                        Console.WriteLine("HELOO" + field);
                    }
                }
            }
        }
        public void WriteCSV( List<Person> people, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Name,Email"); // Write the header row

                foreach (Person person in people)
                {
                    writer.WriteLine($"{person.Name},{person.Email}"); // Write each person's data
                }
            }
        }
        public List<Person> ReadCSV(string filePath)
        {
            List<Person> people = new List<Person>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Skip the header row
                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (fields.Length >= 2)
                    {
                        string name = fields[0];

                        Person person = new Person(name, "");
                        people.Add(person);
                    }
                }
            }

            return people;
        }

        public void GenerateMergeFiles(string templateFilePath, List<Person> people)
        {
            string mergeContent = @"Dear <<Name>>,

We are pleased to inform you that your email address has been added to our mailing list.

Thank you for your interest!

Best regards,
Your Company";

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            foreach (Person person in people)
            {
                string mergedContent = mergeContent.Replace("<<Name>>", person.Name);
                string mergeFileName = $"{person.Name}_merge_file.odt";
                string mergeFilePath = Path.Combine(desktopPath, "MergeFiles", mergeFileName);

                // Copy the template file to the merge file
                //File.Copy(templateFilePath, mergeFilePath, true);

                // Modify the content of the merge file
                using (StreamWriter writer = new StreamWriter(mergeFilePath, true))
                {
                    writer.WriteLine();
                    writer.WriteLine(mergedContent);
                }
            }
        }


    }



}

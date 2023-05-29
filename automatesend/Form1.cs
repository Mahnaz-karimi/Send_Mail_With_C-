using System;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
using System.Timers;
using System.Collections.Generic;
using System.Collections;

namespace automatesend
{
    public partial class Form1 : Form
    {
        public static System.Timers.Timer newTimer = new System.Timers.Timer();
        ListController listCont = new ListController();

        public Form1()
        {
            InitializeComponent();
        }

        public JObject getData()
        {
            // Specify the path to the configuration file 
            string filePath = "C:/json/config.json";

            // Read the contents of the file
            string jsonContent = System.IO.File.ReadAllText(filePath);

            // Convert content to object JObject
            JObject data = JObject.Parse(jsonContent);

            return data;

        }

        public void btnSendMail_Click(object sender, EventArgs e)
        {

            JObject jsonData = getData();

            // Extract values
            string DB_HOST = (string)jsonData["DB_HOST"];

            List<Person> people = listCont.getListOfPersons();

            try
            {
                using (MailMessage mail = new MailMessage())
                {

                    mail.From = new MailAddress((string)jsonData["EMAIL_HOST_USER"]); // from mail
                    Console.WriteLine("List:");
                    for (int i = 0; i < people.Count; i++)
                    {

                        mail.To.Add((people[i].Email));
                        mail.Subject = "subject";
                        mail.Body = "<h1> Hej " + people[i].Name + "</h1>" + "<h2>" + "<br/>This is a " + listCont.randomText() + "</h2>";
                        mail.IsBodyHtml = true;


                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new System.Net.NetworkCredential((string)jsonData["EMAIL_HOST_USER"], (string)jsonData["EMAIL_HOST_PASSWORD"]);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                            label1.Text = "Email send";

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }


        }

        public void Form1_Load(object sender, EventArgs e)
        {
            
            newTimer.Interval = 100000; // starter eventes
            newTimer.Elapsed += Form1_Load; // event for timer            
            newTimer.Start();
            //btnSendMail_Click(null, null);


        }

        private void btnmergeFile_Click(object sender, EventArgs e)
        {
            List<Person> l = listCont.getListOfPersons();
            string outputPath = "C:\\Users\\mahna\\OneDrive\\Skrivebord\\OutputData.csv";
            listCont.WriteCSV(l, outputPath);
            // listCont.PerformMergeFromCSV();
            //listCont.CreateCSVFromPersonList(listCont.getListOfPersons(), outputPath);
            //listCont.ReadDatafromCSV();

            List<Person> people = listCont.ReadCSV(outputPath);
            Console.WriteLine("List of names from CSV:");
            foreach (Person person in people)
            {
                Console.WriteLine(person.Name);
            }
            listCont.GenerateMergeFiles("C:\\Users\\mahna\\OneDrive\\Skrivebord\\MergeFiles\\", people);
            Console.WriteLine("Merge file generated successfully!");
        }


    }
}
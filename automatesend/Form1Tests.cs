using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;
using Castle.Core.Smtp;

namespace automatesend
{
    [TestClass]

    public class Form1Tests
    {
        [TestMethod]
        public void Form1_Load_Tests()
        {
            // Arrange
            var form = new Form1();
            JObject jsonData = form.getData();
            List<Person> people = new List<Person>();

            Person person1 = new Person("John", "mahnaaz2021@gmail.com");
            Person person2 = new Person("Emily", "minexemple@gmail.com");


            people.Add(person1);
            people.Add(person2);

            var smtpClientMock = new Mock<SmtpClient>("smtp.gmail.com", 587);
            smtpClientMock.SetupAllProperties();

            var mailMessageMock = new Mock<MailMessage>();
            mailMessageMock.SetupProperty(m => m.From, new MailAddress((string)jsonData["EMAIL_HOST_USER"]));
            mailMessageMock.Setup(m => m.To.Add(It.IsAny<string>()));
            mailMessageMock.SetupProperty(m => m.Subject, "subject");
            mailMessageMock.SetupProperty(m => m.Body, "");
            mailMessageMock.SetupProperty(m => m.IsBodyHtml, true);

            smtpClientMock.Setup(s => s.Send(mailMessageMock.Object));

            

            // Assert
            smtpClientMock.VerifySet(s => s.DeliveryMethod = SmtpDeliveryMethod.Network);
            smtpClientMock.VerifySet(s => s.UseDefaultCredentials = false);
            smtpClientMock.VerifySet(s => s.Credentials = new System.Net.NetworkCredential((string)jsonData["EMAIL_HOST_USER"], (string)jsonData["EMAIL_HOST_PASSWORD"]));
            smtpClientMock.VerifySet(s => s.EnableSsl = true);
            smtpClientMock.Verify(s => s.Send(mailMessageMock.Object));

        }
    }
}

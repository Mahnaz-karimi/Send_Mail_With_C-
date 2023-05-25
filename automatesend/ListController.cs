using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automatesend
{
    internal class ListController
    {
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
    }
}

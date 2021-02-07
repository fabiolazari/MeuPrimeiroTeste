using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.PersonClasses
{
    public class PersonManager
    {
        public Person CreatePerson(string first, string last, bool isSupervisor)
        {
            Person ret = null;

            if (!string.IsNullOrEmpty(first))
            {
                if (isSupervisor)
                    ret = new Supervisor();
                else
                    ret = new Employee();

                ret.FirstName = first;
                ret.LastName = last;
            }
            return ret;
        }

        public List<Person> GetPeople()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person() { FirstName = "Fabio", LastName = "Lazari" });
            people.Add(new Person() { FirstName = "Lau", LastName = "Martins" });
            people.Add(new Person() { FirstName = "Filipe", LastName = "Rosa" });

            return people;
        }

        public List<Person> GetSupervisors()
        {
            List<Person> people = new List<Person>();

            people.Add(CreatePerson("Fabio", "Lazari", true));
            people.Add(CreatePerson("Felipe", "Rosa", true));

            return people;
        }
    }
}

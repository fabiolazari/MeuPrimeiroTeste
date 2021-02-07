using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClass.PersonClasses;

namespace MyClassTest
{
    [TestClass]
    public class CollectionAssertClassTest
    {
        [TestMethod]
        [Owner("Mariana")]
        public void AreCollectionEqualFailsBecauseNoComparerTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExperted = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleExperted.Add(new Person() { FirstName = "Fabio", LastName = "Lazari"});
            peopleExperted.Add(new Person() { FirstName = "Lau", LastName = "Martins" });
            peopleExperted.Add(new Person() { FirstName = "Filipe", LastName = "Rosa" });

            peopleActual = peopleExperted; // PerMgr.GetPeople(); --> You shall not pass!!!

            CollectionAssert.AreEqual(peopleExperted, peopleActual);
        }

        [TestMethod]
        [Owner("Mariana")]
        public void AreCollectionEqualWithComparerTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExperted = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleExperted.Add(new Person() { FirstName = "Fabio", LastName = "Lazari" });
            peopleExperted.Add(new Person() { FirstName = "Lau", LastName = "Martins" });
            peopleExperted.Add(new Person() { FirstName = "Filipe", LastName = "Rosa" });

            //You shall not pass!!!
            peopleActual = PerMgr.GetPeople();

            CollectionAssert.AreEqual(peopleExperted, peopleActual,
                Comparer<Person>.Create((x, y) => x.FirstName == y.FirstName && x.LastName == y.LastName ? 0 : 1));
        }

        [TestMethod]
        [Owner("Mariana")]
        public void AreCollectionEquivalentTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExperted = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleActual = PerMgr.GetPeople();

            peopleExperted.Add(peopleActual[1]);
            peopleExperted.Add(peopleActual[2]);
            peopleExperted.Add(peopleActual[0]);

            CollectionAssert.AreEquivalent(peopleExperted, peopleActual);
        }

        [TestMethod]
        [Owner("Mariana")]
        public void IsCollectionOfTypeTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleActual = new List<Person>();

            peopleActual = PerMgr.GetSupervisors();

            CollectionAssert.AllItemsAreInstancesOfType(peopleActual, typeof(Supervisor));
        }
    }
}

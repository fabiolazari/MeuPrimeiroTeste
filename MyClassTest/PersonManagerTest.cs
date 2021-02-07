using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClass.PersonClasses;

namespace MyClassTest
{
    [TestClass]
    public class PersonManagerTest
    {
        [TestMethod]
        [Owner("Fabio")]
        public void CreatePerson_OfTypeEmployeeTest()
        {
            PersonManager PerMgr = new PersonManager();
            Person per;

            per = PerMgr.CreatePerson("Fabio","Lazari", false);

            Assert.IsInstanceOfType(per, typeof(Employee));
        }

        [TestMethod]
        [Owner("Fabio")]
        public void DoEmployeeExistTest()
        {
            Supervisor super = new Supervisor();

            super.Employees = new List<Employee>();
            super.Employees.Add(new Employee()
            {
                FirstName = "Fabio",
                LastName = "Lazari"

            });

            Assert.IsTrue(super.Employees.Count > 0);
        }
    }
}

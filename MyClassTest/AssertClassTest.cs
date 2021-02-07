using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClass;
using MyClass.PersonClasses;

namespace MyClassTest
{
    [TestClass]
    public class AssertClassTest
    {
        #region AreEqual/AreNotEqual Tests
        [TestMethod]
        [Owner("Fabio")]
        public void AreEqualTest()
        {
            string str1 = "Fabio";
            string str2 = "Fabio";

            Assert.AreEqual(str1, str2);
        }

        [TestMethod]
        [Owner("Fabio")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AreEqualCaseSenditiveTest()
        {
            string str1 = "Fabio";
            string str2 = "fabio";

            Assert.AreEqual(str1, str2, false);
        }

        [TestMethod]
        [Owner("Fabio")]
        public void AreNotEqualTest()
        {
            string str1 = "Fabio";
            string str2 = "Lau";

            Assert.AreNotEqual(str1, str2);
        }
        #endregion


        #region AreSame/ AreNotSame Tests

        [TestMethod]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            Assert.AreSame(x, y);
        }

        [TestMethod]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y);
        }

        #endregion

        #region IsInstanceofType test

        [TestMethod]
        [Owner("Fabio")]
        public void IsInstanceOfTypeTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("Fabio","Lazari", true);

            Assert.IsInstanceOfType(per, typeof(Supervisor));
        }

        [TestMethod]
        [Owner("Fabio")]
        public void IsNullTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("", "Lazari", true);

            Assert.IsNull(per);
        }
        #endregion
    }
}

using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyClassTest
{
    [TestClass]
    public class StringAssertClassTest
    {
        [TestMethod]
        [Owner("Fabio")]
        public void ContainsTest()
        {
            string str1 = "Fabio Lazari";
            string str2 = "Lazari";

            StringAssert.Contains(str1, str2);
        }

        [TestMethod]
        [Owner("Fabio")]
        public void StartsWithTest()
        {
            string str1 = "Todos Caixa Alta";
            string str2 = "Todos Caixa";

            StringAssert.StartsWith(str1, str2);
        }

        [TestMethod]
        [Owner("Fabio")]
        public void IsAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");

            StringAssert.Matches("todos caixa", reg);
        }

        [TestMethod]
        [Owner("Fabio")]
        public void IsNotAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");

            StringAssert.DoesNotMatch("Todos Caixa", reg);
        }
    }
}

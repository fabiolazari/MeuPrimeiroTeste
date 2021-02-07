using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClass;

namespace MyClassTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\BadfileName.bat";
        private string _GoodFileName;

        public TestContext TestContext { get; set; }

        #region Test Initialize e Cleanup

        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Creating File: {_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some Text");
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Deleting File: {_GoodFileName}");
                    File.Delete(_GoodFileName);
                }
            }
        }

        #endregion

        [TestMethod]
        [Owner("Lau")]
        [Description("Check to see if a file does exist.")]
        [Priority(0)]
        [TestCategory("NoException")]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;
 
            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Owner("Fabio")]
        [DataSource("System.Data.SqlClient",
            @"Data Source = LAZARI_PC\SQLEXPRESS;User ID = sa;Password = 123456;Initial Catalog=TesteUnitario;Connect Timeout = 30;Encrypt = False;TrustServerCertificate = True;ApplicationIntent = ReadWrite;MultiSubnetFailover = False",
            "FileProcessTest",
            DataAccessMethod.Sequential)]
        public void FileExistsTestFromDB()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool expectedValue, causesException, fromCall;

            fileName = TestContext.DataRow["FileName"].ToString();
            expectedValue = Convert.ToBoolean(TestContext.DataRow["ExpectedValue"]);
            causesException = Convert.ToBoolean(TestContext.DataRow["CausesException"]);

            try
            {
                fromCall = fp.FileExists(fileName);
                Assert.AreEqual(expectedValue, fromCall,
                    $"File: {fileName} has failed.  METHOD: FileExistsTestFromDB");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(causesException);
            }
        }

        [TestMethod]
        public void FileNameDoesExistsSimpleMessage()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            //_GoodFileName - Usar o nome de arquivo errado para passar o teste
            TestContext.WriteLine($"Testing File: {BAD_FILE_NAME}");
            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall, "File Does Not Exist");
        }

        [TestMethod]
        public void FileNameDoesExistsMessageFormatting()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            //_GoodFileName - Usar o nome de arquivo errado para passar o teste
            TestContext.WriteLine($"Testing File: {BAD_FILE_NAME}");
            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall, "File '{0}' Does Not Exist", _GoodFileName);
        }

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if(_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        private const string FILE_NAME = @"filetodeploy.txt";

        [TestMethod]
        [Owner("Fabio")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistsUsingDeploymentItem()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = $@"{TestContext.DeploymentDirectory}\{FILE_NAME}";
            TestContext.WriteLine($"Checking File: {fileName}");
            fromCall = fp.FileExists(fileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Timeout(3100)]
        [Owner("Mariana")]
        public void SimulateTimeout()
        {
            Thread.Sleep(3000);
        }

        [TestMethod]
        [Owner("Rosa")]
        [Description("Check to see if a file does not exist.")]
        [Priority(0)]
        [TestCategory("NoException")]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Owner("Fabio")]
        [Priority(1)]
        //[Ignore]
        [TestCategory("Exception")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        [Owner("Fabio")]
        [Priority(1)]
        [TestCategory("Exception")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_usingtryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {
                //sucesso
                return;
            }
            Assert.Fail("Falha esperada!");
        }

    }
}

using System;
using System.IO;

namespace MyClass
{
    public class FileProcess
    {
        public bool FileExists(string FileName)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                throw new ArgumentNullException("FileName");
            }
            return File.Exists(FileName);
        }
    }
}

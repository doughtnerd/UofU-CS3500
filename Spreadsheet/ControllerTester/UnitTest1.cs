using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGUI;
using System.Windows.Forms;

namespace ControllerTester
{
    public class ViewStub : ISpreadsheetView
    {
        public event Action<FormClosingEventArgs> CloseEvent;
        public event Action<FileInfo> OpenEvent;
        public event Action<FileInfo> SaveEvent;

        public bool GetCellValue(string name, out string contents)
        {
            throw new NotImplementedException();
        }

        public bool SetCellValue(string name, string value)
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}

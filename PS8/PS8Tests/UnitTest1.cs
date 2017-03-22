using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoggleClient;
using System.Dynamic;

namespace PS8Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MakePostRequest()
        {
            dynamic data = new ExpandoObject();
            data.NickName = "Chris";
            BoggleController.MakeRequest(
                "http://cs3500-boggle-s17.azurewebsites.net/BoggleService.svc/",
                BoggleController.RequestType.POST,
                "users",
                data,
                (Action<string>)(n => { Console.WriteLine(n); }),
                new System.Threading.CancellationTokenSource().Token);
        }
    }
}

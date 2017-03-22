using BoggleClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = Task.Run(() => MakePostRequest());
            task.Wait();
        }

        public static void MakePostRequest()
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

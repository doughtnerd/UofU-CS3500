using BoggleClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = Task.Run(() => MakePostRequest());
            task.Wait();
            Thread.Sleep(100000);
        }

        public static void MakePostRequest()
        {
            dynamic data = new ExpandoObject();
            data.Nickname = "Chris";
            /*
            BoggleController.MakeRequest(
                "http://cs3500-boggle-s17.azurewebsites.net/BoggleService.svc/",
                RestUtil.RequestType.POST
                "users",
                data,
                (Action<dynamic>)(n => { Console.WriteLine(n.UserToken); }),
                new CancellationTokenSource().Token);
            */
        }
    }
}

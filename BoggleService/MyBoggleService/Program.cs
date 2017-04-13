﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    class Program
    {
        static void Main(string[] args)
        {
            //HttpStatusCode status;
            UserInfo name = new UserInfo();
            name.Nickname = "zzzzz";
            BoggleService service = new BoggleService();
            dynamic user = service.createUser(name);
            Console.WriteLine(user.UserToken);
            //Console.WriteLine(status.ToString());

            // This is our way of preventing the main thread from
            // exiting while the server is in use
            Console.ReadLine();
        }
    }
}

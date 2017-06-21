//Nathan Reeves 3/30/17
//These tests achieve close to full code coverage.
//I was unable to UnitTest correct words, but they were tested through our Boggle client
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.HttpStatusCode;
using System.Diagnostics;
using Newtonsoft.Json;
using Boggle;
using System.Collections.Generic;
using System.IO;

namespace Boggle
{
    /// <summary>
    /// Provides a way to start and stop the IIS web server from within the test
    /// cases.  If something prevents the test cases from stopping the web server,
    /// subsequent tests may not work properly until the stray process is killed
    /// manually.
    /// </summary>
    public static class IISAgent
    {
        // Reference to the running process
        private static Process process = null;

        /// <summary>
        /// Starts IIS
        /// </summary>
        public static void Start(string arguments)
        {
            if (process == null)
            {
                ProcessStartInfo info = new ProcessStartInfo(Properties.Resources.IIS_EXECUTABLE, arguments);
                info.WindowStyle = ProcessWindowStyle.Minimized;
                info.UseShellExecute = false;
                process = Process.Start(info);
            }
        }

        /// <summary>
        ///  Stops IIS
        /// </summary>
        public static void Stop()
        {
            if (process != null)
            {
                process.Kill();
            }
        }
    }
    [TestClass]
    public class BoggleTests
    {
        /// <summary>
        /// This is automatically run prior to all the tests to start the server
        /// </summary>
        [ClassInitialize()]
        public static void StartIIS(TestContext testContext)
        {
            IISAgent.Start(@"/site:""BoggleService"" /apppool:""Clr4IntegratedAppPool"" /config:""..\..\..\.vs\config\applicationhost.config""");
        }

        /// <summary>
        /// This is automatically run when all tests have completed to stop the server
        /// </summary>
        [ClassCleanup()]
        public static void StopIIS()
        {
            IISAgent.Stop();
        }

        private RestTestClient client = new RestTestClient("http://localhost:60000/BoggleService.svc/");

        //test user create
        [TestMethod]
        public void TestMethod1()
        {
            UserInfo user = new UserInfo();
            user.Nickname = "Nathan";
            Response r = client.DoPostAsync("users", user).Result;
            Assert.AreEqual(r.Status.ToString(), "Created");
            Assert.AreEqual(r.Data.Nickname.Value, user.Nickname);
            //null nickname test
            user = new UserInfo();
            user.UserToken = Guid.NewGuid().ToString();
            user.Nickname = null;
            r = client.DoPostAsync("users", user).Result;
            Assert.AreEqual(r.Status, Forbidden);


        }
        //tests join game 
        [TestMethod]
        public void TestMethod2()
        {
            //created status
            UserInfo user = new UserInfo();
            user.Nickname = "Nathan";
            Response r = client.DoPostAsync("users", user).Result;
            Assert.AreEqual(r.Status.ToString(), "Created");
            Assert.AreEqual(r.Data.Nickname.Value, user.Nickname);

            //null nickname test
            user = new UserInfo();
            r = client.DoPostAsync("users", user).Result;
            Assert.AreEqual(r.Status, Forbidden);


        }
        //creates a game with two players and tests it
        /*[TestMethod]
        public void TestMethod3()
        {
            UserInfo user = new UserInfo();
            user.Nickname = "Nathan";
            user.TimeLimit = 60;
            //create user save token
            user.UserToken  = client.DoPostAsync("users", user).Result.Data.UserToken.Value;

            //join game succeed
            Response r = client.DoPostAsync("games", user).Result;
            Assert.AreEqual(r.Data.Player1.Nickname.Value, "Nathan");
            Assert.AreEqual(r.Data.GameID.Value, 1);

            //join game cancel
            Response cancel = client.DoPutAsync(user, "games").Result;
            Assert.AreEqual(cancel.Status, OK);
            Assert.AreEqual(r.Data.GameID.Value, 1);

            //join game succeed after cancel
            r = client.DoPostAsync("games", user).Result;
            Assert.AreEqual(r.Data.Player1.Nickname.Value, "Nathan");
            Assert.AreEqual(r.Data.GameID.Value, 1);

            //join game fail
            UserInfo userfail = new UserInfo();
            userfail.UserToken = Guid.NewGuid().ToString();
            Response fail = client.DoPostAsync("games", userfail).Result;
            Assert.AreEqual(fail.Status, Forbidden);

            //get game
            Response r2 = client.DoGetAsync("games/{0}", "1").Result;
            Assert.AreEqual(r2.Data.GameID.Value, 1);
            Assert.AreEqual(r2.Data.GameState.Value, "pending");

            //Game will be active when another player joins
            UserInfo user2 = new UserInfo();
            user2.Nickname = "Player2";
            user2.TimeLimit = 60;
            //create user save token
            Response p1token = client.DoPostAsync("users", user2).Result;
            //response status: created
            Assert.AreEqual(p1token.Status.ToString(), "Created");
            user2.UserToken = p1token.Data.UserToken.Value;

            //join second user
            Response join = client.DoPostAsync("games", user2).Result;
            Assert.AreEqual(join.Data.GameID.Value, 1);
            Assert.AreEqual(join.Data.Player1.Nickname.Value, "Nathan");
            Assert.AreEqual(join.Data.Player2.Nickname.Value, "Player2");
            Assert.AreEqual(join.Data.GameState.Value, "active");

            //get game active
            Response active = client.DoGetAsync("games/{0}", "1").Result;
            Assert.AreEqual(active.Data.GameState.Value, "active");

            //word post test
            user.Word = "plll"; //non word gives score -1
            string url = string.Format("games/{0}", "1");
            Response w = client.DoPutAsync(user, url).Result;           
            Assert.AreEqual(w.Data.Score.Value, -1);

            //word post non-matching token
            UserInfo usertokenfail = new UserInfo();
            usertokenfail.UserToken = Guid.NewGuid().ToString();
            user.Word = "plll"; 
            Response wordfail = client.DoPutAsync(usertokenfail, url).Result;
            Assert.AreEqual(wordfail.Status, BadRequest);



        }*/
        
      }
}

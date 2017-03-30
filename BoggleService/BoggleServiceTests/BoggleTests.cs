using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.HttpStatusCode;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Dynamic;

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
        private string userToken1 = "", userToken2 = "", userToken3 = "", userToken4 = "", gameID1 = "", gameID2 = "";

        [TestMethod]
        public void CreateUserNicknameNull()
        {
            dynamic data = new ExpandoObject();
            data.Nickname = null;
            Response r = client.DoPostAsync("users", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void CreateUserNicknameEmpty()
        {
            dynamic data = new ExpandoObject();
            data.Nickname = "      ";
            Response r = client.DoPostAsync("users", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void CreateUserNicknameValid()
        {
            dynamic data = new ExpandoObject();
            data.Nickname = "Valid_Name1";
            Response r = client.DoPostAsync("users", data).Result;
            //userToken1 = r.Data.ToString();
            Assert.AreEqual(Created, r.Status);
            data.Nickname = "Valid_Name2";
            r = client.DoPostAsync("users", data).Result;
            userToken2 = r.Data.UserToken;
            Assert.AreEqual(Created, r.Status);
            data.Nickname = "Valid_Name3";
            r = client.DoPostAsync("users", data).Result;
            userToken3 = r.Data.UserToken;
            Assert.AreEqual(Created, r.Status);
            data.Nickname = "Valid_Name4";
            r = client.DoPostAsync("users", data).Result;
            userToken4 = r.Data.UserToken;
            Assert.AreEqual(Created, r.Status);
        }

        [TestMethod]
        public void JoinGameUserTokenInvalid()
        {
            dynamic data = new ExpandoObject();
            data.UserToken = "";
            data.TimeLimit = 60;
            Response r = client.DoPostAsync("games", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void JoinGameTimeLimitTooSmall()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken1;
            data.UserToken = validUserToken;
            data.TimeLimit = 4;
            Response r = client.DoPostAsync("games", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void JoinGameTimeLimitTooLarge()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken1;
            data.UserToken = validUserToken;
            data.TimeLimit = 121;
            Response r = client.DoPostAsync("games", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void JoinGameUserTokenInPendingGame()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken1;
            data.UserToken = validUserToken;
            data.TimeLimit = 60;
            client.DoPostAsync("games", data);
            Response r = client.DoPostAsync("games", data).Result;
            Assert.AreEqual(Conflict, r.Status);
        }

        [TestMethod]
        public void JoinGameUserTokenBecamePlayer2()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken2;
            data.UserToken = validUserToken;
            data.TimeLimit = 60;
            Response r = client.DoPostAsync("games", data).Result;
            gameID1 = r.Data.ToString();
            Assert.AreEqual(Created, r.Status);
        }

        [TestMethod]
        public void JoinGameUserTokenBecamePlayer1()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken3;
            data.UserToken = validUserToken;
            data.TimeLimit = 60;
            Response r = client.DoPostAsync("games", data).Result;
            gameID2 = r.Data.ToString();
            Assert.AreEqual(Accepted, r.Status);
        }

        [TestMethod]
        public void CancelJoinUserTokenInvalid()
        {
            dynamic data = new ExpandoObject();
            data.UserToken = "";
            Response r = client.DoPutAsync("games", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void CancelJoinUserTokenNotInPendingGame()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken4;
            data.UserToken = validUserToken;
            Response r = client.DoPutAsync("games", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void CancelJoinSuccessful()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken4;
            data.UserToken = validUserToken;
            client.DoPostAsync("games", data);
            Response r = client.DoPutAsync("games", data).Result;
            Assert.AreEqual(OK, r.Status);
        }

        [TestMethod]
        public void PlayWordNull()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken1;
            data.UserToken = validUserToken;
            data.Word = null;
            Response r = client.DoPutAsync("games/" + gameID1, data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void PlayWordEmpty()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken2;
            data.UserToken = validUserToken;
            data.Word = "";
            Response r = client.DoPutAsync("games/" + gameID1, data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void PlayWordGameIDMissing()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken1;
            data.UserToken = validUserToken;
            data.Word = "word";
            Response r = client.DoPutAsync("games/" + "", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void PlayWordUserTokenMissing()
        {
            dynamic data = new ExpandoObject();
            data.UserToken = "";
            data.Word = "word";
            Response r = client.DoPutAsync("games/" + gameID1, data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void PlayWordGameIDInvalid()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken1;
            data.UserToken = validUserToken;
            data.Word = "word";
            Response r = client.DoPutAsync("games/" + "13781", data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void PlayWordUserTokenInvalid()
        {
            dynamic data = new ExpandoObject();
            data.UserToken = "";
            data.Word = "word";
            Response r = client.DoPutAsync("games/" + gameID1, data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void PlayWordUserTokenIncorrect()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken3;
            data.UserToken = validUserToken;
            data.Word = "word";
            Response r = client.DoPutAsync("games/" + gameID1, data).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void PlayWordGameStateNotActive()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken3;
            data.UserToken = validUserToken;
            data.Word = "word";
            Response r = client.DoPutAsync("games/" + gameID2, data).Result;
            Assert.AreEqual(Conflict, r.Status);
        }

        [TestMethod]
        public void PlayWordSuccessful()
        {
            dynamic data = new ExpandoObject();
            string validUserToken = userToken1;
            data.UserToken = validUserToken;
            data.Word = "word";
            Response r = client.DoPutAsync("games/" + gameID1, data).Result;
            Assert.AreEqual(OK, r.Status);
        }

        [TestMethod]
        public void GameStatusGameIDInvalid()
        {
            Response r = client.DoGetAsync("games/" + "13781").Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void GameStatusSuccessfulWithBriefYes()
        {
            Response r = client.DoGetAsync("games/" + gameID1 + "?Brief={0}", "Yes").Result;
            Assert.AreEqual(OK, r.Status);
        }

        [TestMethod]
        public void GameStatusSuccessfulWithBriefNo()
        {
            Response r = client.DoGetAsync("games/" + gameID1 + "?Brief={0}", "No").Result;
            Assert.AreEqual(OK, r.Status);
        }

        [TestMethod]
        public void GameStatusSuccessfulWithOutBrief()
        {
            Response r = client.DoGetAsync("games/" + gameID1).Result;
            Assert.AreEqual(OK, r.Status);
        }
    }
}

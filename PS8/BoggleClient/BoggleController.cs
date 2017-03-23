using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoggleClient
{
    public class BoggleController
    {
        /// <summary>
        /// The view this controller is currently managing.
        /// </summary>
        IBoggleView view;

        /// <summary>
        /// The token source used for cancelling server requests.
        /// </summary>
        private CancellationTokenSource tokenSource;

        private string domain;

        /// <summary>
        /// The user's current user token, created when the user registered on the server.
        /// </summary>
        private string userToken;

        private string gameID;

        public BoggleController(IBoggleView view)
        {
            this.view = view;
            tokenSource = new CancellationTokenSource();
            RegisterHandlers();
        }

        void RegisterHandlers()
        {
            view.RegisterEvent += HandleRegisterUser;
            view.JoinGameEvent += HandleJoinGame;
            view.CancelJoinEvent += HandleCancelJoin;
            view.PlayWordEvent += HandlePlayWord;
        }

        void GetGameStatus(bool brief)
        {
            dynamic data = new ExpandoObject();
            data = brief ? "?Brief=yes" : "?Brief=no";
            RestUtil.MakeRequest(domain, RestUtil.RequestType.GET, "games/" + gameID, data, (Action<HttpResponseMessage>)(n => {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    int score = responseData.Score;
                    //TODO: Might need to add a delegate to the method to handle what to do based off of what the game status is.
                }
            }), tokenSource.Token);
        }

        void HandlePlayWord(string word)
        {
            dynamic data = new ExpandoObject();
            data.UserToken = this.userToken;
            data.Word = word;
            RestUtil.MakeRequest(domain, RestUtil.RequestType.PUT, "games/"+gameID, data, (Action<HttpResponseMessage>)(n => {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    int score = responseData.Score;
                    //TODO: Do stuff with the score.
                }
            }), tokenSource.Token);
        }

        public void HandleCancelJoin()
        {
            dynamic data = new ExpandoObject();
            data.UserToken = this.userToken;
            RestUtil.MakeRequest(domain, RestUtil.RequestType.PUT, "games", data, (Action<HttpResponseMessage>)(n => {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    gameID = null;
                    //TODO: Will need to reset view
                }
            }), tokenSource.Token);
        }

        public void HandleJoinGame(long timeLimit)
        {
            dynamic data = new ExpandoObject();
            data.UserToken = this.userToken;
            data.TimeLimit = timeLimit;
            RestUtil.MakeRequest(domain, RestUtil.RequestType.POST, "games", data, (Action<HttpResponseMessage>)(n=> {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    gameID = responseData.GameID;
                    //TODO: Do additional things with view.
                }
            }), tokenSource.Token);
        }

        public void HandleRegisterUser(string domain, string nickName)
        {
            this.domain = domain;
            dynamic data = new ExpandoObject();
            data.Nickname = nickName;
            RestUtil.MakeRequest(domain, RestUtil.RequestType.POST, "users", data, (Action<HttpResponseMessage>)(n=> {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    userToken = data.UserToken;
                    //TODO: Do additional things with view.
                }
            }), tokenSource.Token);
        }
    }
}

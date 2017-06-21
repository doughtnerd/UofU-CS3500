using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        /// <summary>
        /// Current domain used for communications.
        /// </summary>
        private string domain;

        /// <summary>
        /// The user's current user token, created when the user registered on the server.
        /// </summary>
        private string userToken;

        /// <summary>
        /// The gameId of the users current game.
        /// </summary>
        private string gameID;

        /// <summary>
        /// The timer handling the game status updates
        /// </summary>
        System.Windows.Forms.Timer timer;

        /// <summary>
        /// event fired when a game starts
        /// </summary>
        private event Action GameStartedEvent;

        /// <summary>
        /// Event fired when a game ends.
        /// </summary>
        private event Action GameEndEvent;

        public BoggleController(IBoggleView view)
        {
            this.view = view;
            tokenSource = new CancellationTokenSource();
            RegisterHandlers();
            view.SetDomain("http://cs3500-boggle-s17.azurewebsites.net/BoggleService.svc/");
            view.SetJoinGameActive(false);
        }

        void RegisterHandlers()
        {
            view.RegisterEvent += HandleRegisterUser;
            view.JoinGameEvent += HandleJoinGame;
            view.CancelJoinEvent += HandleCancelJoin;
            view.CancelJoinEvent += HandleGameEnded;
            view.PlayWordEvent += HandlePlayWord;
            view.ExitGameEvent += HandleGameEnded;
            view.ExitGameEvent += HandleCancelJoin;
            this.GameStartedEvent += HandleGameStarted;
            this.GameEndEvent += HandleGameEnded;
        }

        private void HandleGameEnded()
        {
            timer.Stop();
            view.SetScores(0, 0);
            view.SetTimeLeft(0 + "");
            view.SetJoinButtonText("Join Game");
            view.SetJoinGameActive(true);
        }

        void HandleGameStarted()
        {
            GetGameStatus(false);
            view.SetWords(new string[0], new string[0]);
            view.SetGameBoard("                ");
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (a,b) => { GetGameStatus(false); };
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            GetGameStatus(false);
        }

        void GetGameStatus(bool brief)
        {
            dynamic data = new ExpandoObject();
            data = brief ? "?Brief=yes" : "";
            
            RestUtil.MakeRequest(domain, RestUtil.RequestType.GET, "games/" + gameID, data, (Action<HttpResponseMessage>)(n => {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    HandleGameStatus(responseData);
                } else
                {
                    MessageBox.Show("Failed to get game status from: " + domain + gameID + data + " Status: " + n.StatusCode);
                }
            }), tokenSource.Token);
        }

        void HandleGameStatus(dynamic responseData)
        {
            string state = responseData.GameState;
            view.SetGameStatus(state);
            if (state.Equals("active"))
            {
                string time = responseData.TimeLeft + "";
                string board = responseData.Board;
                int player1Score = responseData.Player1.Score;
                int player2Score = responseData.Player2.Score;
                view.SetTimeLeft(time);
                view.SetGameBoard(board);
                Console.WriteLine(board);
                view.SetScores(player1Score, player2Score);
            }
            else if (state.Equals("completed"))
            {
                List<string> first = new List<string>();
                foreach(dynamic d in responseData.Player1.WordsPlayed)
                {
                    string s = d.Word + " : " + d.Score;
                    first.Add(s);
                }

                List<string> second = new List<string>();
                foreach (dynamic d in responseData.Player2.WordsPlayed)
                {
                    string s = d.Word + " : " + d.Score;
                    second.Add(s);
                }
                view.SetWords(first.ToArray(), second.ToArray());
                GameEndEvent?.Invoke();
            }
        }

        void HandlePlayWord(string word)
        {
            dynamic data = new ExpandoObject();
            data.UserToken = this.userToken;
            data.Word = word.ToLower();
            RestUtil.MakeRequest(domain, RestUtil.RequestType.PUT, "games/"+gameID, data, (Action<HttpResponseMessage>)(n => {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    int score = responseData.Score;
                } else
                {
                    MessageBox.Show(n.StatusCode.ToString());
                }
            }), tokenSource.Token);
        }

        private void HandleCancelJoin()
        {
            dynamic data = new ExpandoObject();
            data.UserToken = this.userToken;
            RestUtil.MakeRequest(domain, RestUtil.RequestType.PUT, "games", data, (Action<HttpResponseMessage>)(n => {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    gameID = null;
                } else
                {
                    MessageBox.Show(n.StatusCode.ToString());
                }
                view.SetJoinButtonText("Join Game");
            }), tokenSource.Token);
        }

        private void HandleJoinGame(long timeLimit)
        {
            dynamic data = new ExpandoObject();
            data.UserToken = this.userToken;
            data.TimeLimit = timeLimit;
            view.SetJoinButtonText("Cancel");
            RestUtil.MakeRequest(domain, RestUtil.RequestType.POST, "games", data, (Action<HttpResponseMessage>)(n =>
            {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    gameID = responseData.GameID;
                    GameStartedEvent?.Invoke();
                    view.SetJoinGameActive(false);
                    view.SetGameViewEnabled(true);
                } else
                {
                    MessageBox.Show(n.StatusCode.ToString());
                }
                view.SetJoinButtonText("Join Game");
            }), tokenSource.Token);
        }

        private void HandleRegisterUser(string domain, string nickName)
        {
            this.domain = domain;
            dynamic data = new ExpandoObject();
            data.Nickname = nickName;
            RestUtil.MakeRequest(domain, RestUtil.RequestType.POST, "users", data, (Action<HttpResponseMessage>)(n=> {
                if (n.IsSuccessStatusCode)
                {
                    dynamic responseData = RestUtil.GetResponseData(n);
                    userToken = responseData.UserToken;
                    view.SetJoinGameActive(true);
                    view.SetRegistrationActive(false);
                } else
                {
                    MessageBox.Show(n.StatusCode.ToString());
                }
            }), tokenSource.Token);
        }
    }
}

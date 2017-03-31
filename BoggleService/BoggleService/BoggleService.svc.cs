using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        private static readonly ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentQueue<Game> pendingGames = new ConcurrentQueue<Game>();
        private static readonly ConcurrentBag<Game> activeGames = new ConcurrentBag<Game>();
        private static readonly ConcurrentBag<Game> completedGames = new ConcurrentBag<Game>();

        /// <summary>
        /// The most recent call to SetStatus determines the response code used when
        /// an http response is sent.
        /// </summary>
        /// <param name="status"></param>
        private static void SetStatus(HttpStatusCode status)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = status;
        }

        /// <summary>
        /// Returns a Stream version of index.html.
        /// </summary>
        /// <returns></returns>
        public Stream API()
        {
            SetStatus(OK);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "index.html");
        }

        public void CancelJoin(UserInfo user)
        {
            if (users.ContainsKey(user.UserToken))
            {
                if (InGame(user.UserToken, pendingGames))
                {
                    Game g;
                    if (pendingGames.TryDequeue(out g))
                    {
                        SetStatus(OK);
                        return;
                    }
                }
            }
            SetStatus(Forbidden);
            return;
        }

        public IDictionary<string, object> GameStatus(string id, string brief)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            brief = string.IsNullOrEmpty(brief) ? "no" : brief.ToLower();
            Game g = GetGame(int.Parse(id));
            if (g != null)
            {
                //TODO: Need to handle checking if the game is pending or not. If it is, this information will be unavailable.
                if (brief.Equals("yes"))
                {
                    data.Add("GameState", g.GameState.ToString());
                    data.Add("Board", g.Board.ToString());
                    data.Add("TimeLimit", g.TimeLimit);
                    return data;
                }
                else
                {
                    data.Add("GameState", g.GameState.ToString());
                    data.Add("Board", g.Board.ToString());
                    data.Add("TimeLimit", g.TimeLimit);
                    return data;
                }
            }
            else
            {
                SetStatus(Forbidden);
                return null;
            }
        }

        private Game GetGame(int id)
        {
            Game g;
            return SearchForGame(id, pendingGames, out g) ? g : SearchForGame(id, activeGames, out g) ? g : SearchForGame(id, completedGames, out g) ? g : null;
        }

        private bool SearchForGame(int id, ConcurrentQueue<Game> games, out Game game)
        {
            foreach (Game g in games)
            {
                if (g.ID.Equals(id.ToString()))
                {
                    game = g;
                    return true;
                }
            }
            game = null;
            return false;
        }

        private bool SearchForGame(int id, ConcurrentBag<Game> games, out Game game)
        {
            foreach (Game g in games)
            {
                if (g.ID.Equals(id.ToString()))
                {
                    game = g;
                    return true;
                }
            }
            game = null;
            return false;
        }

        public GameInfo Join(JoinInfo user)
        {
            if (!string.IsNullOrEmpty(user.UserToken) && users.ContainsKey(user.UserToken) && user.TimeLimit > 5 && user.TimeLimit < 120)
            {
                if (!InGame(user.UserToken, pendingGames))
                {
                    Game g;
                    if(pendingGames.TryDequeue(out g))
                    {
                        if (g.PlayerOne != null)
                        {
                            GameInfo info =  JoinAsPlayerTwo(user, g);
                            activeGames.Add(g);
                            return info;
                        }
                        else
                        {
                            return JoinAsPlayerOne(user, g);
                        }
                    } else
                    {
                        g = new Game();
                        g.PlayerOne = user;
                        g.ID = activeGames.Count + completedGames.Count + pendingGames.Count + 1 + "";
                        g.GameState = Game.Status.pending;
                        pendingGames.Enqueue(g);
                        SetStatus(Accepted);
                        return new GameInfo() { GameID = g.ID };
                    }
                }
                SetStatus(Conflict);
                return null;
            }
            SetStatus(Forbidden);
            return null;
        }

        private GameInfo JoinAsPlayerTwo(JoinInfo user, Game g)
        {
            g.PlayerTwo = user;
            g.TimeLimit = (g.PlayerOne.TimeLimit + user.TimeLimit) / 2;
            g.Board = new BoggleBoard();
            g.GameState = Game.Status.active;
            SetStatus(Created);
            return new GameInfo() { GameID = g.ID };
        }

        private GameInfo JoinAsPlayerOne(JoinInfo user, Game g)
        {
            g.PlayerOne = user;
            g.ID = activeGames.Count + completedGames.Count + pendingGames.Count + 1 + "";
            g.GameState = Game.Status.pending;
            SetStatus(Accepted);
            return new GameInfo() { GameID = g.ID };
        }

        private bool InGame(string userToken, ConcurrentQueue<Game> pending)
        {
            foreach(Game g in pending)
            {
                if ((g.PlayerOne !=null && g.PlayerOne.UserToken.Equals(userToken)) || (g.PlayerTwo !=null && g.PlayerTwo.UserToken.Equals(userToken)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool InGame(string userToken, ICollection<Game> gameBag)
        {
            foreach(Game g in gameBag)
            {
                if ((g.PlayerOne != null && g.PlayerOne.UserToken.Equals(userToken)) || (g.PlayerTwo != null && g.PlayerTwo.UserToken.Equals(userToken)))
                {
                    return true;
                }
            }
            return false;
        }

        public ScoreInfo PlayWord(string id, PlayInfo play)
        {
            if (!string.IsNullOrEmpty(play.Word.Trim()))
            {
                foreach (Game g in activeGames)
                {
                    if (g.ID.Equals(id))
                    {
                        int score = g.Board.CanBeFormed(play.Word) ? 1 : -1;
                        if (play.UserToken.Equals(g.PlayerOne.UserToken))
                        {
                            g.PlayerOneWords.Add(play.Word, score);
                        }
                        else if (play.UserToken.Equals(g.PlayerTwo.UserToken))
                        {
                            g.PlayerTwoWords.Add(play.Word, score);
                        }
                        else
                        {
                            SetStatus(Forbidden);
                            return null;
                        }
                        SetStatus(OK);
                        return new ScoreInfo() { Score = score };
                    }
                }
                SetStatus(Forbidden);
                return null;
            }
            SetStatus(Forbidden);
            return null;
        }

        public UserInfo Register(RegisterInfo user)
        {
            if (user.Nickname == null || user.Nickname.Trim().Length == 0)
            {
                SetStatus(Forbidden);
                return null;
            }
            UserInfo t = new UserInfo();
            t.UserToken = Guid.NewGuid().ToString();
            Console.WriteLine(t.UserToken);
            users.TryAdd(t.UserToken, user.Nickname);
            SetStatus(Created);
            return t;
        }
    }
}

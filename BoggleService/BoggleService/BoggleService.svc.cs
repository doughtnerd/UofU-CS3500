using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        //private static readonly ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentQueue<dynamic> pendingGames = new ConcurrentQueue<dynamic>();
        //private static readonly ConcurrentDictionary<string, Game> activeGames = new ConcurrentDictionary<string, Game>();
        //private static readonly ConcurrentDictionary<string, Game> completedGames = new ConcurrentDictionary<string, Game>();
        private static readonly HashSet<string> dictionary;
        private static dynamic pendingGame = new ExpandoObject();

        private static string connectionString;

        static BoggleService()
        {
            dictionary = new HashSet<string>();
            string line;
            using (StreamReader file = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    dictionary.Add(line.ToLower().Trim());
                }
            }
            connectionString = ConfigurationManager.ConnectionStrings["BoggleDB"].ConnectionString;
        }

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

        /// <summary>
        /// Handles cancelling a user's registration for a pending game.
        /// </summary>
        /// <param name="user"></param>
        public void CancelJoin(UserInfo user)
        {
            //TODO: Handle checking if the user is in a pending game. If they are, cancel their participation.
            SetStatus(Forbidden);
        }

        /// <summary>
        /// Handles processing and returning of game status data.
        /// </summary>
        /// <param name="id">Id of the game to retrieve data for.</param>
        /// <param name="brief">Whether or not the data should be brief.</param>
        /// <returns>Desired status info as available for the game.</returns>
        public StatusInfo GameStatus(string id, string brief)
        {
            brief = string.IsNullOrEmpty(brief) ? "no" : brief.ToLower();
            Game g = GetGame(id);
            if (g != null)
            {
                if(g.GameState == Game.Status.pending)
                {
                    SetStatus(OK);
                    return new StatusInfo() { GameState = "pending" };
                } else
                {
                    CheckGameComplete(g);
                    int timePassed = (int) (DateTime.Now - g.TimeStarted).TotalSeconds;
                    return new StatusInfo()
                    {
                        GameState = g.GameState.ToString(),
                        Board = g.Board.ToString(),
                        TimeLimit = g.TimeLimit,
                        TimeLeft = timePassed>=g.TimeLimit ? 0 : g.TimeLimit-timePassed,
                        Player1 = new PlayerInfo
                        {
                            //Nickname = users[g.PlayerOne.UserToken],
                            Score = GetScore(g.PlayerOneWords),
                            WordsPlayed = brief.Equals("no") ? CollectWordData(g.PlayerOneWords) : null
                        },
                        Player2 = new PlayerInfo
                        {
                            //Nickname = users[g.PlayerTwo.UserToken],
                            Score = GetScore(g.PlayerTwoWords),
                            WordsPlayed = brief.Equals("no") ? CollectWordData(g.PlayerTwoWords) : null
                        }
                    };
                }
            }
            else
            {
                SetStatus(Forbidden);
                return null;
            }
        }

        //TODO: Implement this, this will handle the basic logic of checking the timelimit against the start time of the game.
        private bool CheckGameComplete(int timeLimit, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        private static WordInfo[] CollectWordData(IDictionary<string, int> dic)
        {
            WordInfo[] arr = new WordInfo[dic.Count];
            int i = 0;
            foreach(KeyValuePair<string, int> pair in dic)
            {
                arr[i] = new WordInfo() { Word = pair.Key, Score = pair.Value };
                i++;
            }
            return arr;
        }

        private static int GetScore(IDictionary<string, int> dic)
        {
            int total = 0;
            foreach (int i in dic.Values)
            {
                total += i;
            }
            return total;
        }

        /// <summary>
        /// Handles joining a pending Boggle game.
        /// </summary>
        /// <param name="user">Data detailing the user token and the time of the match</param>
        /// <returns>Data containing the game's ID.</returns>
        public GameInfo Join(JoinInfo user)
        {
            if (!string.IsNullOrEmpty(user.UserToken) && SQLUtils.TableContains(connectionString, "Users", "UserToken", "'"+user.UserToken+"'") && user.TimeLimit >= 5 && user.TimeLimit <= 120)
            {
                if (!InGame(user.UserToken))
                {
                    if (pendingGame.Player1 != null)
                    {
                        GameInfo info = JoinAsPlayerTwo(user, pendingGame.GameId);
                        pendingGame = new ExpandoObject();
                        if (info == null)
                        {
                            SetStatus(InternalServerError);
                            return null;
                        } else
                        {
                            SetStatus(Created);
                            return info;
                        }
                    } else {
                        GameInfo info = JoinAsPlayerOne(user);
                        if(info == null)
                        {
                            SetStatus(InternalServerError);
                            return null;
                        } else
                        {
                            SetStatus(Accepted);
                            return info;
                        }
                    }
                }
                SetStatus(Conflict);
                return null;
            }
            SetStatus(Forbidden);
            return null;
        }

        private bool InGame(string userToken)
        {
            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            using (conn)
            {
                using (trans)
                {
                    SqlCommand command = new SqlCommand("SELECT GameId from Games where Player1=@token OR Player2=@token", conn, trans);
                    command.Parameters.AddWithValue("@token", userToken);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }

        private GameInfo JoinAsPlayerTwo(JoinInfo user, string gameId)
        {
            int averagedGameTime = 0;
            //int numOfGames = 0;   
            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            using (conn)
            {
                using (trans)
                {
                    SqlCommand getTimeLimit = new SqlCommand("SELECT TimeLimit from Games WHERE GameId=@id");
                    getTimeLimit.Parameters.AddWithValue("@id", gameId);
                    using (SqlDataReader reader = getTimeLimit.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            averagedGameTime = (((int)reader["TimeLimit"]) + user.TimeLimit) / 2;
                            SqlCommand command = new SqlCommand("UPDATE Games SET Player2 = @player2, Board = @board, TimeLimit = @limit, StartTime = @start where GameId = @id", conn, trans);
                            SQLUtils.AddWithValue(command, SQLUtils.BuildMappings("@player2", user.UserToken, "@board", new BoggleBoard().ToString(), "@id", gameId, "@limit", averagedGameTime, "@start", DateTime.Now));
                            int changed = command.ExecuteNonQuery();
                            if (changed != 0)
                            {
                                reader.Close();
                                trans.Commit();
                                return new GameInfo() { GameID = gameId };
                            }
                        }
                    }
                }
            }
            return null;
        }

        private GameInfo JoinAsPlayerOne(JoinInfo user)
        {
            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            SqlCommand command = new SqlCommand("INSERT INTO Games (Player1, Player2 Board, TimeLimit, StartTime) VALUES (@p1, @p2, @board, @limit, @start)", conn, trans);
            SQLUtils.AddWithValue(command, SQLUtils.BuildMappings("@p1", user.UserToken, "@p2", null, "@board", null, "@limit", user.TimeLimit, "@start", null));
            using (conn)
            {
                using (trans)
                {
                    SqlCommand getCount = new SqlCommand("SELECT COUNT(GamId) from Games", conn, trans);
                    using(SqlDataReader reader = getCount.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int count = (int)reader[0] + 1;
                            return new GameInfo() { GameID = count.ToString() };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Handles playing a word to the boggle game
        /// </summary>
        /// <param name="id">ID of the game to submit a word to</param>
        /// <param name="play">Info detailing what player played what word.</param>
        /// <returns>Data containing the score that word had.</returns>
        public ScoreInfo PlayWord(string id, PlayInfo play)
        {
            //
            string word = play.Word.ToLower().Trim();
            if (!string.IsNullOrEmpty(word))
            {
                //TODO: Check if game is active.
                if (false)
                {
                    //TODO:Check if game is complete
                    if (!CheckGameComplete(0, DateTime.Now))
                    {
                    }
                }

                SetStatus(Conflict);
                return null;
            }
            SetStatus(Forbidden);
            return null;
        }

        public int WordScore(string word)
        {
            switch (word.Length)
            {
                case 3:
                    return 1;
                case 4:
                    return 1;
                case 5:
                    return 2;
                case 6:
                    return 3;
                case 7:
                    return 5;
                default:
                    return 11;
            }
        }

        /// <summary>
        /// Registers the user into the database
        /// </summary>
        /// <param name="user">Required registration info.</param>
        /// <returns>The user's user token.</returns>
        public UserInfo Register(RegisterInfo user)
        {
            if (user.Nickname == null || user.Nickname.Trim().Length == 0)
            {
                SetStatus(Forbidden);
                return null;
            }
            UserInfo t = new UserInfo();
            t.UserToken = Guid.NewGuid().ToString();

            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            SqlCommand command = new SqlCommand("insert into Users (UserToken, Nickname) values (@token, @name)", conn, trans);
            SQLUtils.AddWithValue(command, SQLUtils.BuildMappings("@token", t.UserToken, "@name", user.Nickname));
            SQLUtils.ExecuteNonQuery(conn, trans, command, (n) => {
                if (n == 0)
                {
                    SetStatus(InternalServerError);
                    t = null;
                } else
                {
                    SetStatus(Created);
                }
            });
            return t;
        }
    }
}

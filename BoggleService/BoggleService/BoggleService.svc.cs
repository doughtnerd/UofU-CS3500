using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Dynamic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        private static readonly HashSet<string> dictionary;

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
            bool wasfound = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using(SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("select * from Games where Player1 = @id AND Player2 IS NULL", conn, trans);
                    command.Parameters.AddWithValue("@id", user.UserToken);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            wasfound = true;
                        }
                        else
                        {
                            SetStatus(Forbidden);
                        }
                    }
                }

                if (wasfound)
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        SqlCommand command = new SqlCommand("DELETE FROM Games where Player1 = @id AND Player2 IS NULL", conn, trans);
                        command.Parameters.AddWithValue("@id", user.UserToken);
                        int changed = command.ExecuteNonQuery();
                        SetStatus(OK);
                        trans.Commit();
                    }
                }
            }
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
            bool isPending;
            if (TryGameIsPending(id, out isPending))
            {
                if (isPending)
                {
                    SetStatus(OK);
                    return new StatusInfo() { GameState = "pending" };
                }
            } 
            string p1 = "";
            string p2 = "";
            string state = "";
            string board = "";
            DateTime started;
            int limit = 0;
            int left = 0;

            string p1Nickname = "";
            string p2Nickname = "";
            IDictionary<string, int> playerOne = new Dictionary<string, int>();
            IDictionary<string, int> playerTwo = new Dictionary<string, int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Games WHERE GameId = @id", conn, trans);
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            board = (string)reader["Board"];
                            limit = (int)reader["TimeLimit"];
                            started = DateTime.Parse(reader["StartTime"].ToString());
                            left = (int)(DateTime.Now - started).TotalSeconds >= limit ? 0 : limit - (int)(DateTime.Now - started).TotalSeconds;
                            state = left > 0 ? "active" : "completed";
                            p1 = (string)reader["Player1"];
                            p2 = (string)reader["Player2"];
                        } else
                        {
                            SetStatus(Forbidden);
                            return null;
                        }
                    }
                }

                using(SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Words WHERE GameID = @id AND (Player = @p1 OR Player = @p2)", conn, trans);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@p1", p1);
                    command.Parameters.AddWithValue("@p2", p2);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (((string)reader["Player"]).Equals(p1))
                            {
                                playerOne.Add((string)reader["Word"], (int)reader["Score"]);
                            } else
                            {
                                playerTwo.Add((string)reader["Word"], (int)reader["Score"]);
                            }
                        }
                    }
                }
            }

            return new StatusInfo()
            {
                GameState = state,
                Board = board,
                TimeLimit = limit,
                TimeLeft = left,
                Player1 = new PlayerInfo
                {
                    Nickname = GetPlayerNickname(p1),
                    Score = GetScore(playerOne),
                    WordsPlayed = brief.Equals("no") ? CollectWordData(playerOne) : null
                },
                Player2 = new PlayerInfo
                {
                    Nickname = GetPlayerNickname(p2),
                    Score = GetScore(playerTwo),
                    WordsPlayed = brief.Equals("no") ? CollectWordData(playerTwo) : null
                }
            };
        }

        public string GetPlayerNickname(string token)
        {
            string name = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using(SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("SELECT Nickname FROM Users WHERE UserToken = @token", conn, trans);
                    command.Parameters.AddWithValue("@token", token);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            name = (string)reader[0];
                        }
                    }
                }
            }
            return name;
        }

        private bool TryGameIs(string gameId, Func<int, DateTime, bool> check, out bool passedCheck)
        {
            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            using (conn)
            {
                using (trans)
                {
                    SqlCommand command = new SqlCommand("SELECT TimeLimit, StartTime FROM Games WHERE GameId = @id", conn, trans);
                    command.Parameters.AddWithValue("@id", gameId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            DateTime start = DateTime.Parse(reader["StartTime"].ToString());
                            int limit = (int)reader["TimeLimit"];
                            passedCheck = check(limit, start);
                            return true;
                        }
                    }
                }
            }
            passedCheck = false;
            return false;
        }

        private bool TryGameIsPending(string gameId, out bool isPending)
        {
            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            using (conn)
            {
                using (trans)
                {
                    SqlCommand command = new SqlCommand("SELECT TimeLimit, StartTime FROM Games WHERE GameId = @id", conn, trans);
                    command.Parameters.AddWithValue("@id", gameId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            isPending = reader["StartTime"].GetType() == typeof(DBNull);
                            return true;
                        }
                    }
                }
            }
            isPending = false;
            return false;
        }

        private bool TryGameIsComplete(string gameId, out bool isComplete)
        {
            return TryGameIs(gameId, GameIsComplete, out isComplete);
        }

        private bool TryGameIsActive(string gameId, out bool isActive)
        {
            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            using (conn)
            {
                using (trans)
                {
                    SqlCommand command = new SqlCommand("SELECT TimeLimit, StartTime FROM Games WHERE GameId = @id AND StartTime IS NOT NULL", conn, trans);
                    command.Parameters.AddWithValue("@id", gameId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            DateTime startTime = DateTime.Parse(reader["StartTime"].ToString());
                            int timeLimit = (int)reader["TimeLimit"];
                            isActive = ((DateTime.Now - startTime).TotalSeconds < timeLimit);
                            return true;
                        }
                        isActive = false;
                        return true;

                    }
                }
            }
            isActive = false;
            return false;
        }

        private bool GameIsComplete(int timeLimit, DateTime startTime)
        {
            if (startTime != null)
            {
                if ((DateTime.Now - startTime).TotalSeconds >= timeLimit)
                {
                    return true;
                }
            }
            return false;
        }

        private bool GameIsActive(int timeLimit, DateTime startTime)
        {
            if (startTime!=null && (DateTime.Now - startTime).TotalSeconds < timeLimit)
            {
                return true;
            }
            return false;
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
            if (!string.IsNullOrEmpty(user.UserToken) && SQLUtils.TableContains(connectionString, "Users", "UserToken", user.UserToken) && user.TimeLimit >= 5 && user.TimeLimit <= 120)
            {
                if (!InGame(user.UserToken))
                {
                    string pendingId;
                    if (IsPendingGame(out pendingId))
                    {
                        if (!Player1Missing(pendingId))
                        {
                            GameInfo info = JoinAsPlayerTwo(user, pendingId);
                            return info;
                        }
                        else
                        {
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                using (SqlTransaction trans = conn.BeginTransaction())
                                {
                                    SqlCommand command = new SqlCommand("Update Games Set Player1 = @p1, TimeLimit = @limit", conn, trans);
                                    SQLUtils.AddWithValue(command, SQLUtils.BuildMappings("@p1", user.UserToken, "@limit", user.TimeLimit));
                                }
                            }
                            SetStatus(Accepted);
                            return new GameInfo() { GameID = pendingId };
                        }
                    }
                    else
                    {
                        GameInfo info = JoinAsPlayerOne(user);
                        return info;
                    }
                }
                SetStatus(Conflict);
                return null;
            }
            SetStatus(Forbidden);
            return null;
        }

        private bool Player1Missing(string gameId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans= conn.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("SELECT GameId from Games where GameId=@id AND Player1 IS NULL", conn, trans);
                    command.Parameters.AddWithValue("@id", gameId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }

        private bool IsPendingGame(out string pendingId)
        {
            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            using (conn)
            {
                using (trans)
                {
                    SqlCommand command = new SqlCommand("SELECT GameId from Games where Player2 IS NULL", conn, trans);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            pendingId = ((int)reader["GameId"]).ToString();
                            return true;
                        }
                        pendingId = null;
                        return false;
                    }
                }
            }
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand getTimeLimit = new SqlCommand("SELECT TimeLimit from Games WHERE GameId=@id", conn, trans);
                    getTimeLimit.Parameters.AddWithValue("@id", gameId);
                    using (SqlDataReader reader = getTimeLimit.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            averagedGameTime = (((int)reader["TimeLimit"]) + user.TimeLimit) / 2;
                        }
                    }
                }

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("UPDATE Games SET Player2 = @player2, Board = @board, TimeLimit = @limit, StartTime = @start where GameId = @id", conn, trans);
                    SQLUtils.AddWithValue(command, SQLUtils.BuildMappings("@player2", user.UserToken, "@board", new BoggleBoard().ToString(), "@id", gameId, "@limit", averagedGameTime, "@start", DateTime.Now.ToString()));
                    int changed = command.ExecuteNonQuery();
                    trans.Commit();
                }
            }
            SetStatus(Created);
            return new GameInfo() { GameID = gameId };
        }

        private GameInfo JoinAsPlayerOne(JoinInfo user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Games (Player1, Player2, Board, TimeLimit, StartTime) output INSERTED.GameId VALUES ( @p1, null, null, @limit, null)", conn, trans);
                    SQLUtils.AddWithValue(command, SQLUtils.BuildMappings("@p1", user.UserToken, "@limit", user.TimeLimit));
                    int id = (int) command.ExecuteScalar();
                    SetStatus(Accepted);
                    trans.Commit();
                    return new GameInfo() { GameID = id.ToString() };
                }
            }
            
        }

        /// <summary>
        /// Handles playing a word to the boggle game
        /// </summary>
        /// <param name="id">ID of the game to submit a word to</param>
        /// <param name="play">Info detailing what player played what word.</param>
        /// <returns>Data containing the score that word had.</returns>
        public ScoreInfo PlayWord(string id, PlayInfo play)
        {
            if (!string.IsNullOrEmpty(play.Word) && !string.IsNullOrEmpty(play.UserToken) && IsInGame(id, play.UserToken))//SQLUtils.TableContains(connectionString, "Users", "UserToken", play.UserToken))
            {
                string word = play.Word.ToLower().Trim();
                bool active;
                if (TryGameIsActive(id, out active))
                {
                    if (SQLUtils.TableContains(connectionString, "Games", "GameId", id) && active)
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            int score = 0;
                            string board = "";
                            using(SqlTransaction trans = conn.BeginTransaction()){
                                SqlCommand command = new SqlCommand("SELECT Board FROM Games WHERE GameId = @id", conn, trans);
                                command.Parameters.AddWithValue("@id", id);
                                using(SqlDataReader reader = command.ExecuteReader())
                                {
                                    reader.Read();
                                    board = (string)reader["Board"];
                                }
                            }


                            using(SqlTransaction trans = conn.BeginTransaction())
                            {
                                SqlCommand command = new SqlCommand("SELECT Word FROM Words where GameID = @id", conn, trans);
                                command.Parameters.AddWithValue("@id", id);
                                using(SqlDataReader reader = command.ExecuteReader())
                                {
                                    bool flag = true;
                                    while (reader.Read())
                                    {
                                        string current = (string)reader["Word"];
                                        if (current.Equals(word))
                                        {
                                            flag = false;
                                        }
                                    }
                                    score = flag ? WordScore(board, word) : 0;
                                }
                            }

                            using (SqlTransaction trans = conn.BeginTransaction())
                            {
                                SqlCommand command = new SqlCommand("INSERT INTO Words (Word, GameID, Player, Score) VALUES (@word, @id, @player, @score)", conn, trans);
                                SQLUtils.AddWithValue(command, SQLUtils.BuildMappings("@word", word, "@id", id, "@player", play.UserToken, "@score", score));
                                int changed = command.ExecuteNonQuery();
                                trans.Commit();
                            }
                            SetStatus(OK);
                            return new ScoreInfo() { Score = score };
                        }
                    }
                }

                SetStatus(Conflict);
                return null;
            }
            SetStatus(Forbidden);
            return null;
        }

        public bool IsInGame(string gameId, string user)
        {
            SqlConnection conn;
            SqlTransaction trans = SQLUtils.BeginTransaction(connectionString, out conn);
            using (conn)
            {
                using (trans)
                {
                    SqlCommand command = new SqlCommand("SELECT GameId from Games where GameId=@id AND (Player1=@token OR Player2=@token)", conn, trans);
                    command.Parameters.AddWithValue("@token", user);
                    command.Parameters.AddWithValue("@id", gameId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }

        public int WordScore(string board, string word)
        {
            if (word.Length < 3)
            {
                return 0;
            }
            BoggleBoard b = new BoggleBoard(board);
            if (!b.CanBeFormed(word) || !dictionary.Contains(word))
            {
                return -1;
            }
            switch (word.Length)
            {
                case 3:
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

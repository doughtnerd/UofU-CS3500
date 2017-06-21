﻿//Nathan Reeves
//CS3500 4/8/17
//All tests pass and the server is now set up to run on the database BoggleDB for PS10.
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Net;
//using System.ServiceModel.Web;
using System.Threading.Tasks;
//using System.Timers;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService 
    {

        //Dictionary
        private readonly static HashSet<string> words = new HashSet<string>(File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt")));
        //Database
        private static string BoggleDB;

        static BoggleService()
        {
            //connection string set up in web.config
            string dbFolder = System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            BoggleDB = String.Format(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {0}\BoggleDB.mdf; Integrated Security = True", dbFolder);
            
        }
        /// <summary>
        /// Returns a Stream version of index.html.
        /// </summary>
        /// <returns></returns>
        public Stream API(out HttpStatusCode status)
        {
            status = OK;// SetStatus(OK);
            //WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "index.html");
        }

        /// <summary>
        /// Creates a user as long as the name doesn't already contain a userToken and the name is valid.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public UserInfo createUser(UserInfo u, out HttpStatusCode status)
        {
            if ((u.Nickname == null) || u.Nickname.Equals(""))
            {
                status = Forbidden;// SetStatus(Forbidden);
                return null;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(BoggleDB))
                {
                    // open connection
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        //add new user command
                        using (SqlCommand command =
                            new SqlCommand("insert into Users (UserID, Nickname) values(@UserID, @Nickname)",
                                            conn,
                                            trans))
                        {
                            string usertoken = Guid.NewGuid().ToString();

                            command.Parameters.AddWithValue("@UserID", usertoken);
                            command.Parameters.AddWithValue("@Nickname", u.Nickname.Trim());

                            command.ExecuteNonQuery();
                            //SetStatus(Created); <- Doesn't seem necessary.

                            trans.Commit();
                            u.UserToken = usertoken;
                            //SetStatus(Created);
                            status = Created;
                            return u;
                        }
                    }
                }
            }


        }
        /// <summary>
        /// Helper method to get name from a userID.
        /// </summary>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        string getNickname(string usertoken)
        {
            using (SqlConnection conn = new SqlConnection(BoggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand command = new SqlCommand("select * from Users where UserID = @UserID", conn, trans))
                    {
                        command.Parameters.AddWithValue("@UserID", usertoken);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (string)reader["Nickname"];
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Puts the user into a game, creates a new game if all games are filled. Returns the GameID of the game.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public UserInfo joinGame(UserInfo u, out HttpStatusCode status)
        {
            //contains a GameID to return
            UserInfo game;
            if (u.TimeLimit < 6 || u.TimeLimit > 120)
            {
                status = Forbidden;// SetStatus(Forbidden);
                return null;
            }
            using (SqlConnection conn = new SqlConnection(BoggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    //get the most recent game
                    using (SqlCommand useridcommand = new SqlCommand("select UserID from Users where UserID = @UserID", conn, trans))
                    {
                        useridcommand.Parameters.AddWithValue("@UserID", u.UserToken);
                        //Makes sure the user is registered
                        using (SqlDataReader reader = useridcommand.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                status = Forbidden;// SetStatus(Forbidden);
                                reader.Close();
                                trans.Commit();
                                return null;
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand("select top 1 * from Games order by GameID DESC", conn, trans))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();

                            //check if there is a player1 already and that player2 is null
                            if ((reader["Player1"] is System.DBNull) || !(reader["Player2"] is System.DBNull))
                            {
                                //add a new game
                                using (SqlCommand newcommand = new SqlCommand("insert into Games (Player1, TimeLimit) output inserted.GameID values(@UserID, @TimeLimit)", conn, trans))
                                {
                                    newcommand.Parameters.AddWithValue("@UserID", u.UserToken);
                                    newcommand.Parameters.AddWithValue("@TimeLimit", u.TimeLimit);
                                    reader.Close();
                                    //gets GameID
                                    string GameID = newcommand.ExecuteScalar().ToString();


                                    trans.Commit();

                                    game = new UserInfo();
                                    status = Accepted;// SetStatus(Accepted);
                                    game.GameID = GameID;
                                    return game;
                                }
                            }
                            else
                            {

                                // add Player2
                                using (SqlCommand newcommand = new SqlCommand("update Games set Player2 = @UserID , TimeLimit = @TimeLimit , Board = @Board , StartTime = @StartTime where GameID = @GameID", conn, trans))
                                {

                                    //if player1 already in pending game set status to conflict
                                    if ((string)reader["Player1"] == (u.UserToken))
                                    {
                                        game = new UserInfo();
                                        status = Conflict; //SetStatus(Conflict);
                                        return game;
                                    }
                                    int gameID = (int)reader["GameID"];
                                    int timelim = ((int)reader["TimeLimit"] + u.TimeLimit) / 2;
                                    newcommand.Parameters.AddWithValue("@UserID", u.UserToken);
                                    newcommand.Parameters.AddWithValue("@TimeLimit", timelim);
                                    newcommand.Parameters.AddWithValue("@GameID", gameID);
                                    newcommand.Parameters.AddWithValue("@Board", new BoggleBoard().ToString());
                                    newcommand.Parameters.AddWithValue("@StartTime", DateTime.Now);

                                    reader.Close();
                                    newcommand.ExecuteNonQuery();

                                    trans.Commit();

                                    game = new UserInfo();
                                    status = Created;// SetStatus(Created);
                                    game.GameID = gameID + "";
                                    return game;
                                }
                            }

                        }


                    }
                }
            }

        }
        /// <summary>
        /// Helper method that returns a game object that it gets from the database using the GameID.
        /// </summary>
        /// <param name="GameID"></param>
        /// <returns></returns>
        private Game fromGameDB(string GameID)
        {
            using (SqlConnection conn = new SqlConnection(BoggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand command = new SqlCommand("select * from Games where GameID=@GameID", conn, trans))
                    {
                        command.Parameters.AddWithValue("@GameID", GameID);

                        //reads a game from the Games table that matches the ID
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return null;
                            }
                            reader.Read();
                            Game game = new Game();
                            game.Player1 = (string)reader["Player1"];
                            if (!(reader["Player2"] is System.DBNull))
                            {
                                game.Player2 = (string)reader["Player2"];
                                game.Board = (string)reader["Board"];
                                game.TimeLimit = (int)reader["TimeLimit"];
                                game.StartTime = (DateTime)reader["StartTime"];
                            }
                            return game;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Returns a GameStatus from a specific game. 
        /// </summary>
        /// <param name="GameID"></param>
        /// <param name="Brief"></param>
        /// <returns></returns>
        public GameS getGame(string GameID, string Brief, out HttpStatusCode status)
        {
            GameS gameStatus = new GameS();
            bool brief = false;
            if (Brief.Equals("yes"))
            {
                brief = true;
            }
            int gameid;
            if (!int.TryParse(GameID, out gameid))
            {
                status = Forbidden;// SetStatus(Forbidden);
                return null;
            }
            //Get the game object using the helper method.
            Game thisGame = fromGameDB(GameID);
            if (thisGame == null)
            {
                status = Forbidden;// SetStatus(Forbidden);
                return gameStatus;
            }
            if (thisGame.Player2 == null)
            {
                status = OK;// SetStatus(OK);
                gameStatus.GameState = "pending";
                return gameStatus;
            }
            else if (thisGame.StartTime.AddSeconds(thisGame.TimeLimit) < DateTime.Now)
            {
                gameStatus.GameState = "completed";
                gameStatus.TimeLeft = 0;
            }
            else
            {
                gameStatus.GameState = "active";
                gameStatus.TimeLeft = (thisGame.StartTime.AddSeconds(thisGame.TimeLimit) - DateTime.Now).Seconds + (thisGame.StartTime.AddSeconds(thisGame.TimeLimit) - DateTime.Now).Minutes * 60;
            }


            gameStatus.Board = thisGame.Board;
            gameStatus.TimeLimit = thisGame.TimeLimit;

            Player p1 = new Player();
            Player p2 = new Player();
            p1.Nickname = getNickname(thisGame.Player1);
            p1.Score = 0;
            p2.Nickname = getNickname(thisGame.Player2);
            p2.Score = 0;
            var words = getWords(gameid, thisGame.Player1);
            var words2 = getWords(gameid, thisGame.Player2);
            //Player1 calculate score and words played
            List<Words> WordsPlayed = new List<Words>();
            foreach (Words word in words)
            {
                Words curr = new Words();
                curr.Word = word.Word;
                curr.Score = word.Score;
                p1.Score += curr.Score;
                WordsPlayed.Add(curr);
            }
            if (gameStatus.GameState == "completed" && !brief)
            {
                p1.WordsPlayed = WordsPlayed;
            }

            //Player2 calculate score and words played
            WordsPlayed = new List<Words>();
            foreach (Words word in words2)
            {
                Words curr = new Words();
                curr.Word = word.Word;
                curr.Score = word.Score;
                p2.Score += curr.Score;
                WordsPlayed.Add(curr);
            }
            if (gameStatus.GameState == "completed" && !brief)
            {
                p2.WordsPlayed = WordsPlayed;
            }
            gameStatus.Player1 = p1;
            gameStatus.Player2 = p2;

            status = OK; //TODO: SetStatus was missing here, without reading the entire code, this seems to be the correct code to send. I'll review later.
            return gameStatus;

        }
        /// <summary>
        /// Returns a list of words that a player has played in a specific game.
        /// </summary>
        /// <param name="GameID"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        private LinkedList<Words> getWords(int GameID, string Player)
        {
            using (SqlConnection conn = new SqlConnection(BoggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand command = new SqlCommand("select * from Words where GameID=@GameID and Player=@Player", conn, trans))
                    {
                        command.Parameters.AddWithValue("@GameID", GameID);
                        command.Parameters.AddWithValue("@Player", Player);

                        //reads every word/line that a player has played
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            LinkedList<Words> words = new LinkedList<Words>();

                            while (reader.Read())
                            {
                                Words word = new Words();
                                word.ID = (int)reader["Id"];
                                word.Word = (string)reader["Word"];
                                word.GameID = (int)reader["GameID"];
                                word.Player = (string)reader["Player"];
                                word.Score = (int)reader["Score"];
                                words.AddLast(word);
                            }
                            return words;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Helper method to return the score of a played word on a Boggle board.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        private int Score(string word, BoggleBoard board)
        {
            if (word.Length < 3)
            {
                return 0;
            }

            if (!words.Contains(word) || !board.CanBeFormed(word))
            {
                return -1;
            }

            else if (word.Length > 3 && word.Length <= 6)
            {
                return word.Length - 3;
            }
            else if (word.Length == 7)
            {
                return 5;
            }
            else if (word.Length > 7)
            {
                return 11;
            }
            else
            {
                return 1;
            }

        }
        /// <summary>
        /// Posts a word to a given game and returns a score;
        /// </summary>
        /// <param name="u"></param>
        /// <param name="GameID"></param>
        /// <returns></returns>
        public ScoreInfo postWord(UserInfo u, string GameID, out HttpStatusCode status)
        {
            int gameid;
            //gameIDs are auto-generated ints
            int.TryParse(GameID, out gameid);

            ScoreInfo sc = new ScoreInfo(); //score object

            //get the game that user is trying to post to
            Game game = fromGameDB(GameID);
            //null parameters
            if (game == null || u.Word == null || u.Word.Length == 0 || !(game.Player1 == u.UserToken || game.Player2 == u.UserToken))
            {

                status = Forbidden;// SetStatus(Forbidden);
                return null;
            }
            //if game is over, conflict
            if ((game.Player2 == null) || game.StartTime.AddSeconds(game.TimeLimit) < DateTime.Now)
            {
                status = Conflict;// SetStatus(Conflict);
                return null;
            }
            //get words played
            LinkedList<Words> played = getWords(gameid, u.UserToken);
            //calculate score for this given word
            int score = Score(u.Word, new BoggleBoard(game.Board));

            using (SqlConnection conn = new SqlConnection(BoggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    //add new word into database
                    using (SqlCommand command =
                    new SqlCommand("insert into Words (GameID, Player, Score, Word) values(@GameID, @Player, @Score, @Word)",
                                    conn,
                                    trans))
                    {
                        command.Parameters.AddWithValue("@GameID", GameID);
                        command.Parameters.AddWithValue("@Player", u.UserToken);
                        command.Parameters.AddWithValue("@Score", score);
                        command.Parameters.AddWithValue("@Word", u.Word);

                        if (command.ExecuteNonQuery() == 1)
                        {

                        }
                        else
                        {
                            status = Conflict;// SetStatus(Conflict);
                            return null;
                        }
                    }
                    trans.Commit();
                }

                status = OK;// SetStatus(OK);
                sc.Score = score;
                return sc;
            }

        }
        /// <summary>
        /// Cancels a game that a user has created. User must be a part of the game and the game shouldn't have a Player2.
        /// </summary>
        /// <param name="u"></param>
        public void cancel(UserInfo u, out HttpStatusCode status)
        {
            using (SqlConnection conn = new SqlConnection(BoggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    //get the most recent game from the database
                    using (SqlCommand command = new SqlCommand("select top 1 * from Games order by GameID DESC", conn, trans))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            //user must be one of the players
                            if (!((reader["Player2"].Equals(u.UserToken)) || (reader["Player1"].Equals(u.UserToken))))
                            {
                                status = Forbidden;// SetStatus(Forbidden);
                                return;
                            }
                            //must be a Player1 and Player2 is null.
                            if (!(reader["Player1"] is System.DBNull) && (reader["Player2"] is System.DBNull))
                            {
                                using (SqlCommand newcommand = new SqlCommand("delete from Games where GameID = @GameID", conn, trans))
                                {
                                    newcommand.Parameters.AddWithValue("@GameID", (int)reader["GameID"]);
                                    reader.Close();
                                    newcommand.ExecuteNonQuery();
                                    status = OK;// SetStatus(OK);
                                    trans.Commit();

                                }
                            }
                            else
                            {
                                status = Forbidden;// SetStatus(Forbidden);
                            }
                        }
                    }
                }
            }

        }

    }
}

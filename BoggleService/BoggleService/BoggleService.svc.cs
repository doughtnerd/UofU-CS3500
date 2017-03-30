//Nathan Reeves Haoze Zhang
//CS3500 3/30/17
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Web.UI;
//using System.Timers;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        //set of Users
        private readonly static Dictionary<string, string> users = new Dictionary<string, string>();
        //set of gamestates
        private readonly static List<Game> games = new List<Game>();
        //dictionary
        private readonly static HashSet<string> words = new HashSet<string>(File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt")));
        //game timer
        private static Stopwatch time = new Stopwatch();

        //Timer time = new Timer();
        
        /// <summary>
        /// The most recent call to SetStatus determines the response code used when
        /// an http response is sent.
        /// </summary>
        /// <param name="status"></param>
        private static void SetStatus(HttpStatusCode status)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = status;
        }
         public BoggleService()
         {
            // for debug
            //System.Diagnostics.Debug.WriteLine();
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
        //creates a user as long as the name doesn't already contain a userToken
        public UserInfo createUser(UserInfo u)
        {
            if (u.Nickname.Trim() != null)
            {
                u.UserToken = Guid.NewGuid().ToString();
                users.Add(u.UserToken, u.Nickname);
                SetStatus(Created);
                return u;
            }
            else
            {
                SetStatus(Forbidden);
                return null;
            }
            
        }
       
        public Game joinGame(UserInfo u)
        {

            //represents this player
            Player P = new Player();
            P.WordsPlayed = new List<AWord>();
            string nickn;
            if(users.TryGetValue(u.UserToken, out nickn))
            {
                P.Nickname = nickn;
                P.UserToken = u.UserToken;
                Game mostRecent;

                if (games.Count <= 0)
                {
                    mostRecent = new Game();
                    mostRecent.GameState = "pending";
                    mostRecent.GameID = 1;
                    games.Add(mostRecent);
                }

                //gets last game
                mostRecent = games[games.Count - 1];
                if (mostRecent.Player1 == null)
                {
                    SetStatus(Accepted);
                    mostRecent.Player1 = P;
                    mostRecent.TimeLimit = u.TimeLimit;
                    games[games.Count - 1] = mostRecent;
                }
                else
                {
                    //Player2 joins game

                    time.Start();
                    mostRecent.Player2 = P;
                    u.WordsPlayed = new List<AWord>();
                    mostRecent.GameState = "active";
                    mostRecent.TimeLimit = (u.TimeLimit + mostRecent.TimeLimit) / 2;
                    BoggleBoard b = new BoggleBoard();
                    mostRecent.Board = b.ToString();
                    games[games.Count - 1] = mostRecent;
                    SetStatus(Created);



                    Game addEmpty = new Game();
                    addEmpty.GameState = "pending";
                    addEmpty.GameID = games.Count + 1;
                    games.Add(addEmpty);
                }
                return mostRecent;
            }
            else
            {
                SetStatus(Forbidden);
                return null;
            }
            



            
        }
        public Game getGame(string GameID, string Brief)
        {
            bool brief = false;
            if (Brief == "yes")
            { 
                brief = true;
                }
            int gameid;
            int.TryParse(GameID, out gameid);
            System.Diagnostics.Debug.WriteLine(games[gameid - 1].Player1.Nickname + "  " + GameID + "getGame" + " " + games[gameid - 1].Player1.WordsPlayed);
            SetStatus(OK);
            long timeleft = games[gameid - 1].TimeLimit - time.ElapsedMilliseconds / 1000;
            if (timeleft > 0)
                games[gameid - 1].TimeLeft = games[gameid - 1].TimeLimit - time.ElapsedMilliseconds / 1000;
            else
            {
                games[gameid - 1].TimeLeft = 0;
                games[gameid- 1].GameState = "completed";
                time.Reset();
            }
            if(brief)
            {
                dynamic briefGame = new ExpandoObject();
                briefGame.GameState = games[gameid - 1].GameState;
                briefGame.Player1 = games[gameid - 1].Player1;
                briefGame.Player2 = games[gameid - 1].Player2;
                briefGame.TimeLeft = games[gameid - 1].TimeLeft;
                return briefGame;
            }
            else
                return games[gameid - 1];
        }
        //assume UserInfo has a Word and userToken
        public S postWord(UserInfo u, string GameID)
        {
            int gameid;
            int.TryParse(GameID, out gameid);
            System.Diagnostics.Debug.WriteLine(GameID);
            S sc = new S(); //score object
            sc.Score = 60;
            Game thisGame = games[gameid-1];
            AWord word = new AWord();
            u.Word = u.Word.ToUpper();
            word.Word = u.Word;
            if (!thisGame.GameState.Equals("active"))
            {
                SetStatus(Conflict);
            }
            //the player must be player 1 or 2
            bool p1;//true for player 1 posting word, false for player 2 posting word
            if(thisGame.Player1.UserToken.Equals(u.UserToken) || thisGame.Player2.UserToken.Equals(u.UserToken))
            {
                p1 = thisGame.Player1.UserToken.Equals(u.UserToken);
                //create a boggleBoard
                BoggleBoard b = new BoggleBoard(thisGame.Board);
                //check that word can be played on board and is in dictionary
                if (b.CanBeFormed(u.Word) && words.Contains(u.Word))
                {
                    int value;
                    if (u.Word.Length > 3)
                        value = u.Word.Length - 2;
                    else
                        value = 1;
                    sc.Score = value;
                    word.Score = value;
                    if (p1)
                    {
                        thisGame.Player1.Score+=value;
                        thisGame.Player1.WordsPlayed.Add(word);
                    }
                    else
                    {
                        thisGame.Player2.Score+=value;
                        thisGame.Player2.WordsPlayed.Add(word);
                    }
                }
                else
                {
                    sc.Score = 0;
                    word.Score = 0;
                    if (p1)
                    {
                        thisGame.Player1.WordsPlayed.Add(word);
                    }
                    else
                    {
                        thisGame.Player2.WordsPlayed.Add(word);
                    }
                }
                SetStatus(OK);
                return sc;
                

            }
            else
            {
                SetStatus(Forbidden);
            }
            sc.Score = 0;
            return sc;
        }
        public void cancel(UserInfo u)
        {
            games.RemoveAt(games.Count - 1);
            SetStatus(OK);
        }
        
    }
}

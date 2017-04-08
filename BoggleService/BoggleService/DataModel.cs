using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boggle
{
    public class UserInfo
    {
        public string Nickname { get; set; }
        public string UserToken { get; set; }
        public int TimeLimit { get; set; }
        public string GameID { get; set; }
        public string Word { get;  set;}
        public int Score { get; set; }
        public List<AWord> WordsPlayed { get; set; }
    }
    public class Game
    {
        public string GameState { get; set; }
        public string Board { get; set; }
        public int GameID { get; set; }
        public int TimeLimit { get; set; }
        public int TimeLeft { get; set; }
        public DateTime StartTime { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
    }
    public class GameS
    {
        public string GameState { get; set; }
        public string Board { get; set; }
        public int TimeLimit { get; set; }
        public int? TimeLeft { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

    }
    public class AWord
    {
        public string Word { get; set; }
        public int Score { get; set; }
    }
    public class Player
    {
        public string ToString()
        {
            return UserToken;
        }
        public string Nickname { get; set; }
        public string UserToken { get; set; }
        public int TimeLimit { get; set; }
        public int Score { get; set; }
        public List<Words> WordsPlayed { get; set; }
    }
    public class Words
    {
        public int ID { get; set; }
        public string Word { get; set; }
        public int GameID { get; set; }
        public string Player { get; set; }
        public int Score { get; set; }
    }
    public class S
    {
        public int Score { get; set; }
    }
}
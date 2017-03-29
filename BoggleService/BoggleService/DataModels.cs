using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boggle
{
    public class DataModels
    {
        public class RegisterInfo
        {
            public string Nickname { get; set; }
        }

        public class UserInfo
        {
            public string UserToken { get; set; }
        }

        public class JoinInfo
        {
            public string UserToken { get; set; }
            public int TimeLimit { get; set; }
        }

        public class Game
        {
            public string ID { get; set; }
            public Status GameState { get; set; }
            public JoinInfo PlayerOne { get; set; }
            public JoinInfo PlayerTwo { get; set; }
            public int TimeLimit { get; set; }
            public BoggleBoard Board { get; set; }
            public IDictionary<string, int> PlayerOneWords { get; private set; }
            public IDictionary<string, int> PlayerTwoWords { get; private set; }

            public Game()
            {
                this.PlayerOneWords = new Dictionary<string, int>();
                this.PlayerTwoWords = new Dictionary<string, int>();
            }

            public enum Status
            {
                pending,
                active,
                completed
            }
        }

        public class GameInfo
        {
            public string GameID { get; set; }
        }

        public class ScoreInfo
        {
            public int Score { get; set; }
        }

        public class PlayInfo
        {
            public string UserToken { get; set; }
            public string Word { get; set; }
        }
    }
}
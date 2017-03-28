using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boggle
{
    public class DataModels
    {
        public class User
        {
            public string Nickname { get; set; }
        }

        public class Token
        {
            public string UserToken { get; set; }
        }

        public class JoinInfo
        {
            public string UserToken { get; set; }
            public int TimeLimit { get; set; }
        }

        public class GameStatus
        {

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
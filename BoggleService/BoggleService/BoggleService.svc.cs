using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using static Boggle.DataModels;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
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

        public void CancelJoin(RegisterInfo user)
        {
            //TODO:Check if user token is valid;
            //TODO:Ensure player is actually in game.

            //SetStatus(OK);
        }

        public GameStatus GameStatus(int id)
        {
            throw new NotImplementedException();
        }

        public GameInfo Join(JoinInfo user)
        {
            //TODO:Check if user token is valid in the database.
            //TODO:Check if user is currently in a game.
            //TODO:If handle game creation/joining logic.
            //TODO:Calculate average game time.
            GameInfo info = new GameInfo();
            info.GameID = "10"; //TODO: Change to the actual game ID.
            return info;
        }

        public ScoreInfo PlayWord(int id, PlayInfo user)
        {
            //TODO:Ensure word played is not null or empty after being trimmed.
            //TODO:Ensure id is valid
            //TODO:Ensure user is actually in the specified game.
            //TODO:Check if the game is currently active.
            //TODO:Reply with score data.
            return null; //TODO:Change
        }

        public UserInfo Register(RegisterInfo user)
        {
            if (string.IsNullOrEmpty(user.Nickname))
            {
                SetStatus(Forbidden);
                return null;
            }
            //TODO: Store user and usertoken.
            UserInfo t = new UserInfo();
            t.UserToken = Guid.NewGuid().ToString();
            return t;
        }
    }
}

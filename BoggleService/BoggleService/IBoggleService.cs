using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Boggle
{
    [ServiceContract]
    public interface IBoggleService
    {
        /// <summary>
        /// Sends back index.html as the response body.
        /// </summary>
        [WebGet(UriTemplate = "/api")]
        Stream API();

        [WebInvoke(Method = "POST", UriTemplate = "/users")]
        UserInfo createUser(UserInfo u);

        [WebInvoke(Method = "POST", UriTemplate = "/games")]
        UserInfo joinGame(UserInfo u);

        [WebGet(UriTemplate = "/games/{GameID}?brief={brief}")]
        GameS getGame(string GameID, string brief);

        [WebInvoke(Method = "PUT", UriTemplate = "/games/{GameID}")]
        S postWord(UserInfo u, string GameID);

        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        void cancel(UserInfo u);
    }
}

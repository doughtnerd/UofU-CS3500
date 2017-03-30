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
        Game joinGame(UserInfo u);

        [WebGet(UriTemplate = "/games/{GameID}?Brief={Brief}")]
        Game getGame(string GameID, string Brief);

        [WebInvoke(Method = "PUT", UriTemplate = "/games/{GameID}")]
        S postWord(UserInfo u, string GameID);

        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        void cancel(UserInfo u);
    }
}

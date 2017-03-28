using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using static Boggle.DataModels;

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
        UserInfo Register(RegisterInfo user);

        [WebInvoke(Method = "POST", UriTemplate = "/games")]
        GameInfo Join(JoinInfo user);

        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        void CancelJoin(RegisterInfo user);

        [WebInvoke(Method = "PUT", UriTemplate = "/games/{id}")]
        ScoreInfo PlayWord(int id, PlayInfo user);

        [WebGet(UriTemplate = "/games/{id}")]
        IDictionary<string, object> GameStatus(int id);
    }
}

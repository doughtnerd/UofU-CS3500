using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoggleClient
{
    public static class RestUtil
    {
        //public delegate void Callback(HttpResponseMessage response);

        /// <summary>
        /// Creates an HttpClient for communicating with the server.
        /// </summary>
        private static HttpClient CreateClient(string baseAddress)
        {
            // Create a client whose base address is the GitHub server
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);

            // Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            // There is more client configuration to do, depending on the request.
            return client;
        }

        public static async void MakeRequest(string baseURL, RequestType type, string requestExtension, dynamic requestData, Action<HttpResponseMessage> callback, CancellationToken token)
        {
            try
            {
                using (HttpClient client = CreateClient(baseURL))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    switch (type)
                    {
                        case RequestType.POST:
                            {
                                HttpResponseMessage response = await client.PostAsync(requestExtension, content, token);
                                callback(response);
                                break;
                            }
                        case RequestType.GET:
                            {
                                HttpResponseMessage response = await client.GetAsync(requestExtension + "/" + content, token);
                                callback(response);
                                break;
                            }
                        case RequestType.PUT:
                            {
                                HttpResponseMessage response = await client.PutAsync(requestExtension, content, token);
                                callback(response);
                                break;
                            }
                    }
                }
            }
            catch (Exception e)
            {
                if (e is TaskCanceledException)
                {
                    Console.WriteLine("Request Cancelled");
                }
                else
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
        }

        public static dynamic GetResponseData(HttpResponseMessage response)
        {
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic data = JsonConvert.DeserializeObject(result);
            return data;
        }

        static void HandleResponse(HttpResponseMessage response, Action<dynamic> onSuccess)
        {
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                dynamic data = JsonConvert.DeserializeObject(result);
                onSuccess(data);
            }
            else
            {
                Console.WriteLine("Status:" + response.StatusCode);
                Console.WriteLine("Reason:" + response.ReasonPhrase);
            }
        }

        public enum RequestType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}

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
    public class BoggleController
    {

        IBoggleView view;
        private CancellationTokenSource tokenSource;


        public BoggleController(IBoggleView view)
        {
            this.view = view;
            tokenSource = new CancellationTokenSource();
        }


        public static async void MakeRequest(string baseURL, RequestType type, string requestExtension, dynamic requestData, Action<string> callback, CancellationToken token)
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
                                HandleResponse(response, callback);
                                break;
                            }
                        case RequestType.GET:
                            {
                                HttpResponseMessage response = await client.GetAsync(requestExtension + "/" + content, token);
                                HandleResponse(response, callback);
                                break;
                            }
                    }
                }
            }
            catch (Exception e)
            {
                if(e is TaskCanceledException)
                {
                    Console.WriteLine("Request Cancelled");
                }
                else
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void HandleResponse(HttpResponseMessage response, Action<string> callback)
        {
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
                callback(result);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.ReasonPhrase);
            }
        }

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

        public enum RequestType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}

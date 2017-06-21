﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Boggle
{
    public class BoggleServer
    {
        TcpListener server;

        static void Main(string[] args)
        {
            new BoggleServer(IPAddress.Any, 60000);
            Console.ReadLine();
        }

        public BoggleServer(IPAddress ip, int port)
        {
            server = new TcpListener(ip, port);
            server.Start();
            server.BeginAcceptSocket(ConnectionRequested, null);
        }

        private void ConnectionRequested(IAsyncResult result)
        {
            Socket s = server.EndAcceptSocket(result);
            //Reset server-end socket acceptance.
            server.BeginAcceptSocket(ConnectionRequested, null);
            //Wrap the socket in the class that will control the server-client socket interaction.
            new ConnectionManager(s);
        }

        private void TestServerSetup()
        {
            HttpStatusCode status;
            UserInfo name = new UserInfo { Nickname = "Joe" };
            BoggleService service = new BoggleService();
            UserInfo user = service.createUser(name, out status);
            Console.WriteLine(user.UserToken);
            Console.WriteLine(status.ToString());

            // This is our way of preventing the main thread from
            // exiting while the server is in use
            Console.ReadLine();
        }

        class ConnectionManager
        {
            /// <summary>
            /// Manager's socket connection.
            /// </summary>
            private Socket socket;

            /// <summary>
            /// Manager will use a UTF8 encoding for strings.
            /// </summary>
            private static UTF8Encoding encoding = new UTF8Encoding();

            /// <summary>
            /// Used for decoding utf8 formatted strings.
            /// </summary>
            private Decoder decoder = encoding.GetDecoder();

            /// <summary>
            /// Default buffer size will be 1024 bytes.
            /// </summary>
            private const int BUFFER_SIZE = 1024;

            /// <summary>
            /// Contains text that has been received.
            /// </summary>
            private StringBuilder incoming;

            /// <summary>
            /// Contains text that is waiting to be sent.
            /// </summary>
            private StringBuilder outgoing;

            private byte[] incomingBytes = new byte[BUFFER_SIZE];
            private char[] incomingChars = new char[BUFFER_SIZE];

            private bool sendIsOngoing;

            private readonly object sendSync = new object();

            private Dictionary<string, string> headers = new Dictionary<string, string>();
            bool beginBodyCollection = false;
            private BoggleService service = new BoggleService();
            int remainingBody;

            // Bytes that we are actively trying to send, along with the
            // index of the leftmost byte whose send has not yet been completed
            private byte[] pendingBytes = new byte[0];
            private int pendingIndex = 0;

            /// <summary>
            /// Creates a new Connection manager to handle the given socket connection.
            /// </summary>
            /// <param name="s">Active server-client socket connection</param>
            public ConnectionManager(Socket s)
            {
                socket = s;
                incoming = new StringBuilder();
                outgoing = new StringBuilder();
                socket.BeginReceive(incomingBytes, 0, incomingBytes.Length,
                                SocketFlags.None, MessageReceived, null);
            }

            /// <summary>
            /// Called when some data has been received.
            /// </summary>
            private void MessageReceived(IAsyncResult result)
            {
                // Figure out how many bytes have come in
                int bytesRead = socket.EndReceive(result);

                Console.WriteLine("Received " + bytesRead + " bytes.");

                // If no bytes were received, it means the client closed its side of the socket.
                // Report that to the console and close our socket.
                if (bytesRead == 0)
                {
                    Console.WriteLine("Socket closed");
                    socket.Close();
                }

                // Otherwise, decode and display the incoming bytes.  Then request more bytes.
                else
                {
                    // Convert the bytes into characters and appending to incoming
                    int charsRead = decoder.GetChars(incomingBytes, 0, bytesRead, incomingChars, 0, false);
                    incoming.Append(incomingChars, 0, charsRead);

                    int lastNewline = -1;
                    int start = 0;
                    for (int i = 0; i < incoming.Length; i++)
                    {
                        if (incoming[i] == '\n' || i == incoming.Length-1)
                        {
                            string line = incoming.ToString(start, i + 1 - start);
                            string altered = Regex.Replace(line, @"\r|\n", "");
                            if (!string.IsNullOrEmpty(altered))
                            {
                                if (beginBodyCollection)
                                {
                                    CollectBody(line);
                                }
                                else
                                {
                                    CaptureHeaders(altered);
                                }
                            } else
                            {
                                beginBodyCollection = true;
                            }
                            lastNewline = i;
                            start = i + 1;
                        }
                    }
                    
                    incoming.Remove(0, lastNewline + 1);

                    if (beginBodyCollection)
                    {
                        if (remainingBody == 0)
                        {
                            beginBodyCollection = false;
                            BuildAndSendResponse(headers);
                        } 
                    }
                    /*
                    if (headers.ContainsKey("continue"))
                    {
                        headers.Remove("continue");
                        SendMessage(BuildHeader(null, HttpStatusCode.Continue));
                    }
                    */

                    // Ask for some more data
                    socket.BeginReceive(incomingBytes, 0, incomingBytes.Length,
                        SocketFlags.None, MessageReceived, null);
                }
            }

            #region Request Data Capturing

            private void CaptureHeaders(string line)
            {
                Regex r = new Regex(@"^(GET|POST|PUT|DELETE) ?.*\/BoggleService\.svc\/(games|users)\/?(\d+)?(\?[bB]rief=(.?yes.?|.?no.?))?.*$");
                Match m = r.Match(line);
                if (m.Success)
                {
                    for (int index = 1; index < m.Groups.Count; index++)
                    {
                        string match = m.Groups[index].ToString();
                        switch (index)
                        {
                            case 1:
                                if (!string.IsNullOrEmpty(match))
                                    headers.Add("method",match);
                                break;
                            case 2:
                                if (!string.IsNullOrEmpty(match))
                                    headers.Add("action", match);
                                break;
                            case 3:
                                if(!string.IsNullOrEmpty(match))
                                    headers.Add("game", match);
                                break;
                            case 4:
                                if (!string.IsNullOrEmpty(match))
                                    headers.Add("query", match);
                                break;
                        }
                    }
                }
                r = new Regex(@"^[cC]ontent-[lL]ength:.?(\d+)$");
                m = r.Match(line);
                if (m.Success)
                {
                    headers.Add("length", m.Groups[1].ToString());
                    this.remainingBody = int.Parse(m.Groups[1].ToString()) ;
                }
                /*
                r = new Regex(@"^[eE]xpect:.?(100)-[cC]ontinue*$");
                m = r.Match(line);
                if (m.Success)
                {
                    //SendMessage(BuildHeader(null, HttpStatusCode.Continue));
                    headers.Add("continue", "100");
                }
                */
            }

            private void CollectBody(string line)
            {
                if (this.headers.ContainsKey("body"))
                {
                    headers["body"] = headers["body"] + line;
                }
                else
                {
                    headers.Add("body", line);
                }
                int bytesInLine = encoding.GetByteCount(line.ToCharArray());
                remainingBody -= bytesInLine;
            }

            #endregion


            #region Request Handling and Response Building


            private void BuildAndSendResponse(Dictionary<string, string> headers)
            {
                if (headers.ContainsKey("method"))
                {
                    switch (headers["method"])
                    {
                        case "GET":
                            {
                                string game = headers.ContainsKey("game") ? headers["game"] : null;
                                string query = headers.ContainsKey("query") ? headers["query"] : null;
                                HandleGetRequest(headers["action"], game, query);
                                break;
                            }
                        case "POST":
                            HandlePostRequest(headers["action"], headers["body"]);
                            break;
                        case "PUT":
                            {
                                string game = headers.ContainsKey("game") ? headers["game"] : null;
                                HandlePutRequest(headers["action"], game, headers["body"]);
                                break;
                            }
                        case "DELETE":
                            break;
                    }
                }
            }

            private void HandlePutRequest(string action, string game, string body)
            {
                HttpStatusCode status;
                dynamic outgoingData = new ExpandoObject();
                dynamic incomingData = new ExpandoObject();
                switch (action)
                {
                    case "games":
                        incomingData = JsonConvert.DeserializeObject(body);
                        if (game == null)
                        {
                            service.cancel(new UserInfo() { UserToken = incomingData.UserToken }, out status);
                            SendMessage(BuildHeader(null, status));
                        } else
                        {
                            ScoreInfo score = service.postWord(new UserInfo() { UserToken = incomingData.UserToken, Word = incomingData.Word}, game, out status);
                            if (score!=null)
                            {
                                outgoingData.Score = score.Score;
                            }
                            SendMessage(BuildHeader(outgoingData, status));
                        }
                        break;
                }
            }

            private void HandlePostRequest(string action, string body)
            {
                HttpStatusCode status;
                dynamic outgoingData = new ExpandoObject();
                dynamic incomingData = new ExpandoObject();
                switch (action)
                {
                    case "users":
                        {
                            incomingData = JsonConvert.DeserializeObject(body);
                            UserInfo info = service.createUser(new UserInfo() { Nickname = incomingData.Nickname }, out status);
                            if (info != null)
                            {
                                outgoingData.UserToken = info.UserToken;
                            }
                            SendMessage(BuildHeader(outgoingData, status));
                        }
                        break;
                    case "games":
                        {
                            incomingData = JsonConvert.DeserializeObject(body);
                            UserInfo info = service.joinGame(new UserInfo() { TimeLimit = incomingData.TimeLimit, UserToken = incomingData.UserToken }, out status);
                            if(info!=null)
                            {
                                outgoingData.GameID = info.GameID;
                            }
                            SendMessage(BuildHeader(outgoingData, status));
                        }
                        break;
                }
            }

            private void HandleGetRequest(string action, string game, string query)
            {
                HttpStatusCode status;
                dynamic outgoingData = new ExpandoObject();
                dynamic incomingData = new ExpandoObject();
                switch (action)
                {
                    case "games":
                        
                        bool isBrief = IsBrief(query);
                        GameS gameStatus = service.getGame(game, isBrief ? "yes" : "no", out status);
                        if (gameStatus != null)
                        {
                            outgoingData.GameState = gameStatus.GameState;
                            if (gameStatus.GameState.Equals("pending"))
                            {
                                SendMessage(BuildHeader(outgoingData, status));
                            }
                            else
                            {
                                dynamic p1 = new ExpandoObject();
                                List<dynamic> p1Words = new List<dynamic>();
                                List<dynamic> p2Words = new List<dynamic>();

                                dynamic p2 = new ExpandoObject();
                                outgoingData.TimeLeft = gameStatus.TimeLeft;
                                outgoingData.Player1 = p1;
                                outgoingData.Player2 = p2;
                                outgoingData.Player1.Score = gameStatus.Player1.Score;
                                outgoingData.Player2.Score = gameStatus.Player2.Score;
                                if (!isBrief)
                                {
                                    outgoingData.Board = gameStatus.Board;
                                    outgoingData.TimeLimit = gameStatus.TimeLimit;

                                    outgoingData.Player1.Nickname = gameStatus.Player1.Nickname;
                                    gameStatus.Player1.WordsPlayed.ForEach((w) => {
                                        dynamic item = new ExpandoObject();
                                        item.Word = w.Word;
                                        item.Score = w.Score;
                                        p1Words.Add(item);
                                    });
                                    outgoingData.Player1.WordsPlayed = p1Words;

                                    outgoingData.Player2.Nickname = gameStatus.Player2.Nickname;
                                    
                                    gameStatus.Player2.WordsPlayed.ForEach((w) => {
                                        dynamic item = new ExpandoObject();
                                        item.Word = w.Word;
                                        item.Score = w.Score;
                                        p2Words.Add(item);
                                    });
                                    outgoingData.Player2.WordsPlayed = p2Words;
                                }
                                SendMessage(BuildHeader(outgoingData, status)); //TODO Change, I just want to see what this will do.
                            }
                        } else
                        {
                            SendMessage(BuildHeader(null, status));
                        }
                        break;
                }
            }

            private bool IsBrief(string query)
            {
                if (query == null)
                {
                    return false;
                } else
                {
                    Regex r = new Regex(@"^(\?[bB]rief=(.?yes.?|.?no.?))$");
                    Match m = r.Match(query);
                    string match = m.Groups[2].ToString();
                    return match.Equals("yes");
                }
            }

            private string BuildHeader(dynamic content, HttpStatusCode status)
            {
                string header = "HTTP/1.1 " + ((int)status) + " " + status.ToString() + "\r\ncontent-type: application/json; charset=utf-8\r\n";
                string data = content==null ? "":JsonConvert.SerializeObject(content);
                header += "content-length: " + encoding.GetByteCount(data.ToCharArray()) + "\r\n\r\n";
                header += data;
                return header;
            }

            #endregion


            #region Response Sending Mechanism

            /// <summary>
            /// Sends a string to the client
            /// </summary>
            private void SendMessage(string lines)
            {
                // Get exclusive access to send mechanism
                lock (sendSync)
                {
                    // Append the message to the outgoing lines
                    outgoing.Append(lines);

                    // If there's not a send ongoing, start one.
                    if (!sendIsOngoing)
                    {
                        Console.WriteLine("Appending a " + lines.Length + " char line, starting send mechanism");
                        sendIsOngoing = true;
                        SendBytes();
                    }
                    else
                    {
                        Console.WriteLine("\tAppending a " + lines.Length + " char line, send mechanism already running");
                    }
                }
            }

            /// <summary>
            /// Attempts to send the entire outgoing string.
            /// This method should not be called unless sendSync has been acquired.
            /// </summary>
            private void SendBytes()
            {
                // If we're in the middle of the process of sending out a block of bytes,
                // keep doing that.
                if (pendingIndex < pendingBytes.Length)
                {
                    Console.WriteLine("\tSending " + (pendingBytes.Length - pendingIndex) + " bytes");
                    socket.BeginSend(pendingBytes, pendingIndex, pendingBytes.Length - pendingIndex,
                                     SocketFlags.None, MessageSent, null);
                }

                // If we're not currently dealing with a block of bytes, make a new block of bytes
                // out of outgoing and start sending that.
                else if (outgoing.Length > 0)
                {
                    pendingBytes = encoding.GetBytes(outgoing.ToString());
                    pendingIndex = 0;
                    Console.WriteLine("\tConverting " + outgoing.Length + " chars into " + pendingBytes.Length + " bytes, sending them");
                    outgoing.Clear();
                    socket.BeginSend(pendingBytes, 0, pendingBytes.Length,
                                     SocketFlags.None, MessageSent, null);
                }

                // If there's nothing to send, shut down for the time being.
                else
                {
                    Console.WriteLine("Shutting down send mechanism\n");
                    sendIsOngoing = false;
                }
            }

            /// <summary>
            /// Called when a message has been successfully sent
            /// </summary>
            private void MessageSent(IAsyncResult result)
            {
                // Find out how many bytes were actually sent
                int bytesSent = socket.EndSend(result);
                Console.WriteLine("\t" + bytesSent + " bytes were successfully sent");

                // Get exclusive access to send mechanism
                lock (sendSync)
                {
                    // The socket has been closed
                    if (bytesSent == 0)
                    {
                        socket.Close();
                        Console.WriteLine("Socket closed");
                    }

                    // Update the pendingIndex and keep trying
                    else
                    {
                        pendingIndex += bytesSent;
                        SendBytes();
                    }
                }
            }

            #endregion
        }
    }
}
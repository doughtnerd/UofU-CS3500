using System;
using System.Collections.Generic;
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
                    //Console.WriteLine(incoming);

                    // Echo any complete lines, after capitalizing them
                    int lastNewline = -1;
                    int start = 0;
                    for (int i = 0; i < incoming.Length; i++)
                    {
                        Console.WriteLine(incoming[i]);
                        if (incoming[i] == '\n')
                        {
                            String line = incoming.ToString(start, i + 1 - start);
                            line = Regex.Replace(line, @"\r|\n", "");
                            string s;
                            if (ExtractMethod(line, out s))
                            {
                                headers.Add("method", s);
                                if (ExtractAction(line, out s))
                                {
                                    headers.Add("action", s);
                                }
                            } else if (ExtractContentLength(line, out s)){
                                headers.Add("content-length", s);
                            }
                            //SendMessage(line.ToUpper());
                            lastNewline = i;
                            start = i + 1;
                        }
                    }
                    incoming.Remove(0, lastNewline + 1);

                    // Ask for some more data
                    socket.BeginReceive(incomingBytes, 0, incomingBytes.Length,
                        SocketFlags.None, MessageReceived, null);
                }
            }

            private Dictionary<string, string> headers = new Dictionary<string, string>();

            private bool ExtractContentLength(string line, out string length)
            {
                return ExtractFromLine(line, new Regex(@"^content-length:[ ]{0,1}(\d+)$"), 1, out length);
                /*
                Regex r = new Regex(@"^content-length:[ ]{0,1}(\d+)$");
                Match m = r.Match(line);
                if (m.Success)
                {
                    length = int.Parse(m.Groups[1].ToString());
                    return true;
                }
                length = 0;
                return false;
                */
            }

            private bool ExtractMethod(string line, out string method)
            {
                return ExtractFromLine(line, new Regex(@"^(POST|PUT|GET|DELETE) \/BoggleService\.svc.*$"), 1, out method);
                /*
                Regex r = new Regex(@"^(POST|PUT|GET|DELETE) \/BoggleService\.svc.*$");
                Match m = r.Match(line);
                if (m.Success)
                {
                    method = m.Groups[1].ToString();
                    return true;
                }
                method = null;
                return false;
                */
            }

            private bool ExtractAction(string line, out string action)
            {
                return ExtractFromLine(line, new Regex(@"^\w* \/BoggleService\.svc\/(games|users).*$"), 1, out action);
                /*
                Regex r = new Regex(@"^\w* \/BoggleService\.svc\/(games|users).*$");
                Match m = r.Match(line);
                if (m.Success)
                {
                    action = m.Groups[1].ToString();
                    return true;
                }
                action = null;
                return false;
                */

            }

            private bool ExtractFromLine(string line, Regex regex, int group, out string match)
            {
                Match m = regex.Match(line);
                if (m.Success)
                {
                    match = m.Groups[group].ToString();
                    return true;
                }
                match = null;
                return false;
            }

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
        }
    }
}
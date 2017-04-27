// Nathan Reeves
// Chris Carlson
// April 2017
// String Socket implementation, all tests pass except optional last two 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace CustomNetworking
{

    /// <summary>
    /// The type of delegate that is called when a StringSocket send has completed.
    /// </summary>
    public delegate void SendCallback(bool wasSent, object payload);

    /// <summary>
    /// The type of delegate that is called when a receive has completed.
    /// </summary>
    public delegate void ReceiveCallback(String s, object payload);

    /// <summary> 
    /// A StringSocket is a wrapper around a Socket.  It provides methods that
    /// asynchronously read lines of text (strings terminated by newlines) and 
    /// write strings. (As opposed to Sockets, which read and write raw bytes.)  
    ///
    /// StringSockets are thread safe.  This means that two or more threads may
    /// invoke methods on a shared StringSocket without restriction.  The
    /// StringSocket takes care of the synchronization.
    /// 
    /// Each StringSocket contains a Socket object that is provided by the client.  
    /// A StringSocket will work properly only if the client refrains from calling
    /// the contained Socket's read and write methods.
    /// 
    /// We can write a string to a StringSocket ss by doing
    /// 
    ///    ss.BeginSend("Hello world", callback, payload);
    ///    
    /// where callback is a SendCallback (see below) and payload is an arbitrary object.
    /// This is a non-blocking, asynchronous operation.  When the StringSocket has 
    /// successfully written the string to the underlying Socket, or failed in the 
    /// attempt, it invokes the callback.  The parameter to the callback is the payload.  
    /// 
    /// We can read a string from a StringSocket ss by doing
    /// 
    ///     ss.BeginReceive(callback, payload)
    ///     
    /// where callback is a ReceiveCallback (see below) and payload is an arbitrary object.
    /// This is non-blocking, asynchronous operation.  When the StringSocket has read a
    /// string of text terminated by a newline character from the underlying Socket, or
    /// failed in the attempt, it invokes the callback.  The parameters to the callback are
    /// a string and the payload.  The string is the requested string (with the newline removed).
    /// </summary>

    public class StringSocket : IDisposable
    {
        // Underlying socket
        private Socket socket;

        // Encoding used for sending and receiving
        private Encoding encoding;
        //Queues for outgoing and incoming messages
        Queue<SentMessage> sendQueue;
        Queue<ReceivedMessage> receiveQueue;

        //build messages by byte
        Byte[] toSend;
        Byte[] toReceive;
        int bytesSent;

        bool Sending;
        bool Receiving;
        string buildMessage;

        // stored messages 
        Queue<String> receivedMessages;

        // Send Queue object
        private struct SentMessage
        {
            public string message { get; set; }
            public SendCallback callback { get; set; }
            public object payload { get; set; }
        }

        // Recieve Queue object
        private struct ReceivedMessage
        {
            public ReceiveCallback callback { get; set; }
            public object payload { get; set; }
        }
        /// <summary>
        /// Creates a StringSocket from a regular Socket, which should already be connected.  
        /// The read and write methods of the regular Socket must not be called after the
        /// StringSocket is created.  Otherwise, the StringSocket will not behave properly.  
        /// The encoding to use to convert between raw bytes and strings is also provided.
        /// </summary>
        internal StringSocket(Socket s, Encoding e)
        {
            socket = s;
            encoding = e;
            toSend = new Byte[1];
            toReceive = new Byte[1024];
            sendQueue = new Queue<SentMessage>();
            receiveQueue = new Queue<ReceivedMessage>();
            Sending = false;
            Receiving = false;
            receivedMessages = new Queue<string>();
        }

        /// <summary>
        /// Shuts down this StringSocket.
        /// </summary>
        public void Shutdown(SocketShutdown mode)
        {
            socket.Shutdown(mode);
        }

        /// <summary>
        /// Closes this StringSocket.
        /// </summary>
        public void Close()
        {
            socket.Close();
        }

        /// <summary>
        /// We can write a string to a StringSocket ss by doing
        /// 
        ///    ss.BeginSend("Hello world", callback, payload);
        ///    
        /// where callback is a SendCallback (see below) and payload is an arbitrary object.
        /// This is a non-blocking, asynchronous operation.  When the StringSocket has 
        /// successfully written the string to the underlying Socket it invokes the callback.  
        /// The parameters to the callback are true and the payload.
        /// 
        /// If it is impossible to send because the underlying Socket has closed, the callback 
        /// is invoked with false and the payload as parameters.
        ///
        /// This method is non-blocking.  This means that it does not wait until the string
        /// has been sent before returning.  Instead, it arranges for the string to be sent
        /// and then returns.  When the send is completed (at some time in the future), the
        /// callback is called on another thread.
        /// 
        /// This method is thread safe.  This means that multiple threads can call BeginSend
        /// on a shared socket without worrying around synchronization.  The implementation of
        /// BeginSend must take care of synchronization instead.  On a given StringSocket, each
        /// string arriving via a BeginSend method call must be sent (in its entirety) before
        /// a later arriving string can be sent.
        /// </summary>
        public void BeginSend(String s, SendCallback callback, object payload)
        {
            lock (sendQueue) 
            {
                // take data from method call and add it to send queue
                sendQueue.Enqueue(new SentMessage { message = s, callback = callback, payload = payload });

                if (!Sending)
                {
                    Sending = true;
                    SendNextMessage();
                }
            }
        }

        /// <summary>
        /// Sends next message in queue 
        /// </summary>
        /// 
        private void SendNextMessage()
        {
            // toSend becomes next queue item
            toSend = encoding.GetBytes(sendQueue.Peek().message);
            bytesSent = 0;

            try
            {
                // send out toSend
                socket.BeginSend(toSend, bytesSent, toSend.Length - 1, SocketFlags.None, SendCallback, null);
            }
            catch (Exception e)
            {
                SentMessage message = sendQueue.Dequeue();
                ThreadPool.QueueUserWorkItem(o => message.callback(true, message.payload));

            }
        }

        /// <summary>
        /// Send callback to make sure everything was sent 
        /// </summary>
        private void SendCallback(IAsyncResult strng)
        {
            lock (sendQueue)
            {
                // determines how much of message was sent
                bytesSent += socket.EndSend(strng);

                int remainingBytes = toSend.Length - bytesSent;

                // after sending send if there are any remaining bytes
                if (remainingBytes > 0)
                    socket.BeginSend(toSend, bytesSent, remainingBytes, SocketFlags.None, SendCallback, null);
                else
                {
                    //remove from queue
                    SentMessage justSent = sendQueue.Dequeue();
                    //send callback
                    ThreadPool.QueueUserWorkItem(o => justSent.callback(true, justSent.payload));

                    //send next messages
                    if (sendQueue.Count > 0)
                        SendNextMessage();  
                    else
                        Sending = false; 
                }
            }
        }

        /// <summary>
        /// We can read a string from the StringSocket by doing
        /// 
        ///     ss.BeginReceive(callback, payload)
        ///     
        /// where callback is a ReceiveCallback (see below) and payload is an arbitrary object.
        /// This is non-blocking, asynchronous operation.  When the StringSocket has read a
        /// string of text terminated by a newline character from the underlying Socket, it 
        /// invokes the callback.  The parameters to the callback are a string and the payload.  
        /// The string is the requested string (with the newline removed).
        /// 
        /// Alternatively, we can read a string from the StringSocket by doing
        /// 
        ///     ss.BeginReceive(callback, payload, length)
        ///     
        /// If length is negative or zero, this behaves identically to the first case.  If length
        /// is positive, then it reads and decodes length bytes from the underlying Socket, yielding
        /// a string s.  The parameters to the callback are s and the payload
        ///
        /// In either case, if there are insufficient bytes to service a request because the underlying
        /// Socket has closed, the callback is invoked with null and the payload.
        /// 
        /// This method is non-blocking.  This means that it does not wait until a line of text
        /// has been received before returning.  Instead, it arranges for a line to be received
        /// and then returns.  When the line is actually received (at some time in the future), the
        /// callback is called on another thread.
        /// 
        /// This method is thread safe.  This means that multiple threads can call BeginReceive
        /// on a shared socket without worrying around synchronization.  The implementation of
        /// BeginReceive must take care of synchronization instead.  On a given StringSocket, each
        /// arriving line of text must be passed to callbacks in the order in which the corresponding
        /// BeginReceive call arrived.
        /// 
        /// Note that it is possible for there to be incoming bytes arriving at the underlying Socket
        /// even when there are no pending callbacks.  StringSocket implementations should refrain
        /// from buffering an unbounded number of incoming bytes beyond what is required to service
        /// the pending callbacks.
        /// </summary>
        public void BeginReceive(ReceiveCallback callback, object payload, int length = 0)
        {

            lock (receiveQueue) 
            {
                //put data from method call into the queue
                receiveQueue.Enqueue(new ReceivedMessage { callback = callback, payload = payload });

                if (!Receiving)
                {
                    Receiving = true;
                    ReceiveMessage();
                }

            }
        }

        /// <summary>
        /// If there are recieved messages in the queue, run callback recieve more bytes
        /// </summary>
        
        private void ReceiveMessage()
        {
            while (receiveQueue.Count > 0) 
            {
                if (receivedMessages.Count > 0) 
                {
                    //take receive item from top of queue
                    ReceivedMessage justReceived = receiveQueue.Dequeue(); 
                    string message = receivedMessages.Dequeue();    

                    //callback from queue object
                    ThreadPool.QueueUserWorkItem(o => justReceived.callback(message, justReceived.payload));
                }
                else break;
            }

            int bytesReceived = 0;

            // Begin receiving bytes
            if (receiveQueue.Count > 0)
            {
                try
                {
                    socket.BeginReceive(toReceive, bytesReceived, toReceive.Length, SocketFlags.None, ReceiveCallback, null);
                }
                catch (Exception e)
                {
                    ReceivedMessage message = receiveQueue.Dequeue();
                    ThreadPool.QueueUserWorkItem(o => message.callback(e.Message, message.payload));
                }
            }
            else
                Receiving = false;

        }

        /// <summary>
        /// Begin Recieve callback
        /// </summary>

        private void ReceiveCallback(IAsyncResult strng)
        {
            lock (receiveQueue)
            {
                int bytesReceived = socket.EndReceive(strng);

                // build string based on bytes coming in from receive request                            
                buildMessage += encoding.GetString(toReceive, 0, bytesReceived);

                // if there are still new lines to read add those string to queue
                while ((buildMessage.IndexOf("\n") > 0))
                {
                    int messageLength = buildMessage.IndexOf("\n");
                    receivedMessages.Enqueue(buildMessage.Substring(0, messageLength));
                    //keep track of position
                    buildMessage = buildMessage.Substring(messageLength + 1);
                }

                ReceiveMessage();
            }

        }
        /// <summary>
        /// Frees resources associated with this StringSocket.
        /// </summary>
        public void Dispose()
        {
            Shutdown(SocketShutdown.Both);
            Close();
        }
    }
}

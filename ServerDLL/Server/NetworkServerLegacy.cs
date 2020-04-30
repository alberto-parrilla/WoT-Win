using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KernelDLL.Game.Enums;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;
using Newtonsoft.Json;

namespace ServerDLL.Server
{
    // State object for reading client data asynchronously  
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;

        // Size of receive buffer.  
        public const int BufferSize = 1024;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

    /// 
    public class NetworkServerLegacy : IServerLegacy
    {
        // The ip address for the remote device
        private const string remoteIpAddress = "127.0.0.1";

        // The port number for the remote device. 
        private const int remotePort = 11000;

        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        private Action<IRequest, Socket> _manageRequest;

        public Task<IRequest> ReceiveAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Send(IResponse response, Socket handler)
        {
            var content = SerializeToResponse(response);
            Send(handler, content);
        }

        public void StartListening(Action<IRequest, Socket> manageRequest)
        {
            _manageRequest = manageRequest;

            // Establish the local endpoint for the socket.  
            IPAddress ipAddress = IPAddress.Parse(remoteIpAddress);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, remotePort);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject) ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.  
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the
                    // client. Manage them
                    var request = DeserializeToRequest(content.TrimEnd("<EOF>".ToCharArray()));
                    new Thread( () => _manageRequest(request, handler)).Start();
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket) ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private IRequest DeserializeToRequest(string stream)
        {
            string[] parts = stream.Split("#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (EnumRequestType.TryParse(parts[0], out EnumRequestType type))
            {
                return DeserializeToRequest(type, parts[1]);
            }
            return new EmptyRequest();
        }

        private IRequest DeserializeToRequest(EnumRequestType type, string content)
        {
            switch (type)
            {
                case EnumRequestType.Ping:
                    return JsonConvert.DeserializeObject<PingRequestLegacy>(content);
                case EnumRequestType.Login:
                    return JsonConvert.DeserializeObject<LoginRequestLegacy>(content);
                case EnumRequestType.Register:
                    return JsonConvert.DeserializeObject<RegisterRequestLegacy>(content);
                case EnumRequestType.Authentication:
                    return JsonConvert.DeserializeObject<AuthenticationRequest>(content);
            }
            return new EmptyRequest();
        }

        private string SerializeToResponse(IResponse response)
        {
            var content = JsonConvert.SerializeObject(response);
            return string.Format("{0}#{1}#{2}", response.ResponseType, content, "<EOF>");
        }
    }
}

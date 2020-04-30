using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KernelDLL.Game.Enums;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;
using Newtonsoft.Json;

namespace ClientDLL.Client
{
    // State object for receiving data from remote device.  
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

    public class NetworkClientLegacy : IClientLegacy
    {
        // The ip address for the remote device
        private const string remoteIpAddress = "127.0.0.1";
        // The port number for the remote device. 
        private const int remotePort = 11000;
        // The Socket
        private Socket _client;

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        // The response from the remote device.  
        private static String response = String.Empty;

        private Action<IResponse> _manageResponse;

        public void StartClient(Action<IResponse> manageResponse)
        {
            _manageResponse = manageResponse;

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                IPAddress ipAddress = IPAddress.Parse(remoteIpAddress);
                IPEndPoint remoteEp = new IPEndPoint(ipAddress, remotePort);

                // Create a TCP/IP socket.  
                //Socket client = new Socket(ipAddress.AddressFamily,
                //    SocketType.Stream, ProtocolType.Tcp);
                _client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                _client.BeginConnect(remoteEp, new AsyncCallback(ConnectCallback), _client);
                connectDone.WaitOne();

                //// Send test data to the remote device.  
                //var content = JsonConvert.SerializeObject(new PingRequest());
                //var stream = $"{EnumRequestType.Ping}#{content}#{"<EOF>"}";
                ////Send(client, "This is a test<EOF>");
                //Send(client, stream);
                //sendDone.WaitOne();

                //// Receive the response from the remote device.  
                //Receive(client);
                //receiveDone.WaitOne();

                //// Write the response to the console.  
                //Console.WriteLine("Response received : {0}", response);

                //// Release the socket.  
                //client.Shutdown(SocketShutdown.Both);
                //client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void StopClient()
        {
            _client.Shutdown(SocketShutdown.Both);
            _client.Close();
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Task<IResponse> ReceiveAsync()
        {
            throw new System.NotImplementedException();
        }

        public void SendAsync(IRequest request, Action<IResponse> manageResponse)
        {
            StartClient(manageResponse);

            string stream = null;
            // Send test data to the remote device.  
            if (request is PingRequestLegacy)
            {
                var content = JsonConvert.SerializeObject(request);
                stream = $"{EnumRequestType.Ping}#{content}#{"<EOF>"}";
            }
            else if (request is LoginRequestLegacy)
            {
                var content = JsonConvert.SerializeObject(request);
                stream = $"{EnumRequestType.Login}#{content}#{"<EOF>"}";
            }
            else if (request is RegisterRequestLegacy)
            {
                var content = JsonConvert.SerializeObject(request);
                stream = $"{EnumRequestType.Register}#{content}#{"<EOF>"}";
            }
            else if (request is AuthenticationRequest)
            {
                var content = JsonConvert.SerializeObject(request);
                stream = $"{EnumRequestType.Authentication}#{content}#{"<EOF>"}";
            }

            Send(_client, stream);
            sendDone.WaitOne();

            // Receive the response from the remote device.  
            Receive(_client);
            receiveDone.WaitOne();

            //if (request is PingRequest)
            //{
            //    return SendPing();
            //}

            //return null;
            //StopClient();
        }


        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject) ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        var content = state.sb.ToString();
                        var response = DeserializeToResponse(content.TrimEnd("<EOF>".ToCharArray()));
                        new Thread(() =>
                        {
                            _manageResponse(response);
                            StopClient();
                        }).Start();
                    }

                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private IResponse SendPing()
        {
            int timeout = 500;
            Ping pingSender = new Ping();

            PingReply reply = pingSender.Send(remoteIpAddress, timeout);
            if (reply?.Status == IPStatus.Success)
            {
                return new PingResponseLegacy(reply.RoundtripTime);
            }

            return new PingResponseLegacy(null);
        }

        private IResponse DeserializeToResponse(string stream)
        {
            string[] parts = stream.Split("#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (EnumResponseType.TryParse(parts[0], out EnumResponseType type))
            {
                return DeserializeToResponse(type, parts[1]);
            }
            return new EmptyResponse();
        }

        private IResponse DeserializeToResponse(EnumResponseType type, string content)
        {
            switch (type)
            {
                case EnumResponseType.Ping:
                    return JsonConvert.DeserializeObject<PingResponseLegacy>(content);
                case EnumResponseType.Login:
                    return JsonConvert.DeserializeObject<LoginResponseLegacy>(content);
                case EnumResponseType.Register:
                    return JsonConvert.DeserializeObject<RegisterResponseLegacy>(content);
                case EnumResponseType.Authentication:
                    return JsonConvert.DeserializeObject<AuthenticationResponse>(content);
            }
            return new EmptyResponse();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;

namespace ServerDLL.Server
{
    public class NetworkServer : IServer
    {
        private const string remoteIpAddress = "127.0.0.1";
        private const int remotePort = 11000;

        private IMainServer _mainServer;

        public TcpListener Listener { get; set; }
        public bool IsStarted { get; private set; }
        public List<Receiver> Clients { get; private set; }

        public NetworkServer(IMainServer mainServer)
        {
            Clients = new List<Receiver>();
            _mainServer = mainServer;
        }

        public void Start()
        {
            if (!IsStarted)
            {
                Listener = new TcpListener(System.Net.IPAddress.Any, remotePort);
                Listener.Start();
                IsStarted = true;
                Debug.WriteLine("Server Started!");

                //Start Async pattern for accepting new connections
                WaitForConnection();
            }
        }

        public void Stop()
        {
            if (IsStarted)
            {
                Listener.Stop();
                IsStarted = false;

                Debug.WriteLine("Server Stoped!");
            }
        }

        private void WaitForConnection()
        {
            Listener.BeginAcceptTcpClient(new AsyncCallback(ConnectionHandler), null);
        }

        private void ConnectionHandler(IAsyncResult ar)
        {
            lock (Clients)
            {
                Receiver newClient = new Receiver(Listener.EndAcceptTcpClient(ar), _mainServer);
                newClient.Start();
                Clients.Add(newClient);
            }

            Debug.WriteLine("New Client Connected");
            WaitForConnection();
        }
    }
}

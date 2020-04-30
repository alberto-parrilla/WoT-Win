using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using KernelDLL.Network;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public class NetworkClient : IClient
    {
        private const string remoteIpAddress = "127.0.0.1";
        private const int remotePort = 11000;

        private Thread receivingThread;

        public TcpClient TcpClient { get; set; }

        private Action<IResponse> _manageResponse;

        public NetworkClient(Action<IResponse> manageResponse)
        {
            _manageResponse = manageResponse;
        }

        public void Connect()
        {
            TcpClient = new TcpClient();
            TcpClient.Connect(remoteIpAddress, remotePort);
            TcpClient.ReceiveBufferSize = 1024;
            TcpClient.SendBufferSize = 1024;

            receivingThread = new Thread(ReceivingMethod);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        public void Disconnect()
        {
            TcpClient.Client.Disconnect(false);
            TcpClient.Close();
        }

        private void ReceivingMethod(object obj)
        {
            while (true)
            {
                if (TcpClient.Available > 0)
                {
                    try
                    {
                        var f = new BinaryFormatter();
                        f.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                        var msg = f.Deserialize(TcpClient.GetStream()) as ResponseMessageBase;
                        _manageResponse(msg);
                    }
                    catch (Exception e)
                    {
                        Exception ex = new Exception("Unknown message recieved. Could not deserialize the stream.", e);
                        Debug.WriteLine(ex.Message);
                    }
                }

                Thread.Sleep(30);
            }
        }

        public void SendMessage(IRequest request)
        {
            var message = request as RequestMessageBase;
            var sendingThread = new Thread(() => SendingMethod(message));
            sendingThread.IsBackground = true;
            sendingThread.Start();
        }

        private void SendingMethod(RequestMessageBase request)
        {
            BinaryFormatter f = new BinaryFormatter();
            try
            {
                f.Serialize(TcpClient.GetStream(), request);
            }
            catch
            {
                Disconnect();
            }
        }
    }
}

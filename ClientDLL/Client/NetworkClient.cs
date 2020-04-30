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
        //private Thread sendingThread;
        //private List<ResponseCallbackObject> callBacks;

        public TcpClient TcpClient { get; set; }
        //public List<RequestMessageBase> MessageQueue { get; private set; }

        private Action<IResponse> _manageResponse;

        public NetworkClient(Action<IResponse> manageResponse)
        {
            _manageResponse = manageResponse;
        }

        public void Connect()
        {
            TcpClient = new TcpClient();
            TcpClient.Connect(remoteIpAddress, remotePort);
            //Status = StatusEnum.Connected;
            TcpClient.ReceiveBufferSize = 1024;
            TcpClient.SendBufferSize = 1024;

            receivingThread = new Thread(ReceivingMethod);
            receivingThread.IsBackground = true;
            receivingThread.Start();

            //sendingThread = new Thread(SendingMethod);
            //sendingThread.IsBackground = true;
            //sendingThread.Start();
        }

        public void Disconnect()
        {
            //MessageQueue.Clear();
            //callBacks.Clear();
            try
            {
                //SendMessage(new DisconnectRequest());
            }
            catch { }
            Thread.Sleep(1000);
            //Status = StatusEnum.Disconnected;
            TcpClient.Client.Disconnect(false);
            TcpClient.Close();
            //OnClientDisconnected();
        }

        private void ReceivingMethod(object obj)
        {
            //while (Status != StatusEnum.Disconnected)
            //{
            while (true)
            {
                if (TcpClient.Available > 0)
                {
                    try
                    {
                        var f = new BinaryFormatter();
                        f.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                        var msg = f.Deserialize(TcpClient.GetStream()) as ResponseMessageBase;
                        //OnMessageReceived(msg);
                        _manageResponse(msg);
                    }
                    catch (Exception e)
                    {
                        Exception ex = new Exception("Unknown message recieved. Could not deserialize the stream.", e);
                        //OnClientError(this, ex);
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
            //while (Status != StatusEnum.Disconnected)
            //{
            //    if (MessageQueue.Count > 0)
            //    {
            //        MessageBase m = MessageQueue[0];

            BinaryFormatter f = new BinaryFormatter();
            try
            {
                f.Serialize(TcpClient.GetStream(), request);
            }
            catch
            {
                Disconnect();
            }

            //        MessageQueue.Remove(m);
            //    }

            //Thread.Sleep(30);
            //}
        }
    }
}

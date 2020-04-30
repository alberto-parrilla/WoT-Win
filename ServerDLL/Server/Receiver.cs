using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ServerDLL.Server
{
    public class Receiver
    {
        private Thread receivingThread;
        private Thread sendingThread;

        private bool IsConnected { get; set; }

        public Receiver()
        {
            IsConnected = true;
        }

        public Receiver(TcpClient client, IMainServer mainServer)
            : this()
        {
            MainServer = mainServer;
            Client = client;
            Client.ReceiveBufferSize = 1024;
            Client.SendBufferSize = 1024;
        }

       
        private IMainServer MainServer { get; set; }
        public TcpClient Client { get; set; }
        public long TotalBytesUsage { get; set; }

        public void Start()
        {
            receivingThread = new Thread(ReceivingMethod);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        private void Disconnect()
        {
            if (!IsConnected) return;
         
            IsConnected = false;
            Client.Client.Disconnect(false);
            Client.Close();
        }

        public void SendMessage(IResponse response)
        {
            if (IsConnected)
            {
                var message = response as ResponseMessageBase;
                try
                {
                    BinaryFormatter f = new BinaryFormatter();
                    f.Serialize(Client.GetStream(), message);
                }
                catch
                {
                    Disconnect();
                }
            }
        }

        private async void ReceivingMethod()
        {
            //while (Status != StatusEnum.Disconnected)
            while(IsConnected)
            {
                if (Client.Available > 0)
                {
                    TotalBytesUsage += Client.Available;

                    try
                    {
                        BinaryFormatter f = new BinaryFormatter();
                        RequestMessageBase msg = f.Deserialize(Client.GetStream()) as RequestMessageBase;
                        await MainServer.ProcessRequestAsync(msg, this);
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
    }
}

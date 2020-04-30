using System;
using KernelDLL.Network.Request;

namespace ClientDLL.Client
{
    public class LocalClient : IClient
    {
        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(IRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

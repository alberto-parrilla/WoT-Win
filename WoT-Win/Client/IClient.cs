using System.Net;

namespace WoT_Win.Client
{
    public interface IClient
    {
        void Send(IRequest request);
    }
}

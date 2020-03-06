using System;

namespace WoT_Win.Client
{
    public class PingRequest : BaseRequest
    {
        public PingRequest(int userId) : base(0, userId)
        {
            Time = DateTime.Now;
        }

        public DateTime Time { get; private set; }
    }
}

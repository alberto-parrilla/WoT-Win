namespace WoT_Win.Client
{
    public class BaseRequest : IRequest
    {
        public BaseRequest(int type, int userId)
        {
            Type = type;
            UserId = userId;
        }

        public int Type { get; private set; }
        public int UserId { get; private set; }
    }
}

namespace WoT_Win.Client
{
    public interface IRequest
    {
        int Type { get; }
        int UserId { get; }
    }
}

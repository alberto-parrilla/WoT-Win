namespace KernelDLL.Authentication
{
    public class LoginResult
    {
        public LoginResult() : this(false)
        { }

        public LoginResult(string reason) : this(false)
        {
            Reason = reason;
        }

        public LoginResult(int userId) : this(true)
        {
            UserId = userId;
        }

        internal LoginResult(bool success)
        {
            Success = success;
        }

        public int UserId { get; }
        public bool Success { get; }
        public string Reason { get; }
    }
}

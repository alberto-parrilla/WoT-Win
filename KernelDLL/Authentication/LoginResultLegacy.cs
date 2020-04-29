namespace KernelDLL.Authentication
{
    public class LoginResultLegacy
    {
        public LoginResultLegacy() : this(false)
        { }

        public LoginResultLegacy(string reason) : this(false)
        {
            Reason = reason;
        }

        public LoginResultLegacy(int userId) : this(true)
        {
            UserId = userId;
        }

        internal LoginResultLegacy(bool success)
        {
            Success = success;
        }

        public int UserId { get; }
        public bool Success { get; }
        public string Reason { get; }
    }
}

namespace KernelDLL.Authentication
{
    public class RegisterResult
    {
        public RegisterResult() : this(true)
        { }

        public RegisterResult(string reason) : this(false)
        {
            Reason = reason;
        }

        private RegisterResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Reason { get; }
    }
}

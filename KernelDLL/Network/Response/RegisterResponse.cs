namespace KernelDLL.Network.Response
{
    public enum EnumRegisterResponse
    {
        Success,
        UserExists,
        EmailExists,
        UndefinedError
    }

    public class RegisterResponse : BaseResponse
    {
        public RegisterResponse(EnumRegisterResponse status)
        {
            Status = status;
        }

        public EnumRegisterResponse Status { get; }

        public override EnumResponseType ResponseType => EnumResponseType.Register;
    }
}

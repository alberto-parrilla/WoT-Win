using System;

namespace KernelDLL.Network.Response
{
    public enum EnumRegisterResponse
    {
        Success,
        UserExists,
        EmailExists,
        UndefinedError
    }

    [Serializable]
    public class RegisterResponse : ResponseMessageBase
    {
        internal RegisterResponse(int? userId)
        {
            UserId = userId;
        }

        public RegisterResponse(EnumRegisterResponse status) : this(null)
        {
            Status = status;
        }

        public RegisterResponse(EnumRegisterResponse status, int? userId) : this(userId)
        {
            Status = status;
        }

        public EnumRegisterResponse Status { get; }
        public int? UserId { get; }

        public override EnumResponseType ResponseType => EnumResponseType.Register;
    }
}

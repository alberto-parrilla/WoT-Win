using System;

namespace KernelDLL.Network.Response
{
    public enum EnumPlayerDataResponse
    {
        Success,
        Error,
    }

    [Serializable]
    public class PlayerDataResponse<T> : ResponseMessageBase
    {
        public PlayerDataResponse(EnumPlayerDataResponse status, T content)
        {
            Status = status;
            Content = content;
        }

        public EnumPlayerDataResponse Status { get; }
        public T Content { get; }

        public override EnumResponseType ResponseType => EnumResponseType.PlayerData;
    }
}

using System;

namespace KernelDLL.Network.Response
{
    public enum EnumDataResponse
    {
        Success,
        Error,
    }

    [Serializable]
    public class DataResponse<T> : ResponseMessageBase
    {
        public DataResponse(EnumDataResponse status, T content)
        {
            Status = status;
            Content = content;
        }

        public EnumDataResponse Status { get; }
        public T Content { get; }

        public override EnumResponseType ResponseType => EnumResponseType.Data;
    }
}

namespace KernelDLL.Network.Response
{
    public abstract class BaseResponse : IResponse
    {
       public abstract EnumResponseType ResponseType { get; }
    }
}

namespace KernelDLL.Network.Response
{
    public enum EnumResponseType
    {
        Ping,
        Login,
        Register,
        Authentication
    }

    public interface IResponse
    {
        EnumResponseType ResponseType { get; }
    }
}

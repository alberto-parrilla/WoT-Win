namespace KernelDLL.Network.Response
{
    public enum EnumResponseType
    {
        Ping,
        Login,
        Register,
        Authentication,
        Data
    }

    public interface IResponse
    {
        EnumResponseType ResponseType { get; }
    }
}

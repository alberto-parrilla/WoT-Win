namespace KernelDLL.Network.Response
{
    public enum EnumResponseType
    {
        Ping,
        Login,
        Register,
        Authentication,
        Data,
        PlayerData,
        CheckData
    }

    public interface IResponse
    {
        EnumResponseType ResponseType { get; }
    }
}

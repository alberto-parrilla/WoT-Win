namespace KernelDLL.Network.Response
{
    public class PingResponseLegacy : BaseResponse
    {
        public long? PingTime { get; }

        public PingResponseLegacy(long? pingTime)
        {
            PingTime = pingTime;
        }

        public bool TimeOut => !PingTime.HasValue;

        public override EnumResponseType ResponseType => EnumResponseType.Ping;
    }
}

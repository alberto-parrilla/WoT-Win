namespace KernelDLL.Network.Response
{
    public class PingResponse : BaseResponse
    {
        public long? PingTime { get; }

        public PingResponse(long? pingTime)
        {
            PingTime = pingTime;
        }

        public bool TimeOut => !PingTime.HasValue;

        public override EnumResponseType ResponseType => EnumResponseType.Ping;
    }
}

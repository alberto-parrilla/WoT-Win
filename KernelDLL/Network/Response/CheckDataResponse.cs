using System;

namespace KernelDLL.Network.Response
{
    public enum EnumCheckDataResult
    {
        Exists,
        NotExists
    }

    [Serializable]
    public class CheckDataResponse : ResponseMessageBase
    {
        public CheckDataResponse(EnumCheckDataResult checkResult, object data)
        {
            CheckResult = checkResult;
            Data = data;
        }

        public EnumCheckDataResult CheckResult { get; private set; }
        public object Data { get; private set; }
        public override EnumResponseType ResponseType => EnumResponseType.CheckData;
    }
}

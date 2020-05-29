using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Common;
using KernelDLL.Game;
using KernelDLL.Game.Models;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    public enum EnumCheckDataType
    {
        Name
    }

    [Serializable]
    public class CheckDataRequest : RequestMessageBase
    {
        public CheckDataRequest(EnumCheckDataType type, object data)
        {
            Type = type;
            Data = data;
        }

        public EnumCheckDataType Type { get; private set; }
        public object Data { get; private set; }

        public override async Task<IResponse> ProcessRequestAsync()
        {
            var gameManager = new GameManager(new DataManager());
            var exists = await gameManager.CheckDataAsync(Type, Data);
            return new CheckDataResponse(exists ? EnumCheckDataResult.Exists : EnumCheckDataResult.NotExists, Data);
        }
    }
}

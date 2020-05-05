using KernelDLL.Network.Response;
using System;
using System.Threading.Tasks;
using CoreDatabase.Context.Game;
using KernelDLL.Common;
using KernelDLL.Game;
using KernelDLL.Game.Models;

namespace KernelDLL.Network.Request
{
    [Serializable]
    public class DataRequest<T> : RequestMessageBase
    {
        public DataRequest(EnumDataType dataType, int userId)
        {
            DataType = dataType;
            UserId = userId;
        }

        public EnumDataType DataType { get; }
        public int UserId { get; }

        public override async Task<IResponse> ProcessRequestAsync()
        {
            var gameManager = new GameManager(new DataManager());
            if (DataType == EnumDataType.GameSessionInfo)
            {
                var model = await gameManager.GetGameSessionInfoByUserIdAsync(UserId);
                return new DataResponse<GameSessionInfoModel>(EnumDataResponse.Success, model);
            }
          
            return null;
        }
    }
}

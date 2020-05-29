using KernelDLL.Network.Response;
using System;
using System.Threading.Tasks;
using KernelDLL.Common;
using KernelDLL.Game;
using KernelDLL.Game.Models;

namespace KernelDLL.Network.Request
{
    [Serializable]
    public class DataRequest : RequestMessageBase
    {
        public DataRequest(EnumGameDataType dataType, int userId)
        {
            DataType = dataType;
            UserId = userId;
        }

        public EnumGameDataType DataType { get; }
        public int UserId { get; }

        public override async Task<IResponse> ProcessRequestAsync()
        {
            var gameManager = new GameManager(new DataManager());
            if (DataType == EnumGameDataType.GameSessionInfo)
            {
                var model = await gameManager.GetGameSessionInfoByUserIdAsync(UserId);
                return new DataResponse<GameSessionInfoModel>(EnumDataResponse.Success, model);
            }
          
            return null;
        }
    }
}

﻿using System.Threading.Tasks;
using KernelDLL.Game.Models;

namespace KernelDLL.Game
{
    public interface IGameManager
    {
        Task<GameSessionModel> GetGameSessionByUserIdAsync(int userId);
        Task<GameSessionInfoModel> GetGameSessionInfoByUserIdAsync(int userId);
        Task<PlayerModel> GetPlayerAsync(int playerId);
        Task<PlayerInfoModel> GetPlayerInfoAsync(int playerId);
    }
}

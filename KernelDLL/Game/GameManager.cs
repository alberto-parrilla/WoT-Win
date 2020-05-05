using System.Threading.Tasks;
using CoreDatabase.Managers;
using KernelDLL.Common;
using KernelDLL.Game.Models;

namespace KernelDLL.Game
{
    public class GameManager : IGameManager
    {
        private IGameDbManager _gameDbManager;
        private IDataManager _dataManager;

        public GameManager(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _gameDbManager = new GameDbManager();
        }

        public async Task<GameSessionModel> GetGameSessionByUserIdAsync(int userId)
        {
            var gameSession = await _gameDbManager.GetGameSessionByUserIdAsync(userId);
            return new GameSessionModel(gameSession);
        }

        public async Task<GameSessionInfoModel> GetGameSessionInfoByUserIdAsync(int userId)
        {
            var gameSession = await _gameDbManager.GetGameSessionByUserIdAsync(userId);
            if (gameSession == null) return null;
            var area = await _dataManager.GetAreaInfoAsync(gameSession.AreaId);
            var scene = await _dataManager.GetSceneInfoAsync(gameSession.SceneId);
            var player = await GetPlayerInfoAsync(gameSession.PlayerId);
            return new GameSessionInfoModel(gameSession.UserId, area, scene, player, gameSession.SavedTime);
        }

        public async Task<PlayerModel> GetPlayerAsync(int playerId)
        {
            var player = await _gameDbManager.GetPlayerAsync(playerId);
            return new PlayerModel(player);
        }

      public async Task<PlayerInfoModel> GetPlayerInfoAsync(int playerId)
        {
            var player = await _gameDbManager.GetPlayerAsync(playerId);
            return new PlayerInfoModel(new PlayerModel(player));
        }

      
    }
}

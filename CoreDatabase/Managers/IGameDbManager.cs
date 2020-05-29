using System.Threading.Tasks;
using CoreDatabase.Context.Game;

namespace CoreDatabase.Managers
{
    public interface IGameDbManager
    {
        Task<GameSession> GetGameSessionByUserIdAsync(int userId);
        Task<Player> GetPlayerAsync(int playerId);
        Task<bool> CheckNameAsync(string name);
    }
}

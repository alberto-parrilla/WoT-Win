using System.Data.Entity;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using CoreDatabase.Context;
using CoreDatabase.Context.Game;

namespace CoreDatabase.Managers
{
    public class GameDbManager : IGameDbManager
    {
        public async Task<GameSession> GetGameSessionByUserIdAsync(int userId)
        {
            using (var context = new GameDbContext())
            {
                return await context.GameSessions.FirstOrDefaultAsync(u => u.UserId == userId);
            }
        }

        public async Task<Player> GetPlayerAsync(int playerId)
        {
            using (var context = new GameDbContext())
            {
                return await context.Players.FirstOrDefaultAsync(p => p.GameId == playerId);
            }
        }
    }
}

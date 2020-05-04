using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoreDatabase.Context;
using CoreDatabase.Context.Data;

namespace CoreDatabase.Managers
{
    public class DataDbManager : IDataDbManager
    {
        public async Task<Area> GetAreaByIdAsync(int id)
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Areas.FirstOrDefaultAsync(u => u.GameId == id);
            }
        }

        public async Task<Scene> GetSceneByIdAsync(int id)
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Scenes.FirstOrDefaultAsync(u => u.GameId == id);
            }
        }

        public async Task<List<Transition>> GetTransitionsByAreaIdAndSceneIdAsync(int areaId, int sceneId)
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Transitions.Where(t => t.SourceArea == areaId && t.SourceScene == sceneId).ToListAsync();
            }
        }
    }
}

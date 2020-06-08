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
                return await dataDbContext.Areas.FirstOrDefaultAsync(u => u.GameId == id && u.IsEnabled);
            }
        }

        public async Task<Scene> GetSceneByIdAsync(int id)
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Scenes.FirstOrDefaultAsync(u => u.GameId == id && u.IsEnabled);
            }
        }

        public async Task<List<Transition>> GetTransitionsByAreaIdAndSceneIdAsync(int areaId, int sceneId)
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Transitions.Where(t => t.SourceArea == areaId && t.SourceScene == sceneId && t.IsEnabled).ToListAsync();
            }
        }

        public async Task<List<Race>> GetRacesAsync()
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Races.Where(r => r.IsEnabled).Include(r => r.Locations).Include(r => r.AttributeMod).ToListAsync();
            }
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Genders.Where(s => s.IsEnabled).ToListAsync();
            }
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Locations.Where(l => l.IsEnabled).Include(l => l.AttributeMod).ToListAsync();
            }
        }

        public async Task<List<Skill>> GetSkillsAsync()
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Skills.Where(s => s.IsEnabled).ToListAsync();
            }
        }

        public async Task<List<Feat>> GetFeatsAsync()
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Feats.Where(f => f.IsEnabled).ToListAsync();
            }
        }

        public async Task<List<Weave>> GetWeavesAsync()
        {
            using (var dataDbContext = new DataDbContext())
            {
                return await dataDbContext.Weaves.Where(w => w.IsEnabled).ToListAsync();
            }
        }
    }
}

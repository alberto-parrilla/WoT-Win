using System.Collections.Generic;
using System.Threading.Tasks;
using CoreDatabase.Context.Data;

namespace CoreDatabase.Managers
{
    public interface IDataDbManager
    {
        Task<Area> GetAreaByIdAsync(int id);
        Task<Scene> GetSceneByIdAsync(int id);
        Task<List<Transition>> GetTransitionsByAreaIdAndSceneIdAsync(int areaId, int sceneId);

        Task<List<Race>> GetRacesAsync();
        Task<List<Gender>> GetGendersAsync();
        Task<List<Location>> GetLocationsAsync();
        Task<List<Skill>> GetSkillsAsync();
        Task<List<Feat>> GetFeatsAsync();
        Task<List<Weave>> GetWeavesAsync();
    }
}

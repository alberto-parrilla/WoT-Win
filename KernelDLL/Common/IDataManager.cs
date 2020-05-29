using System.Collections.Generic;
using System.Threading.Tasks;
using KernelDLL.Creation.Models;
using KernelDLL.Game.Models;
using KernelDLL.Network.Request;

namespace KernelDLL.Common
{
    public interface IDataManager
    {
        // Game Session
        Task<AreaInfoModel> GetAreaInfoAsync(int areaId);
        Task<SceneInfoModel> GetSceneInfoAsync(int areaId);

        // Creation Player
        Task<List<RaceModel>> GetRacesAsync();
        Task<List<GenderModel>> GetGendersAsync();
        Task<List<LocationModel>> GetLocationsAsync();
        Task<List<BaseSkillModel>> GetSkillsAsync(); 
        Task<List<BaseSkillModel>> GetFeatsAsync();
        Task<List<BaseWeaveModel>> GetWeavesAsync();
        Task<List<AvatarToolsetModel>> GetAvatarToolsetAsync(EnumPlayerDataType type);
    }
}

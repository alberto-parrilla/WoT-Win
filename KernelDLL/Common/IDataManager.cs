using System.Threading.Tasks;
using KernelDLL.Game.Models;

namespace KernelDLL.Common
{
    public interface IDataManager
    {
        Task<AreaInfoModel> GetAreaInfoAsync(int areaId);
        Task<SceneInfoModel> GetSceneInfoAsync(int areaId);
    }
}

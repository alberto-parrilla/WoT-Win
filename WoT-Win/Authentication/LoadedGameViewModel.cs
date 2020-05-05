using KernelDLL.Game.Models;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Authentication
{
    public class LoadedGameViewModel : BaseViewModel
    {
        public LoadedGameViewModel(GameSessionInfoModel model)
        {
            Model = model;
        }

        public GameSessionInfoModel Model { get; private set; }
        public bool IsNull { get { return Model == null; } }
    }
}

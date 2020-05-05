using KernelDLL.Init;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Init
{
    public sealed class LoadedGameViewModelLegacy : BaseViewModel
    {
        public LoadedGameViewModelLegacy(SavedGameModel model)
        {
            Model = model;
        }

        public SavedGameModel Model { get; private set; }
        public bool IsNull { get { return Model == null; } }
    }
}

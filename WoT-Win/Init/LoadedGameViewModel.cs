using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Init;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Init
{
    public sealed class LoadedGameViewModel : BaseViewModel
    {
        public LoadedGameViewModel(SavedGameModel model)
        {
            Model = model;
        }

        public SavedGameModel Model { get; private set; }
        public bool IsNull { get { return Model == null; } }
    }
}

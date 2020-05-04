using KernelDLL.Game.Models;
using System.Collections.Generic;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.GUI
{
    public class AreaViewModel : BaseViewModel
    {
        public AreaViewModel(AreaModelLegacy model)
        {
            Model = model;          
        }

        private AreaModelLegacy Model { get; set; }      
    }
}

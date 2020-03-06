using KernelDLL.Game.Models;
using System.Collections.Generic;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.GUI
{
    public class AreaViewModel : BaseViewModel
    {
        public AreaViewModel(AreaModel model)
        {
            Model = model;          
        }

        private AreaModel Model { get; set; }      
    }
}

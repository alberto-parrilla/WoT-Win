using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Models;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Objects
{
    public class PlayerItemViewModel : BaseViewModel
    {
        public PlayerItemViewModel(PlayerItemModel model)
        {
            Model = model;
            IsEquiped = false;
        }

        private PlayerItemModel Model { get; set; }

        public string Name { get { return Model.Name; } }
        public EnumObjectType ObjectType { get { return Model.ObjectType; } }
        public int ObjectId { get { return Model.ObjectId; } }
        public string Icon { get { return Model.Icon; } }

        private bool _isEquiped;

        public bool IsEquiped
        {
            get { return _isEquiped; }
            set
            {
                _isEquiped = value;
                OnPropertyChanged();
            }
        }
    }
}

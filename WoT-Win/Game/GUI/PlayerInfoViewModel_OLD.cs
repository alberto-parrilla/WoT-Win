using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoT_Win.Common.ViewModels;
using WoT_Win.Game.Objects;

namespace WoT_Win.Game.GUI
{
    public sealed class PlayerInfoViewModel_OLD : BaseViewModel
    {
        public PlayerInfoViewModel_OLD(int page, PlayerViewModel player)
        {
            SelectedIndex = page;
            Player = player;
            InventoryViewModel = new PlayerInventoryViewModel(player);
        }

        public PlayerViewModel Player { get; set; }
        public PlayerInventoryViewModel InventoryViewModel { get; private set; }
        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }
    }
}

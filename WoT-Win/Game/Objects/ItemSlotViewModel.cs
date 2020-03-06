using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using KernelDLL.Common;
using KernelDLL.Game.Enums;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Objects
{
    public sealed class ItemSlotViewModel : BaseViewModel
    {
        public ItemSlotViewModel(EnumSlotType type)
        {
            SlotType = type;
        }

        public EnumSlotType SlotType { get; private set; }
        public EnumObjectType ItemType { get; private set; }
        public int? ItemId { get; private set; }

        public string SlotIcon { get { return ItemId.HasValue ? ItemIcon : BaseIcon; } }
        public string BaseIcon { get { return string.Format(@"{0}\{1}_bw.png", Util.ItemsIconPath, SlotType); } }
        public string ItemIcon { get { return string.Format(@"{0}\{1}_{2}.png", Util.ObjectsIconPath, ItemType, ItemId); } }

        public void SetItem(EnumObjectType type, int? id)
        {
            ItemType = type;
            ItemId = id;
            OnPropertyChanged("SlotIcon");
        }

    }
}

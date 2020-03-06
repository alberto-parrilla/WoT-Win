using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Common;
using KernelDLL.Game.Enums;

namespace KernelDLL.Game.Models
{
    public class PlayerItemModel
    {
        public PlayerItemModel(int objectId, string name, EnumObjectType objectType, EnumSlotType slotType)
        {
            ObjectId = objectId;
            Name = name;
            ObjectType = objectType;
            SlotType = slotType;
        }

        public int ObjectId { get; private set; }
        public string Name { get; private set; }
        public EnumObjectType ObjectType { get; private set; }
        public string Icon { get { return string.Format(@"{0}\{1}_{2}.png", Util.ObjectsIconPath, ObjectType, ObjectId); } }
        public EnumSlotType SlotType { get; private set; }
    }
}

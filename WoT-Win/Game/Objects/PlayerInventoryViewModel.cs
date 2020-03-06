using System.Collections.Generic;
using KernelDLL.Game.Enums;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Objects
{
    public class PlayerInventoryViewModel : BaseViewModel
    {
        public PlayerInventoryViewModel(PlayerViewModel player)
        {
            HeadSlot = new ItemSlotViewModel(EnumSlotType.Head);
            NeckSlot = new ItemSlotViewModel(EnumSlotType.Neck);
            CloakSlot = new ItemSlotViewModel(EnumSlotType.Cloak);
            BodySlot = new ItemSlotViewModel(EnumSlotType.Body);
            GlovesSlot = new ItemSlotViewModel(EnumSlotType.Glove);
            LeftRingSlot = new ItemSlotViewModel(EnumSlotType.Ring);
            LeftItemSlot = new ItemSlotViewModel(EnumSlotType.Item);
            RightRingSlot = new ItemSlotViewModel(EnumSlotType.Ring);
            RightItemSlot = new ItemSlotViewModel(EnumSlotType.Item);
            BeltSlot = new ItemSlotViewModel(EnumSlotType.Belt);
            LegsSlot = new ItemSlotViewModel(EnumSlotType.Legs);
            FeetSlot = new ItemSlotViewModel(EnumSlotType.Foot);

            Player = player;
        }

        private PlayerViewModel Player { get; set; }
        public List<PlayerItemViewModel> Items { get { return Player.Items; } }
        public ItemSlotViewModel HeadSlot { get; private set; }
        public ItemSlotViewModel NeckSlot { get; private set; }
        public ItemSlotViewModel CloakSlot { get; private set; }
        public ItemSlotViewModel BodySlot { get; private set; }
        public ItemSlotViewModel GlovesSlot { get; private set; }
        public ItemSlotViewModel LeftRingSlot { get; private set; }
        public ItemSlotViewModel LeftItemSlot { get; private set; }
        public ItemSlotViewModel RightRingSlot { get; private set; }
        public ItemSlotViewModel RightItemSlot { get; private set; }
        public ItemSlotViewModel BeltSlot { get; private set; }
        public ItemSlotViewModel LegsSlot { get; private set; }
        public ItemSlotViewModel FeetSlot { get; private set; }
    }
}

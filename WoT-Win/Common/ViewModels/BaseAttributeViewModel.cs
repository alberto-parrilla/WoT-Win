using System.Collections.Generic;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Models;

namespace WoT_Win.Common.ViewModels
{
    public class BaseAttributeViewModel : BaseViewModel
    {
        public EnumAttribute Type { get; private set; }
        public int BaseValue { get; private set; }
        public int RaceMod { get; private set; }
        public int NationMod { get; private set; }
        public List<AttributeModModel> DynamicMod { get; private set; }
    }
}

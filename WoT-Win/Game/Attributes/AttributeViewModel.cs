using System.Collections.Generic;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Models;

namespace WoT_Win.Common.ViewModels
{
    public class AttributeViewModel : BaseViewModel
    {
        public AttributeViewModel(AttributeModel model)
        {
            Model = model;
        }

        private AttributeModel Model { get; set; }
        public EnumAttribute Type { get { return Model.Type; } }
        public string Header { get { return Type.ToString(); } }
        public int BaseValue { get { return Model.BaseValue; } }
        public int RaceMod { get { return Model.RaceMod; } }
        public int NationMod { get { return Model.LocationMod; } }   
        public int Value { get { return BaseValue + RaceMod + NationMod; } }
        public List<AttributeModModel> DynamicMod { get { return Model.DynamicMod; } }     
    }
}

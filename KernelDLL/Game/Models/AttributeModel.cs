using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Enums;

namespace KernelDLL.Game.Models
{
    public class AttributeModel
    {
        public AttributeModel(EnumAttribute type, int baseValue, int raceMod, int nationMod, List<AttributeModModel> dynamicMod)
        {
            Type = type;
            BaseValue = baseValue;
            RaceMod = raceMod;
            NationMod = nationMod;
            DynamicMod = dynamicMod;
        }

        public EnumAttribute Type { get; private set; }
        public string Header { get { return Type.ToString(); } }
        public int BaseValue { get; private set; }
        public int RaceMod { get; private set; }
        public int NationMod { get; private set; }     
        public int Value { get { return BaseValue + RaceMod + NationMod; } }
        public List<AttributeModModel> DynamicMod { get; private set; }       
    }
}

using System.Collections.Generic;
using KernelDLL.Common;
using KernelDLL.Game.Enums;

namespace KernelDLL.Game.Models
{
    public class AttributeModel : IMergeable<CoreDatabase.Context.Game.Attribute>
    {
        public AttributeModel(EnumAttribute type, int baseValue, int raceMod, int locationMod, List<AttributeModModel> dynamicMod)
        {
            Type = type;
            BaseValue = baseValue;
            RaceMod = raceMod;
            LocationMod = locationMod;
            DynamicMod = dynamicMod;
        }

        public AttributeModel(CoreDatabase.Context.Game.Attribute model)
        {
            Merge(model);
        }


        public EnumAttribute Type { get; private set; }
        public string Header { get { return Type.ToString(); } }
        public int BaseValue { get; private set; }
        public int RaceMod { get; private set; }
        public int LocationMod { get; private set; }     
        public int Value { get { return BaseValue + RaceMod + LocationMod; } }
        public List<AttributeModModel> DynamicMod { get; private set; }

        public void Merge(CoreDatabase.Context.Game.Attribute source)
        {
            Type = (EnumAttribute)source.Type;
            BaseValue = source.BaseValue;
            RaceMod = source.RaceMod;
            LocationMod = source.LocationMod;
            //DynamicMod = source.DynamicMod;
        }
    }
}

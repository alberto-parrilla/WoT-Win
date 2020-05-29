using System;
using CoreDatabase.Context.Data;
using KernelDLL.Common;

namespace KernelDLL.Creation.Models
{
    [Serializable]
    public class AttributeModifierModel : IMergeable<AttributeModifier>
    {
        public AttributeModifierModel(AttributeModifier attributeModifier)
        {
            Merge(attributeModifier);
        }

        public int? RaceId { get; private set; }
        public int? LocationId { get; private set; }
        public int Attribute { get; private set; }
        public int ModifierValue { get; private set; }

        public void Merge(AttributeModifier source)
        {
            RaceId = source.RaceId;
            LocationId = source.LocationId;
            Attribute = source.Attibute;
            ModifierValue = source.ModifierValue;
        }
    }
}

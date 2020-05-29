using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data
{
    public class AttributeModifier : BusinessEntity
    {
        public int? RaceId { get;set; }
        public int? LocationId { get; set; }
        public int Attibute { get; set; }
        public int ModifierValue { get; set; }
    }
}

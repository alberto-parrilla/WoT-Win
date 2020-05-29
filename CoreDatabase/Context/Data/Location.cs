using System.Collections.Generic;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data
{
    public class Location : GameBusinessEntity
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<AttributeModifier> AttributeMod { get; set; }
    }
}

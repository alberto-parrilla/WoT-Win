using System;
using System.Collections.Generic;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data
{
    public class Race : GameBusinessEntity
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<Location> Locations { get; set; }
        public List<AttributeModifier> AttributeMod { get; set; }
    }
}

using System.Collections.Generic;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Game
{
    public class Attribute : GameBusinessEntity
    {
        public int Type { get; set; }
        public int BaseValue { get; set; }
        public int RaceMod { get; set; }
        public int LocationMod { get; set; }
        public List<AttributeMod> DynamicMod { get; set; }
    }
}

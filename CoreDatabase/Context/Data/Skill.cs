using System.Collections.Generic;
using CoreDatabase.Abstractions;
using CoreDatabase.Context.Game;

namespace CoreDatabase.Context.Data
{
    public class Skill : GameBusinessEntity
    {
        public string Name { get; set; }
        public int Cost { get; protected set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public int Mode { get; set; }
        public int Area { get; set; }
        public int RangeArea { get; set; }
        public int Distance { get; set; }
        public int RangeDistance { get; set; }
        public int Duration { get; set; }
        public int TimeDuration { get; set; }
        public List<AttributeMod> Mods { get; set; }
    }
}

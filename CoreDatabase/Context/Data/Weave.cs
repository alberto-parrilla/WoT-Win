using System.Collections.Generic;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data
{
    public class Weave : GameBusinessEntity
    {
        public List<int> Type { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
    }
}

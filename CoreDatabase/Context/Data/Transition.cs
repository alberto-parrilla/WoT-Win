using System.Collections.Generic;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data
{
    public class Transition : BusinessEntity
    {
        public int SourceArea { get; set; }
        public int SourceScene { get; set; }
        public int TargetArea { get; set; }
        public int TargetScene { get; set; }
        public List<int> Coords { get; set; }
        public string Display { get; set; }
    }
}

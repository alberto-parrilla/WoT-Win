using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Game
{
    public class AttributeMod : BusinessEntity
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }
}

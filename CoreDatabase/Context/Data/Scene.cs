using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data
{
    public class Scene: BusinessEntity
    {
        public int AreaGameId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
    }
}

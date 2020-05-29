using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data
{
    public class Gender : GameBusinessEntity
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}

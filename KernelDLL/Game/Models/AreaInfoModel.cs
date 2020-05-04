using System;
using CoreDatabase.Context.Data;
using KernelDLL.Common;

namespace KernelDLL.Game.Models
{
    [Serializable]
    public class AreaInfoModel : IMergeable<Area>
    {
        public AreaInfoModel(Area source)
        {
            Merge(source);
        }

        public int Id { get; private set; }
        public int GameId { get; private set; }
        public string DisplayName { get; private set; }

        public void Merge(Area source)
        {
            if (source == null) throw new NullReferenceException("Area cannot be null");
            Id = source.Id;
            GameId = source.GameId;
            DisplayName = source.DisplayName;
        }
    }
}

using System;
using CoreDatabase.Context.Data;
using KernelDLL.Common;

namespace KernelDLL.Game.Models
{
    [Serializable]
    public class SceneInfoModel : IMergeable<Scene>
    {
        public SceneInfoModel(Scene source)
        {
            Merge(source);
        }

        public int Id { get; private set; }
        public int GameId { get; private set; }
        public int AreaGameId { get; private set; }
        public string DisplayName { get; private set; }

        public void Merge(Scene source)
        {
            if (source == null) throw new NullReferenceException("Scene cannot be null");
            Id = source.Id;
            GameId = source.GameId;
            AreaGameId = source.AreaGameId;
            DisplayName = source.DisplayName;
        }
    }
}

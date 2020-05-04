using System;
using CoreDatabase.Context.Game;
using KernelDLL.Common;

namespace KernelDLL.Game.Models
{
    [Serializable]
    public class GameSessionModel : IMergeable<GameSession>
    {
        public GameSessionModel(GameSession model)
        {
            Merge(model);
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int AreaId { get; private set; }
        public int SceneId { get; private set; }
        public int PlayerId { get; private set; }
        public DateTime SavedTime { get; private set; }

        public void Merge(GameSession source)
        {
            if (source == null) throw new NullReferenceException("Area cannot be null");
            Id = source.Id;
            UserId = source.UserId;
            AreaId = source.AreaId;
            SceneId = source.SceneId;
            PlayerId = source.PlayerId;
            SavedTime = source.SavedTime;
        }
    }
}

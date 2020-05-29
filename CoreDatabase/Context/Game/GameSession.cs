using System;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Game
{
    public class GameSession : GameBusinessEntity
    {
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public int SceneId { get; set; }
        public int PlayerId { get; set; }
        public DateTime SavedTime { get; set; }
    }
}

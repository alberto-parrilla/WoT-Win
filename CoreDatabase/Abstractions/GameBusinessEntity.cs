using System;

namespace CoreDatabase.Abstractions
{
    [Serializable]
    public abstract class GameBusinessEntity : BusinessEntity
    {
        public int GameId { get; set; }
    }
}

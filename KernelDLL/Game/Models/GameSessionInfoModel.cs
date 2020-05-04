using System;

namespace KernelDLL.Game.Models
{
    [Serializable]
    public class GameSessionInfoModel
    {
        public GameSessionInfoModel(int userId, AreaInfoModel area, SceneInfoModel scene, PlayerInfoModel player)
        {
            UserId = userId;
            Area = area;
            Scene = scene;
            Player = player;
        }

        public int UserId { get; private set; }
        public AreaInfoModel Area { get; private set; }
        public SceneInfoModel Scene { get; private set; }
        public PlayerInfoModel Player { get; private set; }
    }
}

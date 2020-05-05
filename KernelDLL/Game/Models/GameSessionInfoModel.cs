using System;
using System.Globalization;

namespace KernelDLL.Game.Models
{
    [Serializable]
    public class GameSessionInfoModel
    {
        public GameSessionInfoModel(int userId, AreaInfoModel area, SceneInfoModel scene, PlayerInfoModel player, DateTime savedTime)
        {
            UserId = userId;
            Area = area;
            Scene = scene;
            Player = player;
            SavedTime = savedTime.ToString(CultureInfo.InvariantCulture);
        }

        public int UserId { get; private set; }
        public AreaInfoModel Area { get; private set; }
        public SceneInfoModel Scene { get; private set; }
        public PlayerInfoModel Player { get; private set; }

        public int PlayerId { get { return Player.PlayerId; } }
        public string PlayerName { get { return Player.Name; } }
        public string Faceset { get { return Player.Avatar; } }
        public int AreaId { get { return Area.Id; } }
        public string AreaName { get { return Area.DisplayName; } }
        public int SceneId { get { return Scene.Id; } }
        public string SceneName { get { return Scene.DisplayName; } }
        public string SavedTime { get; }
    }
}

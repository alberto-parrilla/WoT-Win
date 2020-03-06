using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Init
{
    public class SavedGameModel
    {
        public SavedGameModel(PlayerSavedModel player, AreaSavedModel area, SceneSavedModel scene)
        {
            Player = player;
            Area = area;
            Scene = scene;
        }

        private PlayerSavedModel Player { get; set; }
        private AreaSavedModel Area { get; set; }
        private SceneSavedModel Scene { get; set; }

        public int PlayerId { get { return Player.Id; } }
        public string PlayerName { get { return Player.Name; } }
        public string Faceset { get { return Player.Faceset; } }
        public int AreaId { get { return Area.Id; } }
        public string AreaName { get { return Area.Name; } }
        public int SceneId { get { return Scene.Id; } }
        public string SceneName { get { return Scene.Name; } }
        public string Savetime { get { return Player.Savetime; } }
    }
}

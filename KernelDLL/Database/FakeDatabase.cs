using System;
using System.Collections.Generic;
using System.Linq;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Models;
using KernelDLL.Init;

namespace KernelDLL.Database
{
    public class FakeDatabase : IDatabase
    {
        private IList<PlayerModel> Players { get; set; }
        private IList<AreaModelLegacy> Areas { get; set; }
        private IList<SceneModelLegacy> Scenes { get; set; }
        private IList<SavedGameModel> SavedGames { get; set; }

        public FakeDatabase()
        {
            Players = InitPlayers();
            Areas = InitAreas();
            Scenes = InitScenes();
            SavedGames = InitSavedGames();
        }

        private IList<PlayerModel> InitPlayers()
        {
            return new List<PlayerModel>()
            {
                new PlayerModel(1, "Gwalchaved", 0, 0, 0, false,
                    new AttributeModel(EnumAttribute.Str, 15, 0, 0, null),
                    new AttributeModel(EnumAttribute.Dex, 12, 0, 0, null),
                    new AttributeModel(EnumAttribute.Con, 11, 0, 0, null),
                    new AttributeModel(EnumAttribute.Int, 16, 0, 0, null),
                    new AttributeModel(EnumAttribute.Wis, 10, 0, 0, null),
                    new AttributeModel(EnumAttribute.Cha, 17, 0, 0, null),
                    null, null, GetPlayerItems())
            };
        }

        public PlayerModel LoadPlayer(int id)
        {
            return Players.FirstOrDefault(p => p.Id == id);
        }

        private List<PlayerItemModel> GetPlayerItems()
        {
            return new List<PlayerItemModel>()
            {
                new PlayerItemModel(1, "Yelmo acero", EnumObjectType.Sword, EnumSlotType.Head),
                new PlayerItemModel(1, "Yelmo cuero", EnumObjectType.Sword, EnumSlotType.Head),
                new PlayerItemModel(1, "Capa viaje", EnumObjectType.Sword, EnumSlotType.Cloak),
                new PlayerItemModel(1, "Capa guardian", EnumObjectType.Sword, EnumSlotType.Cloak),
                new PlayerItemModel(1, "Collar oro", EnumObjectType.Sword, EnumSlotType.Neck),
                new PlayerItemModel(1, "Collar perlas", EnumObjectType.Sword, EnumSlotType.Neck),
                new PlayerItemModel(1, "Camisa", EnumObjectType.Sword, EnumSlotType.Body),
                new PlayerItemModel(1, "Peto cuero", EnumObjectType.Sword, EnumSlotType.Body),
                new PlayerItemModel(1, "Anillo oro", EnumObjectType.Sword, EnumSlotType.Ring),
                new PlayerItemModel(1, "Anillo plata", EnumObjectType.Sword, EnumSlotType.Ring),
                new PlayerItemModel(1, "Anillo bronce", EnumObjectType.Sword, EnumSlotType.Ring),
                new PlayerItemModel(2, "Espada corta", EnumObjectType.Sword, EnumSlotType.Item),
                new PlayerItemModel(3, "Espada larga", EnumObjectType.Sword, EnumSlotType.Item),
                new PlayerItemModel(2, "Daga", EnumObjectType.Sword, EnumSlotType.Item),
                new PlayerItemModel(2, "Cinturon cuero", EnumObjectType.Sword, EnumSlotType.Belt),
                new PlayerItemModel(2, "Cinturon viaje", EnumObjectType.Sword, EnumSlotType.Belt),
                new PlayerItemModel(2, "Pantalones tela", EnumObjectType.Sword, EnumSlotType.Legs),
                new PlayerItemModel(2, "Perneras cuero", EnumObjectType.Sword, EnumSlotType.Legs),
                new PlayerItemModel(2, "Botas cuero", EnumObjectType.Sword, EnumSlotType.Foot),
                new PlayerItemModel(2, "Botas viaje", EnumObjectType.Sword, EnumSlotType.Foot),
            };
        }

        private IList<SavedGameModel> InitSavedGames()
        {
            return new List<SavedGameModel>()
            {
                new SavedGameModel(new PlayerSavedModel(1, "Gwalchaved", DateTime.Now.ToString("dd/MM/yyyy HH:mm")), new AreaSavedModel(LoadArea(1).Id, LoadArea(1).Header), new SceneSavedModel(LoadScene(1, 1).Id, LoadScene(1, 1).Header)),
                new SavedGameModel(new PlayerSavedModel(1, "Gwalchaved", "01.01.2019 10:00"), new AreaSavedModel(LoadArea(1).Id, LoadArea(1).Header), new SceneSavedModel(LoadScene(2, 1).Id, LoadScene(2,1).Header)), 
                null
            };
        }

        public IList<SavedGameModel> LoadSavedGames()
        {
         return SavedGames;
        }

        private IList<AreaModelLegacy> InitAreas()
        {
            return new List<AreaModelLegacy>()
            {
                new AreaModelLegacy(1, "Campo de Emond"),                
            };
        }

        public AreaModelLegacy LoadArea(int id)
        {
            return Areas.FirstOrDefault(a => a.Id == id);
        }

        private IList<SceneModelLegacy> InitScenes()
        {
            return new List<SceneModelLegacy>()
            {
                new SceneModelLegacy(1, "CampoEmond", "Campo de Emond", 1, 12288,12288, new List<TransitionModelLegacy>(), null),
                new SceneModelLegacy(2, "PosadaManantialPB", "Posada del Manantial (Planta Baja)", 1, 2304, 1536,  new List<TransitionModelLegacy>(), null),
                new SceneModelLegacy(3, "PosadaManantial1P", "Posada del Manantial (1 Planta)", 1, -1, -1,  new List<TransitionModelLegacy>(), null),
                new SceneModelLegacy(4, "PosadaManantialS", "Posada del Manantial (Sotano)", 1, -1, -1, new List<TransitionModelLegacy>(), null ),
                new SceneModelLegacy(1, "BoesqueOeste", "Bosque del Oeste", 2, -1, -1, new List<TransitionModelLegacy>(), null),
                new SceneModelLegacy(2, "CasaTam", "Casa de Tam", 2, -1, -1, new List<TransitionModelLegacy>(),  null)
            };
        }

        public SceneModelLegacy LoadScene(int id, int areaId)
        {
            return Scenes.FirstOrDefault(s => s.Id == id && s.AreaId == areaId);
        }
    }
}

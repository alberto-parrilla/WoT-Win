using System.Collections.Generic;
using KernelDLL.Game.Models;
using KernelDLL.Init;
using KernelDLL.Database;
using KernelDLL.Game.Enums;

namespace KernelDLL.Common
{
    public class DataManager : IDataManager
    {
        private readonly string _appVersion = "v0.2";

        private IDatabase _db;
        private IRepositoryManager _repositoryManager;
        private PlayerModel _player;
        private AreaModel _area;
        private SceneModel _scene;

        public DataManager(IDatabase db, IRepositoryManager repositoryManager)
        {
            _db = db;
            _repositoryManager = repositoryManager;
            Feats = new List<BaseSkillModel>()
            {
                new FeatModel(1, "Competencia en arma", null, null, null),
                new FeatModel(2, "Competencia en armadura", null, null, null),
                new FeatModel(3, "Competencia en escudo", null, null, null),
            };

            Skills = new List<BaseSkillModel>()
            {
                new SkillActiveTargetNear(1001, "Abrir cerraduras", 2, null, null, null),
                new SkillActiveTargetNear(1002, "Averiguar intenciones", 3, null, null, null),
                new SkillActiveZoneNear(1003, "Buscar",2, null, null, null, 10)
            };

            Weaves = new List<BaseWeaveModel>()
            {
                new BaseWeaveModel(1, new List<EnumWeaveType>() {EnumWeaveType.Earth}, "Crear tierra", 1, null, null, null),
                new BaseWeaveModel(2, new List<EnumWeaveType>() {EnumWeaveType.Earth}, "Mover tierra", 1, null, null, null),
                new BaseWeaveModel(3, new List<EnumWeaveType>() {EnumWeaveType.Earth}, "Herramienta de tierra", 1, null, null, null),
                new BaseWeaveModel(101, new List<EnumWeaveType>() {EnumWeaveType.Fire}, "Crear fuego", 1, null, null, null),
                new BaseWeaveModel(102, new List<EnumWeaveType>() {EnumWeaveType.Fire}, "Crear luz", 1, null, null, null),
                new BaseWeaveModel(103, new List<EnumWeaveType>() {EnumWeaveType.Fire}, "Herramienta de fuego", 1, null, null, null),
                new BaseWeaveModel(104, new List<EnumWeaveType>() {EnumWeaveType.Fire}, "Calentar objeto", 1, null, null, null),
                new BaseWeaveModel(201, new List<EnumWeaveType>() {EnumWeaveType.Water}, "Crear agua", 1, null, null, null),
                new BaseWeaveModel(202, new List<EnumWeaveType>() {EnumWeaveType.Water}, "Mover agua", 1, null, null, null),
                new BaseWeaveModel(203, new List<EnumWeaveType>() {EnumWeaveType.Water}, "Eliminar agua (secar)", 1, null, null, null),
                new BaseWeaveModel(204, new List<EnumWeaveType>() {EnumWeaveType.Water}, "Herramienta de agua", 1, null, null, null),
                new BaseWeaveModel(301, new List<EnumWeaveType>() {EnumWeaveType.Wind}, "Crear aire", 1, null, null, null),
                new BaseWeaveModel(302, new List<EnumWeaveType>() {EnumWeaveType.Wind}, "Mover aire", 1, null, null, null),
                new BaseWeaveModel(303, new List<EnumWeaveType>() {EnumWeaveType.Wind}, "Herramienta de aire", 1, null, null, null),
                new BaseWeaveModel(304, new List<EnumWeaveType>() {EnumWeaveType.Wind}, "Endurecer aire", 1, null, null, null),
                //new BaseWeaveModel(401, new List<EnumWeaveType>() {EnumWeaveType.Energy}, "", 1, null, null, null),
                //new BaseWeaveModel(402, new List<EnumWeaveType>() {EnumWeaveType.Energy}, "", 1, null, null, null),
                //new BaseWeaveModel(403, new List<EnumWeaveType>() {EnumWeaveType.Energy}, "", 1, null, null, null),
            };
                
            CurrentCulture = "es-ES";
        }

        public string AppTitle => $"WoT- Win {_appVersion}";

        public static string CurrentCulture { get; set; }
        public List<BaseSkillModel> Skills { get; private set; }
        public List<BaseSkillModel> Feats { get; private set; }
        public List<BaseWeaveModel> Weaves { get; private set; }

        public IList<SavedGameModel> LoadedGames { get; private set; }

        public void LoadGames()
        {
            LoadedGames = _db.LoadSavedGames();
        }

        public void LoadGame()
        {

        }

        public void LoadPlayer(int id)
        {
            _player = _db.LoadPlayer(id);
        }

        public PlayerModel GetPlayer()
        {
            return _player;
        }

        public void LoadArea(int id)
        {
            _area = _db.LoadArea(id);            
        }

        public AreaModel GetArea()
        {
            return _area;
        }

        public void LoadScene(int id, int areaId)
        {
            _scene = _db.LoadScene(id, areaId);
            _scene.LoadTransitions(_repositoryManager);
        }

        public SceneModel GetScene()
        {
            return _scene;
        }

        public List<BaseSkillModel> GetSkills()
        {
            return Skills;
        }

        public List<BaseSkillModel> GetFeats()
        {
            return Feats;
        }

        public List<BaseWeaveModel> GetWeaves()
        {
            return Weaves;
        }
    }

    
}

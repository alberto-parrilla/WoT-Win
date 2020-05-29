using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreDatabase.Managers;
using KernelDLL.Creation.Models;
using KernelDLL.Game.Models;
using KernelDLL.Init;
using KernelDLL.Database;
using KernelDLL.Game.Enums;
using KernelDLL.Network.Request;

namespace KernelDLL.Common
{
    public class DataManager : IDataManager
    {
        //private IDatabase _db;
        //private IRepositoryManager _repositoryManager;
        private IDataDbManager _dataDbManager;
        private PlayerModel _player;
        private AreaModelLegacy _area;
        private SceneModelLegacy _scene;
        private ICreateFactory _createFactory;

        private List<RaceModel> Races { get; set; }
        private List<GenderModel> Genders { get; set; }

        public DataManager()
        {
            _dataDbManager = new DataDbManager();
            _createFactory = new CreateFactory();
        }

        public DataManager(IDatabase db, IRepositoryManager repositoryManager)
        {
            //_db = db;
            //_repositoryManager = repositoryManager;
            _dataDbManager = new DataDbManager();
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
        }

        public List<BaseSkillModel> Skills { get; private set; }
        public List<BaseSkillModel> Feats { get; private set; }
        public List<BaseWeaveModel> Weaves { get; private set; }

        public IList<SavedGameModel> LoadedGames { get; private set; }

        //////public void LoadGames()
        //////{
        //////    LoadedGames = _db.LoadSavedGames();
        //////}

        public void LoadGame()
        {

        }

        //////public void LoadPlayer(int id)
        //////{
        //////    _player = _db.LoadPlayer(id);
        //////}

        public PlayerModel GetPlayer()
        {
            return _player;
        }

        //////public void LoadArea(int id)
        //////{
        //////    _area = _db.LoadArea(id);            
        //////}

        public AreaModelLegacy GetArea()
        {
            return _area;
        }

        public async Task<AreaInfoModel> GetAreaInfoAsync(int areaId)
        {
            var area = await _dataDbManager.GetAreaByIdAsync(areaId);
            return new AreaInfoModel(area);
        }

        public async Task<AreaModel> GetAreaAsync(int areaId)
        {
            var area = await _dataDbManager.GetAreaByIdAsync(areaId);
            return new AreaModel(area);
        }

        //////public void LoadScene(int id, int areaId)
        //////{
        //////    _scene = _db.LoadScene(id, areaId);
        //////    _scene.LoadTransitions(_repositoryManager);
        //////}

        public SceneModelLegacy GetScene()
        {
            return _scene;
        }

        public async Task<SceneInfoModel> GetSceneInfoAsync(int sceneId)
        {
            var scene = await _dataDbManager.GetSceneByIdAsync(sceneId);
            return new SceneInfoModel(scene);
        }

        public async Task<SceneModel> GetSceneAsync(int sceneId)
        {
            var scene = await _dataDbManager.GetSceneByIdAsync(sceneId);
            return new SceneModel(scene);
        }

        public async Task<List<TransitionModel>> GetTransitionsAsync(int areaId, int sceneId)
        {
            var transitions = await _dataDbManager.GetTransitionsByAreaIdAndSceneIdAsync(areaId, sceneId);

            return transitions.Select(t => new TransitionModel(t)).ToList();
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

        public async Task<List<RaceModel>> GetRacesAsync()
        {
            var races = await _dataDbManager.GetRacesAsync();
            return races.Select(r => new RaceModel(r)).ToList();
        }

        public async Task<List<GenderModel>> GetGendersAsync()
        {
            var Genders = await _dataDbManager.GetGendersAsync();
            return Genders.Select(r => new GenderModel(r)).ToList();
        }

        public async Task<List<LocationModel>> GetLocationsAsync()
        {
            var locations = await _dataDbManager.GetLocationsAsync();
            return locations.Select(l => new LocationModel(l)).ToList();
        }

        public async Task<List<BaseSkillModel>> GetSkillsAsync()
        {
            var skills = await _dataDbManager.GetSkillsAsync();
            return skills.Select(s => _createFactory.CreateBaseSkillModel(s)).ToList();
        }

        public async Task<List<BaseSkillModel>> GetFeatsAsync()
        {
            var feats = await _dataDbManager.GetFeatsAsync();
            return feats.Select(f => _createFactory.CreateBaseFeatModel(f)).ToList();
        }

        public async Task<List<BaseWeaveModel>> GetWeavesAsync()
        {
            var weaves = await _dataDbManager.GetWeavesAsync();
            return weaves.Select(w => _createFactory.CreateBaseWeaveModel(w)).ToList();
        }

        public async Task<List<AvatarToolsetModel>> GetAvatarToolsetAsync(EnumPlayerDataType type)
        {
            if (Races == null)
            {
                Races = await GetRacesAsync();
            }

            if (Genders == null)
            {
                Genders = await GetGendersAsync();
            }

            List<AvatarToolsetModel> list = new List<AvatarToolsetModel>();
            if (type == EnumPlayerDataType.AvatarBodyToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int bodyAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= bodyAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Skin", i, -1));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarFaceToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int faceAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= faceAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Face", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarBaseHairToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int baseHairAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= baseHairAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "BaseHair", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarFrontHairToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int faceAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= faceAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "FrontHair", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarRearHairToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int faceAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= faceAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "RearHair", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarEyebrowsToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int eyebrowsAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= eyebrowsAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Eyebrows", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarBaseEyesToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int baseEyesAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= baseEyesAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "BaseEyes", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarEyesToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int eyesAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= eyesAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Eyes", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarEarsToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int earsAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= earsAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Ears", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarNoseToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int noseAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= noseAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Nose", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarMouthToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int mouthAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= mouthAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Mouth", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarBeardToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int beardAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= beardAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Beard", 1, i));
                        }
                    }
                }
            }
            else if (type == EnumPlayerDataType.AvatarExtrasToolset)
            {
                foreach (var race in Races)
                {
                    foreach (var gender in Genders)
                    {
                        int extrasAmount = GetResourcesAmount(type, race, gender);
                        for (int i = 1; i <= extrasAmount; i++)
                        {
                            list.Add(new AvatarToolsetModel(race.DisplayName, gender.DisplayName, type, "Extras", 1, i));
                        }
                    }
                }
            }

            return list;
        }

        private int GetResourcesAmount(EnumPlayerDataType type, RaceModel race, GenderModel gender)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            var resources = myAssembly.GetManifestResourceNames();
            switch (type)
            {
                case EnumPlayerDataType.AvatarBodyToolset:
                    string pattern = $"Charaset.{race.DisplayName}_{gender.DisplayName}_Skin";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarFaceToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_Face";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarBaseHairToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_BaseHair";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarFrontHairToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_FrontHair";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarRearHairToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_RearHair";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarEyebrowsToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_Eyebrows";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarBaseEyesToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_BaseEyes";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarEyesToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_Eyes";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarEarsToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_Ears";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarNoseToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_Nose";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarMouthToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_Mouth";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarBeardToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_Beard";
                    return resources.Count(r => r.Contains(pattern));
                case EnumPlayerDataType.AvatarExtrasToolset:
                    pattern = $"Faceset.{race.DisplayName}_{gender.DisplayName}_Extras";
                    return resources.Count(r => r.Contains(pattern));
            }
            return resources.Length;
        }
    }
}

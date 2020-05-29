using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KernelDLL.Common;
using KernelDLL.Creation.Models;
using KernelDLL.Game.Models;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    [Serializable]
    public class PlayerDataRequest : RequestMessageBase
    {
        public PlayerDataRequest(EnumPlayerDataType dataType)
        {
            DataType = dataType;
        }

        public EnumPlayerDataType DataType { get; }

        public override async Task<IResponse> ProcessRequestAsync()
        {
            IDataManager dataManager = new DataManager();
            switch (DataType)
            {
                case EnumPlayerDataType.Race:
                    var races = await dataManager.GetRacesAsync();
                    return new PlayerDataResponse<List<RaceModel>>(
                        races != null ? EnumPlayerDataResponse.Success : EnumPlayerDataResponse.Error, races);
                case EnumPlayerDataType.Gender:
                    var Genders = await dataManager.GetGendersAsync();
                    return new PlayerDataResponse<List<GenderModel>>(
                        Genders != null ? EnumPlayerDataResponse.Success : EnumPlayerDataResponse.Error, Genders);
                case EnumPlayerDataType.Location:
                    var locations = await dataManager.GetLocationsAsync();
                    return new PlayerDataResponse<List<LocationModel>>(locations != null ? EnumPlayerDataResponse.Success : EnumPlayerDataResponse.Error, locations);
                case EnumPlayerDataType.Skill:
                    var skills = await dataManager.GetSkillsAsync();
                    return new PlayerDataResponse<List<BaseSkillModel>>(skills != null ? EnumPlayerDataResponse.Success : EnumPlayerDataResponse.Error, skills);
                case EnumPlayerDataType.Feat:
                    var feats = await dataManager.GetFeatsAsync();
                    return new PlayerDataResponse<List<BaseSkillModel>>(feats != null ? EnumPlayerDataResponse.Success : EnumPlayerDataResponse.Error, feats);
                case EnumPlayerDataType.Weave:
                    var weaves = await dataManager.GetWeavesAsync();
                    return new PlayerDataResponse<List<BaseWeaveModel>>(weaves != null ? EnumPlayerDataResponse.Success : EnumPlayerDataResponse.Error, weaves);
                case EnumPlayerDataType.AvatarBodyToolset:
                case EnumPlayerDataType.AvatarFaceToolset:
                case EnumPlayerDataType.AvatarBaseHairToolset:
                case EnumPlayerDataType.AvatarFrontHairToolset:
                case EnumPlayerDataType.AvatarRearHairToolset:
                case EnumPlayerDataType.AvatarEyebrowsToolset:
                case EnumPlayerDataType.AvatarBaseEyesToolset:
                case EnumPlayerDataType.AvatarEyesToolset:
                case EnumPlayerDataType.AvatarEarsToolset:
                case EnumPlayerDataType.AvatarNoseToolset:
                case EnumPlayerDataType.AvatarMouthToolset:
                case EnumPlayerDataType.AvatarBeardToolset:
                case EnumPlayerDataType.AvatarExtrasToolset:
                    var avatarBodyToolset = await dataManager.GetAvatarToolsetAsync(DataType);
                    return new PlayerDataResponse<List<AvatarToolsetModel>>(avatarBodyToolset != null ? EnumPlayerDataResponse.Success : EnumPlayerDataResponse.Error, avatarBodyToolset);
            }
            return null;
        }
    }
}

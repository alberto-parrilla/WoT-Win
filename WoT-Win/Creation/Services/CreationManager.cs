using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using KernelDLL.Common;
using KernelDLL.Creation.Models;
using KernelDLL.Game.Enums;

namespace WoT_Win.Creation.Services
{
    public class CreationManager
    {
        //public int GetResourcesCounter(EnumRace race, EnumGender Gender, EnumResourceType type, int skin)
        //{
        //    var dir = type == EnumResourceType.Skin ? Util.GeneratorCharasetPath : Util.GeneratorPortraitPath;      
        //    string prefix = string.Format("{0}_{1}_{2}", race, Gender, type);
        //    if (type == EnumResourceType.Skin)
        //        prefix = string.Format("{0}_{1}_Skin", race, Gender);
        //    string sufix = "";
        //    switch (type)
        //    {
        //        case EnumResourceType.Face:
        //        case EnumResourceType.Nose:
        //            sufix = string.Format("Skin{0}", skin);
        //            break;
        //    }
          
        //    var pattern = string.Format("{0}*{1}.png", prefix, sufix);
        //    var files = Directory.GetFiles(dir, pattern);
        //    return files.Length;
        //}

        //////public int GetResourcesCounter(RaceModel race, EnumGender Gender, EnumResourceType type, int skin)
        //////{
        //////    var dir = type == EnumResourceType.Skin ? Util.GeneratorCharasetPath : Util.GeneratorPortraitPath;
        //////    string prefix = string.Format("{0}_{1}_{2}", race.GameId, Gender, type);
        //////    if (type == EnumResourceType.Skin)
        //////        prefix = string.Format("{0}_{1}_Skin", race.GameId, Gender);
        //////    string sufix = "";
        //////    switch (type)
        //////    {
        //////        case EnumResourceType.Face:
        //////        case EnumResourceType.Nose:
        //////            sufix = string.Format("Skin{0}", skin);
        //////            break;
        //////    }

        //////    var pattern = string.Format("{0}*{1}.png", prefix, sufix);
        //////    var files = Directory.GetFiles(dir, pattern);
        //////    return files.Length;
        //////}

        //////public string GetResourcesPath(bool isPortrait, int index, EnumRace race, EnumGender Gender, EnumResourceType type, int skin)
        //////{
        //////    string basePath = isPortrait ? Util.GeneratorPortraitPath : Util.GeneratorCharasetPath;
        //////    string path = "";
        //////    switch (type)
        //////    {
        //////        case EnumResourceType.Skin:
        //////            path = string.Format(@"{0}/{1}_{2}_Skin{3}.png", basePath, race, Gender, skin);
        //////            break;
        //////        case EnumResourceType.Face:
        //////        case EnumResourceType.Nose:
        //////            path = string.Format(@"{0}/{1}_{2}_{3}{4}_Skin{5}.png", basePath, race, Gender, type, index, skin);
        //////            break;
        //////        case EnumResourceType.Ears:
        //////            path = string.Format(@"{0}/{1}_{2}_{3}_Skin{4}.png",basePath, race, Gender, type, skin);
        //////            break;
        //////        case EnumResourceType.Hair1:
        //////        case EnumResourceType.Hair2:
        //////            path = string.Format(@"{0}/{1}_{2}_{3}_{4}.png", basePath, race, Gender, type, index);
        //////            break;
        //////        default:
        //////            path = string.Format(@"{0}/{1}_{2}_{3}{4}.png", basePath, race, Gender, type, index);
        //////            break;
        //////    }

        //////    return path;
        //////}

        //////public string GetResourcesPath(bool isPortrait, int index, RaceModel race, EnumGender Gender, EnumResourceType type, int skin)
        //////{
        //////    string basePath = isPortrait ? Util.GeneratorPortraitPath : Util.GeneratorCharasetPath;
        //////    string path = "";
        //////    switch (type)
        //////    {
        //////        case EnumResourceType.Skin:
        //////            path = string.Format(@"{0}/{1}_{2}_Skin{3}.png", basePath, race.GameId, Gender, skin);
        //////            break;
        //////        case EnumResourceType.Face:
        //////        case EnumResourceType.Nose:
        //////            path = string.Format(@"{0}/{1}_{2}_{3}{4}_Skin{5}.png", basePath, race.GameId, Gender, type, index, skin);
        //////            break;
        //////        case EnumResourceType.Ears:
        //////            path = string.Format(@"{0}/{1}_{2}_{3}_Skin{4}.png", basePath, race.GameId, Gender, type, skin);
        //////            break;
        //////        case EnumResourceType.Hair1:
        //////        case EnumResourceType.Hair2:
        //////            path = string.Format(@"{0}/{1}_{2}_{3}_{4}.png", basePath, race.GameId, Gender, type, index);
        //////            break;
        //////        default:
        //////            path = string.Format(@"{0}/{1}_{2}_{3}{4}.png", basePath, race.GameId, Gender, type, index);
        //////            break;
        //////    }

        //////    return path;
        //////}
    }
}

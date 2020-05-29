using System.Collections.Generic;
using KernelDLL.Creation.Models;

namespace KernelDLL.Common
{
    public interface IDataContainer
    {
        List<RaceModel> Races { get; set; }
        List<GenderModel> Genders { get; set; }
        List<LocationModel> Locations { get; set; }

        List<AvatarToolsetModel> AvatarToolsetItems { get; set; }
    }
}


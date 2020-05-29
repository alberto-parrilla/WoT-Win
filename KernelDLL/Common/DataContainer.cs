using System.Collections.Generic;
using KernelDLL.Creation.Models;

namespace KernelDLL.Common
{
    public class DataContainer : IDataContainer
    {
        public List<RaceModel> Races { get; set; }
        public List<GenderModel> Genders { get; set; }
        public List<LocationModel> Locations { get; set; }
        public List<AvatarToolsetModel> AvatarToolsetItems { get; set; }
    }
}


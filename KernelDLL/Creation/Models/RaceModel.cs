using System;
using System.Collections.Generic;
using System.Linq;
using CoreDatabase.Context.Data;
using KernelDLL.Common;
using KernelDLL.Creation.Interfaces;

namespace KernelDLL.Creation.Models
{
    [Serializable]
    public class RaceModel : IMergeable<Race>, ISelectable
    {
        public RaceModel(Race race)
        {
            Merge(race);
        }

        public int GameId { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public List<LocationModel> Locations { get; private set; }
        public List<AttributeModifierModel> AttributeMod { get; private set; }

        public void Merge(Race source)
        {
            GameId = source.GameId;
            DisplayName = source.DisplayName;
            Description = source.Description;
            Locations = source.Locations != null ? source.Locations.Select(l => new LocationModel(l)).ToList() : null;
            AttributeMod = source.AttributeMod != null ? source.AttributeMod.Select(a => new AttributeModifierModel(a)).ToList() : null;
        }
    }
}

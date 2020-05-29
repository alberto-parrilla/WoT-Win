using System;
using System.Collections.Generic;
using System.Linq;
using CoreDatabase.Context.Data;
using KernelDLL.Common;
using KernelDLL.Creation.Interfaces;

namespace KernelDLL.Creation.Models
{
    [Serializable]
    public class LocationModel : IMergeable<Location>, ISelectable
    {
        public LocationModel(Location location)
        {
            Merge(location);
        }

        public int GameId { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public List<AttributeModifierModel> AttributeMod { get; private set; }
        
        public void Merge(Location source)
        {
            GameId = source.GameId;
            DisplayName = source.DisplayName;
            Description = source.Description;
            AttributeMod = source.AttributeMod != null ? source.AttributeMod.Select(a => new AttributeModifierModel(a)).ToList() : null;
        }
    }
}

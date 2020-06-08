using System.Linq;
using KernelDLL.Common;
using WoT_Win.Creation.Models;

namespace WoT_Win.Creation.Services
{
    public class CreationManager
    {
        public CreationManager(IDataContainer dataContainer)
        {
            DataContainer = dataContainer;
            Player = new CreationPlayerModel();
        }
        public CreationPlayerModel Player { get; }
        private IDataContainer DataContainer { get; }

        public int GetRaceMod(int attributeId)
        {
            var race = DataContainer.Races.FirstOrDefault(r => r.GameId == Player.Race.GameId);
            if (race == null) return 0;
            var attribute = race.AttributeMod.FirstOrDefault(a => a.Attribute == attributeId);
            if (attribute == null) return 0;
            return attribute.ModifierValue;
        }

        public int GetLocationMod(int attributeId)
        {
            var location = DataContainer.Locations.FirstOrDefault(l => l.GameId == Player.Location.GameId);
            if (location == null) return 0;
            var attribute = location.AttributeMod.FirstOrDefault(a => a.Attribute == attributeId);
            if (attribute == null) return 0;
            return attribute.ModifierValue;
        }
    }
}

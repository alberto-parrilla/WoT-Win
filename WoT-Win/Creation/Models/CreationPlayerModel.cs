using KernelDLL.Creation.Models;

namespace WoT_Win.Creation.Models
{
    public class CreationPlayerModel
    {
        public string Name { get; set; }
        public RaceModel Race { get; set; }
        public GenderModel Gender { get; set; }
        public LocationModel Location { get; set; }
    }
}

using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Weaves
{
    public class PlayerWeaveViewModel : BaseViewModel
    {
        public PlayerWeaveViewModel(BaseWeaveViewModel weave, int progress)
        {
            Weave = weave;
            Progress = progress;
        }

        public BaseWeaveViewModel Weave { get; private set; }
        public int Progress { get; private set; }
        public string Name { get { return Weave.Name; } }
    }
}

using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Skills
{
    public class PlayerFeatViewModel : BaseViewModel
    {
        public PlayerFeatViewModel(BaseSkillViewModel feat, int progress)
        {
            Feat = feat;
            Progress = progress;
        }

        public BaseSkillViewModel Feat { get; private set; }
        private int _progress;

        public int Progress
        {
            get { return _progress;}
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public string Name { get { return Feat.Name; } }
        public string SmallIcon { get { return Feat.SmallIcon; } }
        public string LargeIcon { get { return Feat.LargeIcon; } }
        public bool IsCompleted { get { return Progress == 100; } }
    }
}

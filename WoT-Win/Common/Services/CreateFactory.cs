using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KernelDLL.Game.Models;
using KernelDLL.Init;
using WoT_Win.Common.ViewModels;
using WoT_Win.Game.Skills;
using WoT_Win.Game.Weaves;
using WoT_Win.Init;

namespace WoT_Win.Common.Services
{
    public class CreateFactory
    {
        public LoadedGameViewModel Create(SavedGameModel model)
        {
            return new LoadedGameViewModel(model);
        }

        public BaseSkillViewModel Create(BaseSkillModel model)
        {
            return new BaseSkillViewModel(model);
        }

        public PlayerSkillViewModel CreatePlayerSkill(BaseSkillViewModel skill, int progress)
        {
            return new PlayerSkillViewModel(skill, progress);
        }

        public PlayerFeatViewModel CreatePlayerFeat(BaseSkillViewModel feat, int progress)
        {
            return new PlayerFeatViewModel(feat, progress);
        }

        public BaseWeaveViewModel Create(BaseWeaveModel model)
        {
            return new BaseWeaveViewModel(model);
        }

        public PlayerWeaveViewModel CreatePlayerWeave(BaseWeaveViewModel skill, int progress)
        {
            return new PlayerWeaveViewModel(skill, progress);
        }
    }
}

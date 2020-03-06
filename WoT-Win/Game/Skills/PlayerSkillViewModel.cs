using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Models;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Skills
{
    public class PlayerSkillViewModel : BaseViewModel
    {
        public PlayerSkillViewModel(BaseSkillViewModel skill, int progress)
        {
            Skill = skill;
            Progress = progress;
        }

        public BaseSkillViewModel Skill { get; private set; }
        public int Progress { get; private set; }
        public string Name { get { return Skill.Name; } }       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Game.Models
{
    public class PlayerSkillModel
    {
        public PlayerSkillModel(BaseSkillModel skill)
        {
            Skill = skill;          
        }

        public BaseSkillModel Skill { get; private set; }
       
    }
}

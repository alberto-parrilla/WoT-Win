using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Game.Models
{
    public class PlayerFeatModel
    {
        public PlayerFeatModel(BaseSkillModel feat)
        {
            Feat = feat;            
        }

        public BaseSkillModel Feat { get; private set; }                
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public class FeatModel : BaseSkillModel
    {
        public FeatModel(int id, string name, string shortDesc, string longDesc, Func<SkillResult, ISkillParameter> action) : base(id, true, name, 1, shortDesc, longDesc, action)
        {
        }
    }
}

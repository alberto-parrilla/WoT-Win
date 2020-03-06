using System;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public class SkillActiveTargetNear : BaseSkillModel
    {
        public SkillActiveTargetNear(int id, string name, int cost, string shortDesc, string longDesc, Func<SkillResult, ISkillParameter> action) : base(id, false, name, cost, shortDesc, longDesc, action)
        {
            Mode = EnumSkillMode.Active;
            Area = EnumSkillArea.Target;
            Distance = EnumSkillDistance.Near;
        }
    }
}

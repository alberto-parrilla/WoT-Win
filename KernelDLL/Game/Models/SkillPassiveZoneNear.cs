using System;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public class SkillPassiveZoneNear : BaseSkillModel
    {
        public SkillPassiveZoneNear(int id, string name, int cost, string shortDesc, string longDesc, Func<SkillResult, ISkillParameter> action, int rangeArea) : base(id, false, name, cost, shortDesc, longDesc, action)
        {
            Mode = EnumSkillMode.Pasive;
            Area = EnumSkillArea.Zone;
            RangeArea = rangeArea;
            Distance = EnumSkillDistance.Near;            
        }
    }
}

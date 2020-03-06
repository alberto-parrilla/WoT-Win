﻿using System;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public class SkillPassiveTargetNear : BaseSkillModel
    {
        public SkillPassiveTargetNear(int id, string name, int cost, string shortDesc, string longDesc, Func<SkillResult, ISkillParameter> action) : base(id, false, name, cost, shortDesc, longDesc, action)
        {
            Mode = EnumSkillMode.Pasive;
            Area = EnumSkillArea.Target;
            Distance = EnumSkillDistance.Near;
        }
    }
}

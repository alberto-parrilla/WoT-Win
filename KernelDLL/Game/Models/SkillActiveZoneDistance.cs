﻿using System;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public class SkillActiveZoneDistance : BaseSkillModel
    {
        public SkillActiveZoneDistance(int id, string name, int cost, string shortDesc, string longDesc, Func<SkillResult, ISkillParameter> action, int rangeArea, int rangeDistance) : base(id, false, name, cost, shortDesc, longDesc, action)
        {
            Mode = EnumSkillMode.Active;
            Area = EnumSkillArea.Zone;
            RangeArea = rangeArea;
            Distance = EnumSkillDistance.Distance;
            RangeDistance = rangeDistance;            
        }
    }
}

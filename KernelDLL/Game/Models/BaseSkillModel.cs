using System;
using System.Collections.Generic;
using KernelDLL.Common;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public abstract class BaseSkillModel
    {
        protected BaseSkillModel(int id, bool isFeat, string name, int cost, string shortDesc, string longDesc, Func<SkillResult, ISkillParameter> action)
        {
            Id = id;
            IsFeat = isFeat;
            Name = name;
            Cost = cost;
            ShortDesc = shortDesc;
            LongDesc = longDesc;
            LargeIcon = string.Format(@"{0}\{1}_large.png", Util.SkillIconPath, Id);
            SmallIcon = string.Format(@"{0}\{1}_small.png", Util.SkillIconPath, Id);
            Action = action;
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public int Cost { get; protected set; }
        public string ShortDesc { get; protected set; }
        public string LongDesc { get; protected set; }
        public string LargeIcon { get; protected set; }
        public string SmallIcon { get; protected set; }
        public EnumSkillMode Mode { get; protected set; }
        public EnumSkillArea Area { get; protected set; }
        public int RangeArea { get; protected set; }
        public EnumSkillDistance Distance { get; protected set; }
        public  int RangeDistance { get; protected set; }
        public EnumSkillDuration Duration { get; protected set; }
        public int TimeDuration { get; protected set; } 
        public List<AttributeModModel> Mods { get; protected set; } 
        public Func<SkillResult, ISkillParameter> Action { get; protected set; }
        public bool IsFeat { get; protected set; }             
    }
}

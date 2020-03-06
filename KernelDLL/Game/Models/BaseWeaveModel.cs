using System;
using System.Collections.Generic;
using KernelDLL.Common;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public class BaseWeaveModel
    {
        public BaseWeaveModel(int id, List<EnumWeaveType> type, string name, int level, string shortDesc, string longDesc, Func<WeaveResult, IWeaveParameter> action)
        {
            Id = id;
            Type = type;
            Name = name;
            Level = level;
            ShortDesc = shortDesc;
            LongDesc = longDesc;
            LargeIcon = string.Format(@"{0}\{1}_large.png", Util.WeaveIconPath, Id);
            SmallIcon = string.Format(@"{0}\{1}_small.png", Util.WeaveIconPath, Id);
            Action = action;
        }

        public int Id { get; private set; }
        public List<EnumWeaveType> Type { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public string ShortDesc { get; private set; }
        public string LongDesc { get; private set; }

        //TODO: Fix this to use a list of Icons: one for every power involved
        public string LargeIcon { get; private set; }
        public string SmallIcon { get; private set; }

        //public EnumSkillMode Mode { get; private set; }
        //public EnumSkillArea Area { get; private set; }
        //public int RangeArea { get; private set; }
        //public EnumSkillDistance Distance { get; private set; }
        //public int RangeDistance { get; private set; }
        //public EnumSkillDuration Duration { get; private set; }
        //public int TimeDuration { get; private set; }
        //public List<AttributeModModel> Mods { get; private set; }
        public Func<WeaveResult, IWeaveParameter> Action { get; private set; }
    }
}

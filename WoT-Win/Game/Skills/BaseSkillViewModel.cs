using System;
using System.Collections.Generic;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Interfaces;
using KernelDLL.Game.Models;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Skills
{
    public class BaseSkillViewModel : BaseViewModel
    {
        public BaseSkillViewModel(BaseSkillModel model)
        {
            Model = model;
        }

        private BaseSkillModel Model { get; set; }

        public int Id => Model.Id;
        public string Name => Model.Name;
        public int Cost => Model.Cost;
        public string ShortDesc => Model.ShortDesc;
        public string LongDesc => Model.LongDesc;
        public string LargeIcon => Model.LargeIcon;
        public string SmallIcon => Model.SmallIcon;
        public EnumSkillMode Mode => Model.Mode;
        public EnumSkillArea Area => Model.Area;
        public int RangeArea => Model.RangeArea;
        public EnumSkillDistance Distance => Model.Distance;
        public int RangeDistance => Model.RangeDistance;
        public EnumSkillDuration Duration => Model.Duration;
        public int TimeDuration => Model.TimeDuration;
        public List<AttributeModModel> Mods => Model.Mods;
        public Func<SkillResult, ISkillParameter> Execute => Model.Action;

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}

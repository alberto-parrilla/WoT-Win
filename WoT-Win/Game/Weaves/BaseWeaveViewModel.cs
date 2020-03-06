using System;
using System.Collections.Generic;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Interfaces;
using KernelDLL.Game.Models;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Weaves
{
    public class BaseWeaveViewModel : BaseViewModel
    {
        public BaseWeaveViewModel(BaseWeaveModel model)
        {
            Model = model;
        }

        private BaseWeaveModel Model { get; set; }

        public int Id => Model.Id;
        public List<EnumWeaveType> Type => Model.Type;
        public string Name => Model.Name;
        public int Level => Model.Level;
        public string ShortDesc => Model.ShortDesc;
        public string LongDesc => Model.LongDesc;
        public string LargeIcon => Model.LargeIcon;
        public string SmallIcon => Model.SmallIcon;
        //////public EnumSkillMode Mode { get { return Model.Mode; } }
        //////public EnumSkillArea Area { get { return Model.Area; } }
        //////public int RangeArea { get { return Model.RangeArea; } }
        //////public EnumSkillDistance Distance { get { return Model.Distance; } }
        //////public int RangeDistance { get { return Model.RangeDistance; } }
        //////public EnumSkillDuration Duration { get { return Model.Duration; } }
        //////public int TimeDuration { get { return Model.TimeDuration; } }
        //////public List<AttributeModModel> Mods { get { return Model.Mods; } }
        public Func<WeaveResult, IWeaveParameter> Execute => Model.Action;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Enums;

namespace WoT_Win.Common.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        public string Name { get; private set; }
        public EnumRace Race { get; private set; }
        public EnumSex Sex { get; private set; }
        public int Nation { get; private set; }      
    }
}

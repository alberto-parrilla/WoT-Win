using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Enums;

namespace KernelDLL.Game.Models
{
    public class BaseObjectModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public EnumObjectType Type { get; private set; }
        public string Icon { get; private set; }
        public string Image { get; private set; }
    }
}

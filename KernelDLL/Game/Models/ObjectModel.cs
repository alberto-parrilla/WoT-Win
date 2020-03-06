using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Game.Models
{
    public class ObjectModel
    {
        public ObjectModel(int id, BaseObjectModel objectBase)
        {
            Id = id;
            Base = objectBase;
        }

        private BaseObjectModel Base { get; set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public string Image { get; private set; }
    }
}

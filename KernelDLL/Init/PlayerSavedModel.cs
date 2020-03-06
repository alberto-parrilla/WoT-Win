using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Common;

namespace KernelDLL.Init
{
    public class PlayerSavedModel
    {
        public PlayerSavedModel(int id, string name, string savetime)
        {
            Id = id;
            Name = name;
            Savetime = savetime;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Faceset { get { return string.Format(@"{0}\{1}\faceset.png", Util.SavedPlayerPath, Id); } }
        public string Savetime { get; private set; }
    }
}

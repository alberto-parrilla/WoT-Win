using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Common;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public class AreaModel
    {
        public AreaModel(int id, string header)
        {
            Id = id;
            Header = header;           
        }

        public int Id { get; private set; }
        public string Header { get; private set; }      
    }
}

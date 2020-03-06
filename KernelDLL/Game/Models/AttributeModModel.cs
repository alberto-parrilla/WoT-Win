using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Game.Models
{
    public class AttributeModModel
    {
        public AttributeModModel(int id, int value, string description)
        {
            Id = id;
            Value = value;
            Description = description;
        }

        public int Id { get; private set; }
        public int Value { get; private set; }
        public string Description { get; private set; }        
    }
}

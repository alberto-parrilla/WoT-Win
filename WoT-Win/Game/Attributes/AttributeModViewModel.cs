using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using KernelDLL.Game.Models;

namespace WoT_Win.Game.Attributes
{
    public class AttributeModViewModel
    {
        public AttributeModViewModel(AttributeModModel model)
        {
            Model = model;
        }

        private AttributeModModel Model { get; set; }

        public int Id { get { return Model.Id; } }
        public int Value { get { return Model.Value; } }
        public string Description { get { return Model.Description; } }

        public Color ForeColor
        {
            get { return GetForeColor(); }
        }

        private Color GetForeColor()
        {
            if (Value > 0) return Colors.Green;
            if (Value < 0) return Colors.Red;
            return Colors.Black;
        }
    
    }
}

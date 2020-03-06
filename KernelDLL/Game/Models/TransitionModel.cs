using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Game.Models
{
    public class TransitionModel
    {
        public TransitionModel(int id, int x, int y, int width, int height)
        {
            Id = id;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int Id { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }        
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int TargetArea { get; private set; }
        public int TargetScene { get; private set; }
        public int TargetTransition { get; private set; }
        public int TargetX { get; private set; }
        public int TargetY { get; private set; }

    }
}

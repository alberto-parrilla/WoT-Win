using KernelDLL.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.GUI
{
    public sealed class TransitionViewModel : BaseViewModel
    {
        public TransitionViewModel(TransitionModel model, SceneViewModel parent)
        {
            Model = model;
            Parent = parent;
        }

        private TransitionModel Model { get; set; }
        private SceneViewModel Parent { get; set; }

        public int X { get { return Model.X; } }
        public int Y { get { return Model.Y; } }
        public Thickness Offset { get {  return new Thickness( Parent.OffsetX + X, Parent.OffsetY + Y, 0, 0); } }
        public int Width { get { return Model.Width; } }
        public int Height { get { return Model.Height; } }

        public Rect Collider2D { get {  return new Rect(new Point(Parent.OffsetX + X, Parent.OffsetY + Y), new Size(Width, Height)); } }

        public void RefreshOffset()
        {
            OnPropertyChanged("Offset");
        } 
    }
}

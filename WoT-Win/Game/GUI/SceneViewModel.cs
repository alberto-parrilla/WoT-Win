using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KernelDLL.Common;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Models;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.GUI
{
    public class SceneViewModel : BaseViewModel
    {
        private int _movRange = Util.MovRange;
        public SceneViewModel(SceneModel model)
        {
            Model = model;
            Transitions = new List<TransitionViewModel>( Model.Transitions.Select(t => new TransitionViewModel(t, this)));
            OffsetX = -Width / 2;
            OffsetY = -Height / 2;         
        }

        private SceneModel Model { get; set; }
        public string Map { get { return Model.Map; } }
        public string BackgroundMap { get { return Model.BackgroundMap; } }
        public int Width { get { return Model.Width; } }
        public int Height { get { return Model.Height; } }
        public List<TransitionViewModel> Transitions { get; private set; }

        private int _offsetX;
        public int OffsetX
        {
            get { return _offsetX; }
            set
            {
                _offsetX = value;
                OnPropertyChanged();
            }
        }

        private int _offsetY;
        public int OffsetY
        {
            get { return _offsetY;}
            set
            {
                _offsetY = value;
                OnPropertyChanged();
            }
        }

        public void Move(EnumMove move)
        {
            switch (move)
            {
                case EnumMove.Up:
                    if (OffsetY < 0) OffsetY = OffsetY + _movRange;
                    break;
                case EnumMove.Down:
                    if (OffsetY >  Util.CanvasHeight - Height) OffsetY = OffsetY - _movRange;
                    break;
                case EnumMove.Right:
                    if (OffsetX > Util.CanvasWidth - Width) OffsetX = OffsetX - _movRange;
                    break;
                case EnumMove.Left:
                    if (OffsetX < 0) OffsetX = OffsetX + _movRange;
                    break;               
            }
            foreach (var transition in Transitions)
            {
                transition.RefreshOffset();
            }
        }

        public TransitionViewModel CheckTransition(PlayerViewModel player)
        {
            var playerRect = player.Collider2D;
            foreach (var transition in Transitions)
            {
                if (transition.Collider2D.IntersectsWith(playerRect))
                    return transition;
            }            
            return null;
        }
    }
}

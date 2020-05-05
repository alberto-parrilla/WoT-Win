using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KernelDLL.Common;
using KernelDLL.Game.Enums;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;
using WoT_Win.Game.Objects;
using MessageBox = System.Windows.MessageBox;

namespace WoT_Win.Game.GUI
{
    public sealed class MainGuiViewModel : BaseViewModel
    {
        private Window _view;
        private DataManager _datamanager;

        public MainGuiViewModel(Window view, Canvas canvas)
        {
            _view = view;

            Player = new PlayerViewModel(_datamanager.GetPlayer());
            MenuOpenCommand = new RelayCommand((o) => MenuOpen(o), (o) => true);
            MoveCommand = new RelayCommand((o) => Move(o), (o) => true);
            //Area = new AreaViewModel(datamanager.GetArea());
            //Scene = new SceneViewModel(datamanager.GetScene());
            Canvas = canvas;
        }

        private Canvas Canvas { get; set; }
        public ICommand MenuOpenCommand { get; private set; }
        public ICommand MoveCommand { get; private set; }
        public PlayerViewModel Player { get; private set; }
        public AreaViewModel Area { get; private set; }
        public SceneViewModel Scene { get; private set; } 
        public List<TransitionViewModel> Transitions { get { return Scene.Transitions; } }

        private void MenuOpen(object parameter)
        {
            Window window = null;
            if (parameter.ToString().ToUpper() == "INVENTORY")
            {
                window = new PlayerInventoryView(Player);
            }

            if (window != null)
            {
                window.Owner = Window.GetWindow(Canvas);
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                window.ShowDialog();
            }
        }

        private void Move(object direction)
        {
            if (direction.ToString() == "Up")
            {
                Player.Move(EnumMove.Up);
                Scene.Move(EnumMove.Up);
            }
            else if (direction.ToString() == "Down")
            {
                Player.Move(EnumMove.Down);
                Scene.Move(EnumMove.Down);
            }
            else if (direction.ToString() == "Left")
            {
                Player.Move(EnumMove.Left);
                Scene.Move(EnumMove.Left);
            }
            else if (direction.ToString() == "Right")
            {
                Player.Move(EnumMove.Right);
                Scene.Move(EnumMove.Right);
            }

            CheckColision();
        }

        private void CheckColision()
        {
            var transition = Scene.CheckTransition(Player);
            if (transition != null)
            {
                MessageBox.Show("Transition");
            }
        }
    }
}

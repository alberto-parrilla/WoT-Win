using System.Collections.ObjectModel;
using KernelDLL.Common;
using WoT_Win.Common.ViewModels;
using System.Windows.Input;
using WoT_Win.Common.Commands;
using System.Windows;
using ClientDLL.Client;
using WoT_Win.Game.GUI;

namespace WoT_Win.Init
{
    public class LoadViewModel : BaseViewModel
    {
        private Window _view;
        private Common.Services.CreateFactory _factory;
        private DataManager _dataManager;

        public LoadViewModel(Window view, DataManager dataManager, Common.Services.CreateFactory factory, IMainClient client) : base(client)
        {
            _view = view;
            _factory = factory;
            _dataManager = dataManager;
            LoadCommand = new RelayCommand((o) => Load(), (o) => CanLoad());
            CancelCommand = new RelayCommand((o) => Exit(), (o) => true);    
        }

        public string AppTitle => Util.AppTitle;

        public ICommand LoadCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }   

        public ObservableCollection<LoadedGameViewModelLegacy> Items { get; private set; }
        private LoadedGameViewModelLegacy _selectedItem;
        public LoadedGameViewModelLegacy SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private bool CanLoad()
        {
            return SelectedItem != null && !SelectedItem.IsNull;
        }

        private void Load()
        {
            //var splashLoadView = new SplashLoadView(_dataManager);            
            //_view.Close();
            //splashLoadView.Show();
            //splashLoadView.LoadGame(SelectedItem);

            _dataManager.LoadGame();
            //////////_dataManager.LoadPlayer(SelectedItem.Model.PlayerId);
            //////////_dataManager.LoadArea(SelectedItem.Model.AreaId);
            //////////_dataManager.LoadScene(SelectedItem.Model.SceneId, SelectedItem.Model.AreaId);

            var gui = new MainGui();

            gui.Show();
            _view.Close();
        }

        private void Exit()
        {
            if (CustomMessageBox.Show("Atención", "Desea salir de la aplicación? /S/N)", EnumMessageBox.YesNo))
            {
                Application.Current.Shutdown();
            }
        }
    }
}

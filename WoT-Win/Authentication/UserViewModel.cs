using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Common;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;
using WoT_Win.Game.GUI;
using WoT_Win.Init;

namespace WoT_Win.Authentication
{
    public class UserViewModel : CustomBaseViewModel
    {
        public UserViewModel(Window view, int userId, DataManager dataManager, IMainClient client) : base(view, client, dataManager)
        {
            BtnNewCommand = new RelayCommand((o) => New(), (o) => CanNew());
            BtnLoadCommand = new RelayCommand((o) => Load(), (o) => CanLoad());
            BtnExitCommand = new RelayCommand((o) =>Exit(), (o) => true);
            //_dataManager.LoadGameByUser(userId);
            //LoadedGame = _dataManager.GetGame();
        }

        public string AppTitle => _dataManager.AppTitle;

        public ICommand BtnNewCommand { get; }
        public ICommand BtnLoadCommand { get; }
        public ICommand BtnExitCommand { get; }

        private LoadedGameViewModel _loadedGame;
        public LoadedGameViewModel LoadedGame
        {
            get { return _loadedGame; }
            set
            {
                _loadedGame = value;
                OnPropertyChanged();
            }
        }

        private bool CanNew()
        {
            return LoadedGame == null || LoadedGame.IsNull;
        }

        private void New()
        {
        }

        private bool CanLoad()
        {
            return LoadedGame != null && !LoadedGame.IsNull;
        }

        private void Load()
        {
            //var splashLoadView = new SplashLoadView(_dataManager);            
            //_view.Close();
            //splashLoadView.Show();
            //splashLoadView.LoadGame(SelectedItem);

            _dataManager.LoadGame();
            _dataManager.LoadPlayer(LoadedGame.Model.PlayerId);
            _dataManager.LoadArea(LoadedGame.Model.AreaId);
            _dataManager.LoadScene(LoadedGame.Model.SceneId, LoadedGame.Model.AreaId);

            //OpenWindowSafe(() => new MainGui(_dataManager, _client).Show());
            OpenWindowSafe(() => new MainGui(_dataManager).Show());
            CloseWindowSafe(_view);
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

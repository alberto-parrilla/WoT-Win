using System.Windows;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Common;
using KernelDLL.Game.Models;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.Views;
using WoT_Win.Game.GUI;

namespace WoT_Win.Authentication
{
    public class UserViewModel : CustomBaseViewModel
    { 
        public UserViewModel(Window view, int userId, IMainClient client) : base(view, client)
        {
            UserId = userId;
            BtnLoadCommand = new RelayCommand((o) => Load(), (o) => CanLoad());
            BtnNewCommand = new RelayCommand((o) => New(), (o) => CanNew());
            BtnDeleteCommand = new RelayCommand((o) => Delete(), (o) => CanDelete());
            BtnExitCommand = new RelayCommand((o) => Exit(), (o) => true);
            IsLoading = true;
        }

        public override void Init()
        {
            _client.Send(new DataRequest(EnumGameDataType.GameSessionInfo, UserId));
        }

        private int UserId { get; set; }
        public string AppTitle => Util.AppTitle;

        public ICommand BtnLoadCommand { get; }
        public ICommand BtnNewCommand { get; }
        public ICommand BtnDeleteCommand { get; }
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

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool CanLoad()
        {
            return GetIsOnline() && LoadedGame != null && !LoadedGame.IsNull;
        }

        private void Load()
        {
            //var splashLoadView = new SplashLoadView(_dataManager);            
            //_view.Close();
            //splashLoadView.Show();
            //splashLoadView.LoadGame(SelectedItem);

            //_dataManager.LoadGame();
            //_dataManager.LoadPlayer(LoadedGame.Model.PlayerId);
            //_dataManager.LoadArea(LoadedGame.Model.AreaId);
            //_dataManager.LoadScene(LoadedGame.Model.SceneId, LoadedGame.Model.AreaId);

            //OpenWindowSafe(() => new MainGui(_dataManager, _client).Show());
            OpenWindowSafe(() => new MainGui().Show());
            CloseWindowSafe(_view);
        }

        private bool CanNew()
        {
            return GetIsOnline() && (LoadedGame == null || LoadedGame.IsNull);
        }

        private void New()
        {
            OpenWindowSafe(() => new CreationView(_client, new Common.Services.CreateFactory()).Show());
            CloseWindowSafe(_view);
        }

        private bool CanDelete()
        {
            return GetIsOnline() && LoadedGame != null && !LoadedGame.IsNull;
        }

        private void Delete()
        {
        }

        private void Exit()
        {
            if (CustomMessageBox.Show("Atención", "Desea salir de la aplicación? /S/N)", EnumMessageBox.YesNo))
            {
                Application.Current.Shutdown();
            }
        }

        protected override async void ManageResponse(IResponse e)
        {
            var response = e as DataResponse<GameSessionInfoModel>;
            if (response == null) return;

            switch (response.Status)
            {
                case EnumDataResponse.Success:
                    LoadedGame = CreateLoadedGame(response.Content);
                    break;
                case EnumDataResponse.Error:
                    //Error(LanguageManager.GetResourceValue("LoginView", "ErrorNotVerified"));
                    break;
            }

            IsLoading = false;
        }

        private LoadedGameViewModel CreateLoadedGame(GameSessionInfoModel model)
        {
            if (model == null) return null;
            return new LoadedGameViewModel(model);
        }
    }
}

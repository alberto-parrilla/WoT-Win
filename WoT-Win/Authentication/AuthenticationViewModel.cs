using System.Windows;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Common;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Authentication
{
    public class AuthenticationViewModel : CustomBaseViewModel
    {
        public AuthenticationViewModel(Window view, int userId, DataManager dataManager, IMainClient client) : base(view, client, dataManager)
        {
            UserId = userId;
            ContinueCommand = new RelayCommand((o) => Continue(), (o) => GetIsOnline());
            ExitCommand = new RelayCommand((o) => Exit(), (o) => true);
        }

        public ICommand ContinueCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        private int UserId { get; set; }

        private string _authenticationCode;

        public string AuthenticationCode
        {
            get => _authenticationCode;
            set
            {
                _authenticationCode = value;
                OnPropertyChanged();
            }
        }

        protected override async void ManageResponse(IResponse e)
        {
            var response = e as AuthenticationResponse;
            if (response == null) return;

            switch (response.Status)
            {
                case EnumAuthenticationResponse.Success:
                    OpenWindowSafe(() => CustomMessageBox.Show("Info", LanguageManager.GetResourceValue("AuthenticationView", "Success"), EnumMessageBox.OkCancel));
                    Login();
                    break;
                case EnumAuthenticationResponse.InvalidCode:
                    Error(LanguageManager.GetResourceValue("AuthenticationView", "ErrorInvalidCode"));
                    break;
                case EnumAuthenticationResponse.UndefinedError:
                    Error(LanguageManager.GetResourceValue("AuthenticationView", "ErrorUndefined"));
                    break;
            }
        }

        private void Login()
        {
            OpenWindowSafe(() => new LoginView(_dataManager, _client).Show());
            CloseWindowSafe(_view);
        }

        private void Continue()
        {
            if (string.IsNullOrEmpty(AuthenticationCode) )
                OpenWindowSafe(() => CustomMessageBox.Show("Info", LanguageManager.GetResourceValue("AuthenticationView", "WarningEmptyCode"), EnumMessageBox.OkCancel));

            var request = new AuthenticationRequest(UserId, AuthenticationCode);
            _client.Send(request);
        }

        private void Exit()
        {
            if (CustomMessageBox.Show("Atención", "Desea salir de la aplicación? /S/N)", EnumMessageBox.YesNo))
            {
                Application.Current.Shutdown();
            }
        }

        private void Error(string error)
        {
            OpenWindowSafe(() => CustomMessageBox.Show("Error", error, EnumMessageBox.OkCancel));
        }
    }
}

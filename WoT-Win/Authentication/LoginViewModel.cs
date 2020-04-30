using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Common;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Authentication
{
    public class LoginViewModel : CustomBaseViewModel
    {
        public LoginViewModel(Window view, DataManager dataManager, IMainClient client) : base(view, client, dataManager)
        {
            LoginCommand = new RelayCommand((o) => Login(o), (o) => GetIsOnline());
            RegisterCommand = new RelayCommand((o) => Register(), (o) => GetIsOnline());
            ExitCommand = new RelayCommand((o) => Exit(), (o) => true);
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        protected override async void ManageResponse(IResponse e)
        {
            var response = e as LoginResponse;
            if (response == null) return;

            switch (response.Status)
            {
                case EnumLoginResponse.Success:
                    Load(response.UserId);
                    break;
                case EnumLoginResponse.NotVerified:
                    Error(LanguageManager.GetResourceValue("LoginView", "ErrorNotVerified"));
                    break;
                case EnumLoginResponse.WrongUserOrEmail:
                    Error(LanguageManager.GetResourceValue("LoginView", "ErrorWrongEmail"));
                    break;
                case EnumLoginResponse.WrongPassword:
                    Error(LanguageManager.GetResourceValue("LoginView", "ErrorWrongPassword"));
                    break;
                case EnumLoginResponse.UserBlocked:
                    Error(LanguageManager.GetResourceValue("LoginView", "ErrorUserBlocked"));
                    break;
            }
        }

        private void Login(object parameter)
        {
            var hashPassword = Util.HashPassword((parameter as PasswordBox)?.Password);
            var request = new LoginRequest(Email, hashPassword);
            _client.Send(request);
        }

        private void Register()
        {
            OpenWindowSafe(() => { new RegisterView(_dataManager, _client).Show(); });
            CloseWindowSafe(_view);
        }

        private void Exit()
        {
            if (CustomMessageBox.Show("Atención", "Desea salir de la aplicación? /S/N)", EnumMessageBox.YesNo))
            {
                Application.Current.Shutdown();
            }
        }

        private void Load(int? userId)
        {
            if (!userId.HasValue) return;

            OpenWindowSafe(() => new UserView(userId.Value, _dataManager, _client).Show());
            CloseWindowSafe(_view);
        }

        private void Error(string error)
        {
            OpenWindowSafe(() =>  CustomMessageBox.Show("Error", error, EnumMessageBox.OkCancel));
        }
    }
}

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
    public class RegisterViewModel : CustomBaseViewModel
    {
        public RegisterViewModel(Window view, DataManager dataManager, IMainClient client) : base(view, client, dataManager)
        {
            ContinueCommand = new RelayCommand((o) => Continue(o), (o) => GetIsOnline());
            ExitCommand = new RelayCommand((o) => Exit(), (o) => true);
        }

        public ICommand ContinueCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

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
            var response = e as RegisterResponse;
            if (response == null) return;

            switch (response.Status)
            {
                case EnumRegisterResponse.Success:
                    Authentication(response.UserId);
                    break;
                case EnumRegisterResponse.UserExists:
                    Error(LanguageManager.GetResourceValue("RegisterView", "ErrorUserExists"));
                    break;
                case EnumRegisterResponse.EmailExists:
                    Error(LanguageManager.GetResourceValue("RegisterView", "ErrorEmailExists"));
                    break;
                case EnumRegisterResponse.UndefinedError:
                    Error(LanguageManager.GetResourceValue("RegisterView", "ErrorUndefined"));
                    break;
            }
        }

        private void Continue(object parameter)
        {
            var hashPassword = Util.HashPassword((parameter as PasswordBox)?.Password);
            //var request = new RegisterRequestLegacy(Username, Email, hashPassword);
            var request = new RegisterRequest(Username, Email, hashPassword);
            _client.Send(request);
        }

        private void Authentication(int? userId)
        {
            if (!userId.HasValue) return;

            OpenWindowSafe(() => new AuthenticationView(userId.Value, _dataManager, _client).Show());
            CloseWindowSafe(_view);
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

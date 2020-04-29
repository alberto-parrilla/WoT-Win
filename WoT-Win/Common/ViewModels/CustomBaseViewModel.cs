using System.Windows;
using ClientDLL.Client;
using KernelDLL.Common;

namespace WoT_Win.Common.ViewModels
{
    public class CustomBaseViewModel : BaseViewModel
    {
        protected Window _view;

        public CustomBaseViewModel(Window view, IMainClient client) : base(client)
        {
            _view = view;
            ServerStatusViewModel = new ServerStatusViewModel(_client);
        }

        public CustomBaseViewModel(Window view, IMainClient client, DataManager dataManager) : base(client, dataManager)
        {
            _view = view;
            ServerStatusViewModel = new ServerStatusViewModel(_client);
        }

        protected new void Register()
        {
            base.Register();
            ServerStatusViewModel.PropertyChanged += ServerStatusViewModel_PropertyChanged;
        }

        protected new void Unregister()
        {
            base.Unregister();
            ServerStatusViewModel.PropertyChanged -= ServerStatusViewModel_PropertyChanged;
        }


        public ServerStatusViewModel ServerStatusViewModel { get; private set; }

        public bool IsOnline { get { return ServerStatusViewModel.IsOnline; } }

        protected bool GetIsOnline()
        {
            return ServerStatusViewModel.IsOnline;
        }

        protected void ServerStatusViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOnline")
            {
                OnPropertyChanged("IsOnline");
            }
        }
    }
}

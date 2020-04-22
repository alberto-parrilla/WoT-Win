using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Common;
using WoT_Win.Common.Commands;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.Views;

namespace WoT_Win.Init
{
    public sealed class InitViewModel : BaseViewModel
    {
        private Window _view;
        private CreateFactory _createFactory;
        private IMainClient _client;

        public InitViewModel(Window view, DataManager dataManager, CreateFactory createFactory, IMainClient client) : base(dataManager)
        {
            NewCommand = new RelayCommand((o) => New(false), (o) => true);
            NewOnlineCommand = new RelayCommand((o) => New(true), (o) => IsOnline);
            PlayCommand = new RelayCommand((o) => Play(false), (o) => true);
            PlayOnlineCommand = new RelayCommand((o) => Play(true), (o) => IsOnline); 
            ExitCommand = new RelayCommand((o) => Exit(), (o) => true);
            _view = view;
            _createFactory = createFactory;
            _client = client;
            ServerStatusViewModel = new ServerStatusViewModel(_client);
        }

        public bool IsOnline { get { return false; } }
        public ICommand NewCommand { get; private set; }
        public ICommand NewOnlineCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }
        public ICommand PlayOnlineCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public ServerStatusViewModel ServerStatusViewModel { get; private set; }

        private void New(bool isOnline)
        {
            new CreationView(_dataManager, _createFactory).Show();
            _view.Close();
        }

        private void Play(bool isOnline)
        {
            new LoadView(_dataManager, _createFactory, _client).Show();
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

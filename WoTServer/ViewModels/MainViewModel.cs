using System;
using System.Windows;
using System.Windows.Input;
using KernelDLL.Common;
using ServerDLL.Server;

namespace WoTServer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Window _view;
        private IMainServer _server;

        public MainViewModel(Window view, DataManager dataManager, IMainServer server) : base(dataManager)
        {
            _view = view;
            _server = server;
            BtnStartCommand = new RelayCommand((o) => Start(), (o) => !IsRunning);
            BtnStopCommand = new RelayCommand((o) => Stop(), (o) => IsRunning);
            BtnClearServerInfoCommand = new RelayCommand((o) => ClearServerInfo(), (o) => true);
        }

        public ICommand BtnStartCommand { get; }
        public ICommand BtnStopCommand { get; }
        public ICommand BtnClearServerInfoCommand { get; }

        private string _serverInfo;

        public string ServerInfo
        {
            get => _serverInfo;
            set
            {
                _serverInfo = value;
                OnPropertyChanged();
            }
        }

        public bool IsRunning => _server.IsRunning;

        public override void Init()
        {
            Start();
        }

        private void Start()
        {
            _server.Start();
            ShowServerInfo("Starting server...");
            OnPropertyChanged("IsRunning");
        }

        private void Stop()
        {
            _server.Stop();
            ShowServerInfo("Stopping server...");
            OnPropertyChanged("IsRunning");
        }

        private void ClearServerInfo()
        {
            ServerInfo = string.Empty;
        }

        private void ShowServerInfo(string text)
        {
            ServerInfo += string.Format("{0}{1}", text, Environment.NewLine);
        }

    }
}

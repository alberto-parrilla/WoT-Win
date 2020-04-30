using System;
using System.Diagnostics;
using System.Timers;
using ClientDLL.Client;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace WoT_Win.Common.ViewModels
{
    public class ServerStatusViewModel : BaseViewModel
    {
        private readonly Timer _serverTimer;
        private readonly Stopwatch _stopwatch;

        public ServerStatusViewModel(IMainClient client) : base(client)
        {
            _serverTimer = new Timer();
            _serverTimer.Elapsed += ServerTimer_Tick;
            _serverTimer.Interval = 5000;
            _serverTimer.Start();
            _stopwatch = new Stopwatch();
            ServerStatusDisplay = "Connecting...";
            ServerStatusIcon = @"..\..\Resources\Icons\Common\Fail_Icon-128.png";
        }

        private bool _isOnline;

        public bool IsOnline
        {
            get => _isOnline;
            set
            {
                _isOnline = value;
                OnPropertyChanged();
            }
        }

        private string _serverStatusDisplay;

        public string ServerStatusDisplay
        {
            get => _serverStatusDisplay;
            set
            {
                _serverStatusDisplay = value;
                OnPropertyChanged();
            }
        }

        private string _serverStatusIcon;

        public string ServerStatusIcon
        {
            get => _serverStatusIcon;
            set
            {
                _serverStatusIcon = value;
                OnPropertyChanged();
            }
        }

        private void ServerTimer_Tick(object sender, ElapsedEventArgs e)
        {
            //only to test
            _serverTimer.Stop();

            _stopwatch.Start();
            //_client.Send(new PingRequestLegacy());
            _client.Send(new PingRequest());
        }

        protected override void ManageResponse(IResponse e)
        {
            var response = e as PingResponse;
            if (response == null) return;

            _stopwatch.Stop();
            //bool connect = !response.TimeOut;
            bool connect = true;
            if (connect)
            {
                ServerStatusDisplay = $"Server connected: Ping = {_stopwatch.ElapsedMilliseconds} ms";
                ServerStatusIcon = @"..\..\Resources\Icons\Common\Check_Icon-128.png";
                IsOnline = true;
            }
            else
            {
                ServerStatusDisplay = "Server disconnected";
                ServerStatusIcon = @"..\..\Resources\Icons\Common\Fail_Icon-128.png";
                IsOnline = false;
            }
            _stopwatch.Reset();
            _serverTimer.Start();
        }
    }
}

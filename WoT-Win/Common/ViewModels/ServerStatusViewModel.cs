using System.Timers;

namespace WoT_Win.Common.ViewModels
{
    public class ServerStatusViewModel : BaseViewModel
    {
        private readonly Timer _serverTimer;

        public ServerStatusViewModel()
        {
            _serverTimer = new Timer();
            _serverTimer.Elapsed += ServerTimer_Tick;
            _serverTimer.Interval = 5000;
            _serverTimer.Start();
            ServerStatusDisplay = "Connecting...";
            ServerStatusIcon = @"..\..\Resources\Icons\Common\Fail_Icon-128.png"; 
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
            bool connect = false;
            if (connect)
            {
                ServerStatusDisplay = "Server connected";
                ServerStatusIcon = @"..\..\Resources\Icons\Common\Check_Icon-128.png";
            }
            else
            {
                ServerStatusDisplay = "Server disconnected";
                ServerStatusIcon = @"..\..\Resources\Icons\Common\Fail_Icon-128.png";
            }
        }
    }
}

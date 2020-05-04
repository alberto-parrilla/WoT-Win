using System.Threading.Tasks;
using KernelDLL.Authentication;
using KernelDLL.Common;
using KernelDLL.Game;
using KernelDLL.Network.Request;

namespace ServerDLL.Server
{
    public class MainServer : IMainServer
    {
        private IServer _server;
        private IUserManager _userManager;
        private IDataManager _dataManager;


        public bool IsRunning { get; private set; }

        public MainServer(IDataManager dataManager)
        {
            _server = new NetworkServer(this);
            _dataManager = dataManager;
            //new UserManager().LoginUserAsync("x", "x");
            //_dataManager.GetAreaInfoAsync(1);
            //new GameManager(_dataManager).GetPlayerInfoAsync(1);
        }

        public void Start()
        {
            IsRunning = true;
            _server.Start();
        }

        public void Stop()
        {
            IsRunning = false;
            _server.Stop();
        }

        public async Task ProcessRequestAsync(RequestMessageBase request, Receiver clientrReceiver)
        {
            var response = await request.ProcessRequestAsync();
            clientrReceiver.SendMessage(response);
        }
    }
}
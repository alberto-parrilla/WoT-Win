using System.Windows;
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
        }
    }
}

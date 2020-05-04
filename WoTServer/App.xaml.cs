using System.Windows;
using KernelDLL.Common;
using ServerDLL.Server;
using WoTServer.Views;

namespace WoTServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IMainServer _server;
        private DataManager _dataManager;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Init();
            // Create the startup window
            MainView wnd = new MainView(_dataManager, _server);
            // Do stuff here, e.g. to the window

            // Show the window
            wnd.Show();
        }

        private void Init()
        {
            _dataManager = new DataManager();
            _server = new MainServer(_dataManager);
            //_server.Init();
        }
    }
}

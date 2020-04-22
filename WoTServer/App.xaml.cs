using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KernelDLL.Common;
using KernelDLL.Database;
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
            _server = new MainServer();
            _server.Init();
            _dataManager = new DataManager(new FakeDatabase(), new RepositoryManager());
        }
    }
}

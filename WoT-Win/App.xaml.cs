using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KernelDLL.Common;
using WoT_Win.Common.Services;
using WoT_Win.Init;
using KernelDLL.Database;

namespace WoT_Win
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DataManager _dataManager;
        private CreateFactory _createFactory;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Init();
            // Create the startup window
            InitView wnd = new InitView(_dataManager, _createFactory);
            // Do stuff here, e.g. to the window

            // Show the window
            wnd.Show();
        }

        private void Init()
        {
            _dataManager = new DataManager(new FakeDatabase(), new RepositoryManager());
            _createFactory = new CreateFactory();
        }
    }
}

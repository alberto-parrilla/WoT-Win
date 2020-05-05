using System.Windows;
using ClientDLL.Client;
using KernelDLL.Common;
using KernelDLL.Database;
using WoT_Win.Authentication;

namespace WoT_Win
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IMainClient _client;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Init();
            // Create the startup window
            LoginView wnd = new LoginView(_client);
            // Do stuff here, e.g. to the window

            // Show the window
            wnd.Show();
        }

        private void Init()
        {
            _client = new MainClient(new DataManager(new FakeDatabase(), new RepositoryManager()));
        }
    }
}

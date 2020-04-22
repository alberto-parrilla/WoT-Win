using System.Windows;
using KernelDLL.Common;
using ServerDLL.Server;
using WoTServer.ViewModels;

namespace WoTServer.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(DataManager dataManager, IMainServer server)
        {
            InitializeComponent();
            DataContext = new MainViewModel(this, dataManager, server);
        }
    }
}

using System.Windows;
using ClientDLL.Client;
using KernelDLL.Common;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Authentication
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView(int userId, IMainClient client)
        {
            InitializeComponent();
            DataContext = new UserViewModel(this, userId, client);

            LanguageManager.SwitchLanguage(this, Util.CurrentCulture);
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as BaseViewModel)?.Init();
        }
    }
}

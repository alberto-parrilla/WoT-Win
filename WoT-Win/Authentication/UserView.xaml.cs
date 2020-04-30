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
        public UserView(int userId, DataManager dataManager, IMainClient client)
        {
            InitializeComponent();
            DataContext = new UserViewModel(this, userId, dataManager, client);

            LanguageManager.SwitchLanguage(this, DataManager.CurrentCulture);
        }
    }
}

using System.Windows;
using ClientDLL.Client;
using KernelDLL.Common;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Authentication
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView(DataManager dataManager, IMainClient client)
        {
            InitializeComponent();
            DataContext = new RegisterViewModel(this, dataManager, client);

            LanguageManager.SwitchLanguage(this, DataManager.CurrentCulture);
        }
    }
}

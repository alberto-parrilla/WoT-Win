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
        public RegisterView(IMainClient client)
        {
            InitializeComponent();
            DataContext = new RegisterViewModel(this, client);

            LanguageManager.SwitchLanguage(this, Util.CurrentCulture);
        }
    }
}

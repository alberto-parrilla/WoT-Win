using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClientDLL.Client;
using KernelDLL.Common;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;
using WoT_Win.Init;

namespace WoT_Win.Authentication
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView(DataManager dataManager, IMainClient client)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this, dataManager, client);

            LanguageManager.SwitchLanguage(this, DataManager.CurrentCulture);
        }
    }
}

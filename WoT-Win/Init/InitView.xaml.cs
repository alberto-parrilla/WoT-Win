using KernelDLL.Common;
using System.Windows;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Init
{
    /// <summary>
    /// Interaction logic for InitView.xaml
    /// </summary>
    public partial class InitView : Window
    {
        public InitView(DataManager dataManager, CreateFactory createFactory)
        {
            InitializeComponent();
            DataContext = new InitViewModel(this, dataManager, createFactory);

            LanguageManager.SwitchLanguage(this, DataManager.CurrentCulture);
        }
    }
}

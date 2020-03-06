using System.Windows.Controls;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Common.Views
{
    /// <summary>
    /// Interaction logic for LanguageSelectorView.xaml
    /// </summary>
    public partial class LanguageSelectorView : UserControl
    {
        public LanguageSelectorView()
        {
            DataContext = new LanguageSelectorViewModel();
            InitializeComponent();
        }
    }
}

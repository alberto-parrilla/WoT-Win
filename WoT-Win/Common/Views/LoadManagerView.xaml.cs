using System.Windows;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Common.Views
{
    /// <summary>
    /// Interaction logic for LoadManagerView.xaml
    /// </summary>
    public partial class LoadManagerView : Window
    {
        public LoadManagerView(LoadManagerViewModel viewModel)
        {
            InitializeComponent();
            viewModel.SetView(this);
            DataContext = viewModel;
        }

    }
}

using System.Windows;
using System.Windows.Controls;
using ClientDLL.Client;
using KernelDLL.Common;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.ViewModels;

namespace WoT_Win.Creation.Views
{
    /// <summary>
    /// Interaction logic for CreationView.xaml
    /// </summary>
    public partial class CreationView : Window
    {
        public CreationView(IMainClient client, Common.Services.CreateFactory createFactory)
        {
            InitializeComponent();
            var viewModel = new CreationViewModel(this, client, createFactory);
            viewModel.Init();
            DataContext = viewModel;
            Loaded += delegate(object sender, RoutedEventArgs args)
            {
                LanguageManager.SwitchLanguage(this, Util.CurrentCulture);
                viewModel.Load();
            };
        }        

        private CreationViewModel ViewModel
        {
            get { return DataContext as CreationViewModel; }
        }

        private int _tabIndex;

        private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource == TabControl)
            {
                var currentIndex = TabControl.ItemContainerGenerator.Items.IndexOf(ViewModel.CurrentCreationSection);
                if (TabControl.SelectedIndex != currentIndex)
                {
                    TabControl.SelectedIndex = currentIndex;
                    TabControl.SelectedItem = ViewModel.CurrentCreationSection;
                }

                e.Handled = true;
            }
        }
    }
}

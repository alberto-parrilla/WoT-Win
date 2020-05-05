using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using KernelDLL.Common;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.ViewModels;

namespace WoT_Win.Creation.Views
{
    /// <summary>
    /// Interaction logic for CreationView.xaml
    /// </summary>
    public partial class CreationView : Window
    {
        public CreationView(DataManager dataManager, CreateFactory createFactory)
        {
            InitializeComponent();
            var viewModel = new CreationViewModel(dataManager, createFactory);
            viewModel.Init();
            DataContext = viewModel;
            Loaded += delegate(object sender, RoutedEventArgs args)
            {
                LanguageManager.SwitchLanguage(this, Util.CurrentCulture);
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

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
using KernelDLL.Common;
using WoT_Win.Common.ViewModels;
using WoT_Win.Game.GUI;
using WoT_Win.Init;

namespace WoT_Win.Common.Views
{
    /// <summary>
    /// Interaction logic for SplashLoadView.xaml
    /// </summary>
    public partial class SplashLoadView : Window
    {
        private SplashLoadViewModel ViewModel { get; set; }
        public SplashLoadView(DataManager datamanager)
        {
            InitializeComponent();
            ViewModel = new SplashLoadViewModel(this, datamanager);
            DataContext = ViewModel;
        }

        public void LoadGame(LoadedGameViewModelLegacy game)
        {
            ViewModel.LoadGame(game);
        }

        public void LoadArea()
        {
            //ViewModel.LoadArea();
        }
    }
}

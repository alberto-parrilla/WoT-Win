using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KernelDLL.Common;

namespace WoT_Win.Game.GUI
{
    /// <summary>
    /// Interaction logic for MainGui.xaml
    /// </summary>
    public partial class MainGui : Window
    {
        public MainGui(DataManager datamanager)
        {
            InitializeComponent();
            DataContext = new MainGuiViewModel(this, datamanager, MainCanvas);
            MainCanvas.Opacity = 0;          
            FadeOut();
        }

        private void FadeIn()
        {
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(5));
            anim.Completed += (s, _) =>
            {
                
            };
            MainCanvas.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void FadeOut()
        {
            var anim = new DoubleAnimation(1, (Duration)TimeSpan.FromSeconds(5));
            anim.Completed += (s, _) =>
            {

            };
            MainCanvas.BeginAnimation(UIElement.OpacityProperty, anim);
        }
    }
}

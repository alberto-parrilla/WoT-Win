using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WoT_Win.Common.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickerControl.xaml
    /// </summary>
    public partial class ColorPickerControl : UserControl
    {
        public ColorPickerControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ColorItemsProperty =
            DependencyProperty.Register("ColorItems", typeof(IList), typeof(ColorPickerControl), new PropertyMetadata(null, ColorItemsChangedCallback));

        public IList ColorItems
        {
            get
            {
                return (IList)GetValue(ColorItemsProperty);
            }
            set
            {
                SetValue(ColorItemsProperty, value);
            }
        }

        private static void ColorItemsChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as ColorPickerControl;
            if (control != null)
            {
                //control.UpdateSelectedItem();
            }
        }

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(ColorPickerControl));

        public SolidColorBrush SelectedColor
        {
            get
            {
                return (SolidColorBrush)GetValue(SelectedColorProperty);
            }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            if (button != null)
                SelectedColor = button.DataContext as SolidColorBrush;
        }
    }
}

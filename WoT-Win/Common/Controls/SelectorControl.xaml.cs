using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WoT_Win.Common.Controls
{
    /// <summary>
    /// Interaction logic for SelectorControl.xaml
    /// </summary>
    public partial class SelectorControl : UserControl
    {
        private int _index = 0;        

        public SelectorControl()
        {
            InitializeComponent();          
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IList), typeof(SelectorControl), new PropertyMetadata(null, ItemsChangedCallback));

        public IList Items
        {
            get
            {
                return (IList)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        private static void ItemsChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as SelectorControl;
            if (control != null)
            {                
               control.UpdateSelectedItem();
            }
        }

        public static readonly  DependencyProperty SelectedElementProperty = 
            DependencyProperty.Register("SelectedElement", typeof(int), typeof(SelectorControl));

        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof(int), typeof(SelectorControl));

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register("ContentHeight", typeof(int), typeof(SelectorControl));

        public int ContentWidth
        {
            get
            {
                return (int)GetValue(ContentWidthProperty);
            }
            set
            {
                SetValue(ContentWidthProperty, value);                
            }
        }

        public int ContentHeight
        {
            get
            {
                return (int)GetValue(ContentHeightProperty);
            }
            set
            {
                SetValue(ContentHeightProperty, value);
            }
        }

        public int SelectedElement
        {
            get
            {
                return (int)GetValue(SelectedElementProperty);
            }
            set
            {
                SetValue(SelectedElementProperty, value);
            }
        }

        private void UpdateSelectedItem()
        {
            if (Items.Count == 0) 
                return;
            if (_index >= Items.Count)
                _index = 0;
            Label.Content = Items[_index].ToString();
            SelectedElement = _index;
        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            if (_index < Items.Count)
                _index++;
            if (_index == Items.Count)
                _index = 0;

            UpdateSelectedItem();
        }

        private void BtnPrev_OnClick(object sender, RoutedEventArgs e)
        {
            if (_index >= 0)
                _index--;
            if (_index < 0)
                _index = Items.Count - 1;

            UpdateSelectedItem();
        }
    }
}

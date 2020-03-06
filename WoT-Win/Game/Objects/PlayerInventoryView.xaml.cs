using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Objects
{
    /// <summary>
    /// Interaction logic for PlayerInventoryView.xaml
    /// </summary>
    public partial class PlayerInventoryView : Window
    {
        public PlayerInventoryView(PlayerViewModel player)
        {
            InitializeComponent();
            DataContext = new PlayerInventoryViewModel(player);
        }

        private Point _startPoint;
        private static readonly string _dropIdentifier = "dropIdentifier";

        private void listBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // The initial mouse position
            _startPoint = e.GetPosition(null);
        }

        private void listBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = _startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListBoxItem
                var listBox = sender as ListBox;
                var listBoxItem = listBox.SelectedItem;

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject(_dropIdentifier, listBoxItem);
                DragDrop.DoDragDrop(listBox, dragData, DragDropEffects.Move);
            }
        }




        private void slotView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(_dropIdentifier))
            {
                var item = e.Data.GetData(_dropIdentifier) as PlayerItemViewModel;
                DropOnCanvas(sender as ItemSlotView, item);
            }
        }

        private void slotView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(_dropIdentifier) ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public void DropOnCanvas(ItemSlotView targetSlot, PlayerItemViewModel item)
        {
            if (targetSlot != null && item != null)
            {
                var viewModel = targetSlot.DataContext as ItemSlotViewModel;
                if (viewModel != null)
                {
                    viewModel.SetItem(item.ObjectType, item.ObjectId);
                    item.IsEquiped = true;
                }
            }

        }
    }
}

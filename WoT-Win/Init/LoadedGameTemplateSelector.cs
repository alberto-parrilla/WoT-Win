using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WoT_Win.Init
{
    public sealed class LoadedGameTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NullTemplate { get; set; }
        public DataTemplate ValidTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dt = null;
            var vm = item as LoadedGameViewModelLegacy;
            if (vm == null)
                dt = NullTemplate;
            else if (vm.IsNull)
                dt = NullTemplate;
            else
                dt = ValidTemplate;

            return dt;
        } 
    }
}

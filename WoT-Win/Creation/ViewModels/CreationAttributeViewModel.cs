using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using KernelDLL.Game.Enums;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Creation.ViewModels
{
    public class CreationAttributeViewModel : BaseViewModel
    {
        private CreationAttributesViewModel _parent;
        public CreationAttributeViewModel(EnumAttribute type, int baseValue, CreationAttributesViewModel parent)
        {
            _parent = parent;
            Type = type;
            BaseValue = baseValue;
            RaceMod = 0;
            NationMod = 0;
            UpCommand = new RelayCommand((o) => Up(), (o) => CanUp());
            DownCommand = new RelayCommand((o) => Down(), (o) => CanDown());
        }

        public ICommand UpCommand { get; private set; }
        public ICommand DownCommand { get; private set; }

        public EnumAttribute Type { get; private set; }
        public string Header => GetHeader();
        public int BaseValue { get; private set; }
        public int RaceMod { get; private set; }
        public int NationMod { get; private set; }
        public int Value { get { return BaseValue + RaceMod + NationMod; } }

        private string GetHeader()
        {
            var currentCulture = CultureInfo.CurrentUICulture.Name;
            var dic = $"CreationAttributesView.{currentCulture}.xaml";
            var uri = $"/WoT-Win;component/Resources/Languages/{dic}";
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri(uri, UriKind.Relative));
            var resName = $"Lbl{Type.ToString()}";
            return (string) res[resName];
        }

        private bool CanUp()
        {
            return BaseValue < 18 && _parent.Points > 0;
        }

        private void Up()
        {
            BaseValue++;
            _parent.Points--;
            Refresh();
        }

        private bool CanDown()
        {
            return BaseValue > 1 && _parent.Points < 6;
        }

        private void Down()
        {
            BaseValue--;
            _parent.Points++;
            Refresh();
        }

        private void Refresh()
        {
            OnPropertyChanged("BaseValue");
            OnPropertyChanged("Value");
            CanDown();
            CanUp();
        }

        public override void RefreshUI()
        {
            OnPropertyChanged("Header");
        }
    }
}

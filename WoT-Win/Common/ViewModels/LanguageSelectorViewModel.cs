using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using KernelDLL.Common;
using WoT_Win.Common.Commands;

namespace WoT_Win.Common.ViewModels
{
    public class LanguageItem : BaseViewModel
    {
        public LanguageItem()
        {
            SelectCommand = new RelayCommand((o) => SelectLanguage(o), (o) => true);
        }

        public ICommand SelectCommand { get; private set; }

        public string Icon { get; set; }
        public string Loc { get; set; }

        private void SelectLanguage(object parameter)
        {
            var control = parameter as DependencyObject;
            if (control == null) throw new NullReferenceException("Control cannot be null");
            var window = Window.GetWindow(control);
            Util.CurrentCulture = Loc;
            LanguageManager.SwitchLanguage(window, Util.CurrentCulture);
        }
    }

    public class LanguageSelectorViewModel
    {
        public LanguageSelectorViewModel()
        {
            LanguageItems = new List<LanguageItem>()
            {
                new LanguageItem()
                {
                    Icon = "../../Resources/Icons/Common/united-kingdom-flag-32x32.png",
                    Loc = "en-UK"
                },
                new LanguageItem()
                {
                    Icon = "../../Resources/Icons/Common/spain-flag-32x32.png",
                    Loc = "es-ES"
                }
            };
        }

        public List<LanguageItem> LanguageItems { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Common;

namespace WoT_Win.Common.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected DataManager _dataManager;

        public BaseViewModel()
        { }

        public BaseViewModel(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public string AppTitle => _dataManager?.AppTitle;


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Init()
        { }

        public virtual void RefreshUI()
        { }
    }
}

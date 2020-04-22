using System.ComponentModel;
using System.Runtime.CompilerServices;
using KernelDLL.Common;

namespace WoTServer.ViewModels
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

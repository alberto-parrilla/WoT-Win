using System.ComponentModel;
using System.Runtime.Remoting.Contexts;
using ClientDLL.Client;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Creation.ViewModels
{
    public abstract class BaseCreationViewModel : BaseViewModel, IDataErrorInfo
    {
        public BaseCreationViewModel(IMainClient client) :base(client)
        { }

        public string Header => GetHeader();
        public bool IsValid { get; protected set; }
        public abstract void Validate();
        public bool IsEnabled { get { return false; } }
        public virtual bool IsVisible { get; set; }

        protected abstract string Validate(string propertyName);
        public virtual string Error { get; set; }
    
        public string this[string columnName]
        {
            get { return Validate(columnName); }
        }

        protected abstract string GetHeader();

        public abstract void OnLoaded();
    }
}

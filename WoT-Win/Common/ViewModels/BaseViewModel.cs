using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ClientDLL.Client;
using KernelDLL.Common;
using KernelDLL.Network.Response;

namespace WoT_Win.Common.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected DataManager _dataManager;
        protected IMainClient _client;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        { }

        public BaseViewModel(IMainClient client)
        {
            _client = client;
          
            Register();
        }


        public BaseViewModel(IMainClient client, DataManager dataManager)
        {
            _dataManager = dataManager;
            _client = client;
            Register();
        }

        protected void Register()
        {
            _client.OnManageResponse += ClientOnManageResponse;
           
        }

        protected void Unregister()
        {
            _client.OnManageResponse -= ClientOnManageResponse;
          
        }

        protected void ClientOnManageResponse(object sender, IResponse e)
        {
            ManageResponse(e);
        }

        protected virtual void ManageResponse(IResponse response)
        { }

        public string AppTitle => _dataManager?.AppTitle;


        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Init()
        { }

        public virtual void RefreshUI()
        { }

        protected void CloseWindowSafe(Window w)
        {
            Unregister();
            if (w.Dispatcher.CheckAccess())
                w.Close();
            else
                w.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(w.Close));
        }

        protected void OpenWindowSafe(Action action)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { action(); }));
        }
    }
}

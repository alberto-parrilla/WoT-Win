using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using ClientDLL.Client;
using KernelDLL.Network.Request;

namespace WoT_Win.Common.ViewModels
{
    public class LoadItem
    {
        public LoadItem(RequestMessageBase message, string display)
        {
            Message = message;
            Display = display;
        }

        public RequestMessageBase Message { get; }
        public string Display { get; }
    }

    public class LoadManagerViewModel : BaseViewModel
    {
        private Window _view;

        public LoadManagerViewModel(IMainClient client) : base(client)
        {
            Items = new Queue<LoadItem>();
        }

        private Queue<LoadItem> Items { get; set; }

        public bool IsLoading => Items.Any();

        private string _loadMessage;

        public string LoadMessage
        {
            get => _loadMessage;
            set
            {
                _loadMessage = value;
                OnPropertyChanged();
            }
        }

        private int _maxAmount;

        public int MaxAmount
        {
            get => _maxAmount;
            set
            {
                _maxAmount = value;
                OnPropertyChanged();
            }
        }

        private int _amount;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }


        public string Count => $"{Amount}/{MaxAmount}";

        public void SetView(Window view)
        {
            _view = view;
        }

        public void Add(LoadItem item)
        {
            Items.Enqueue(item);
            MaxAmount = Items.Count;
        }

        public void Next()
        {
            Thread.Sleep(100);
            if (Items.Any())
            {
                var item = Items.Peek();
                Items.Dequeue();
                LoadMessage = item.Display;
                Amount = MaxAmount - Items.Count;
                OnPropertyChanged("Count");
                _client.Send(item.Message);
            }
            else
            {
                CloseWindowSafe(_view);
            }
        }
    }
}

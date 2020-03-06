using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WoT_Win.Common.Commands;
using WoT_Win.Common.Views;

namespace WoT_Win.Common.ViewModels
{
    public enum EnumMessageBox
    {
        YesNo,
        OkCancel
    }

    public static class CustomMessageBox
    {
        public static bool Show(string title, string message, EnumMessageBox mode )
        {
            var messageBox = new CustomMessageBoxView();
            var messageBoxViewModel = new CustomMesssageBox(title, message, mode, messageBox);
            messageBox.DataContext = messageBoxViewModel;
            messageBox.ShowDialog();            
            return messageBoxViewModel.Result;
        }
    }

    public class CustomMesssageBox : BaseViewModel
    {
        private CustomMessageBoxView _view;

        public CustomMesssageBox(string title, string message, EnumMessageBox mode, CustomMessageBoxView view)
        {
            _view = view;
            Title = title;
            Message = message;
            TrueButton = "Aceptar";
            FalseButton = "Cancelar";
            if (mode == EnumMessageBox.OkCancel)
            {
                TrueButton = "Aceptar";
                FalseButton = "Cancelar";
            }
            else if (mode == EnumMessageBox.YesNo)
            {
                TrueButton = "Si";
                FalseButton = "No";
            }

            TrueCommand = new RelayCommand((o) => ResultMessage(true), (o) => true);
            FalseCommand = new RelayCommand((o) => ResultMessage(false), (o) => true);
        }

        public ICommand TrueCommand { get; private set; }
        public ICommand FalseCommand { get; private set; }
        public bool Result { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public string TrueButton { get; private set; }
        public string FalseButton { get; private set; }

        private void ResultMessage(bool close)
        {
            Result = close;
            _view.Close();
        }       
    }
}

﻿using KernelDLL.Common;
using System.Windows;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Init
{
    /// <summary>
    /// Interaction logic for LoadView.xaml
    /// </summary>
    public partial class LoadView : Window
    {
        public LoadView(DataManager dataManager, CreateFactory createFactory)
        {
            InitializeComponent();
            var viewModel = new LoadViewModel(this, dataManager, createFactory);
            viewModel.Init();
            DataContext = viewModel;

            LanguageManager.SwitchLanguage(this, DataManager.CurrentCulture);
        }
    }
}

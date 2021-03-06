﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Common;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;
using System.Windows.Input;
using WoT_Win.Common.Commands;
using System.Windows;
using WoT_Win.Common.Views;
using WoT_Win.Game.GUI;

namespace WoT_Win.Init
{
    public class LoadViewModel : BaseViewModel
    {
        private Window _view;
        private CreateFactory _factory;
        public LoadViewModel(Window view, DataManager dataManager, CreateFactory factory) : base(dataManager)
        {
            _view = view;
            _factory = factory;
            //_dataManager.LoadGames();
            //Items = new ObservableCollection<LoadedGameViewModel>(_dataManager.LoadedGames.Select((m) => _factory.Create(m)).ToList() );
            LoadCommand = new RelayCommand((o) => Load(), (o) => CanLoad());
            CancelCommand = new RelayCommand((o) => Exit(), (o) => true);    
        }

        public override void Init()
        {
            _dataManager.LoadGames();
            Items = new ObservableCollection<LoadedGameViewModel>(_dataManager.LoadedGames.Select((m) => _factory.Create(m)).ToList());
        }

        public string AppTitle => _dataManager.AppTitle;

        public ICommand LoadCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }   

        public ObservableCollection<LoadedGameViewModel> Items { get; private set; }
        private LoadedGameViewModel _selectedItem;
        public LoadedGameViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private bool CanLoad()
        {
            return SelectedItem != null && !SelectedItem.IsNull;
        }

        private void Load()
        {
            //var splashLoadView = new SplashLoadView(_dataManager);            
            //_view.Close();
            //splashLoadView.Show();
            //splashLoadView.LoadGame(SelectedItem);

            _dataManager.LoadGame();
            _dataManager.LoadPlayer(SelectedItem.Model.PlayerId);
            _dataManager.LoadArea(SelectedItem.Model.AreaId);
            _dataManager.LoadScene(SelectedItem.Model.SceneId, SelectedItem.Model.AreaId);

            var gui = new MainGui(_dataManager);

            gui.Show();
            _view.Close();
        }

        private void Exit()
        {
            if (CustomMessageBox.Show("Atención", "Desea salir de la aplicación? /S/N)", EnumMessageBox.YesNo))
            {
                Application.Current.Shutdown();
            }
        }
    }
}

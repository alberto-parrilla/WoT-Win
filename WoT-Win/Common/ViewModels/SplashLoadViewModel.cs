using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using KernelDLL.Common;
using WoT_Win.Game.GUI;
using WoT_Win.Init;

namespace WoT_Win.Common.ViewModels
{
    public class SplashLoadViewModel : BaseViewModel
    {
        private Window _view;
        private DataManager _datamanager;

        public SplashLoadViewModel(Window view, DataManager datamanager)
        {
            _view = view;
            _datamanager = datamanager;
        }

        private string _infoText;

        public string InfoText
        {
            get { return _infoText; }
            set
            {
                _infoText = value;
                OnPropertyChanged();
            }
        }

        private int _progressValue;

        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                OnPropertyChanged();
            }
        }

        public void LoadGame(LoadedGameViewModelLegacy game)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_Completed;

            worker.RunWorkerAsync(game);
        }

        private void worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            var gui = new MainGui();
            gui.Show();
            _view.Close();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadedGameViewModelLegacy game = e.Argument as LoadedGameViewModelLegacy;
            if (game == null) throw new ArgumentNullException("Error loading saved game");

            InfoText = "Cargando juego...";
            _datamanager.LoadGame();
            //Thread.Sleep(1000);
            ProgressValue = 1;

            InfoText = "Cargando jugador...";
            _datamanager.LoadPlayer(game.Model.PlayerId);
            //Thread.Sleep(1000);
            ProgressValue = 2;

            InfoText = "Cargando area...";
            _datamanager.LoadArea(game.Model.AreaId);
            //Thread.Sleep(1000);
            ProgressValue = 3;

            InfoText = "Cargando escena...";
            _datamanager.LoadScene(game.Model.SceneId, game.Model.AreaId);

            InfoText = "Inicializando...";
            _datamanager.LoadGame();
            //Thread.Sleep(1000);
            ProgressValue = 4;             
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressValue = e.ProgressPercentage;
        }

        public void LoadArea(int id)
        {
            _datamanager.LoadArea(id);  
            _view.Close();
        }
      
    }
}

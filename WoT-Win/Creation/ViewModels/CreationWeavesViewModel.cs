using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Common;
using KernelDLL.Game.Enums;
using WoT_Win.Common.Commands;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;
using WoT_Win.Game.Weaves;

namespace WoT_Win.Creation.ViewModels
{
    public class CreationWeavesViewModel : BaseCreationViewModel
    {
        private const int CustomWeavesPoints = 2;

        public CreationWeavesViewModel(IMainClient client, DataManager dataManager, Common.Services.CreateFactory createFactory) : base(client)
        {
            SelectTypeCommand = new RelayCommand((o) => SelectType(o), (o) => true);
            AddCommand = new RelayCommand((o) => Add(), (o) => CanAdd());
            RemoveCommand = new RelayCommand((o) => Remove(), (o) => CanRemove());
            WeavePoints = CustomWeavesPoints;
            Weaves = new ObservableCollection<BaseWeaveViewModel>();
            PlayerWeaves = new ObservableCollection<PlayerWeaveViewModel>();
            
            // TODO: To remove when dataManager is also removed
            AllWeaves = new ObservableCollection<BaseWeaveViewModel>(dataManager.GetWeaves().Select(s => createFactory.Create(s)));
            
            //IsValid = true;
            //IsVisible = false;
        }

        protected override string GetHeader()
        {
            var currentCulture = CultureInfo.CurrentUICulture.Name;
            var dic = $"CreationWeavesView.{currentCulture}.xaml";
            var uri = $"/WoT-Win;component/Resources/Languages/{dic}";
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri(uri, UriKind.Relative));
            return (string)res["Header"];
        }

        public ICommand SelectTypeCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        private bool _isVisible;
        public override bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        private int _weavePoints;
        public int WeavePoints
        {
            get { return _weavePoints; }
            set
            {
                _weavePoints = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BaseWeaveViewModel> AllWeaves { get; private set; }
        public ObservableCollection<BaseWeaveViewModel> Weaves { get; private set; }
        public ObservableCollection<PlayerWeaveViewModel> PlayerWeaves { get; private set; }

        private BaseWeaveViewModel _selectedWeave;

        public BaseWeaveViewModel SelectedWeave
        {
            get { return _selectedWeave; }
            set
            {
                _selectedWeave = value;
                OnPropertyChanged();
            }
        }

        private PlayerWeaveViewModel _selectedPlayerWeave;

        public PlayerWeaveViewModel SelectedPlayerWeave
        {
            get { return _selectedPlayerWeave; }
            set
            {
                _selectedPlayerWeave = value;
                OnPropertyChanged();
            }
        }

        private void SelectType(object parameter)
        {
            EnumWeaveType type = EnumWeaveType.Energy;
            if (parameter.ToString().ToUpper() == "EARTH")
            {
                type = EnumWeaveType.Earth;
            }
            else if (parameter.ToString().ToUpper() == "FIRE")
            {
                type = EnumWeaveType.Fire;
            }
            else if (parameter.ToString().ToUpper() == "WATER")
            {
                type = EnumWeaveType.Water;
            }
            else if (parameter.ToString().ToUpper() == "WIND")
            {
                type = EnumWeaveType.Wind;
            }
            else if (parameter.ToString().ToUpper() == "ENERGY")
            {
                type = EnumWeaveType.Energy;
            }

            Weaves = new ObservableCollection<BaseWeaveViewModel>(AllWeaves.Where(w => w.Type.Contains(type) && w.Level == 1));
            
            Refresh();
        }

        private bool CanAdd()
        {
            return WeavePoints > 0;
        }

        private void Add()
        {

            if (SelectedWeave != null)
            {
                PlayerWeaves.Add(new PlayerWeaveViewModel(SelectedWeave, 0));
                WeavePoints = WeavePoints--;
                Weaves.Remove(SelectedWeave);
                SelectedWeave = null;
            }

            Refresh();
        }

        private bool CanRemove()
        {
            return PlayerWeaves.Any();
        }

        private void Remove()
        {

            if (SelectedPlayerWeave != null)
            {
                Weaves.Add(SelectedPlayerWeave.Weave);
                WeavePoints++;
                PlayerWeaves.Remove(SelectedPlayerWeave);
                SelectedPlayerWeave = null;
            }

            Refresh();
        }

        private void Refresh()
        {
            //OnPropertyChanged("Skills");
            //OnPropertyChanged("Feats");
            //OnPropertyChanged("PlayerSkills");
            //OnPropertyChanged("PlayerFeats");

            foreach (var weave in Weaves)
            {
                weave.IsEnabled = WeavePoints > 0;
            }

            OnPropertyChanged("Weaves");

            Validate();
        }

        public override void Validate()
        {
            if (!PlayerWeaves.Any())
            {
                IsValid = false;
                Error = LanguageManager.GetResourceValue("CreationWeavesView", "ErrorWeaves");
            }
            else
            {
                IsValid = true;
                Error = "";
            }
            RaiseEvents();
        }

        protected override string Validate(string propertyName)
        {
            return "";
        }

        public override string Error { get; set; }

        private void RaiseEvents()
        {
            OnPropertyChanged("IsValid");
            OnPropertyChanged("Error");
        }

        public override void RefreshUI()
        {
            OnPropertyChanged("Header");
            Refresh();
        }

        public override void OnLoaded()
        {
            //TODO: To add again when dataManager is removed
            //AllWeaves = new ObservableCollection<BaseWeaveViewModel>(_dataManager.GetWeaves().Select(s => createFactory.Create(s)));
            IsValid = true;
            IsVisible = false;
        }
    }
}

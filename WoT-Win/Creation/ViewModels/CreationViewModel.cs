using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Common;
using KernelDLL.Creation.Models;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;
using WoT_Win.Common.Views;

namespace WoT_Win.Creation.ViewModels
{
    public sealed class CreationViewModel : CustomBaseViewModel
    {
        private Common.Services.CreateFactory _createFactory;
        private LoadManagerViewModel _loadManager;

        public CreationViewModel(Window view, IMainClient client, Common.Services.CreateFactory createFactory) : base(view, client)
        {
            _createFactory = createFactory;

            NextCommand = new RelayCommand((o) => ChangeSection(true), (o) => CanChangeSection(true));
            PrevCommand = new RelayCommand((o) => ChangeSection(false), (o) => CanChangeSection(false));
            CancelCommand = new RelayCommand((o) => Cancel(), (o) => true);
            FinishCommand = new RelayCommand((o) => Finish(), (o) => CanFinish);

            IsDataLoaded = false;
        }

        public override void Init()
        {
            Items = new ObservableCollection<BaseCreationViewModel>()
            {
                new CreationDataViewModel(_client),
                new CreationAttributesViewModel(_client),
                new CreationSkillsViewModel(_client, _client.DataManager as DataManager, _createFactory),
                new CreationWeavesViewModel(_client, _client.DataManager as DataManager, _createFactory)
            };

            using (CanChangeDisposable())
            {
                CurrentCreationSection = Items[0];
            }
        }

        public void Load()
        {
            _loadManager = new LoadManagerViewModel(_client);
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.Race), "Loading races"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.Gender), "Loading genders"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.Location), "Loading locations"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarBodyToolset), "Loading bodies"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarFaceToolset), "Loading faces"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarBaseHairToolset), "Loading base hair"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarFrontHairToolset), "Loading front hair"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarRearHairToolset), "Loading rear hair"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarEyebrowsToolset), "Loading eyebrows"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarBaseEyesToolset), "Loading base eyes"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarEyesToolset), "Loading eyes"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarEarsToolset), "Loading ears"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarNoseToolset), "Loading noses"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarMouthToolset), "Loading mouths"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarBeardToolset), "Loading beards"));
            _loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.AvatarExtrasToolset), "Loading extras"));
            //_loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.Skill), "Loading skills"));
            //_loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.Feat), "Loading feats"));
            //_loadManager.Add(new LoadItem(new PlayerDataRequest(EnumPlayerDataType.Weave), "Loading weaves"));
            var view = new LoadManagerView(_loadManager);
            OpenWindowSafe(() => view.Show());
            _loadManager.Next();
        }

        public bool CanChange = false;

        public ICommand NextCommand { get; private set; }
        public ICommand PrevCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand FinishCommand { get; private set; }

        private BaseCreationViewModel _currentCreationSection;
        public BaseCreationViewModel CurrentCreationSection
        {
            get { return _currentCreationSection; }
            set
            {
                if (!CanChange) return;
                if (_currentCreationSection != null)
                    _currentCreationSection.PropertyChanged -= CurrentCreationSection_PropertyChanged;
                _currentCreationSection = value;
                if (_currentCreationSection != null)
                    _currentCreationSection.PropertyChanged += CurrentCreationSection_PropertyChanged;
                OnPropertyChanged();
            }
        }

        private CreationDataViewModel CreationData { get { return Items[0] as CreationDataViewModel; } }
        private CreationAttributesViewModel CreationAttributes { get { return Items[1] as CreationAttributesViewModel; } }
        private CreationSkillsViewModel CreationSkills { get { return Items[2] as CreationSkillsViewModel; } }
        private CreationWeavesViewModel CreationWeaves { get { return Items[3] as CreationWeavesViewModel; } }

        public ObservableCollection<BaseCreationViewModel> Items { get; private set; }
        public bool HasWeaves { get { return CreationData.HasWeaves; } }
        private string _info;

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged();
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        private bool _isDataLoaded;

        public bool IsDataLoaded
        {
            get => _isDataLoaded;
            set
            {
                _isDataLoaded = value;
                OnPropertyChanged();
            }
        }

        void CurrentCreationSection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsValid")
            {
                CommandManager.InvalidateRequerySuggested();
            }
            else if (e.PropertyName == "Error")
            {            
                Message = CurrentCreationSection.Error;             
            }
            else if (e.PropertyName == "HasWeaves")
            {                
                if (CreationData != null)
                    Items[3].IsVisible = CreationData.HasWeaves;
            }        
        }

        public bool CanChangeSection(bool next)
        {
            if (next)
            {
                return ((CurrentCreationSection.IsValid) &&
                        (
                            CurrentCreationSection != Items.Last() ||
                            (!CreationData.HasWeaves && CurrentCreationSection != Items[2])
                        ));
                
            }
            else
            {
                return (CurrentCreationSection.IsValid && CurrentCreationSection != Items.First());
            }            
        }

        private void ChangeSection(bool next)
        {
            var index = Items.IndexOf(CurrentCreationSection);
            if (next)
            {
                index++;
            }
            else
            {
                index--;
            }

            using (CanChangeDisposable())
            {
                CurrentCreationSection = Items[index];
            }
        }

        private void Cancel()
        {           
            if (CustomMessageBox.Show("Atención", "Desea salir del proceso de creación? (S/N)", EnumMessageBox.YesNo))
            {
                Application.Current.Shutdown();
            }
        }

        private bool CanFinish
        {
            get { return Items.All(s => s.IsValid); }
        }

        private void Finish()
        {

        }

        IDisposable CanChangeDisposable()
        {
            CanChange = true;
            return DisposableAction.InvokeOnDispose(() => CanChange = false);
        }

        protected override void ManageResponse(IResponse response)
        {
            if (response.ResponseType != EnumResponseType.PlayerData) return;

            if (response is PlayerDataResponse<List<RaceModel>>)
            {
                var races = response as PlayerDataResponse<List<RaceModel>>;
                var listRaces = races.Content;
                _client.DataContainer.Races = listRaces;
            }
            if (response is PlayerDataResponse<List<GenderModel>>)
            {
                var genders = response as PlayerDataResponse<List<GenderModel>>;
                var listGenders = genders.Content;
                _client.DataContainer.Genders = listGenders;
            }
            else if (response is PlayerDataResponse<List<LocationModel>>)
            {
                var locations = response as PlayerDataResponse<List<LocationModel>>;
                var listLocations = locations.Content;
                _client.DataContainer.Locations = listLocations;
            }
            else if (response is PlayerDataResponse<List<AvatarToolsetModel>>)
            {
                var avatarToolset = response as PlayerDataResponse<List<AvatarToolsetModel>>;
                var listAvatarToolset = avatarToolset.Content;
                if (_client.DataContainer.AvatarToolsetItems == null)
                {
                    _client.DataContainer.AvatarToolsetItems = listAvatarToolset;
                }
                else
                {
                    _client.DataContainer.AvatarToolsetItems.AddRange(listAvatarToolset);
                }
            }

            _loadManager.Next();
            if (!_loadManager.IsLoading && !IsDataLoaded)
            {
                IsDataLoaded = true;
                OnLoaded();
            }
        }

        private void OnLoaded()
        {
          Items.ToList().ForEach(i => i.OnLoaded());
        }
    }
}

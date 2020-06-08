using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using KernelDLL.Common;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Creation.Models;
using KernelDLL.Network.Request;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.Views;
using KernelDLL.Network.Response;
using WoT_Win.Creation.Services;

namespace WoT_Win.Creation.ViewModels
{
    public sealed class CreationDataViewModel : BaseCreationViewModel
    {
        public CreationDataViewModel(IMainClient client, CreationManager creationManager) : base(client, creationManager)
        {
            EditAvatarCommand = new RelayCommand((o) => EditAvatar(), (o) => true);
            CheckNameCommand = new RelayCommand((o) => CheckName(), (o) => true);

            IsVisible = true;            
            PropertyChanged += OnPropertyChanged;
        }

        protected override string GetHeader()
        {
            var currentCulture = CultureInfo.CurrentUICulture.Name;
            var dic = $"CreationDataView.{currentCulture}.xaml";
            var uri = $"/WoT-Win;component/Resources/Languages/{dic}";
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri(uri, UriKind.Relative));
            return (string)res["Header"];
        }

        public ICommand EditAvatarCommand { get; private set; }
        public ICommand CheckNameCommand { get; private set; }

        private string _error;
        public override string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged("Error");
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var property = e.PropertyName;            
            switch (property)
            {
                case "SelectedRace":
                    OnRaceChanged(MyRaceItems[SelectedRace]);
                    break;        
                case "SelectedGender":
                    OnGenderChanged(MyGenderItems[SelectedGender]);
                    break;
                case "SelectedLocation":
                    OnLocationChanged(MyLocationItems[SelectedLocation]);
                    break;
            }

            UpdateAvatar();
            Validate();
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public RaceModel Race { get; private set; }
        public GenderModel Gender { get; private set; }
        public LocationModel Location { get; private set; }
        public int Skin { get; private set; }
        public int Hair { get; private set; }
        public int HairColor { get; private set; }

        private IList<RaceModel> _myRaceItems;
        public IList<RaceModel> MyRaceItems 
        {
            get => _myRaceItems;
            set
            {
                _myRaceItems = value;
                OnPropertyChanged();
            }
        }

        public IList<string> RaceItems { get; private set; }
        
        private RaceModel _mySelectedRace;
        public RaceModel MySelectedRace
        {
            get { return _mySelectedRace; }
            set
            {
                _mySelectedRace = value;
                OnPropertyChanged();
            }
        }

        private int _selectedRace;
        public int SelectedRace
        {
            get { return _selectedRace; }
            set
            {
                _selectedRace = value;
                OnPropertyChanged();
            }
        }

        private IList<GenderModel> _myGenderItems;
        public IList<GenderModel> MyGenderItems
        {
            get => _myGenderItems;
            set
            {
                _myGenderItems = value;
                OnPropertyChanged();
            }
        }

        public IList<string> GenderItems { get; private set; }

        private GenderModel _mySelectedGender;
        public GenderModel MySelectedGender
        {
            get { return _mySelectedGender; }
            set
            {
                _mySelectedGender = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGender;
        public int SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                _selectedGender = value;
                OnPropertyChanged();
            }
        }

        public IList<string> LocationItems { get; private set; }
        public IList<LocationModel> MyLocationItems { get; private set; }
        
        private LocationModel _mySelectedLocation;
        public LocationModel MySelectedLocation
        {
            get { return _mySelectedLocation; }
            set
            {
                _mySelectedLocation = value;
                OnPropertyChanged();
            }
        }

        private int _selectedLocation;
        public int SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
            }
        }

        private bool _hasWeaves;
        public bool HasWeaves
        {
            get { return _hasWeaves; }
            set
            {
                _hasWeaves = value;
                OnPropertyChanged();
            }
        }

        private string _portrait;

        public string Portrait
        {
            get { return _portrait; }
            set
            {
                _portrait = value;
                OnPropertyChanged("Portrait");
            }
        }

        private string _avatar;

        public string Avatar
        {
            get
            {
                return _avatar; 
            }
            set
            {
                _avatar = value;
                OnPropertyChanged();
            }
        }

        private void OnRaceChanged(RaceModel race)
        {
            MyLocationItems = race.Locations;
            LocationItems = MyLocationItems.Select(l => l.DisplayName).ToList();
            MySelectedLocation = MyLocationItems.FirstOrDefault();
            SelectedLocation = MyLocationItems.IndexOf(MySelectedLocation);
            OnPropertyChanged("MyLocationItems");
            OnPropertyChanged("LocationItems");
            OnPropertyChanged("SelectedLocation");
            OnPropertyChanged("MySelectedLocation");
            Race = race;
            Manager.Player.Race = Race;
        }

        private void OnGenderChanged(GenderModel gender)
        {
            Gender = gender;
            Manager.Player.Gender = Gender;
        }

        private void OnLocationChanged(LocationModel selectedLocation)
        {
            Location = selectedLocation;
            Manager.Player.Location = Location;
        }

        private void UpdateAvatar()
        {

        }

        public override void Validate()
        {
            IsValid = Error == String.Empty;
        }

        protected override string Validate(string propertyName)
        {
            Error = string.Empty;
            if (propertyName == "Name")
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Error = LanguageManager.GetResourceValue("CreationDataView", "ErrorName");
                }
            }
           
            return Error;
        }

        private void EditAvatar()
        {
            CreatePlayerAvatarView view = new CreatePlayerAvatarView();
            CreatePlayerAvatarViewModel viewModel = new CreatePlayerAvatarViewModel(view, Race, Gender, 0,  0,  0, _client);
            view.DataContext = viewModel;
         
            view.ShowDialog();
        }

        private void CheckName()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                _client.Send(new CheckDataRequest(EnumCheckDataType.Name, Name));
            }
        }

        public override void RefreshUI()
        {
            OnPropertyChanged("Header");
            Validate("Name");
        }

        public override void OnLoaded()
        {
            MyRaceItems = _client.DataContainer.Races.OrderBy(r => r.GameId).ToList();
            RaceItems = MyRaceItems.Select(r => r.DisplayName).ToList();
            MySelectedRace = MyRaceItems.FirstOrDefault();
            SelectedRace = MyRaceItems.IndexOf(MySelectedRace);
            MyGenderItems = _client.DataContainer.Genders.OrderBy(r => r.GameId).ToList();
            GenderItems = MyGenderItems.Select(r => r.DisplayName).ToList();
            MySelectedGender = MyGenderItems.FirstOrDefault();
            SelectedGender = MyGenderItems.IndexOf(MySelectedGender);
            MyLocationItems = MySelectedRace?.Locations;
            LocationItems = MyLocationItems?.Select(r => r.DisplayName).ToList();
            MySelectedLocation = MyLocationItems?.FirstOrDefault();
            SelectedLocation = MyLocationItems.IndexOf(MySelectedLocation);
            HasWeaves = false;
            Portrait = Path.Combine(Util.TestPortraitPath, "faceset.png");
            Avatar = Path.Combine(Util.TestCharasetPath, "charaset.png");
            OnPropertyChanged("RaceItems");
            OnPropertyChanged("LocationItems");
            OnPropertyChanged("GenderItems");
        }

        protected override void ManageResponse(IResponse response)
        {
            if (response.ResponseType != EnumResponseType.CheckData) return;

            var responseMessage = response as CheckDataResponse;
            if (responseMessage == null) return;

            if (responseMessage.CheckResult == EnumCheckDataResult.Exists)
            {
                Error = LanguageManager.GetResourceValue("CreationDataView", "ErrorValidationName");
            }
        }
    }
}

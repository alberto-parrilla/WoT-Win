using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using KernelDLL.Common;
using KernelDLL.Game.Enums;
using System.Windows.Input;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.Views;

namespace WoT_Win.Creation.ViewModels
{
    public sealed class CreationDataViewModel : BaseCreationViewModel
    {
        public CreationDataViewModel()
        {
            RaceItems = Enum.GetNames(typeof(EnumRace)).ToList();
            SelectedRace = 0;
            SexItems = Enum.GetNames(typeof(EnumSex)).ToList();
            SelectedSex = 0;
            NationItems = Enum.GetNames(typeof(EnumHumanNation)).ToList();
            SelectedNation = 0;
            HasWeaves = false;            
            Portrait = Path.Combine(Util.TestPortraitPath, "faceset.png");
            Avatar = Path.Combine(Util.TestCharasetPath, "charaset.png");

            EditAvatarCommand = new RelayCommand((o) => EditAvatar(), (o) => true);

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
                    OnRaceChanged((EnumRace)SelectedRace);
                    break;        
                case "SelectedSex":
                    OnSexChanged((EnumSex) SelectedSex);
                    break;
                case "SelectedNation":
                    OnNationChanged(SelectedNation);
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

        public EnumRace Race { get; private set; }
        public EnumSex Sex { get; private set; }
        public int Nation { get; private set; } 
        public int Skin { get; private set; }
        public int Hair { get; private set; }
        public int HairColor { get; private set; }
        public IList<string> RaceItems { get; private set; }
        
        private int _selectedRace;
        public int SelectedRace
        {
            get { return _selectedRace; }
            set
            {
                _selectedRace = value;
                OnPropertyChanged("SelectedRace");
            }
        }

        public IList<string> SexItems { get; private set; }
        
        private int _selectedSex;
        public int SelectedSex
        {
            get { return _selectedSex; }
            set
            {
                _selectedSex = value;
                OnPropertyChanged("SelectedSex");
            }
        }
        public IList<string> NationItems { get; private set; }
        
        private int _selectedNation;
        public int SelectedNation
        {
            get { return _selectedNation; }
            set
            {
                _selectedNation = value;
                OnPropertyChanged("SelectedNation");
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
                OnPropertyChanged("Avatar");
            }
        }
        private void OnRaceChanged(EnumRace race)
        {
            if (race == EnumRace.Human)
            {            
                NationItems = Enum.GetNames(typeof(EnumHumanNation)).ToList();
            }
            else
            {               
                NationItems = Enum.GetNames(typeof(EnumOgierNation)).ToList();
            }

            OnPropertyChanged("NationItems");
            Race = race;
        }

        private void OnSexChanged(EnumSex sex)
        {
            Sex = sex;
        }

        private void OnNationChanged(int selectedNation)
        {
            var race = (EnumRace) SelectedRace;
            if (race == EnumRace.Human)
            {
                var nation = (EnumHumanNation) selectedNation;                
            }
            else
            {
                var stedding = (EnumOgierNation) selectedNation;                
            }

            Nation = selectedNation;
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
            CreatePlayerAvatarViewModel viewModel = new CreatePlayerAvatarViewModel(view, Race, Sex, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            view.DataContext = viewModel;
         
            view.ShowDialog();
          
        }

        public override void RefreshUI()
        {
            OnPropertyChanged("Header");
            Validate("Name");
        }
    }
}

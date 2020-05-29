using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using ClientDLL.Client;
using KernelDLL.Common;
using WoT_Win.Common.Commands;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;
using WoT_Win.Game.Skills;

namespace WoT_Win.Creation.ViewModels
{
    public class CreationSkillsViewModel : BaseCreationViewModel
    {
        private const int CustomSkillPoints = 4;
        private const int CustomFeatPoints = 3;

        public CreationSkillsViewModel(IMainClient client, DataManager dataManager, Common.Services.CreateFactory createFactory) : base(client)
        {
            AddCommand = new RelayCommand((o) => Add(),(o) =>  CanAdd());
            RemoveCommand = new RelayCommand((o) => Remove(), (o) => CanRemove());
            SkillPoints = CustomSkillPoints;
            FeatPoints = CustomFeatPoints;
            Skills = new ObservableCollection<BaseSkillViewModel>(dataManager.GetSkills().Select(s => createFactory.Create(s)));
            Feats = new ObservableCollection<BaseSkillViewModel>(dataManager.GetFeats().Select(f => createFactory.Create(f)));
            PlayerSkills = new ObservableCollection<PlayerSkillViewModel>();
            PlayerFeats = new ObservableCollection<PlayerFeatViewModel>();
            IsSkillChecked = true;

            IsVisible = true;
        }

        protected override string GetHeader()
        {
            var currentCulture = CultureInfo.CurrentUICulture.Name;
            var dic = $"CreationSkillsView.{currentCulture}.xaml";
            var uri = $"/WoT-Win;component/Resources/Languages/{dic}";
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri(uri, UriKind.Relative));
            return (string)res["Header"];
        }

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        private bool _isSkillChecked;

        public bool IsSkillChecked
        {
            get { return _isSkillChecked; }
            set
            {
                _isSkillChecked = value;
                OnPropertyChanged();
            }
        }

        private int _skillPoints;

        public int SkillPoints
        {
            get { return _skillPoints; }
            set
            {
                _skillPoints = value;
                OnPropertyChanged();
            }
        }

        private int _featPoints;

        public int FeatPoints
        {
            get { return _featPoints; }
            set
            {
                _featPoints = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BaseSkillViewModel> Skills { get; private set; }
        public ObservableCollection<PlayerSkillViewModel> PlayerSkills { get; private set; }

        private BaseSkillViewModel _selectedSkill;

        public BaseSkillViewModel SelectedSkill
        {
            get { return _selectedSkill; }
            set
            {
                _selectedSkill = value;
                OnPropertyChanged();
            }
        }

        private PlayerSkillViewModel _selectedPlayerSkill;
        
        public PlayerSkillViewModel SelectedPlayerSkill
        {
            get { return _selectedPlayerSkill; }
            set
            {
                _selectedPlayerSkill = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseSkillViewModel> Feats { get; private set; }
        public ObservableCollection<PlayerFeatViewModel> PlayerFeats { get; private set; }
        
        private BaseSkillViewModel _selectedFeat;

        public BaseSkillViewModel SelectedFeat
        {
            get { return _selectedFeat; }
            set
            {
                _selectedFeat = value;
                OnPropertyChanged();
            }
        }

        private PlayerFeatViewModel _selectedPlayerFeat;

        public PlayerFeatViewModel SelectedPlayerFeat
        {
            get { return _selectedPlayerFeat; }
            set
            {
                _selectedPlayerFeat = value;
                OnPropertyChanged();
            }
        }

        private bool CanAdd()
        {
            if (IsSkillChecked) return SkillPoints > 0;
            return FeatPoints > 0;
        }

        private void Add()
        {
            if (IsSkillChecked)
            {
                if (SelectedSkill != null)
                {
                    PlayerSkills.Add(new PlayerSkillViewModel(SelectedSkill, 0));
                    SkillPoints = SkillPoints - SelectedSkill.Cost;
                    Skills.Remove(SelectedSkill);
                    SelectedSkill = null;
                }
            }
            else
            {
                if (SelectedFeat != null)
                {
                    PlayerFeats.Add(new PlayerFeatViewModel(SelectedFeat, 0));
                    FeatPoints = FeatPoints - SelectedFeat.Cost;
                    Feats.Remove(SelectedFeat);
                    SelectedFeat = null;
                   
                }
            }

            Refresh();
        }

        private bool CanRemove()
        {
            if (IsSkillChecked) return PlayerSkills.Any();
            return PlayerFeats.Any();
        }

        private void Remove()
        {
            if (IsSkillChecked)
            {
                if (SelectedPlayerSkill != null)
                {
                    Skills.Add(SelectedPlayerSkill.Skill);
                    SkillPoints = SkillPoints + SelectedPlayerSkill.Skill.Cost;
                    PlayerSkills.Remove(SelectedPlayerSkill);
                    SelectedPlayerSkill = null;
                }
            }
            else
            {
                if (SelectedPlayerFeat != null)
                {
                    Feats.Add(SelectedPlayerFeat.Feat);
                    FeatPoints = FeatPoints + SelectedPlayerFeat.Feat.Cost;
                    PlayerFeats.Remove(SelectedPlayerFeat);
                    SelectedPlayerFeat = null;
                }
            }

            Refresh();
        }

        private void Refresh()
        {
            //OnPropertyChanged("Skills");
            //OnPropertyChanged("Feats");
            //OnPropertyChanged("PlayerSkills");
            //OnPropertyChanged("PlayerFeats");

            foreach (var skill in Skills)
            {
                skill.IsEnabled = SkillPoints >= skill.Cost;
            }
            foreach (var feat in Feats)
            {
                feat.IsEnabled = FeatPoints >= feat.Cost;
            }

            OnPropertyChanged("Skills");
            OnPropertyChanged("Feats");

            Validate();
        }

        public override void Validate()
        {
            if (!PlayerSkills.Any())
            {
                IsValid = false;
                Error = LanguageManager.GetResourceValue("CreationSkillsView", "ErrorSkills");
            }
            else if (!PlayerFeats.Any())
            {
                IsValid = false;
                Error = LanguageManager.GetResourceValue("CreationSkillsView", "ErrorFeats");
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
            //Skills = new ObservableCollection<BaseSkillViewModel>(dataManager.GetSkills().Select(s => createFactory.Create(s)));
            //Feats = new ObservableCollection<BaseSkillViewModel>(dataManager.GetFeats().Select(f => createFactory.Create(f)));
            //PlayerSkills = new ObservableCollection<PlayerSkillViewModel>();
            //PlayerFeats = new ObservableCollection<PlayerFeatViewModel>();
            //IsSkillChecked = true;

            //IsVisible = true;
        }
    }
}

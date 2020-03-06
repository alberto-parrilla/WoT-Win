using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using KernelDLL.Common;
using KernelDLL.Game.Enums;
using Microsoft.Win32;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.Services;
using WoT_Win.Creation.Views;

namespace WoT_Win.Creation.ViewModels
{
    public sealed class CreatePlayerAvatarViewModel : BaseViewModel
    {
        private Window _view;
        private readonly CreationManager _creationManager;
        private readonly EnumRace _race;
        private readonly EnumSex _sex;

        public CreatePlayerAvatarViewModel(CreatePlayerAvatarView view, EnumRace race, EnumSex sex,
            int skin, int face, int hair1, int hair2, int hairColor, int eyes, int eyesColor, int nose, int mouth, int beard, int beardColor, int extra)
        {
            _view = view;
            FinishCommand = new RelayCommand((o) => Finish(), (o) => true);
            CancelCommand = new RelayCommand((o) => Cancel(), (o) => true);
            _creationManager = new CreationManager();
            _race = race;
            _sex = sex;
            SelectedSkin = skin;
            SelectedFace = face;
            SelectedHair1 = hair1;
            SelectedHair2 = hair2;
            SelectedEyes = eyes;
            SelectedNose = nose;
            SelectedMouth = mouth;
            SelectedBeard = beard;
            SelectedExtra = extra;

            SkinItems = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Skin, SelectedSkin + 1));
            FaceItems = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Face, SelectedSkin + 1));
            Hair1Items = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Hair1, -1) + 1);
            Hair2Items = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Hair2, -1) + 1);  
            EyesItems = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Eyes, SelectedSkin + 1));
            EyesColorItems = new List<SolidColorBrush>()
            {
                new SolidColorBrush(Colors.LightGray),
                new SolidColorBrush(Colors.DeepSkyBlue),
                new SolidColorBrush(Colors.Green),
                new SolidColorBrush(Colors.SaddleBrown),
                new SolidColorBrush(Colors.Black)
            };
            NoseItems = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Nose, SelectedSkin + 1));
            MouthItems = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Mouth, -1));
            BeardItems = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Beard, -1) + 1);
            ExtraItems = CreateItemsList(_creationManager.GetResourcesCounter(_race, _sex, EnumResourceType.Extras, -1));
    
            PropertyChanged += CreatePlayerAvatarViewModel_PropertyChanged;
            Refresh();
        }

        void CreatePlayerAvatarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Contains("ColorSelected"))
            {

            }
            else if (!e.PropertyName.Contains("Path"))
                Refresh();
            
        }

        public ICommand FinishCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public IList<string> SkinItems { get; private set; }
        public IList<string> FaceItems { get; private set; }
        public IList<string> Hair1Items { get; private set; }
        public IList<string> Hair2Items { get; private set; }
        //public IList<Color> HairColorItems { get; private set; }
        public IList<string> EyesItems { get; private set; }
        public IList<SolidColorBrush> EyesColorItems { get; private set; }
        public IList<string> NoseItems { get; private set; }
        public IList<string> MouthItems { get; private set; }
        public IList<string> BeardItems { get; private set; }
        public IList<Color> BeardColorItems { get; private set; }
        public IList<string> ExtraItems { get; private set; }

        private int _selectedSkin;
        public int SelectedSkin
        {
            get { return _selectedSkin; }
            set
            {
                _selectedSkin = value;
                OnPropertyChanged();
            }
        }

        private int _selectedFace;
        public int SelectedFace
        {
            get { return _selectedFace; }
            set
            {
                _selectedFace = value;
                OnPropertyChanged();
            }
        }

        private int _selectedHair1;
        public int SelectedHair1
        {
            get { return _selectedHair1; }
            set
            {
                _selectedHair1 = value;
                OnPropertyChanged();
            }
        }

        private int _selectedHair2;
        public int SelectedHair2
        {
            get { return _selectedHair2; }
            set
            {
                _selectedHair2 = value;
                OnPropertyChanged();
            }
        }

        private int _selectedEyes;
        public int SelectedEyes
        {
            get { return _selectedEyes; }
            set
            {
                _selectedEyes = value;
                OnPropertyChanged();
            }
        }


        private SolidColorBrush _eyeColorSelected;
        public SolidColorBrush EyeColorSelected
        {
            get { return _eyeColorSelected; }
            set
            {
                _eyeColorSelected = value;
                OnPropertyChanged();
            }
        }

        private int _selectedNose;
        public int SelectedNose
        {
            get { return _selectedNose; }
            set
            {
                _selectedNose = value;
                OnPropertyChanged();
            }
        }

        private int _selectedMouth;
        public int SelectedMouth
        {
            get { return _selectedMouth; }
            set
            {
                _selectedMouth = value;
                OnPropertyChanged();
            }
        }

        private int _selectedBeard;
        public int SelectedBeard
        {
            get { return _selectedBeard; }
            set
            {
                _selectedBeard = value;
                OnPropertyChanged();
            }
        }

        private int _selectedExtra;
        public int SelectedExtra
        {
            get { return _selectedExtra; }
            set
            {
                _selectedExtra = value;
                OnPropertyChanged();
            }
        }

        public int Skin { get; private set; }
        public int Face { get; private set; }
        public int Hair1 { get; private set; }
        public int Hair2 { get; private set; }
        public int ColorHair { get; private set; }
        public int Eyes { get; private set; }
        public int ColorEyes { get; private set; }
        public int Nose { get; private set; }
        public int Mouth { get; private set; }
        public int Beard { get; private set; }
        public int ColorBeard { get; private set; }
        public int Extras { get; private set; }

        public string SkinPath { get { return _creationManager.GetResourcesPath(true, -1, _race, _sex, EnumResourceType.Skin, _selectedSkin + 1); } }
        public string FacePath { get { return _creationManager.GetResourcesPath(true, SelectedFace + 1, _race, _sex, EnumResourceType.Face, _selectedSkin + 1); } }
        public string Hair1Path { get { return _creationManager.GetResourcesPath(true, SelectedHair1, _race, _sex, EnumResourceType.Hair1, -1); } }
        public string Hair2Path { get { return _creationManager.GetResourcesPath(true, SelectedHair2, _race, _sex, EnumResourceType.Hair2, -1); } }
        public string EarsPath { get { return _creationManager.GetResourcesPath(true, 0, _race, _sex, EnumResourceType.Ears, _selectedSkin + 1); } }
        public string EyesPath { get { return _creationManager.GetResourcesPath(true, SelectedEyes + 1, _race, _sex, EnumResourceType.Eyes, -1); } }
        public string EyesBasePath { get { return _creationManager.GetResourcesPath(true, SelectedEyes + 1, _race, _sex, EnumResourceType.BaseEyes, -1); } }
        public string NosePath { get { return  _creationManager.GetResourcesPath(true, SelectedNose + 1, _race, _sex, EnumResourceType.Nose, _selectedSkin + 1); } }
        public string MouthPath { get { return _creationManager.GetResourcesPath(true, SelectedMouth + 1, _race, _sex, EnumResourceType.Mouth, -1); } }
        public string BeardPath { get { return _creationManager.GetResourcesPath(true, SelectedBeard, _race, _sex, EnumResourceType.Beard, -1); } }
        public string ExtrasPath { get { return _creationManager.GetResourcesPath(true, SelectedExtra + 1, _race, _sex, EnumResourceType.Extras, -1); } }

        public string SkinCharaPath { get { return _creationManager.GetResourcesPath(false, -1, _race, _sex, EnumResourceType.Skin, _selectedSkin + 1); } }
        public string Hair1CharaPath { get { return _creationManager.GetResourcesPath(false, SelectedHair1, _race, _sex, EnumResourceType.Hair1, -1); } }
        public string Hair2CharaPath { get { return _creationManager.GetResourcesPath(false, SelectedHair2, _race, _sex, EnumResourceType.Hair2, -1); } }        
        public string EarsCharaPath { get { return _creationManager.GetResourcesPath(false, 0, _race, _sex, EnumResourceType.Ears, _selectedSkin + 1); } }
        public string EyesCharaPath { get { return _creationManager.GetResourcesPath(false, SelectedEyes + 1, _race, _sex, EnumResourceType.Eyes, -1); } }
        public string BeardCharaPath { get { return _creationManager.GetResourcesPath(false, SelectedBeard, _race, _sex, EnumResourceType.Beard, -1); } }
        public string ExtrasCharaPath { get { return _creationManager.GetResourcesPath(false, SelectedExtra + 1, _race, _sex, EnumResourceType.Extras, -1); } }

        private void Finish()
        {
            Save();
            Close();
        }

        private void Save()
        {

        }

        private void Cancel()
        {
            Close();
        }

        private void Close()
        {
            _view.Close();
        }

        private void Refresh()
        {
            OnPropertyChanged("SkinPath");
            OnPropertyChanged("FacePath");
            OnPropertyChanged("Hair1Path");
            OnPropertyChanged("Hair2Path");
            OnPropertyChanged("EarsPath");
            OnPropertyChanged("NosePath");
            OnPropertyChanged("EyesPath");
            OnPropertyChanged("EyesBasePath");
            OnPropertyChanged("MouthPath");
            OnPropertyChanged("BeardPath");
            OnPropertyChanged("ExtrasPath");

            OnPropertyChanged("SkinCharaPath");    
            OnPropertyChanged("Hair1CharaPath");
            OnPropertyChanged("Hair2CharaPath");
            OnPropertyChanged("EarsCharaPath");
            OnPropertyChanged("EyesCharaPath");
            OnPropertyChanged("BeardCharaPath");
            OnPropertyChanged("ExtrasCharaPath");
        }   

        private List<string> CreateItemsList(int count)
        {
            var list = new List<string>();
            if (count > 0)
            {
                for (int i = 1; i <= count; i++)
                {
                    list.Add(string.Format("{0}/{1}", i, count));
                }
            }
            return list;
        }
    }
}

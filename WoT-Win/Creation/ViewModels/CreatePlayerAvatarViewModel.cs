using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ClientDLL.Client;
using KernelDLL.Creation.Models;
using KernelDLL.Network.Request;
using WoT_Win.Common.Commands;
using WoT_Win.Common.ViewModels;
using WoT_Win.Creation.Services;
using WoT_Win.Creation.Views;

namespace WoT_Win.Creation.ViewModels
{
    public sealed class CreatePlayerAvatarViewModel : BaseViewModel
    {
        private IMainClient _client;
        private Window _view;
        private readonly CreationManager _creationManager;
        private readonly RaceModel _race;
        private readonly GenderModel _gender;

        public CreatePlayerAvatarViewModel(CreatePlayerAvatarView view, RaceModel race, GenderModel gender,
            int hairColor, int eyesColor, int beardColor, IMainClient client)
        {
            _client = client;
            _view = view;
            FinishCommand = new RelayCommand((o) => Finish(), (o) => true);
            CancelCommand = new RelayCommand((o) => Cancel(), (o) => true);
            _creationManager = new CreationManager();
            _race = race;
            _gender = gender;

            SkinToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarBodyToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            SkinItems = CreateItemsList(SkinToolsetItems.Count);
            SelectedSkin = 0;

            FaceToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarFaceToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            FaceItems = CreateItemsList(FaceToolsetItems.Count);
            SelectedFace = 0;

            BaseHairToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarBaseHairToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());

            FrontHairToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarFrontHairToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            FrontHairItems = CreateItemsList(FrontHairToolsetItems.Count);
            SelectedFrontHair = 0;

            RearHairToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarRearHairToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            RearHairItems = CreateItemsList(RearHairToolsetItems.Count);
            SelectedRearHair = 0;

            HairColorItems = new List<SolidColorBrush>()
            {
                new SolidColorBrush(Colors.Yellow),
                new SolidColorBrush(Colors.SaddleBrown),
                new SolidColorBrush(Colors.Red),
                new SolidColorBrush(Colors.LightGray),
                new SolidColorBrush(Colors.Black)
            };

            EyebrowsToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarEyebrowsToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            EyebrowsItems = CreateItemsList(EyebrowsToolsetItems.Count);
            SelectedEyebrows = 0;

            BaseEyesToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarBaseEyesToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            EyesToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarEyesToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            EyesItems = CreateItemsList(EyesToolsetItems.Count);
            SelectedEyes = 0;

            EyesColorItems = new List<SolidColorBrush>()
            {
                new SolidColorBrush(Colors.LightGray),
                new SolidColorBrush(Colors.DeepSkyBlue),
                new SolidColorBrush(Colors.Green),
                new SolidColorBrush(Colors.SaddleBrown),
                new SolidColorBrush(Colors.Black)
            };

            EarsToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarEarsToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            EarsItems = CreateItemsList(EarsToolsetItems.Count);
            SelectedEars = 0;

            NoseToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarNoseToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            NoseItems = CreateItemsList(NoseToolsetItems.Count);
            SelectedNose = 0;

            MouthToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarMouthToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            MouthItems = CreateItemsList(MouthToolsetItems.Count);
            SelectedMouth = 0;

            BeardToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarBeardToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            BeardItems = CreateItemsList(BeardToolsetItems.Count);
            SelectedBeard = 0;

            ExtrasToolsetItems = CreateAvatarToolsetItemsList(_client.DataContainer.AvatarToolsetItems.Where(m => m.DataType == EnumPlayerDataType.AvatarExtrasToolset && m.Gender == _gender.DisplayName && m.Race == _race.DisplayName).ToList());
            ExtrasItems = CreateItemsList(ExtrasToolsetItems.Count);
            SelectedExtras = 0;

            TestPalette = GetTestPalette();

            PropertyChanged += CreatePlayerAvatarViewModel_PropertyChanged;
            Refresh();
        }

        private BitmapSource GetTestPalette()
        {
            Assembly myAssembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "KernelDLL");
            using (Stream myStream = myAssembly.GetManifestResourceStream("KernelDLL.Resources.Player.Generator.TestPalette.png"))
            {
                Bitmap bitmap = new Bitmap(myStream);
                return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height));
            }
        }

        void CreatePlayerAvatarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Contains("EyesSelectedColor"))
            {
                //EyesToolsetItems.ElementAt(SelectedEyes).CharasetImage = ChangeColor(EyesCharaImage, EyesSelectedColor);
                EyesToolsetItems.ElementAt(SelectedEyes).FacesetImage = ChangeColor(EyesFaceImage, EyesSelectedColor);

                OnPropertyChanged("EyesCharaImage");
                OnPropertyChanged("EyesFaceImage");
            }
            else if (e.PropertyName.Contains("HairSelectedColor"))
            {
                BaseHairToolsetItems.ElementAt(SelectedRearHair).CharasetImage = ChangeColor(BaseHairFaceImage, HairSelectedColor);

                FrontHairToolsetItems.ElementAt(SelectedFrontHair).CharasetImage = ChangeColor(FrontHairCharaImage, HairSelectedColor);
                FrontHairToolsetItems.ElementAt(SelectedFrontHair).FacesetImage = ChangeColor(FrontHairFaceImage, HairSelectedColor); 

                RearHairToolsetItems.ElementAt(SelectedRearHair).CharasetImage = ChangeColor(RearHairCharaImage, HairSelectedColor);
                RearHairToolsetItems.ElementAt(SelectedRearHair).FacesetImage = ChangeColor(RearHairFaceImage, HairSelectedColor);

                EyebrowsToolsetItems.ElementAt(SelectedEyebrows).FacesetImage = ChangeColor(EyebrowsFaceImage, HairSelectedColor);
                
                BeardToolsetItems.ElementAt(SelectedBeard).CharasetImage = ChangeColor(BeardCharaImage, HairSelectedColor);
                BeardToolsetItems.ElementAt(SelectedBeard).FacesetImage = ChangeColor(BeardFaceImage, HairSelectedColor);

                OnPropertyChanged("BaseHairFaceImage");
                OnPropertyChanged("FrontHairCharaImage");
                OnPropertyChanged("FrontHairFaceImage");
                OnPropertyChanged("RearHairCharaImage");
                OnPropertyChanged("RearHairFaceImage");
                OnPropertyChanged("EyebrowsFaceImage");
                OnPropertyChanged("BeardCharaImage");
                OnPropertyChanged("BeardFaceImage");
            }
            else if (!e.PropertyName.Contains("Image"))
                Refresh();
            
        }

        private BitmapSource TestPalette { get; set; }

        public ICommand FinishCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public IList<string> SkinItems { get; private set; }
        public IList<CreateAvatarToolsetItem> SkinToolsetItems { get; private set; }

        public IList<string> FaceItems { get; private set; }
        public IList<CreateAvatarToolsetItem> FaceToolsetItems { get; private set; }

        public IList<CreateAvatarToolsetItem> BaseHairToolsetItems { get; private set; }

        public IList<string> FrontHairItems { get; private set; }
        public IList<CreateAvatarToolsetItem> FrontHairToolsetItems { get; private set; }

        public IList<string> RearHairItems { get; private set; }
        public IList<CreateAvatarToolsetItem> RearHairToolsetItems { get; private set; }

        public IList<SolidColorBrush> HairColorItems { get; private set; }

        public IList<string> EyebrowsItems { get; private set; }
        public IList<CreateAvatarToolsetItem> EyebrowsToolsetItems { get; private set; }

        public IList<string> EyesItems { get; private set; }
        public IList<CreateAvatarToolsetItem> BaseEyesToolsetItems { get; private set; }
        public IList<CreateAvatarToolsetItem> EyesToolsetItems { get; private set; }
        public IList<SolidColorBrush> EyesColorItems { get; private set; }

        public IList<string> EarsItems { get; private set; }
        public IList<CreateAvatarToolsetItem> EarsToolsetItems { get; private set; }

        public IList<string> NoseItems { get; private set; }
        public IList<CreateAvatarToolsetItem> NoseToolsetItems { get; private set; }

        public IList<string> MouthItems { get; private set; }
        public IList<CreateAvatarToolsetItem> MouthToolsetItems { get; private set; }

        public IList<string> BeardItems { get; private set; }
        public IList<CreateAvatarToolsetItem> BeardToolsetItems { get; private set; }

        public IList<string> ExtrasItems { get; private set; }
        public IList<CreateAvatarToolsetItem> ExtrasToolsetItems { get; private set; }

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

        private int _selectedFrontHair;
        public int SelectedFrontHair
        {
            get { return _selectedFrontHair; }
            set
            {
                _selectedFrontHair = value;
                OnPropertyChanged();
            }
        }

        private int _selectedRearHair;
        public int SelectedRearHair
        {
            get { return _selectedRearHair; }
            set
            {
                _selectedRearHair = value;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _hairSelectedColor;
        public SolidColorBrush HairSelectedColor
        {
            get { return _hairSelectedColor; }
            set
            {
                _hairSelectedColor = value;
                OnPropertyChanged();
            }
        }

        private int _selectedEyebrows;
        public int SelectedEyebrows
        {
            get { return _selectedEyebrows; }
            set
            {
                _selectedEyebrows = value;
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


        private SolidColorBrush _eyesSelectedColor;
        public SolidColorBrush EyesSelectedColor
        {
            get { return _eyesSelectedColor; }
            set
            {
                _eyesSelectedColor = value;
                OnPropertyChanged();
            }
        }

        private int _selectedEars;
        public int SelectedEars
        {
            get { return _selectedEars; }
            set
            {
                _selectedEars = value;
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

        private int _selectedExtras;
        public int SelectedExtras
        {
            get { return _selectedExtras; }
            set
            {
                _selectedExtras = value;
                OnPropertyChanged();
            }
        }

        public int Skin { get; private set; }
        public int Face { get; private set; }
        public int FrontHair { get; private set; }
        public int RearHair { get; private set; }
        public int ColorHair { get; private set; }
        public int Eyes { get; private set; }
        public int ColorEyes { get; private set; }
        public int Ears { get; private set; }
        public int Nose { get; private set; }
        public int Mouth { get; private set; }
        public int Beard { get; private set; }
        public int ColorBeard { get; private set; }
        public int Extras { get; private set; }

        public BitmapSource SkinImage { get { return SkinToolsetItems.ElementAt(_selectedSkin).CharasetImage; } }
        public BitmapSource FaceImage { get { return FaceToolsetItems.ElementAt(SelectedFace).FacesetImage; } }
        public BitmapSource BaseHairFaceImage { get { return BaseHairToolsetItems.ElementAt(SelectedRearHair).FacesetImage; } }
        public BitmapSource FrontHairCharaImage { get { return FrontHairToolsetItems.ElementAt(SelectedFrontHair).CharasetImage; } }
        public BitmapSource FrontHairFaceImage { get { return FrontHairToolsetItems.ElementAt(SelectedFrontHair).FacesetImage; } }
        public BitmapSource RearHairCharaImage { get { return RearHairToolsetItems.ElementAt(SelectedRearHair).CharasetImage; } }
        public BitmapSource RearHairFaceImage { get { return RearHairToolsetItems.ElementAt(SelectedRearHair).FacesetImage; } }
        public BitmapSource EyebrowsFaceImage { get { return EyebrowsToolsetItems.Any() ? EyebrowsToolsetItems.ElementAt(SelectedEyebrows).FacesetImage : null; } }
        public BitmapSource BaseEyesFaceImage { get { return BaseEyesToolsetItems.Any() ? BaseEyesToolsetItems.ElementAt(SelectedEyes).FacesetImage : null; } }
        public BitmapSource EyesFaceImage { get { return EyesToolsetItems.Any() ? EyesToolsetItems.ElementAt(SelectedEyes).FacesetImage : null; } }
        public BitmapSource EarsCharaImage { get { return EarsToolsetItems.Any() ? EarsToolsetItems.ElementAt(SelectedEars).CharasetImage : null; } }
        public BitmapSource EarsFaceImage { get { return EarsToolsetItems.Any() ? EarsToolsetItems.ElementAt(SelectedEars).FacesetImage : null; } }
        public BitmapSource NoseFaceImage { get { return NoseToolsetItems.Any() ? NoseToolsetItems.ElementAt(SelectedNose).FacesetImage : null; } }
        public BitmapSource MouthFaceImage { get { return MouthToolsetItems.Any() ? MouthToolsetItems.ElementAt(SelectedMouth).FacesetImage : null; } }
        public BitmapSource BeardCharaImage { get { return BeardToolsetItems.Any() ? BeardToolsetItems.ElementAt(SelectedBeard).CharasetImage : null; } }
        public BitmapSource BeardFaceImage { get { return BeardToolsetItems.Any() ? BeardToolsetItems.ElementAt(SelectedBeard).FacesetImage : null; } }
        public BitmapSource ExtrasCharaImage { get { return ExtrasToolsetItems.Any() ? ExtrasToolsetItems.ElementAt(SelectedExtras).CharasetImage : null; } }
        public BitmapSource ExtrasFaceImage { get { return ExtrasToolsetItems.Any() ? ExtrasToolsetItems.ElementAt(SelectedExtras).FacesetImage : null; } }
        

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
            OnPropertyChanged("FaceImage");

            OnPropertyChanged("BaseHairFaceImage");
            OnPropertyChanged("FrontHairFaceImage");
            OnPropertyChanged("RearHairFaceImage");
            OnPropertyChanged("EyebrowsFaceImage");
            OnPropertyChanged("BaseEyesFaceImage");
            OnPropertyChanged("EyesFaceImage");
            OnPropertyChanged("EarsFaceImage");
            OnPropertyChanged("NoseFaceImage");
            OnPropertyChanged("MouthFaceImage");
            OnPropertyChanged("BeardFaceImage");
            OnPropertyChanged("ExtrasFaceImage");

            OnPropertyChanged("SkinImage");
            OnPropertyChanged("FrontHairCharaImage");
            OnPropertyChanged("RearHairCharaImage");
            OnPropertyChanged("EarsCharaImage");
            //OnPropertyChanged("EyesCharaPath");
            OnPropertyChanged("BeardCharaImage");
            OnPropertyChanged("ExtrasCharaImage");
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

        private List<CreateAvatarToolsetItem> CreateAvatarToolsetItemsList(List<AvatarToolsetModel> items)
        {
            var list = new List<CreateAvatarToolsetItem>();
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items.ElementAt(i);
                    list.Add( new CreateAvatarToolsetItem(i, item.CharasetImage, item.FacesetImage));
                }
            }
            return list;
        }

        private BitmapSource ChangeColor(BitmapSource source, SolidColorBrush color)
        {
            int offset = 0;
            if (color.Color == Colors.Yellow)
            {
                offset = 0;
            }
            else if (color.Color == Colors.SaddleBrown)
            {
                offset = 1;
            }
            else if (color.Color == Colors.Red)
            {
                offset = 2;
            }
            else if (color.Color == Colors.LightGray)
            {
                offset = 3;
            }
            else if (color.Color == Colors.Black)
            {
                offset = 4;
            }

            int baseOffset = 4080 * offset;

            var format = PixelFormats.Bgra32;
            var temp = new FormatConvertedBitmap(source, format, null, 0); // force BGRA
            var bitmap = new WriteableBitmap(temp);

            var width = bitmap.PixelWidth;
            var height = bitmap.PixelHeight;
            var stride = bitmap.BackBufferStride;
            var buffer = new byte[stride * height];
            bitmap.CopyPixels(buffer, stride, 0);

            var tempPalette = new FormatConvertedBitmap(TestPalette, format, null, 0); // force BGRA
            var bitmapPalette = new WriteableBitmap(tempPalette);

            var widthPalette = bitmapPalette.PixelWidth;
            var heightPalette = bitmapPalette.PixelHeight;
            var stridePalette = bitmapPalette.BackBufferStride;
            var bufferPalette = new byte[stridePalette * heightPalette];
            bitmapPalette.CopyPixels(bufferPalette, stridePalette, 0);

            for (int i = 0; i < buffer.Length; i += 4)
            {
                byte b = buffer[i + 0];
                byte g = buffer[i + 1];
                byte r = buffer[i + 2];
                byte a = buffer[i + 3];

                if (b != 0 || g != 0 || r != 0)
                {
                    int index = -1;
                    bool notFound = true;
                    int j = 0;
                    while (notFound && j < bufferPalette.Length)
                    {
                        if (bufferPalette[j + 0] == b && bufferPalette[j + 1] == g && bufferPalette[j + 2] == r) // && buffer[j + 3] == a)
                        {
                            index = j;
                            notFound = false;
                        }
                        j += 4;
                    }

                    if (index != -1)
                    {
                        var factor = index % 4080;
                        int newIndex = factor + baseOffset;
                        if (newIndex >= 0)
                        {
                            buffer[i + 0] = bufferPalette[newIndex + 0];
                            buffer[i + 1] = bufferPalette[newIndex + 1];
                            buffer[i + 2] = bufferPalette[newIndex + 2];
                            buffer[i + 3] = bufferPalette[newIndex + 3];
                        }
                    }
                }

                // Blue
                //buffer[i + 0] = color.Color.B;

                // Green
                //buffer[i + 1] = color.Color.G;

                // Red
                //buffer[i + 2] = color.Color.R;

                // Alpha
                //buffer[i + 3] = color.Color.A;

            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), buffer, stride, 0);

            return ConvertWriteableBitmapToBitmapImage(bitmap);
        }

        public BitmapImage ConvertWriteableBitmapToBitmapImage(WriteableBitmap wbm)
        {
            BitmapImage bmImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(wbm));
                encoder.Save(stream);
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = stream;
                bmImage.EndInit();
                bmImage.Freeze();
            }
            return bmImage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KernelDLL.Common;
using KernelDLL.Creation.Models;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Models;
using WoT_Win.Common.Commands;
using WoT_Win.Game.Actor;
using WoT_Win.Game.GUI;
using WoT_Win.Game.Objects;

namespace WoT_Win.Common.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        //private int _offsetMovRange = Util.MovRange;
        private int _indexMov;
        private int _indexIndexMov;
        private BitmapSource[] _avatarList;

        public PlayerViewModel(PlayerModel model)
        {
            PlayerInfoCommand = new RelayCommand((o) => PlayerMenu(0), (o) => CanExecute());
            PlayerSkillsCommand = new RelayCommand((o) => PlayerMenu(1), (o) => CanExecute());
            PlayerItemsCommand = new RelayCommand((o) => PlayerMenu(2), (o) => CanExecute());
            PlayerWeavesCommand = new RelayCommand((o) => PlayerMenu(3), (o) => CanExecute());

            Model = model;
            Str = new AttributeViewModel(Model.Str);
            Dex = new AttributeViewModel(Model.Dex);
            Con = new AttributeViewModel(Model.Con);
            Int = new AttributeViewModel(Model.Int);
            Wis = new AttributeViewModel(Model.Wis);
            Cha = new AttributeViewModel(Model.Cha);

            HitPoints = new ValueBarViewModel(100,20, new SolidColorBrush(Colors.Red));
            Energy = new ValueBarViewModel(80, 70, new SolidColorBrush(Colors.DodgerBlue));

            Items = Model.Items.Select(m => new PlayerItemViewModel(m)).ToList();

            _indexIndexMov = 0;
            _indexMov = 0;
            _avatarList = CutImage(Charaset);
            X = Util.CanvasWidth / 2;
            Y = Util.CanvasHeight / 2;
        }

        public ICommand PlayerInfoCommand { get; private set; }
        public ICommand PlayerSkillsCommand { get; private set; }
        public ICommand PlayerItemsCommand { get; private set; }
        public ICommand PlayerWeavesCommand { get; private set; }

        private PlayerModel Model { get; set; }
        public int Id { get { return Model.Id; } }
        public string Name { get { return Model.Name; } }
        //public EnumRace Race { get { return Model.Race; } }
        public RaceModel Race { get { return Model.Race; } }
        public GenderModel Gender { get { return Model.Gender; } }
        public int Nation { get { return Model.Location; } }
        public bool IsChanneler { get { return Model.IsChanneler; } }

        public string Faceset { get { return Path.Combine(Util.SavedPlayerPath, string.Format(@"{0}\faceset.png", Id)); } }
        public string Charaset { get { return Path.Combine(Util.SavedPlayerPath, string.Format(@"{0}\charaset.png", Id)); } }
        public BitmapSource Avatar { get { return _avatarList[_indexMov]; } } 

        public AttributeViewModel Str { get; private set; }
        public AttributeViewModel Dex { get; private set; }
        public AttributeViewModel Con { get; private set; }
        public AttributeViewModel Int { get; private set; }
        public AttributeViewModel Wis { get; private set; }
        public AttributeViewModel Cha { get; private set; }

        public ValueBarViewModel HitPoints { get; private set; }
        public ValueBarViewModel Energy { get; private set; }

        public List<PlayerItemViewModel> Items { get; private set; }

        private int _x;

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        private int _y;

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        public Rect Collider2D { get { return new Rect(new Point(X, Y + 48), new Size(48, 6));} }
        private void PlayerMenu(int page)
        {
            PlayerInfoView view = new PlayerInfoView(page, this);
            view.ShowDialog();
        }

        private BitmapSource[] CutImage(string img)
        {
            int count = 0;
            var objImg = new BitmapSource[12];
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(img, UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();

            for (int i = 0; i < 4; i++)
            for (int j = 0; j < 3; j++)
                objImg[count++] = new CroppedBitmap(src, new Int32Rect(j * 48, i * 48, 48, 48));
            return objImg;
        }

        public void Move(EnumMove move)
        {
            int offsetX = 0;
            int offsetY = 0;

            _indexIndexMov++;
            if (_indexIndexMov == 5)
            {

                _indexIndexMov = 0;
                switch (move)
                {
                    case EnumMove.Up:
                        //offsetY = -_offsetMovRange;
                        if (_indexMov > 8 && _indexMov < 12)
                        {
                            _indexMov++;
                            if (_indexMov > 11)
                                _indexMov = 9;
                        }
                        else
                        {
                            _indexMov = 10;
                        }

                        break;
                    case EnumMove.Down:
                        //offsetY = _offsetMovRange;
                        if (_indexMov < 3)
                        {
                            _indexMov++;
                            if (_indexMov > 2)
                                _indexMov = 0;
                        }
                        else
                        {
                            _indexMov = 1;
                        }

                        break;
                    case EnumMove.Left:
                        //offsetX = -_offsetMovRange;
                        if (_indexMov > 2 && _indexMov < 6)
                        {
                            _indexMov++;
                            if (_indexMov > 5)
                                _indexMov = 3;
                        }
                        else
                        {
                            _indexMov = 4;
                        }

                        break;
                    case EnumMove.Right:
                        //offsetX = _offsetMovRange;
                        if (_indexMov > 5 && _indexMov < 9)
                        {
                            _indexMov++;
                            if (_indexMov > 8)
                                _indexMov = 6;
                        }
                        else
                        {
                            _indexMov = 7;
                        }

                        break;
                }
            }

            X = X + offsetX;
            Y = Y + offsetY;
            OnPropertyChanged("Avatar");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KernelDLL.Common;
using WoT_Win.Common.Commands;
using WoT_Win.Common.Services;
using WoT_Win.Common.ViewModels;
using WoT_Win.Common.Views;

namespace WoT_Win.Creation.ViewModels
{
    public sealed class CreationViewModel : BaseViewModel
    {
        private CreateFactory _createFactory;

        public CreationViewModel(DataManager dataManager, CreateFactory createFactory) : base(dataManager)
        {
            _createFactory = createFactory;

            NextCommand = new RelayCommand((o) => ChangeSection(true), (o) => CanChangeSection(true));
            PrevCommand = new RelayCommand((o) => ChangeSection(false), (o) => CanChangeSection(false));
            CancelCommand = new RelayCommand((o) => Cancel(), (o) => true);
            FinishCommand = new RelayCommand((o) => Finish(), (o) => CanFinish);

            //Items = new ObservableCollection<BaseCreationViewModel>()
            //{
            //    new CreationDataViewModel(),
            //    new CreationAttributesViewModel(),
            //    new CreationSkillsViewModel(_dataManager, _createFactory),
            //    new CreationWeavesViewModel()
            //};

            //using (CanChangeDisposable())
            //{
            //    CurrentCreationSection = Items[0];
            //}         
        }

        public override void Init()
        {
            Items = new ObservableCollection<BaseCreationViewModel>()
            {
                new CreationDataViewModel(),
                new CreationAttributesViewModel(),
                new CreationSkillsViewModel(_dataManager, _createFactory),
                new CreationWeavesViewModel(_dataManager, _createFactory)
            };

            using (CanChangeDisposable())
            {
                CurrentCreationSection = Items[0];
            }
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
    }
}

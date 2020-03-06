using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Game.Actor
{
    public class ValueBarViewModel : BaseViewModel
    {
        public ValueBarViewModel(int max, int current, Brush color)
        {
            MaxValue = max;
            CurrentValue = current;
            Color = color;
        }

        public int MaxValue { get; private set; }
        private int _currentValue;

        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                OnPropertyChanged();
            }
        }

        public Brush Color { get; private set; }

        public string ValueText
        {
            get { return string.Format("{0}/{1}", CurrentValue, MaxValue); }
        }

        public void SetMax(int value)
        {
            MaxValue = value;
            OnPropertyChanged("MaxValue");
        }

        public void EditValue(int range)
        {
            CurrentValue = CurrentValue + range;
        }
    }
}

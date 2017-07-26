using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.Pages.Statistic.MainElements;

namespace FamilyAsset.Pages.Statistic
{
    class StatisticViewModel : UserControlViewModelBase
    {
        private YMDSwitcherViewModel _ymdSwitcher;

        public YMDSwitcherViewModel YMDSwitcher
        {
            get { return _ymdSwitcher; }
            set
            {
                _ymdSwitcher = value;
                RaisePropertyChanged("YMDSwitcher");
            }
        }



        private YMDSwitcher _currentYMD;

        public StatisticViewModel()
        {
            Vis = System.Windows.Visibility.Hidden;

            YMDSwitcher = new YMDSwitcherViewModel();
            YMDSwitcher.YMDSwitcherEvent += OnYMDSwitcher;
        }

        private void OnYMDSwitcher(object sender, YMDSwitcherEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

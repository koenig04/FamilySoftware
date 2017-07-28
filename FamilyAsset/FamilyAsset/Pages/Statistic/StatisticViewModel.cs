using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.Pages.Statistic.NaviBar;

namespace FamilyAsset.Pages.Statistic
{
    class StatisticViewModel : UserControlViewModelBase
    {
        private NaviBarViewModel _naviBar;

        public NaviBarViewModel NaviBar
        {
            get { return _naviBar; }
            set
            {
                _naviBar = value;
                RaisePropertyChanged("NaviBar");
            }
        }


    }
}

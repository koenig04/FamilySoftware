using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.NaviBar
{
    class NaviBarViewModel : NotificationObject
    {
        public event EventHandler<NaviBarInfo> NaviBarInfoChanged;
        private NaviBarInfo _naviBarInfo = new NaviBarInfo(DateTime.Now.Date, DateTime.Now.Date);

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

        private StartEndDatePickerViewModel _startEndDatePicker;

        public StartEndDatePickerViewModel StartEndDatePicker
        {
            get { return _startEndDatePicker; }
            set
            {
                _startEndDatePicker = value;
                RaisePropertyChanged("StartEndDatePicker");
            }
        }

        private StatisticTypeSwitcherViewModel _statisticTypeSwitcher;

        public StatisticTypeSwitcherViewModel StatisticTypeSwitcher
        {
            get { return _statisticTypeSwitcher; }
            set
            {
                _statisticTypeSwitcher = value;
                RaisePropertyChanged("StatisticTypeSwitcher");
            }
        }

        public NaviBarViewModel()
        {
            YMDSwitcher = new YMDSwitcherViewModel();
            YMDSwitcher.YMDSwitcherEvent += OnYMDChanged;
            StartEndDatePicker = new StartEndDatePickerViewModel();
            StartEndDatePicker.PickedDateChanged += OnPickedDateChanged;
            StatisticTypeSwitcher = new StatisticTypeSwitcherViewModel();
            StatisticTypeSwitcher.StatisticTypeChangedEvent += OnStatisticTypeChanged;
        }

        private void OnStatisticTypeChanged(object sender, StatisticTypeChangedEvnetArgs e)
        {
            _naviBarInfo.CurrentStatisticType = e.SelectedStatisticType;
            RaiseNaviBarInfoChanged(_naviBarInfo);
        }

        private void OnPickedDateChanged(object sender, PickedDateChangedEventArgs e)
        {
            if (e.DateType == PickedDateType.StartDate)
                _naviBarInfo.CurrentStartDate = e.PickedDate;
            else
                _naviBarInfo.CurrentEndDate = e.PickedDate;
            RaiseNaviBarInfoChanged(_naviBarInfo);
        }

        private void OnYMDChanged(object sender, YMDSwitcherEventArgs e)
        {
            _naviBarInfo.CurrentYMD = e.YMDInfo;
            RaiseNaviBarInfoChanged(_naviBarInfo);
        }

        private void RaiseNaviBarInfoChanged(NaviBarInfo info)
        {
            if (NaviBarInfoChanged != null)
            {
                NaviBarInfoChanged(null, info);
            }
        }
    }

    class NaviBarInfo
    {
        public YMDSwitcher CurrentYMD { get; set; }
        public DateTime CurrentStartDate { get; set; }
        public DateTime CurrentEndDate { get; set; }
        public StatisticType CurrentStatisticType { get; set; }

        public NaviBarInfo(DateTime currentStartDate, DateTime currentEndDate,
            YMDSwitcher currentYMD = YMDSwitcher.Day, StatisticType currentStatisticType = StatisticType.Volumn)
        {
            CurrentYMD = currentYMD;
            CurrentStartDate = currentStartDate;
            CurrentEndDate = currentEndDate;
            CurrentStatisticType = currentStatisticType;
        }
    }
}

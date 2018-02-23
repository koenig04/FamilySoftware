using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.StatisticProcess;
using Common;
using FamilyAsset.Pages.Statistic.StatisticItems;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.NaviBar
{
    class NaviBarViewModel : NotificationObject
    {
        /// <summary>
        /// true:Curve; false:Pie
        /// </summary>
        public event EventHandler<BoolenEventArgs> StatisticTypeChanged;

        private NaviBarInfo _naviBarInfo = new NaviBarInfo( DateTime.Now.AddMonths(-3).Date,DateTime.Now.Date);

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

        private StatisticItemSelecterViewModel _itemSelecter;

        public StatisticItemSelecterViewModel ItemSelecter
        {
            get { return _itemSelecter; }
            set
            {
                _itemSelecter = value;
                RaisePropertyChanged("ItemSelecter");
            }
        }

        private DelegateCommand _selectItem;

        public DelegateCommand SelectItem
        {
            get
            {
                if (_selectItem == null)
                {
                    _selectItem = new DelegateCommand(o =>
                        {
                            if (ItemSelecter.Vis == System.Windows.Visibility.Visible)
                                ItemSelecter.Vis = System.Windows.Visibility.Collapsed;
                            else
                                ItemSelecter.Vis = System.Windows.Visibility.Visible;
                        });
                }
                return _selectItem;
            }
            set
            {
                _selectItem = value;
                RaisePropertyChanged("SelectItem");
            }
        }

        private DelegateCommand _search;

        public DelegateCommand Search
        {
            get
            {
                if (_search == null)
                {
                    _search = new DelegateCommand(
                        o =>
                        {
                            _statisticProcess.SearchDiagramData(_naviBarInfo);
                            RaiseStaticTypeChanged(_naviBarInfo.CurrentType == StatisticType.Curve ? true : false);
                        });
                }
                return _search;
            }
            set
            {
                _search = value;
                RaisePropertyChanged("Search");
            }
        }


        private IStatiticProcess _statisticProcess;

        public NaviBarViewModel(IStatiticProcess statisticProcess)
        {
            YMDSwitcher = new YMDSwitcherViewModel();
            YMDSwitcher.YMDSwitcherEvent += OnYMDChanged;
            StartEndDatePicker = new StartEndDatePickerViewModel();
            StartEndDatePicker.PickedDateChanged += OnPickedDateChanged;
            StatisticTypeSwitcher = new StatisticTypeSwitcherViewModel();
            StatisticTypeSwitcher.StatisticTypeChangedEvent += OnStatisticTypeChanged;
            ItemSelecter = new StatisticItemSelecterViewModel(statisticProcess);

            _statisticProcess = statisticProcess;
        }

        /// <summary>
        /// when statistic type is changed
        /// we store the changed information and in the meanwhile raise the statistic type changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStatisticTypeChanged(object sender, StatisticTypeChangedEvnetArgs e)
        {
            this._naviBarInfo.CurrentType = e.SelectedStatisticType;
            RaiseStaticTypeChanged(e.SelectedStatisticType == StatisticType.Curve ? true : false);
        }

        /// <summary>
        /// when start/end date is changed
        /// the changed info will be stored in naviBarInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickedDateChanged(object sender, PickedDateChangedEventArgs e)
        {
            if (e.DateType == PickedDateType.StartDate)
                _naviBarInfo.CurrentStartDate = e.PickedDate;
            else
                _naviBarInfo.CurrentEndDate = e.PickedDate;

        }

        /// <summary>
        /// when user change the time interval(year/month/day)
        /// the changed infomation will be stored in naviBarInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnYMDChanged(object sender, YMDSwitcherEventArgs e)
        {
            _naviBarInfo.CurrentYMD = e.YMDInfo;
        }

        private void RaiseStaticTypeChanged(bool isCurve)
        {
            if (StatisticTypeChanged != null)
            {
                StatisticTypeChanged(null, new BoolenEventArgs(isCurve));
            }
        }
    }

    class NaviBarInfo
    {
        public YMDSwitcher CurrentYMD { get; set; }
        public DateTime CurrentStartDate { get; set; }
        public DateTime CurrentEndDate { get; set; }
        public StatisticType CurrentType { get; set; }

        public NaviBarInfo(DateTime currentStartDate, DateTime currentEndDate, YMDSwitcher currentYMD = YMDSwitcher.Day,
            StatisticType currentType = StatisticType.Curve)
        {
            CurrentYMD = currentYMD;
            CurrentStartDate = currentStartDate;
            CurrentEndDate = currentEndDate;
            CurrentType = currentType;
        }

        public static implicit operator StaticSearchInfo(NaviBarInfo info)
        {
            StaticSearchInfo res = new StaticSearchInfo();
            res.StartDate = info.CurrentStartDate;
            res.EndDate = info.CurrentEndDate;
            res.TimeIntervalType = (StatisticIntervalType)info.CurrentYMD;
            return res;
        }
    }
}

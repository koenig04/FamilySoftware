using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.MainElements
{
    class YMDSwitcherViewModel : NotificationObject
    {
        public event EventHandler<YMDSwitcherEventArgs> YMDSwitcherEvent;

        private bool _isYearChecked;

        public bool IsYearChecked
        {
            get { return _isYearChecked; }
            set
            {
                _isYearChecked = value;
                RaisePropertyChanged("IsYearChecked");
            }
        }

        private bool _isMonthChecked;

        public bool IsMonthChecked
        {
            get { return _isMonthChecked; }
            set
            {
                _isMonthChecked = value;
                RaisePropertyChanged("IsMonthChecked");
            }
        }

        private bool _isDayChecked;

        public bool IsDayChecked
        {
            get { return _isDayChecked; }
            set
            {
                _isDayChecked = value;
                RaisePropertyChanged("IsDayChecked");
            }
        }

        private DelegateCommand _yearPressed;

        public DelegateCommand YearPressed
        {
            get
            {
                if (_yearPressed == null)
                {
                    _yearPressed = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            PressButton(YMDSwitcher.Year);
                        }));
                }
                return _yearPressed;
            }
            set
            {
                _yearPressed = value;
                RaisePropertyChanged("YearPressed");
            }
        }

        private DelegateCommand _monthPressed;

        public DelegateCommand MonthPressed
        {
            get
            {
                if (_monthPressed == null)
                {
                    _monthPressed = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            PressButton(YMDSwitcher.Month);
                        }));
                }
                return _monthPressed;
            }
            set
            {
                _monthPressed = value;
                RaisePropertyChanged("MonthPressed");
            }
        }

        private DelegateCommand _dayPressed;

        public DelegateCommand DayPressed
        {
            get
            {
                if (_dayPressed == null)
                {
                    _dayPressed = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            PressButton(YMDSwitcher.Day);
                        }));
                }
                return _dayPressed;
            }
            set
            {
                _dayPressed = value;
                RaisePropertyChanged("DayPressed");
            }
        }


        private void PressButton(YMDSwitcher switcher)
        {
            switch (switcher)
            {
                case YMDSwitcher.Year:
                    if (IsYearChecked)
                    {
                        IsMonthChecked = false;
                        IsDayChecked = false;
                        RaiseYMDSwitcherEvent(switcher);
                    }
                    else
                    {
                        IsYearChecked = !IsYearChecked;
                    }
                    break;
                case YMDSwitcher.Month:
                    if (IsMonthChecked)
                    {
                        IsYearChecked = false;
                        IsDayChecked = false;
                        RaiseYMDSwitcherEvent(switcher);
                    }
                    else
                    {
                        IsMonthChecked = !IsMonthChecked;
                    }
                    break;
                case YMDSwitcher.Day:
                    if (IsDayChecked)
                    {
                        IsYearChecked = false;
                        IsMonthChecked = false;
                        RaiseYMDSwitcherEvent(switcher);
                    }
                    else
                    {
                        IsDayChecked = !IsDayChecked;
                    }
                    break;
            }
        }

        private void RaiseYMDSwitcherEvent(YMDSwitcher switcher)
        {
            if (YMDSwitcherEvent != null)
            {
                YMDSwitcherEvent(null, new YMDSwitcherEventArgs() { YMDInfo = switcher });
            }
        }

    }

    enum YMDSwitcher
    {
        Year,
        Month,
        Day
    }

    class YMDSwitcherEventArgs : EventArgs
    {
        public YMDSwitcher YMDInfo { get; set; }
    }
}

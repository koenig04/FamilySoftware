using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.NaviBar
{
    class StartEndDatePickerViewModel : NotificationObject
    {
        public event EventHandler<PickedDateChangedEventArgs> PickedDateChanged;

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }

        private DelegateCommand _startDateChanged;

        public DelegateCommand StartDateChanged
        {
            get
            {
                if (_startDateChanged == null)
                {
                    _startDateChanged = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            RaisePickedDateChanged(PickedDateType.StartDate, StartDate);
                        }));
                }
                return _startDateChanged;
            }
            set
            {
                _startDateChanged = value;
                RaisePropertyChanged("StartDateChanged");
            }
        }

        private DelegateCommand _endDateChanged;

        public DelegateCommand EndDateChanged
        {
            get
            {
                if (_endDateChanged == null)
                {
                    _endDateChanged = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            RaisePickedDateChanged(PickedDateType.EndDate, EndDate);
                        }));
                }
                return _endDateChanged;
            }
            set
            {
                _endDateChanged = value;
                RaisePropertyChanged("EndDateChanged");
            }
        }

        private void RaisePickedDateChanged(PickedDateType dateType, DateTime pickedDate)
        {
            if (PickedDateChanged != null)
            {
                PickedDateChanged(null, new PickedDateChangedEventArgs(dateType, pickedDate));
            }
        }
    }

    enum PickedDateType
    {
        StartDate,
        EndDate
    }

    class PickedDateChangedEventArgs : EventArgs
    {
        public PickedDateType DateType { get; set; }
        public DateTime PickedDate { get; set; }

        public PickedDateChangedEventArgs() { }
        public PickedDateChangedEventArgs(PickedDateType dateType, DateTime pickedDate)
        {
            this.DateType = dateType;
            this.PickedDate = pickedDate;
        }
    }
}

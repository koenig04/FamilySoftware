using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.NaviBar
{
    class StatisticTypeSwitcherViewModel : NotificationObject
    {
        public event EventHandler<StatisticTypeChangedEvnetArgs> StatisticTypeChangedEvent;

        private Dictionary<StatisticType, string> _dicStatisticTypeImg;
        private StatisticType _statisticType;

        private BitmapImage _statisticTypeImg;

        public BitmapImage StatisticTypeImg
        {
            get { return _statisticTypeImg; }
            set
            {
                _statisticTypeImg = value;
                RaisePropertyChanged("StatisticTypeImg");
            }
        }

        private DelegateCommand _statisticTypeChanged;

        public DelegateCommand StatisticTypeChanged
        {
            get
            {
                if (_statisticTypeChanged == null)
                {
                    _statisticTypeChanged = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            _statisticType = _statisticType == StatisticType.Pie ? StatisticType.Curve : StatisticType.Pie;
                            SwitchStatisticImg(_statisticType);
                            RaiseStatisticTypeChangedEvent(_statisticType);
                        }));
                }
                return _statisticTypeChanged;
            }
            set { _statisticTypeChanged = value; }
        }

        public StatisticTypeSwitcherViewModel()
        {
            _dicStatisticTypeImg = new Dictionary<StatisticType, string>();
            _dicStatisticTypeImg.Add(StatisticType.Curve, "pie.png");
            _dicStatisticTypeImg.Add(StatisticType.Pie, "curve.png");

            _statisticType = StatisticType.Curve;
            SwitchStatisticImg(_statisticType);
        }

        private void SwitchStatisticImg(StatisticType statisticType)
        {
            this.StatisticTypeImg = new BitmapImage(new Uri(Common.GlobalVariables.systemIconPath.Replace("\\", "/") + _dicStatisticTypeImg[statisticType], UriKind.RelativeOrAbsolute));
        }

        private void RaiseStatisticTypeChangedEvent(StatisticType statisticType)
        {
            if (StatisticTypeChangedEvent != null)
            {
                StatisticTypeChangedEvent(null, new StatisticTypeChangedEvnetArgs() { SelectedStatisticType = statisticType });
            }
        }

    }

    enum StatisticType
    {
        Curve,
        Pie
    }

    class StatisticTypeChangedEvnetArgs : EventArgs
    {
        public StatisticType SelectedStatisticType { get; set; }
    }
}

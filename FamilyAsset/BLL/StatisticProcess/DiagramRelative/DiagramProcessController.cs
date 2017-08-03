using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StatisticProcess.DiagramRelative
{
    class DiagramProcessController
    {
        public event EventHandler<StatisticByTime> StatisticByTimeCallbackEvent;
        public event EventHandler<StatisticBySort> StatisticBySortCallbackEvent;

        private DiagramProcessByTime _timeProcess;
        private DiagramProcessBySort _sortProcess;

        public DiagramProcessController()
        {
            _timeProcess = new DiagramProcessByTime();
            _sortProcess = new DiagramProcessBySort();

            _timeProcess.StatisticByTimeCallbackEvent += OnStatisticByTimeCallbackEvent;
            _sortProcess.StatisticBySortCallbackEvent += OnStatisticBySortCallbackEvent;
        }

        private void OnStatisticByTimeCallbackEvent(object sender, StatisticByTime e)
        {
            if (StatisticByTimeCallbackEvent != null)
            {
                StatisticByTimeCallbackEvent(sender, e);
            }
        }

        private void OnStatisticBySortCallbackEvent(object sender, StatisticBySort e)
        {
            if (StatisticBySortCallbackEvent != null)
            {
                StatisticBySortCallbackEvent(sender, e);
            }
        }

        public void GetStatisticInfo(StaticSearchInfo info)
        {
            switch (info.SearchType)
            {
                case StaticType.Time:
                    _timeProcess.GetStatisticInfo(info);
                    break;
                case StaticType.Sort:
                    _sortProcess.GetStatisticInfo(info);
                    break;
            }
        }
    }
}

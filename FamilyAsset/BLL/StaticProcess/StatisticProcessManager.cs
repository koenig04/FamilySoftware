using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StaticProcess
{
    public class StatisticProcessManager : IStatiticProcess
    {
        public event EventHandler<StatisticByTime> StatisticByTimeCallbackEvent;
        public event EventHandler<StatisticBySort> StatisticBySortCallbackEvent;

        private StatisticProcessBase _timeProcess;
        private StatisticProcessBase _sortProcess;

        public StatisticProcessManager()
        {
            _timeProcess = new StatisticProcessByTime();
            _sortProcess = new StatisticProcessBySort();

            _timeProcess.StatisticByTimeCallbackEvent += OnStatisticByTimeCallbackEvent;
            _sortProcess.StatisticBySortCallbackEvent += OnStatisticBySortCallbackEvent;
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
    }
}

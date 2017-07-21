using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StaticProcess
{
    interface IStatiticProcess
    {
        void GetStatisticInfo(StaticSearchInfo info);
        event EventHandler<StatisticProcessByTime> StatisticByTimeCallbackEvent;
        event EventHandler<StatisticBySort> StatisticBySortCallbackEvent;
    }
}

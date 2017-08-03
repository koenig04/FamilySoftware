using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL.StatisticProcess.DiagramRelative
{
    class DiagramProcessByTime : DiagramProcessBase
    {
        public override void GetStatisticInfo(StaticSearchInfo info)
        {
            List<StatisticByTimeListItem> lstStatistic = new List<StatisticByTimeListItem>();
            foreach (Model.TimeStatistic item in _dalStatistic.GetStatisticByTime((int)info.TimeIntervalType, info.StartDate, info.EndDate, info.IsIncome ? 1 : 0))
            {
                lstStatistic.Add(item);
            }

            List<AccountInfo> lstAccount = GetAccountInfoList(info);
            RaiseStatisticByTimeCallbackEvent(new StatisticByTime()
            {
                AccountCollection = lstAccount,
                IntervalType = info.TimeIntervalType,
                StatisticInfoCollection = lstStatistic
            });
        }
    }
}

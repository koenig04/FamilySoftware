using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL.StaticProcess
{
    class StatisticProcessBySort : StatisticProcessBase
    {
        public override void GetStatisticInfo(StaticSearchInfo info)
        {
            List<StatisticBySortListItem> lstStatistic = new List<StatisticBySortListItem>();
            foreach (Model.SortStatistic item in _dalStatistic.GetStatisticBySort(info.ItemOneID, info.StartDate, info.EndDate, info.IsIncome ? 1 : 0))
            {
                lstStatistic.Add(item);
            }

            List<AccountInfo> lstAccount = GetAccountInfoList(info);
            RaiseStatisticBySortCallbackEvent(new StatisticBySort()
            {
                AccountCollection = lstAccount,
                StaticInfoCollection = lstStatistic
            });
        }
    }
}

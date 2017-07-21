using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL.StaticProcess
{
    abstract class StatisticProcessBase
    {
        public abstract void GetStatisticInfo(StaticSearchInfo info);
        public event EventHandler<StatisticByTime> StatisticByTimeCallbackEvent;
        public event EventHandler<StatisticBySort> StatisticBySortCallbackEvent;

        private DAL.AccountInfo _dalAccount;
        protected DAL.Statistic _dalStatistic;

        public StatisticProcessBase()
        {
            _dalAccount = new DAL.AccountInfo();
            _dalStatistic = new DAL.Statistic();
        }

        protected List<AccountInfo> GetAccountInfoList(StaticSearchInfo info)
        {
            return _dalAccount.GetList(info.ItemOneID, info.ItemTwoID, info.StartDate, info.EndDate, info.IsIncome ? 1 : 0);
        }

        protected void RaiseStatisticByTimeCallbackEvent(StatisticByTime e)
        {
            if (StatisticByTimeCallbackEvent != null)
            {
                StatisticByTimeCallbackEvent(null, e);
            }
        }

        protected void RaiseStatisticBySortCallbackEvent(StatisticBySort e)
        {
            if (StatisticBySortCallbackEvent != null)
            {
                StatisticBySortCallbackEvent(null, e);
            }
        }
    }
}

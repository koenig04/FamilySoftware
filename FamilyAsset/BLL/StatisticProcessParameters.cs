using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL
{
    public class StaticSearchInfo
    {
        public StaticType SearchType { get; set; }
        public StatisticIntervalType TimeIntervalType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsIncome { get; set; }
        public string ItemOneID { get; set; }
        public string ItemTwoID { get; set; }
    }

    public class StatisticByTimeCallbackEventArgs : EventArgs
    {
        public StatisticByTime CallbackInfo { get; set; }
    }

    public class StatisticBySortCallbackEventArgs : EventArgs
    {
        public StatisticBySort CallbackInfo { get; set; }
    }

    public enum StatisticIntervalType
    {
        Day,
        Month,
        Year
    }

    public enum StaticType
    {
        Time,
        Sort
    }

    public class StatisticByTimeListItem
    {
        public string ItemDate { get; set; }
        public decimal ItemAmount { get; set; }

        public static implicit operator StatisticByTimeListItem(Model.TimeStatistic info)
        {
            StatisticByTimeListItem model = new StatisticByTimeListItem()
            {
                ItemAmount = info.StatisticAmount,
                ItemDate = info.StatisticTime
            };
            return model;
        }       
    }

    public class StatisticByTime
    {
        public StatisticIntervalType IntervalType { get; set; }
        public List<StatisticByTimeListItem> StatisticInfoCollection { get; set; }
        public List<AccountInfo> AccountCollection { get; set; }
    }

    public class StatisticBySortListItem
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemAmount { get; set; }

        public static implicit operator StatisticBySortListItem(Model.SortStatistic info)
        {
            StatisticBySortListItem model = new StatisticBySortListItem()
            {
                ItemAmount = info.SortAmount,
                ItemID = info.SortID,
                ItemName = info.SortName
            };
            return model;
        }
    }

    public class StatisticBySort
    {
        public List<StatisticBySortListItem> StaticInfoCollection { get; set; }
        public List<AccountInfo> AccountCollection { get; set; }
    }
}

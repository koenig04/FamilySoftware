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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ItemOneID { get; set; }
        public string ItemTwoID { get; set; }
    }

    public class StatisticSearchedArgs : EventArgs
    {

    }

    public enum StaticIntervalType
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

    public class AccountShowInfo : AccountInfo
    {
        public string ItemOneName { get; set; }
        public string ItemTwoName { get; set; }
    }

    public class StaticByTimeListItem
    {
        public DateTime ItemDate { get; set; }
        public decimal ItemAmount { get; set; }
    }

    public class StaticByTime
    {
        public StaticIntervalType IntervalType { get; set; }
        public List<StaticByTimeListItem> StaticInfoCollection { get; set; }
        public List<AccountShowInfo> AccountCollection { get; set; }
    }

    public class StaticBySortListItem
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemAmount { get; set; }
    }

    public class StaticBySort
    {
        public List<StaticBySortListItem> StaticInfoCollection { get; set; }
        public List<AccountShowInfo> AccountCollection { get; set; }
    }
}

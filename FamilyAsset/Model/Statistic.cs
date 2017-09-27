using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SingleCurveData
    {
        public string CurveDate { get; set; }
        public decimal CurveAmount { get; set; }
    }

    public class SinglePieData
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string Icon { get; set; }
        public bool IsIncome { get; set; }
        public decimal SumAmount { get; set; }
    }    

    public class AccountDetail
    {
        public string AccountID { get; set; }
        public DateTime AccountDate { get; set; }
        public string ItemOneID { get; set; }
        public string JZItemOneName { get; set; }
        public string ItemTwoID { get; set; }
        public string JZItemTwoName { get; set; }
        public decimal AccountAmount { get; set; }
        public bool IncomeOrCost { get; set; }
        public string IconName { get; set; }
    }

    //public class StaticSearchInfo
    //{
    //    public StatisticIntervalType TimeIntervalType { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public bool IsIncome { get; set; }
    //    public string ItemOneIDs { get; set; }
    //    public string ItemTwoIDs { get; set; }
    //}

    //public enum StatisticIntervalType
    //{
    //    Day,
    //    Month,
    //    Year
    //}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace BLL.StatisticProcess.DiagramRelative
{
    public class AccountDetail
    {
        public string AccountID { get; set; }
        public DateTime AccountDate { get; set; }
        public string ItemOneID { get; set; }
        public string ItemOneName { get; set; }
        public string ItemTwoID { get; set; }
        public string ItemTwoName { get; set; }
        public decimal AccountAmount { get; set; }
        public bool IsIncome { get; set; }
        public string IconURL { get; set; }

        public static implicit operator AccountDetail(Model.AccountDetail model)
        {
            AccountDetail res = new AccountDetail();
            res.AccountAmount = model.AccountAmount;
            res.AccountDate = model.AccountDate;
            res.AccountID = model.AccountID;
            res.IconURL = model.IconName;
            res.IsIncome = model.IncomeOrCost;
            res.ItemOneID = model.ItemOneID;
            res.ItemOneName = model.JZItemOneName;
            res.ItemTwoID = model.ItemTwoID;
            res.ItemTwoName = model.JZItemTwoName;
            return res;
        }
    }

    public class AccountDetailByDate
    {
        public DateTime AccountDate { get; set; }
        public List<AccountDetail> AccountDetailCollection { get; set; }

        public static implicit operator AccountDetailByDate(List<AccountDetail> inputList)
        {
            if (inputList.Count > 0)
            {
                return new AccountDetailByDate()
                {
                    AccountDate = inputList[0].AccountDate,
                    AccountDetailCollection = inputList
                };
            }
            else
            {
                return null;
            }
        }
    }

    public class AccountDetailBySort
    {
        public string ItemName { get; set; }
        public string IconURL { get; set; }
        public List<AccountDetail> AccountDetailCollection { get; set; }

        public static AccountDetailBySort ConvertFromAccountDetails(List<AccountDetail> inputList, ItemType sortType)
        {
            AccountDetailBySort result = null;
            if (inputList.Count > 0)
            {
                result = new AccountDetailBySort();
                result.ItemName = sortType == ItemType.ItemOne ? inputList[0].ItemOneName : inputList[0].ItemTwoName;
                result.IconURL = inputList[0].IconURL;
                result.AccountDetailCollection = inputList;
            }
            return result;
        }
    }

    public class CurveDataDetail
    {
        public string TimeIntervalTitle { get; set; }
        public decimal SumAmount { get; set; }

        public static implicit operator CurveDataDetail(SingleCurveData model)
        {
            return new CurveDataDetail()
            {
                TimeIntervalTitle = model.CurveDate,
                SumAmount = model.CurveAmount
            };
        }
    }

    public class CurveDataDetailSet
    {
        public string ItemName { get; set; }
        public List<CurveDataDetail> CurveDataDetailCollection { get; set; }
    }

    public class CurveData : EventArgs
    {
        public List<CurveDataDetailSet> CurveDataDetailCollectioion { get; set; }
        public List<AccountDetailByDate> Details { get; set; }
    }

    public class PieDataDetail
    {
        public string ItemName { get; set; }
        public decimal SumAmount { get; set; }

        public static implicit operator PieDataDetail(SinglePieData model)
        {
            return new PieDataDetail()
            {
                ItemName = model.ItemName,
                SumAmount = model.SumAmount
            };
        }
    }

    public class PieData : EventArgs
    {
        public List<PieDataDetail> PieDataDetailCollection { get; set; }
        public List<AccountDetailBySort> Details { get; set; }
    }
}

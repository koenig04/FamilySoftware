using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBUtility;
using Model;

namespace DAL
{
    public class Statistic
    {
        public List<SingleCurveData> GetSignalCurveData(DateTime startDate, DateTime endDate, int statisticMode,
            int inOrOutFlag, string itemOneID, string itemTwoID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@StartDate", SqlDbType.Date),
                    new SqlParameter("@EndDate", SqlDbType.Date),
                    new SqlParameter("@StatisticMode", SqlDbType.Int),
                    new SqlParameter("@InOrOutFlag", SqlDbType.Int),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;
            parameters[2].Value = statisticMode;
            parameters[3].Value = inOrOutFlag;
            parameters[4].Value = itemOneID;
            parameters[5].Value = itemTwoID;

            DataSet ds = DbHelperSQL.RunProcedure("Get_Curve_Data_LK", parameters, "");
            if (ds != null && ds.Tables.Count > 0)
            {
                return (from d in ds.Tables[0].AsEnumerable()
                        select new SingleCurveData()
                        {
                            CurveDate = d.Field<string>("StatisticDate"),
                            CurveAmount = d.Field<decimal>("StatisticAmount")
                        }).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<SinglePieData> GetPieData(DateTime startDate, DateTime endDate, int statisticMode, int inOrOutFlag, string itemOneID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@StartDate", SqlDbType.Date),
                    new SqlParameter("@EndDate", SqlDbType.Date),
                    new SqlParameter("@StatisticMode", SqlDbType.Int),
                    new SqlParameter("@InOrOutFlag", SqlDbType.Int),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;
            parameters[2].Value = statisticMode;
            parameters[3].Value = inOrOutFlag;
            parameters[4].Value = itemOneID;

            DataSet ds = DbHelperSQL.RunProcedure("Get_Next_ItemSum_LK", parameters, "");
            if (ds != null && ds.Tables.Count > 0)
            {
                return (from d in ds.Tables[0].AsEnumerable()
                        select new SinglePieData()
                        {
                            ItemID = d.Field<string>("ItemID"),
                            ItemName = d.Field<string>("ItemName"),
                            Icon = d.Field<string>("Icon"),
                            IsIncome = d.Field<bool>("IsIncome"),
                            SumAmount = d.Field<decimal>("SumAmount")
                        }).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<AccountDetail> GetAccountDetails(DateTime startDate, DateTime endDate, int statisticMode,
            int inOrOutFlag, string itemOneID, string itemTwoID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@StartDate", SqlDbType.Date),
                    new SqlParameter("@EndDate", SqlDbType.Date),
                    new SqlParameter("@StatisticMode", SqlDbType.Int),
                    new SqlParameter("@InOrOutFlag", SqlDbType.Int),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;
            parameters[2].Value = statisticMode;
            parameters[3].Value = inOrOutFlag;
            parameters[4].Value = itemOneID;
            parameters[5].Value = itemTwoID;

            DataSet ds = DbHelperSQL.RunProcedure("Get_AccountDetail_LK", parameters, "");
            if (ds != null && ds.Tables.Count > 0)
            {
                return (from d in ds.Tables[0].AsEnumerable()
                        select new AccountDetail()
                        {
                            AccountID = d.Field<string>("AccountID"),
                            AccountAmount = d.Field<decimal>("AccountAmount"),
                            AccountDate = d.Field<DateTime>("AccountDate"),
                            IconName = d.Field<string>("IconName"),
                            IncomeOrCost = d.Field<bool>("IncomeOrCost"),
                            ItemOneID = d.Field<string>("ItemOneID"),
                            ItemTwoID = d.Field<string>("ItemTwoID"),
                            JZItemOneName = d.Field<string>("JZItemOneName"),
                            JZItemTwoName = d.Field<string>("JZItemTwoName")
                        }).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}

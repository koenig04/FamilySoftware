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
        public List<TimeStatistic> GetStatisticByTime(int timeType, DateTime startDate, DateTime endDate, int isIncome)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@TimeType", SqlDbType.Int),
                    new SqlParameter("@StartDate", SqlDbType.Date),
                    new SqlParameter("@EndDate", SqlDbType.Date),
                    new SqlParameter("@IsIncome", SqlDbType.Bit)
                                        };
            parameters[0].Value = timeType;
            parameters[1].Value = startDate;
            parameters[2].Value = endDate;
            parameters[3].Value = isIncome;

            DataSet ds = DbHelperSQL.RunProcedure("StatisticByTime_LK", parameters, "");
            if (ds != null && ds.Tables.Count > 0)
            {
                return (from d in ds.Tables[0].AsEnumerable()
                        select new TimeStatistic()
                        {
                            StatisticTime = d.Field<string>("StatisticTime"),
                            StatisticAmount = d.Field<decimal>("StatisticAmount")
                        }).ToList<TimeStatistic>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemOneID">when it's null, it means we select all income or all cost</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isIncome"></param>
        /// <returns></returns>
        public List<SortStatistic> GetStatisticBySort(string itemOneID, DateTime startDate, DateTime endDate, int isIncome)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@StartDate", SqlDbType.Date),
                    new SqlParameter("@EndDate", SqlDbType.Date),
                    new SqlParameter("@IsIncome", SqlDbType.Bit)
                                        };
            parameters[0].Value = itemOneID;
            parameters[1].Value = startDate;
            parameters[2].Value = endDate;
            parameters[3].Value = isIncome;

            DataSet ds = DbHelperSQL.RunProcedure("StatisticBySort_LK", parameters, "");
            if (ds != null && ds.Tables.Count > 0)
            {
                return (from d in ds.Tables[0].AsEnumerable()
                        select new SortStatistic()
                        {
                            SortAmount = d.Field<decimal>("SortAmount"),
                            SortID = d.Field<string>("SortID"),
                            SortName = d.Field<string>("SortName"),
                            IsIncome = isIncome == 0 ? false : true,
                            ItemIcon = d.Field<string>("ItemIcon")
                        }).ToList<SortStatistic>();
            }
            else
            {
                return null;
            }
        }
    }
}

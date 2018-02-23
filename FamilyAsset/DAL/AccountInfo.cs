using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBUtility;

namespace DAL
{
    public class AccountInfo
    {
        public bool Add(Model.AccountInfo model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@AccountDate", SqlDbType.Date),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50),
                    new SqlParameter("@AccountAmount",SqlDbType.Decimal,9),
                    new SqlParameter("@Notice",SqlDbType.VarChar,200),
                    new SqlParameter("@Reserve1",SqlDbType.VarChar,200),
                    new SqlParameter("@Reserve2",SqlDbType.VarChar,200),
                    new SqlParameter("@Reserve3",SqlDbType.VarChar,200)
                                        };
            parameters[0].Value = model.AccountDate;
            parameters[1].Value = model.ItemOneID;
            parameters[2].Value = model.ItemTwoID;
            parameters[3].Value = model.AccountAmount;
            parameters[4].Value = model.Notice;
            parameters[5].Value = model.Reserve1;
            parameters[6].Value = model.Reserve2;
            parameters[7].Value = model.Reserve3;

            DbHelperSQL.RunProcedure("AccountInfo_ADD_LK", parameters, out rowsAffected);
            return true;
        }

        public bool Update(Model.AccountInfo model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@AccountID", SqlDbType.VarChar,50),
					new SqlParameter("@AccountDate", SqlDbType.Date),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50),
                    new SqlParameter("@AccountAmount",SqlDbType.Decimal,9),
                    new SqlParameter("@Notice",SqlDbType.VarChar,200),
                    new SqlParameter("@Reserve1",SqlDbType.VarChar,200),
                    new SqlParameter("@Reserve2",SqlDbType.VarChar,200),
                    new SqlParameter("@Reserve3",SqlDbType.VarChar,200)
                                        };
            parameters[0].Value = model.AccountID;
            parameters[1].Value = model.AccountDate;
            parameters[2].Value = model.ItemOneID;
            parameters[3].Value = model.ItemTwoID;
            parameters[4].Value = model.AccountAmount;
            parameters[5].Value = model.Notice;
            parameters[6].Value = model.Reserve1;
            parameters[7].Value = model.Reserve2;
            parameters[8].Value = model.Reserve3;

            DbHelperSQL.RunProcedure("AccountInfo_Update_LK", parameters, out rowsAffected);
            return true;
        }

        public bool Del(Model.AccountInfo model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@AccountID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.AccountID;

            DbHelperSQL.RunProcedure("AccountInfo_Del_LK", parameters, out rowsAffected);

            return true;
        }

        public List<Model.AccountInfo> GetList(string itemOneID, string itemTwoID, DateTime startDate, DateTime endDate, int isIncome)
        {
            SqlParameter[] parameters = {                   
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50),
                    new SqlParameter("@StartDate", SqlDbType.Date),
					new SqlParameter("@EndDate", SqlDbType.Date),
                    new SqlParameter("@IsIncome",SqlDbType.Bit)
                                        };
            parameters[0].Value = itemOneID;
            parameters[1].Value = itemTwoID;
            parameters[2].Value = startDate;
            parameters[3].Value = endDate;
            parameters[4].Value = isIncome;

            DataSet ds = DbHelperSQL.RunProcedure("AccountInfo_GetList_LK", parameters, " ");

            List<Model.AccountInfo> lst = (from d in ds.Tables[0].AsEnumerable()
                                           select new Model.AccountInfo()
                                           {
                                               AccountID = d.Field<string>("AccountID"),
                                               AccountDate = d.Field<DateTime>("AccountDate"),
                                               ItemOneID = d.Field<string>("ItemOneID"),
                                               ItemTwoID = d.Field<string>("ItemTwoID"),
                                               AccountAmount = d.Field<decimal>("AccountAmount"),
                                               Notice = d.Field<string>("Notice"),
                                               Reserve1 = d.Field<string>("Reserve1"),
                                               Reserve2 = d.Field<string>("Reserve2"),
                                               Reserve3 = d.Field<string>("Reserve3"),
                                               ItemOneName = d.Field<string>("ItemOneName"),
                                               ItemTwoName = d.Field<string>("ItemTwoName")
                                           }).ToList<Model.AccountInfo>();
            return lst;
        }

        public Model.AccountInfo GetModel(string accountID)
        {
            SqlParameter[] parameters = {                   
                    new SqlParameter("@AccountID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = accountID;

            DataSet ds = DbHelperSQL.RunProcedure("AccountInfo_GetModel_LK", parameters, " ");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return new Model.AccountInfo()
                {
                    AccountID = ds.Tables[0].Rows[0].Field<string>("AccountID"),
                    AccountDate = ds.Tables[0].Rows[0].Field<DateTime>("AccountDate"),
                    ItemOneID = ds.Tables[0].Rows[0].Field<string>("ItemOneID"),
                    ItemTwoID = ds.Tables[0].Rows[0].Field<string>("ItemTwoID"),
                    AccountAmount = ds.Tables[0].Rows[0].Field<decimal>("AccountAmount"),
                    Notice = ds.Tables[0].Rows[0].Field<string>("Notice"),
                    Reserve1 = ds.Tables[0].Rows[0].Field<string>("Reserve1"),
                    Reserve2 = ds.Tables[0].Rows[0].Field<string>("Reserve2"),
                    Reserve3 = ds.Tables[0].Rows[0].Field<string>("Reserve3"),
                    ItemOneName = ds.Tables[0].Rows[0].Field<string>("ItemOneName"),
                    ItemTwoName = ds.Tables[0].Rows[0].Field<string>("ItemTwoName")
                };
            }
            else
            {
                return null;
            }            
        }
    }
}

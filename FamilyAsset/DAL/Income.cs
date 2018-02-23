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
    public class Income
    {
        public bool Update(Model.Income model, int incomeYear, int incomeMonth)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@IncomeYear", SqlDbType.Int),
					new SqlParameter("@IncomeMonth", SqlDbType.Int),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50),
                    new SqlParameter("@IncomeAmount",SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = incomeYear;
            parameters[1].Value = incomeMonth;
            parameters[2].Value = model.ItemOneID;
            parameters[3].Value = model.ItemTwoID;
            parameters[4].Value = model.IncomeAmount;

            DbHelperSQL.RunProcedure("Income_Update_LK", parameters, out rowsAffected);
            return true;
        }

        public List<Model.Income> GetList(int incomeYear, int incomeMonth, string itemOneID = null, string itemTwoID = null)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@IncomeYear", SqlDbType.Int),
					new SqlParameter("@IncomeMonth", SqlDbType.Int),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = incomeYear;
            parameters[1].Value = incomeMonth;
            parameters[2].Value = itemOneID;
            parameters[3].Value = itemTwoID;

            DataSet ds = DbHelperSQL.RunProcedure("Income_GetList_LK", parameters, " ");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return (from d in ds.Tables[0].AsEnumerable()
                        select new Model.Income()
                        {
                            IncomeID = d.Field<string>("IncomeID"),
                            ItemOneID = d.Field<string>("ItemOneID"),
                            ItemTwoID = d.Field<string>("ItemTwoID"),
                            IncomeAmount = d.Field<decimal>("IncomeAmount")
                        }).ToList<Model.Income>();
            }
            else
            {
                return null;
            }
        }
    }
}

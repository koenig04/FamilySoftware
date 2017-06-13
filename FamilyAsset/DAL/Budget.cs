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
    public class Budget
    {
        public bool Update(Model.Budget model,int budgetYear,int budgetMonth)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@BudgetYear", SqlDbType.Int),
					new SqlParameter("@BudgetMonth", SqlDbType.Int),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50),
                    new SqlParameter("@BudgetAmount",SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = budgetYear;
            parameters[1].Value = budgetMonth;
            parameters[2].Value = model.ItemOneID;
            parameters[3].Value = model.ItemTwoID;
            parameters[4].Value = model.BudgetAmount;

            DbHelperSQL.RunProcedure("Budget_Update_LK", parameters, out rowsAffected);
            return true;
        }

        public List<Model.Budget> GetList(int budgetYear, int budgetMonth)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@BudgetYear", SqlDbType.Int),
					new SqlParameter("@BudgetMonth", SqlDbType.Int)
                                        };
            parameters[0].Value = budgetYear;
            parameters[1].Value = budgetMonth;

            DataSet ds = DbHelperSQL.RunProcedure("Budget_GetList_LK", parameters, "");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return (from d in ds.Tables[0].AsEnumerable()
                        select new Model.Budget()
                        {
                            BudgetID = d.Field<string>("BudgetID"),
                            ItemOneID = d.Field<string>("ItemOneID"),
                            ItemTwoID = d.Field<string>("ItemTwoID"),
                            BudgetAmount = d.Field<decimal>("BudgetAmount")
                        }).ToList<Model.Budget>();
            }
            else
            {
                return null;
            }
        }
    }
}

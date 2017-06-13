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
    }
}

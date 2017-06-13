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
    }
}

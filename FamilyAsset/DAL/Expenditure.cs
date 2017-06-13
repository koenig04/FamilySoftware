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
    public class Expenditure
    {
        public bool Update(Model.Expenditure model, int expenditureYear, int expenditureMonth)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@ExpenditureYear", SqlDbType.Int),
					new SqlParameter("@ExpenditureMonth", SqlDbType.Int),
                    new SqlParameter("@ItemOneID", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemTwoID", SqlDbType.VarChar,50),
                    new SqlParameter("@ExpenditureAmount",SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = expenditureYear;
            parameters[1].Value = expenditureMonth;
            parameters[2].Value = model.ItemOneID;
            parameters[3].Value = model.ItemTwoID;
            parameters[4].Value = model.ExpenditureAmount;

            DbHelperSQL.RunProcedure("Expenditure_Update_LK", parameters, out rowsAffected);
            return true;
        }
    }
}

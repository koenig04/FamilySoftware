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
    public class JZItemOne
    {
        public bool Add(Model.JZItemOne model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ItemOneName", SqlDbType.VarChar,50),
                    new SqlParameter("@IncomeOrCost", SqlDbType.Bit),
                    new SqlParameter("@IconName", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.JZItemOneName;
            parameters[1].Value = model.IncomeOrCost;
            parameters[2].Value = model.IconName;

            DbHelperSQL.RunProcedure("JZItemOne_ADD_LK", parameters, out rowsAffected);

            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public bool Del(string Id)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@JZItemOneID", SqlDbType.VarChar,10)
                                        };
            parameters[0].Value = Id;

            DbHelperSQL.RunProcedure("JZItemOne_DEL_LK", parameters, out rowsAffected);

            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public bool Update(Model.JZItemOne model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@JZItemOneID", SqlDbType.VarChar,10),
                    new SqlParameter("@JZItemOneName", SqlDbType.VarChar,50),
                    new SqlParameter("@IncomeOrCost", SqlDbType.Bit),
                    new SqlParameter("@IconName", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.JZItemOneID;
            parameters[1].Value = model.JZItemOneName;
            parameters[2].Value = model.IncomeOrCost;
            parameters[3].Value = model.IconName;

            DbHelperSQL.RunProcedure("JZItemOne_UPDATE_LK", parameters, out rowsAffected);

            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public DataTable GetList(bool inOrOut)
        {
            DataSet ds;
            SqlParameter[] parameters = {
					new SqlParameter("@InOrOut", SqlDbType.Bit)};
            parameters[0].Value = inOrOut;
            ds = DbHelperSQL.RunProcedure("JZItemOne_GetList_LK", parameters, "ds");
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
    }
}

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
    public class JZItemTwo
    {
        public bool Add(Model.JZItemTwo model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ItemOneID", SqlDbType.VarChar,10),
                    new SqlParameter("@ItemTwoName", SqlDbType.VarChar,50),
                    new SqlParameter("@IconName", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.JZItemOneID;
            parameters[1].Value = model.JZItemTwoID;
            parameters[2].Value = model.IconName;

            DbHelperSQL.RunProcedure("JZItemTwo_ADD", parameters, out rowsAffected);

            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public bool Del(string Id)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@JZItemTwoID", SqlDbType.VarChar,10)
                                        };
            parameters[0].Value = Id;

            DbHelperSQL.RunProcedure("JZItemTwo_DEL_LK", parameters, out rowsAffected);

            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public bool Update(Model.JZItemTwo model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ItemTwoID", SqlDbType.VarChar,10),
                    new SqlParameter("@ItemTwoName", SqlDbType.VarChar,50),
                    new SqlParameter("@IconName", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.JZItemTwoID;
            parameters[1].Value = model.JZItemTwoName;
            parameters[2].Value = model.IconName;

            DbHelperSQL.RunProcedure("JZItemTwo_UPDATE_LK", parameters, out rowsAffected);

            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public DataTable GetList(string Id)
        {
            DataSet ds;
            SqlParameter[] parameters = {
					new SqlParameter("@ItemOneID", SqlDbType.VarChar,10)
                                        };
            parameters[0].Value = Id;

            ds = DbHelperSQL.RunProcedure("JZItemTwo_GetList_LK", parameters, "ds");

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

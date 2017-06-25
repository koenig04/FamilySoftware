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
        public bool Add(Model.JZItemTwo model, out string itemTwoID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ItemOneID", SqlDbType.VarChar,10),
                    new SqlParameter("@ItemTwoName", SqlDbType.VarChar,50),
                    new SqlParameter("@IconName", SqlDbType.VarChar,50),
                    new SqlParameter("@IconNamePressed", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.JZItemOneID;
            parameters[1].Value = model.JZItemTwoName;
            parameters[2].Value = model.IconName;
            parameters[3].Value = model.IconNamePressed;
            parameters[4].Direction = ParameterDirection.Output;

            DbHelperSQL.RunProcedure("JZItemTwo_ADD", parameters, out rowsAffected);
            itemTwoID = parameters[4].Value.ToString();

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
                    new SqlParameter("@IconName", SqlDbType.VarChar,50),
                    new SqlParameter("@IconNamePressed", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.JZItemTwoID;
            parameters[1].Value = model.JZItemTwoName;
            parameters[2].Value = model.IconName;
            parameters[4].Value = model.IconNamePressed;

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

        public Model.JZItemTwo GetModel(string id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@JZItemTwoID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = id;

            DataSet ds = DbHelperSQL.RunProcedure("JZItemTwo_GetModel_LK", parameters, "ds");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return new Model.JZItemTwo()
                {
                    JZItemOneID = ds.Tables[0].Rows[0]["JZItemOneID"].ToString(),
                    JZItemTwoID = ds.Tables[0].Rows[0]["JZItemTwoID"].ToString(),
                    JZItemTwoName = ds.Tables[0].Rows[0]["JZItemTwoName"].ToString(),
                    IconName = ds.Tables[0].Rows[0]["IconName"].ToString(),
                    IconNamePressed = ds.Tables[0].Rows[0]["IconNamePressed"].ToString()
                };
            }
            else
            {
                return null;
            }
        }
    }
}

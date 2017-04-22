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
    public class Phrase
    {
        public bool Add(Model.Phrase model,out string phraseID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@PhraseContent", SqlDbType.VarChar,200),
                    new SqlParameter("@ItemID", SqlDbType.VarChar,10),
                    new SqlParameter("@ID",SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.PhraseContent;
            parameters[1].Value = model.ItemID;
            parameters[2].Direction = ParameterDirection.Output;

            DbHelperSQL.RunProcedure("Phrase_ADD_LK", parameters, out rowsAffected);
            phraseID = parameters[2].Value.ToString();
            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public bool Del(string Id)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@PhraseID", SqlDbType.VarChar,10)
                                        };
            parameters[0].Value = Id;

            DbHelperSQL.RunProcedure("Phrase_DEL_LK", parameters, out rowsAffected);

            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public bool Update(Model.Phrase model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@PhraseID", SqlDbType.VarChar,10),
                    new SqlParameter("@PhraseContent", SqlDbType.VarChar,200),
                    new SqlParameter("@ItemID", SqlDbType.VarChar,10)
                                        };
            parameters[0].Value = model.PhraseID;
            parameters[1].Value = model.PhraseContent;
            parameters[2].Value = model.ItemID;

            DbHelperSQL.RunProcedure("Phrase_Update_LK", parameters, out rowsAffected);

            //return rowsAffected > 0 ? true : false;
            return true;
        }

        public DataTable GetPhrase(string Id)
        {
            DataSet ds;
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.VarChar,10)
                                        };
            parameters[0].Value = Id;

            ds = DbHelperSQL.RunProcedure("Phrase_GetPhrase_LK", parameters, "ds");

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

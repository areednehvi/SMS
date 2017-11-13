using SMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Businness_Layer.Businness
{
    public class LoginManager
    {
        public static Boolean ValidateUser(LoginModel objLogin)
        {
            Boolean isValidUser = false;


            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {

                    new SqlParameter() {ParameterName = "@Username",     SqlDbType = SqlDbType.NVarChar, Value = objLogin.Username},
                    new SqlParameter() {ParameterName = "@Password",  SqlDbType = SqlDbType.NVarChar, Value = objLogin.Password}
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.AuthenticateUser, lstSqlParameters);
                if (objDatable.Rows.Count > 0)
                {
                    isValidUser = true;
                    objLogin.ID =  objDatable.Rows[0]["id_offline"] != DBNull.Value ? objDatable.Rows[0]["id_offline"].ToString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isValidUser;

        }
    }

}
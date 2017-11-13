using SMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace SMS_Businness_Layer.Businness
{
    public class GradesListManager
    {        
        public static ObservableCollection<GradesListModel> GetGradesList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetGradesList, lstSqlParameters);
                return MapDatatableTogradesListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        private static ObservableCollection<GradesListModel> MapDatatableTogradesListObject(DataTable objDatatable)
        {
            ObservableCollection<GradesListModel> objGradesList = new ObservableCollection<GradesListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    GradesListModel obj = new GradesListModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.block = row["block"] != DBNull.Value ? Convert.ToString(row["block"]) : string.Empty;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.order = row["order"] != DBNull.Value ? Convert.ToString(row["order"]) : string.Empty;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : DateTime.MinValue;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : DateTime.MinValue;
                    obj.createdBy = row["createdBy"] != DBNull.Value ? Convert.ToString(row["createdBy"]) : string.Empty;
                    objGradesList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objGradesList;
        }

      
    }
}

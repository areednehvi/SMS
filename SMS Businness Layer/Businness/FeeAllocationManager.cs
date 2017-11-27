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
    public class FeeAllocationManager
    {

        #region List      
        public static ObservableCollection<FeeAllocationListModel> GetFeeAllocationList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetFeeAllocationList, lstSqlParameters);
                return MapDatatableToFeeAllocationListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        public static ObservableCollection<FeeAllocationListModel> MapDatatableToFeeAllocationListObject(DataTable objDatatable)
        {
            ObservableCollection<FeeAllocationListModel> objFeeAllocationList = new ObservableCollection<FeeAllocationListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    FeeAllocationListModel obj = new FeeAllocationListModel();
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.id_online = row["id_online"] != DBNull.Value ? Convert.ToString(row["id_online"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.name = row["name"] != DBNull.Value ? Convert.ToString(row["name"]) : string.Empty;
                    obj.recur = row["recur"] != DBNull.Value ? Convert.ToString(row["recur"]) : string.Empty;
                    obj.is_transport = row["is_transport"] != DBNull.Value ? Convert.ToString(row["is_transport"]) : string.Empty;
                    obj.order = row["order"] != DBNull.Value ? Convert.ToString(row["order"]) : string.Empty;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    obj.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToString(row["CreatedBy"]) : string.Empty;
                    objFeeAllocationList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objFeeAllocationList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiyFeeAllocation(FeeAllocationListModel objFeeAllocation, LoginModel CurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                if (objFeeAllocation.id_offline == null) // New FeeCategory
                {
                    objFeeAllocation.id_offline = Guid.NewGuid().ToString();
                    objFeeAllocation.id_online = Guid.Empty.ToString();
                    objFeeAllocation.created_by = CurrentLogin.User.id_offline;
                    objFeeAllocation.created_on = DateTime.Now;
                    objFeeAllocation.school_id = SchoolInfo.id_offline;
                    objFeeAllocation.order = "0";
                    objFeeAllocation.recur = "0";
                }
                objFeeAllocation.updated_by = CurrentLogin.User.id_offline;
                objFeeAllocation.updated_on = DateTime.Now;
                                                
                DataTable objDatatable = MapFeeAllocationListObjectToDataTable(objFeeAllocation);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.fee_categories;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifyFeeAllocation, objSqlParameter);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;
        }

        private static DataTable MapFeeAllocationListObjectToDataTable(FeeAllocationListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("recur", typeof(string));
                table.Columns.Add("is_transport", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(string));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.name,
                                obj.recur,
                                obj.is_transport,
                                obj.order,
                                obj.created_by,
                                obj.created_on,
                                obj.updated_by,
                                obj.updated_on
                              );
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public static DataTable MapFeeAllocationObjectToDataTable(fee_categoriesModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("recur", typeof(string));
                table.Columns.Add("is_transport", typeof(string));
                table.Columns.Add("order", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(string));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.name,
                                obj.recur,
                                obj.is_transport,
                                obj.order,
                                obj.created_by,
                                obj.created_on,
                                obj.updated_by,
                                obj.updated_on
                              );
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        #endregion

    }
}

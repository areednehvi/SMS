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
                    FeeAllocationListModel obj = new FeeAllocationListModel()
                    {
                        FeeCategory = new fee_categoriesModel()
                    };
                    //fees
                    obj.id_offline = row["fees.id_offline"] != DBNull.Value ? Convert.ToString(row["fees.id_offline"]) : string.Empty;
                    obj.id_online = row["fees.id_online"] != DBNull.Value ? Convert.ToString(row["fees.id_online"]) : string.Empty;
                    obj.school_id = row["fees.school_id"] != DBNull.Value ? Convert.ToString(row["fees.school_id"]) : string.Empty;
                    obj.fee_category_id = row["fees.fee_category_id"] != DBNull.Value ? Convert.ToString(row["fees.fee_category_id"]) : string.Empty;
                    obj.session_id = row["fees.session_id"] != DBNull.Value ? Convert.ToString(row["fees.session_id"]) : string.Empty;
                    obj.amount = row["fees.amount"] != DBNull.Value ? Convert.ToDouble(row["fees.amount"]) : 0;
                    obj.fee_cources = row["fees.fee_cources"] != DBNull.Value ? Convert.ToString(row["fees.fee_cources"]) : string.Empty;
                    obj.last_date = row["fees.last_date"] != DBNull.Value ? Convert.ToString(row["fees.last_date"]) : string.Empty;
                    obj.last_day = row["fees.last_day"] != DBNull.Value ? Convert.ToString(row["fees.last_day"]) : string.Empty;
                    obj.fine_per_day = row["fees.fine_per_day"] != DBNull.Value ? Convert.ToDouble(row["fees.fine_per_day"]) : 0;
                    obj.is_allocated = row["fees.is_allocated"] != DBNull.Value ? Convert.ToString(row["fees.is_allocated"]) : string.Empty;
                    obj.remarks = row["fees.remarks"] != DBNull.Value ? Convert.ToString(row["fees.remarks"]) : string.Empty;
                    obj.created_by = row["fees.created_by"] != DBNull.Value ? Convert.ToString(row["fees.created_by"]) : string.Empty;
                    obj.created_on = row["fees.created_on"] != DBNull.Value ? Convert.ToDateTime(row["fees.created_on"]) : (DateTime?)null;
                    obj.updated_by = row["fees.updated_by"] != DBNull.Value ? Convert.ToString(row["fees.updated_by"]) : string.Empty;
                    obj.updated_on = row["fees.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["fees.updated_on"]) : (DateTime?)null;
                    //fee_categories
                    obj.FeeCategory.id_offline = row["fee_categories.id_offline"] != DBNull.Value ? Convert.ToString(row["fee_categories.id_offline"]) : string.Empty;
                    obj.FeeCategory.id_online = row["fee_categories.id_online"] != DBNull.Value ? Convert.ToString(row["fee_categories.id_online"]) : string.Empty;
                    obj.FeeCategory.school_id = row["fee_categories.school_id"] != DBNull.Value ? Convert.ToString(row["fee_categories.school_id"]) : string.Empty;
                    obj.FeeCategory.name = row["fee_categories.name"] != DBNull.Value ? Convert.ToString(row["fee_categories.name"]) : string.Empty;
                    obj.FeeCategory.recur = row["fee_categories.recur"] != DBNull.Value ? Convert.ToString(row["fee_categories.recur"]) : string.Empty;
                    obj.FeeCategory.is_transport = row["fee_categories.is_transport"] != DBNull.Value ? Convert.ToString(row["fee_categories.is_transport"]) : string.Empty;
                    obj.FeeCategory.order = row["fee_categories.order"] != DBNull.Value ? Convert.ToString(row["fee_categories.order"]) : string.Empty;
                    obj.FeeCategory.created_by = row["fee_categories.created_by"] != DBNull.Value ? Convert.ToString(row["fee_categories.created_by"]) : string.Empty;
                    obj.FeeCategory.created_on = row["fee_categories.created_on"] != DBNull.Value ? Convert.ToDateTime(row["fee_categories.created_on"]) : (DateTime?)null;
                    obj.FeeCategory.updated_by = row["fee_categories.updated_by"] != DBNull.Value ? Convert.ToString(row["fee_categories.updated_by"]) : string.Empty;
                    obj.FeeCategory.updated_on = row["fee_categories.updated_on"] != DBNull.Value ? Convert.ToDateTime(row["fee_categories.updated_on"]) : (DateTime?)null;
                    //grades applied to
                    obj.GradesAppliedTo = row["GradesAppliedTo"] != DBNull.Value ? Convert.ToString(row["GradesAppliedTo"]) : string.Empty;
                    //applied to Grade IDs
                    obj.AppliedToGradeIDs = row["AppliedToGradeIDs"] != DBNull.Value ? Convert.ToString(row["AppliedToGradeIDs"]) : string.Empty;
                    //student count
                    obj.StudentCount = row["StudentCount"] != DBNull.Value ? Convert.ToInt64(row["StudentCount"]) : 0;
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
                table.Columns.Add("fee_category_id", typeof(string));
                table.Columns.Add("session_id", typeof(string));
                table.Columns.Add("amount", typeof(string));
                table.Columns.Add("fee_cources", typeof(string));
                table.Columns.Add("last_date", typeof(string));
                table.Columns.Add("last_day", typeof(string));
                table.Columns.Add("fine_per_day", typeof(string));
                table.Columns.Add("is_allocated", typeof(string));
                table.Columns.Add("remarks", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(string));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(string));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.fee_category_id,
                                obj.session_id,
                                obj.amount,
                                obj.fee_cources,
                                obj.last_date,
                                obj.last_day,
                                obj.fine_per_day,
                                obj.is_allocated,
                                obj.remarks,
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

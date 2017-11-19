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
    public class StudentsManager
    {

        #region List      
        public static ObservableCollection<StudentsListModel> GetStudentsList(Int64 fromRowNo, Int64 toRowNo)
        {            
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {                    
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo}                   
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentsList, lstSqlParameters);
                return MapDatatableToStudentsListObject(objDatable);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }            
            
        }

        private static ObservableCollection<StudentsListModel> MapDatatableToStudentsListObject(DataTable objDatatable)
        {
            ObservableCollection<StudentsListModel> objStudentsList = new ObservableCollection<StudentsListModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    StudentsListModel obj = new StudentsListModel();
                    /*obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.id_online = row["id_online"] != DBNull.Value ? Convert.ToString(row["id_online"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.student_id = row["student_id"] != DBNull.Value ? Convert.ToString(row["student_id"]) : string.Empty;
                    obj.role_id = row["role_id"] != DBNull.Value ? Convert.ToString(row["role_id"]) : string.Empty;
                    obj.Student_type = row["Student_type"] != DBNull.Value ? Convert.ToString(row["Student_type"]) : string.Empty;
                    obj.Studentname = row["Studentname"] != DBNull.Value ? Convert.ToString(row["Studentname"]) : string.Empty;
                    obj.email = row["email"] != DBNull.Value ? Convert.ToString(row["email"]) : string.Empty;
                    obj.phone = row["phone"] != DBNull.Value ? Convert.ToString(row["phone"]) : string.Empty;
                    obj.address_line_one = row["address_line_one"] != DBNull.Value ? Convert.ToString(row["address_line_one"]) : string.Empty;
                    obj.address_line_two = row["address_line_two"] != DBNull.Value ? Convert.ToString(row["address_line_two"]) : string.Empty;
                    obj.area = row["area"] != DBNull.Value ? Convert.ToString(row["area"]) : string.Empty;
                    obj.first_name = row["first_name"] != DBNull.Value ? Convert.ToString(row["first_name"]) : string.Empty;
                    obj.middle_name = row["middle_name"] != DBNull.Value ? Convert.ToString(row["middle_name"]) : string.Empty;
                    obj.last_name = row["last_name"] != DBNull.Value ? Convert.ToString(row["last_name"]) : string.Empty;
                    obj.full_name = row["full_name"] != DBNull.Value ? Convert.ToString(row["full_name"]) : string.Empty;
                    obj.gender = row["gender"] != DBNull.Value ? Convert.ToString(row["gender"]) : string.Empty;
                    obj.blood_group = row["blood_group"] != DBNull.Value ? Convert.ToString(row["blood_group"]) : string.Empty;
                    obj.password = row["password"] != DBNull.Value ? Convert.ToString(row["password"]) : string.Empty;
                    obj.birth_date = row["birth_date"] != DBNull.Value ? Convert.ToDateTime(row["birth_date"]) : DateTime.MinValue;
                    obj.other_phones = row["other_phones"] != DBNull.Value ? Convert.ToString(row["other_phones"]) : string.Empty;
                    obj.default_phone_number_id = row["default_phone_number_id"] != DBNull.Value ? Convert.ToString(row["default_phone_number_id"]) : string.Empty;
                    obj.adhaar_number = row["adhaar_number"] != DBNull.Value ? Convert.ToString(row["adhaar_number"]) : string.Empty;
                    obj.bank_name = row["bank_name"] != DBNull.Value ? Convert.ToString(row["bank_name"]) : string.Empty;
                    obj.bank_branch = row["bank_branch"] != DBNull.Value ? Convert.ToString(row["bank_branch"]) : string.Empty;
                    obj.bank_account_number = row["bank_account_number"] != DBNull.Value ? Convert.ToString(row["bank_account_number"]) : string.Empty;
                    obj.bank_ifsc_code = row["bank_ifsc_code"] != DBNull.Value ? Convert.ToString(row["bank_ifsc_code"]) : string.Empty;
                    obj.flags = row["flags"] != DBNull.Value ? Convert.ToString(row["flags"]) : string.Empty;
                    obj.last_login_time = row["last_login_time"] != DBNull.Value ? Convert.ToDateTime(row["last_login_time"]) : DateTime.MinValue;
                    obj.Student_avatar_file_id = row["Student_avatar_file_id"] != DBNull.Value ? Convert.ToString(row["Student_avatar_file_id"]) : string.Empty;
                    obj.is_active = row["is_active"] != DBNull.Value ? Convert.ToBoolean(row["is_active"]) : false;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : DateTime.MinValue;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : DateTime.MinValue;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;*/
                    objStudentsList.Add(obj);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objStudentsList;
        }

        #endregion

        #region view
        public static Boolean CreateOrModfiyStudents(StudentsListModel objStudents, LoginModel objCurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                objStudents.id_offline = objStudents.id_offline == null ? Guid.NewGuid().ToString() : objStudents.id_offline;
                objStudents.id_online = Guid.Empty.ToString();
                objStudents.created_by = objCurrentLogin.ID;
                objStudents.updated_by = objCurrentLogin.ID;
                objStudents.created_on = DateTime.Now;
                objStudents.updated_on = DateTime.Now;
                objStudents.school_id = SchoolInfo.id_offline;
                DataTable objDatatable = MapStudentsListObjectToDataTable(objStudents);
                SqlParameter objSqlParameter = new SqlParameter("@Model", SqlDbType.Structured);
                objSqlParameter.TypeName = DBTableTypes.students;
                objSqlParameter.Value = objDatatable;
                IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.CreateOrModifyStudents, objSqlParameter);
                
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

        private static DataTable MapStudentsListObjectToDataTable(StudentsListModel obj)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("user_id", typeof(string));
                table.Columns.Add("parent_id", typeof(string));
                table.Columns.Add("grade_id", typeof(string));
                table.Columns.Add("section_id", typeof(string));
                table.Columns.Add("session_id", typeof(string));
                table.Columns.Add("trip_stop_id", typeof(string));
                table.Columns.Add("registration_id", typeof(string));
                table.Columns.Add("roll_number", typeof(string));
                table.Columns.Add("exam_roll_number", typeof(string));
                table.Columns.Add("enrollment_date", typeof(DateTime));
                table.Columns.Add("status", typeof(string));
                table.Columns.Add("dc_number", typeof(string));
                table.Columns.Add("dc_date_of_issue", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                obj.id_offline,
                                obj.id_online,
                                obj.school_id,
                                obj.user_id,
                                obj.parent_id,
                                obj.grade_id,
                                obj.section_id,
                                obj.session_id,
                                obj.trip_stop_id,
                                obj.registration_id,
                                obj.roll_number,
                                obj.exam_roll_number,
                                obj.enrollment_date,
                                obj.status,
                                obj.dc_number,
                                obj.dc_date_of_issue,
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

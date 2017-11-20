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
                    StudentsListModel obj = new StudentsListModel()
                    {
                        User = new usersModel(),
                        Grade = new gradesModel(),
                        Section = new sectionsModel(),
                        Parents = new parentsModel()
                    };
                    //Student
                    obj.id_offline = row["id_offline"] != DBNull.Value ? Convert.ToString(row["id_offline"]) : string.Empty;
                    obj.id_online = row["id_online"] != DBNull.Value ? Convert.ToString(row["id_online"]) : string.Empty;
                    obj.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.user_id = row["user_id"] != DBNull.Value ? Convert.ToString(row["user_id"]) : string.Empty;
                    obj.parent_id = row["parent_id"] != DBNull.Value ? Convert.ToString(row["parent_id"]) : string.Empty;
                    obj.grade_id = row["grade_id"] != DBNull.Value ? Convert.ToString(row["grade_id"]) : string.Empty;
                    obj.section_id = row["section_id"] != DBNull.Value ? Convert.ToString(row["section_id"]) : string.Empty;
                    obj.session_id = row["session_id"] != DBNull.Value ? Convert.ToString(row["session_id"]) : string.Empty;
                    obj.trip_stop_id = row["trip_stop_id"] != DBNull.Value ? Convert.ToString(row["trip_stop_id"]) : string.Empty;
                    obj.registration_id = row["registration_id"] != DBNull.Value ? Convert.ToString(row["registration_id"]) : string.Empty;
                    obj.roll_number = row["roll_number"] != DBNull.Value ? Convert.ToString(row["roll_number"]) : string.Empty;
                    obj.exam_roll_number = row["exam_roll_number"] != DBNull.Value ? Convert.ToString(row["exam_roll_number"]) : string.Empty;
                    obj.enrollment_date = row["enrollment_date"] != DBNull.Value ? Convert.ToDateTime(row["enrollment_date"]) : DateTime.MinValue;
                    obj.status = row["status"] != DBNull.Value ? Convert.ToString(row["status"]) : string.Empty;
                    obj.dc_number = row["dc_number"] != DBNull.Value ? Convert.ToString(row["dc_number"]) : string.Empty;
                    obj.dc_date_of_issue = row["dc_date_of_issue"] != DBNull.Value ? Convert.ToDateTime(row["dc_date_of_issue"]) : DateTime.MinValue;
                    obj.created_by = row["created_by"] != DBNull.Value ? Convert.ToString(row["created_by"]) : string.Empty;
                    obj.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : DateTime.MinValue;
                    obj.updated_by = row["updated_by"] != DBNull.Value ? Convert.ToString(row["updated_by"]) : string.Empty;
                    obj.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : DateTime.MinValue;
                    //User
                    obj.User.id_offline = row["user_id"] != DBNull.Value ? Convert.ToString(row["user_id"]) : string.Empty;
                    obj.User.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.User.student_id = row["student_id"] != DBNull.Value ? Convert.ToString(row["student_id"]) : string.Empty;
                    obj.User.role_id = row["role_id"] != DBNull.Value ? Convert.ToString(row["role_id"]) : string.Empty;
                    obj.User.user_type = row["user_type"] != DBNull.Value ? Convert.ToString(row["user_type"]) : string.Empty;
                    obj.User.username = row["username"] != DBNull.Value ? Convert.ToString(row["username"]) : string.Empty;
                    obj.User.email = row["email"] != DBNull.Value ? Convert.ToString(row["email"]) : string.Empty;
                    obj.User.phone = row["phone"] != DBNull.Value ? Convert.ToString(row["phone"]) : string.Empty;
                    obj.User.address_line_one = row["address_line_one"] != DBNull.Value ? Convert.ToString(row["address_line_one"]) : string.Empty;
                    obj.User.address_line_two = row["address_line_two"] != DBNull.Value ? Convert.ToString(row["address_line_two"]) : string.Empty;
                    obj.User.area = row["area"] != DBNull.Value ? Convert.ToString(row["area"]) : string.Empty;
                    obj.User.first_name = row["first_name"] != DBNull.Value ? Convert.ToString(row["first_name"]) : string.Empty;
                    obj.User.middle_name = row["middle_name"] != DBNull.Value ? Convert.ToString(row["middle_name"]) : string.Empty;
                    obj.User.last_name = row["last_name"] != DBNull.Value ? Convert.ToString(row["last_name"]) : string.Empty;
                    obj.User.full_name = row["full_name"] != DBNull.Value ? Convert.ToString(row["full_name"]) : string.Empty;
                    obj.User.gender = row["gender"] != DBNull.Value ? Convert.ToString(row["gender"]) : string.Empty;
                    obj.User.blood_group = row["blood_group"] != DBNull.Value ? Convert.ToString(row["blood_group"]) : string.Empty;
                    obj.User.password = row["password"] != DBNull.Value ? Convert.ToString(row["password"]) : string.Empty;
                    obj.User.birth_date = row["birth_date"] != DBNull.Value ? Convert.ToDateTime(row["birth_date"]) : DateTime.MinValue;
                    obj.User.other_phones = row["other_phones"] != DBNull.Value ? Convert.ToString(row["other_phones"]) : string.Empty;
                    obj.User.default_phone_number_id = row["default_phone_number_id"] != DBNull.Value ? Convert.ToString(row["default_phone_number_id"]) : string.Empty;
                    obj.User.adhaar_number = row["adhaar_number"] != DBNull.Value ? Convert.ToString(row["adhaar_number"]) : string.Empty;
                    obj.User.bank_name = row["bank_name"] != DBNull.Value ? Convert.ToString(row["bank_name"]) : string.Empty;
                    obj.User.bank_branch = row["bank_branch"] != DBNull.Value ? Convert.ToString(row["bank_branch"]) : string.Empty;
                    obj.User.bank_account_number = row["bank_account_number"] != DBNull.Value ? Convert.ToString(row["bank_account_number"]) : string.Empty;
                    obj.User.bank_ifsc_code = row["bank_ifsc_code"] != DBNull.Value ? Convert.ToString(row["bank_ifsc_code"]) : string.Empty;
                    obj.User.flags = row["flags"] != DBNull.Value ? Convert.ToString(row["flags"]) : string.Empty;
                    obj.User.last_login_time = row["last_login_time"] != DBNull.Value ? Convert.ToDateTime(row["last_login_time"]) : DateTime.MinValue;
                    obj.User.user_avatar_file_id = row["user_avatar_file_id"] != DBNull.Value ? Convert.ToString(row["user_avatar_file_id"]) : string.Empty;
                    obj.User.is_active = row["is_active"] != DBNull.Value ? Convert.ToBoolean(row["is_active"]) : false;
                    //Grade
                    obj.Grade.id_offline = row["grade_id"] != DBNull.Value ? Convert.ToString(row["grade_id"]) : string.Empty;
                    obj.Grade.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.Grade.block = row["block"] != DBNull.Value ? Convert.ToString(row["block"]) : string.Empty;
                    obj.Grade.name = row["grade"] != DBNull.Value ? Convert.ToString(row["grade"]) : string.Empty;
                    //Section
                    obj.Section.id_offline = row["section_id"] != DBNull.Value ? Convert.ToString(row["section_id"]) : string.Empty;
                    obj.Section.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.Section.name = row["section"] != DBNull.Value ? Convert.ToString(row["section"]) : string.Empty;
                    obj.Section.capacity = row["capacity"] != DBNull.Value ? Convert.ToInt16(row["capacity"]) : 0;
                    //Parents
                    obj.Parents.id_offline = row["parent_id"] != DBNull.Value ? Convert.ToString(row["parent_id"]) : string.Empty;
                    obj.Parents.school_id = row["school_id"] != DBNull.Value ? Convert.ToString(row["school_id"]) : string.Empty;
                    obj.Parents.f_first_name = row["f_first_name"] != DBNull.Value ? Convert.ToString(row["f_first_name"]) : string.Empty;
                    obj.Parents.f_middle_name = row["f_middle_name"] != DBNull.Value ? Convert.ToString(row["f_middle_name"]) : string.Empty;
                    obj.Parents.f_last_name = row["f_last_name"] != DBNull.Value ? Convert.ToString(row["f_last_name"]) : string.Empty;
                    obj.Parents.f_full_name = row["f_full_name"] != DBNull.Value ? Convert.ToString(row["f_full_name"]) : string.Empty;
                    obj.Parents.f_mobile = row["f_mobile"] != DBNull.Value ? Convert.ToString(row["f_mobile"]) : string.Empty;
                    obj.Parents.f_phone = row["f_phone"] != DBNull.Value ? Convert.ToString(row["f_phone"]) : string.Empty;
                    obj.Parents.f_office = row["f_office"] != DBNull.Value ? Convert.ToString(row["f_office"]) : string.Empty;
                    obj.Parents.f_email = row["f_email"] != DBNull.Value ? Convert.ToString(row["f_email"]) : string.Empty;
                    obj.Parents.m_first_name = row["m_first_name"] != DBNull.Value ? Convert.ToString(row["m_first_name"]) : string.Empty;
                    obj.Parents.m_middle_name = row["m_middle_name"] != DBNull.Value ? Convert.ToString(row["m_middle_name"]) : string.Empty;
                    obj.Parents.m_last_name = row["m_last_name"] != DBNull.Value ? Convert.ToString(row["m_last_name"]) : string.Empty;
                    obj.Parents.m_full_name = row["m_full_name"] != DBNull.Value ? Convert.ToString(row["m_full_name"]) : string.Empty;
                    obj.Parents.m_mobile = row["m_mobile"] != DBNull.Value ? Convert.ToString(row["m_mobile"]) : string.Empty;
                    obj.Parents.m_phone = row["m_phone"] != DBNull.Value ? Convert.ToString(row["m_phone"]) : string.Empty;
                    obj.Parents.m_office = row["m_office"] != DBNull.Value ? Convert.ToString(row["m_office"]) : string.Empty;
                    obj.Parents.m_email = row["m_email"] != DBNull.Value ? Convert.ToString(row["m_email"]) : string.Empty;
                    obj.Parents.g_fullname = row["g_fullname"] != DBNull.Value ? Convert.ToString(row["g_fullname"]) : string.Empty;
                    obj.Parents.g_mobile = row["g_mobile"] != DBNull.Value ? Convert.ToString(row["g_mobile"]) : string.Empty;
                    obj.Parents.g_email = row["g_email"] != DBNull.Value ? Convert.ToString(row["g_email"]) : string.Empty;


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

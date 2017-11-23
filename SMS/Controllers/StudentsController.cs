﻿using SMS.Models;
using SMS.Shared;
using SMS_Businness_Layer.Businness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SMS_Models.Models.DBModels;

namespace SMS.Controllers
{
    public class StudentsController : INotifyPropertyChanged
    {
        #region Fields
        private StudentsModel _Students;

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _addNewStudentCommand;
        private ICommand _cancelNewStudentCommand;
        private ICommand _saveStudentsCommand;
        #endregion

        #region Constructor
        public StudentsController()
        {

            _Students = new StudentsModel()
            {
                CurrentLogin = new LoginModel(),
                SchoolInfo = new SchoolModel(),              
            };

            //Get Global Objects
            GetGlobalObjects();

            // Get drop down Lists
            this.GetDropDownLists();

            //Get Settings
            this.GetSettings();
            // Set pagination
            this.ResetPagination();

            //Subscribe to Model's Property changed event
            this.Students.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedItemInStudentsList")
                {
                    Students.Student = Students.SelectedItemInStudentsList;
                    if (Students.Student != null)
                    {
                        Students.Student.Section = Students.SectionsList.Find(x => x.id_offline == Students.Student.section_id);
                        Students.Student.Grade = Students.GradesList.Find(x => x.id_offline == Students.Student.grade_id);
                        Students.Student.BloodGroup = Students.BloodGroupList.Find(x => x.id == Students.Student.User.blood_group);
                        Students.Student.Gender = Students.GenderList.Find(x => x.id == Students.Student.User.gender);
                        Students.Student.Status = Students.StatusList.Find(x => x.id == Students.Student.status);
                    }
                    this.ShowForm();
                }
            };



            //Get Initial Students list
            this.GetStudentsList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewStudentCommand = new RelayCommand(AddNewStudent, CanAddNewStudent);
            _cancelNewStudentCommand = new RelayCommand(CancelNewStudent, CanCancelNewStudent);
            _saveStudentsCommand = new RelayCommand(SaveStudents, CanSaveStudents);

            this.ShowList();
        }

        #endregion

        #region Properties

        public StudentsModel Students
        {
            get
            {
                return _Students;
            }
            set
            {
                _Students = value;
                OnPropertyChanged("Students");
            }
        }

        #endregion

        #region NextPageCommand
        public ICommand NextPageCommand
        {
            get { return _nextPageCommand; }
        }


        public bool CanMoveToNextPage(object obj)
        {
            return true;
        }

        public void MoveToNextPage(object obj)
        {
            try
            {
                Students.pageNo++;
                Students.PageNo = "Page No : " + Students.pageNo;
                Students.fromRowNo = Students.toRowNo + 1;
                Students.toRowNo = Students.pageNo * Students.NoOfRecordsPerPage;
                this.GetStudentsList();
                if (Students.pageNo > 1 && Students.StudentsList.Count == 0)
                    MoveToPreviousPage(obj);
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }

        }

        #endregion

        #region PreviousPageCommand

        public ICommand PreviousPageCommand
        {
            get { return _previousPageCommand; }
        }


        public bool CanMoveToPreviousPage(object obj)
        {
            return true;
        }


        public void MoveToPreviousPage(object obj)
        {
            try
            {
                if (Students.pageNo > 1)
                {
                    Students.pageNo--;
                    Students.PageNo = "Page No : " + Students.pageNo;
                    Students.toRowNo = Students.fromRowNo - 1;
                    Students.fromRowNo = (Students.toRowNo + 1) - Students.NoOfRecordsPerPage;
                    this.GetStudentsList();
                }

            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }
        }
        #endregion

        #region AddNewStudentCommand

        public ICommand AddNewStudentCommand
        {
            get { return _addNewStudentCommand; }
        }


        public bool CanAddNewStudent(object obj)
        {
            return true;
        }


        public void AddNewStudent(object obj)
        {
            try
            {
                Students.Student = new StudentsListModel()
                {
                    User = new usersModel()
                    {
                        id_offline = Guid.NewGuid().ToString(),
                        id_online = Guid.Empty.ToString(),
                        created_by = Students.CurrentLogin.ID,
                        updated_by = Students.CurrentLogin.ID,
                        created_on = DateTime.Now,
                        updated_on = DateTime.Now,
                        school_id = Students.SchoolInfo.id_offline,
                        user_type = "student",
                        student_id = Guid.Empty.ToString(),
                        role_id = Guid.Empty.ToString(),
                        user_avatar_file_id = Guid.Empty.ToString(),
                        default_phone_number_id = Guid.Empty.ToString(),
                    },
                    Parents = new parentsModel()
                    {
                        id_offline = Guid.NewGuid().ToString(),
                        id_online = Guid.Empty.ToString(),
                        created_by = Students.CurrentLogin.ID,
                        updated_by = Students.CurrentLogin.ID,
                        created_on = DateTime.Now,
                        updated_on = DateTime.Now,
                        school_id = Students.SchoolInfo.id_offline,
                    },
                    Section = new sectionsModel(),
                    Grade = new gradesModel(),
                    BloodGroup = new ListModel(),
                    Gender = new ListModel(),  
                    Status = new ListModel(),                 
                    CreatedBy = Students.CurrentLogin.full_name
                };
                if(Students.PasswordBox != null)
                    Students.PasswordBox.Password = null;
                this.ShowForm();
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }
        }
        #endregion

        #region CancelNewStudentCommand

        public ICommand CancelNewStudentCommand
        {
            get { return _cancelNewStudentCommand; }
        }


        public bool CanCancelNewStudent(object obj)
        {
            return true;
        }


        public void CancelNewStudent(object obj)
        {
            try
            {
                this.ShowList();
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }
        }
        #endregion

        #region SaveStudentsCommand
        public ICommand SaveStudentsCommand
        {
            get { return _saveStudentsCommand; }
        }


        public bool CanSaveStudents(object obj)
        {
            return Students.Student != null &&
                    Students.Student.User.full_name != null &&
                    Students.Student.roll_number != null &&
                    Students.Student.Grade != null &&
                    Students.Student.Section != null &&
                    Students.Student.Gender != null &&
                    Students.Student.Status != null &&
                    Students.Student.registration_id != null &&
                    Students.Student.enrollment_date != null &&
                    Students.Student.BloodGroup != null &&
                    Students.Student.User.birth_date != null &&
                    Students.Student.User.address_line_one != null;
                    
        }

        public void SaveStudents(object obj)
        {
            try
            {

                if (StudentsManager.CreateOrModfiyStudents(Students.Student, Students.CurrentLogin, Students.SchoolInfo))
                {
                    GeneralMethods.ShowNotification("Notification", "Student Saved Successfully");
                    this.GetStudentsList();
                    this.ShowList();
                }



            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }

        }

        #endregion      

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void GetStudentsList()
        {
            try
            {
                Students.StudentsList = StudentsManager.GetStudentsList(Students.fromRowNo, Students.toRowNo);
                Students.NoRecordsFound = Students.StudentsList.Count > 0 ? "Collapsed" : "Visible";
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }

        }

        private void ShowForm()
        {
            Students.ListVisibility = "Collapsed";
            Students.FormVisibility = "Visible";
        }

        private void ShowList()
        {
            Students.ListVisibility = "Visible";
            Students.FormVisibility = "Collapsed";
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            Students.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get School Info
            Students.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }

        private void ResetPagination()
        {
            Students.fromRowNo = 1;
            Students.pageNo = 1;
            Students.PageNo = "Page No : " + Students.pageNo;
            Students.NoOfRecordsPerPage = Students.NoOfRecords;
            Students.toRowNo = Students.pageNo * Students.NoOfRecordsPerPage;
        }


        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            Students.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }

        private void GetDropDownLists()
        {
            Students.GradesList = GradesSetupManager.GetAllGrades();
            Students.SectionsList = SectionsSetupManager.GetAllSections();
            Students.BloodGroupList = GetListManager.GetStudentBloodGroupList();
            Students.GenderList = GetListManager.GetGenderList();
            Students.StatusList = GetListManager.GetStudentStatusList();
        }


    }
}

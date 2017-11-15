﻿using SMS.Models;
using SMS.Shared;
using SMS.Views;
using SMS_Businness_Layer.Businness;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SMS.Controllers
{
    public class GradesSetupController :INotifyPropertyChanged
    {
        #region Fields
        private GradesSetupModel _GradesSetup;
        private DataGrid _dataGrid;        

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _addNewGradeCommand;
        private ICommand _cancelNewGradeCommand;
        private ICommand _saveGradesCommand;
        #endregion

        #region Constructor
        public GradesSetupController()
        {

            _GradesSetup = new GradesSetupModel()
            {
                CurrentLogin = new LoginModel(),
                SchoolInfo = new SchoolModel()
            };

            //Get Global Objects
            GetGlobalObjects();

            // Get Lists
            //this.GetDropDownLists();

            //Get Settings
            this.GetSettings();
            // Set pagination
            this.ResetPagination();

            //Subscribe to Model's Property changed event
            this.GradesSetup.PropertyChanged += (s, e) => {
                if (e.PropertyName == "SelectedItemInGradesList")
                {
                    GradesSetup.Grade = GradesSetup.SelectedItemInGradesList;
                    this.ShowForm();
                }
            };

            

            //Get Initial Grades list
            this.GetGradesList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewGradeCommand = new RelayCommand(AddNewGrade, CanAddNewGrade);
            _cancelNewGradeCommand = new RelayCommand(CancelNewGrade, CanCancelNewGrade);
            _saveGradesCommand = new RelayCommand(SaveGrades, CanSaveGrades);

            this.ShowList();
        }
        
        #endregion

        #region Properties

        public GradesSetupModel GradesSetup
        {
            get
            {
                return _GradesSetup;
            }
            set
            {
                _GradesSetup = value;
                OnPropertyChanged("GradesSetup");
            }
        }

        public DataGrid GradesListDataGrid
        {
            get
            {
                return _dataGrid;
            }
            set
            {
                _dataGrid = value;
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
                GradesSetup.pageNo++;
                GradesSetup.PageNo = "Page No : " + GradesSetup.pageNo;
                GradesSetup.fromRowNo = GradesSetup.toRowNo + 1;
                GradesSetup.toRowNo = GradesSetup.pageNo * GradesSetup.NoOfRecordsPerPage;
                this.GetGradesList();
                if (GradesSetup.pageNo > 1 && GradesSetup.GradesList.Count == 0)
                    MoveToPreviousPage(obj);
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                MessageBox.Show(errorMessage);
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
                if (GradesSetup.pageNo > 1)
                {
                    GradesSetup.pageNo--;
                    GradesSetup.PageNo = "Page No : " + GradesSetup.pageNo;
                    GradesSetup.toRowNo = GradesSetup.fromRowNo - 1;
                    GradesSetup.fromRowNo = (GradesSetup.toRowNo + 1) - GradesSetup.NoOfRecordsPerPage;
                    this.GetGradesList();
                }

            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                MessageBox.Show(errorMessage);
            }
            finally
            {

            }
        }
        #endregion

        #region AddNewGradeCommand

        public ICommand AddNewGradeCommand
        {
            get { return _addNewGradeCommand; }
        }


        public bool CanAddNewGrade(object obj)
        {
            return true;
        }


        public void AddNewGrade(object obj)
        {
            try
            {
                GradesSetup.Grade = new GradesListModel();
                this.ShowForm();
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                MessageBox.Show(errorMessage);
            }
            finally
            {

            }
        }
        #endregion

        #region CancelNewGradeCommand

        public ICommand CancelNewGradeCommand
        {
            get { return _cancelNewGradeCommand; }
        }


        public bool CanCancelNewGrade(object obj)
        {
            return true;
        }


        public void CancelNewGrade(object obj)
        {
            try
            {
                this.ShowList();
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                MessageBox.Show(errorMessage);
            }
            finally
            {

            }
        }
        #endregion

        #region SaveGradesCommand
        public ICommand SaveGradesCommand
        {
            get { return _saveGradesCommand; }
        }


        public bool CanSaveGrades(object obj)
        {
            if (GradesSetup.Grade != null && GradesSetup.Grade.name != null && GradesSetup.Grade.block != null)
                return true;
            else
                return false;
        }

        public void SaveGrades(object obj)
        {
            try
            {
                if (GradesSetupManager.CreateOrModfiyGrades(GradesSetup.Grade, GradesSetup.CurrentLogin, GradesSetup.SchoolInfo))
                {
                    GeneralMethods.ShowNotification("Notification", "Grade Saved Successfully");
                    this.GetGradesList();
                    this.ShowList();
                }

            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                MessageBox.Show(errorMessage);
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

        private void GetGradesList()
        {
            try
            {
                GradesSetup.GradesList = GradesSetupManager.GetGradesList(GradesSetup.fromRowNo, GradesSetup.toRowNo);
                GradesSetup.NoRecordsFound = GradesSetup.GradesList.Count > 0 ? "Collapsed" : "Visible";
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                MessageBox.Show(errorMessage);
            }
            finally
            {

            }

        }

        private void ShowForm()
        {
            GradesSetup.ListVisibility = "Collapsed";
            GradesSetup.FormVisibility = "Visible";
        }

        private void ShowList()
        {
            GradesSetup.ListVisibility = "Visible";
            GradesSetup.FormVisibility = "Collapsed";
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            GradesSetup.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get School Info
            GradesSetup.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }

        private void ResetPagination()
        {
            GradesSetup.fromRowNo = 1;
            GradesSetup.pageNo = 1;
            GradesSetup.PageNo = "Page No : " + GradesSetup.pageNo;
            GradesSetup.NoOfRecordsPerPage = GradesSetup.NoOfRecords;
            GradesSetup.toRowNo = GradesSetup.pageNo * GradesSetup.NoOfRecordsPerPage;
        }


        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            GradesSetup.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }


    }
}

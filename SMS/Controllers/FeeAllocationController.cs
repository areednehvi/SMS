﻿using SMS.Models;
using SMS.Shared;
using SMS_Businness_Layer.Businness;
using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SMS_Models.Models.DBModels;

namespace SMS.Controllers
{
    public class FeeAllocationController : NotifyPropertyChanged
    {
        #region Fields
        private FeeAllocationModel _FeeAllocation;

        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private ICommand _addNewFeeAllocationCommand;
        private ICommand _cancelNewFeeAllocationCommand;
        private ICommand _saveFeeAllocationCommand;
        #endregion

        #region Constructor
        public FeeAllocationController()
        {
           
            FeeAllocation = new FeeAllocationModel()
            {
                CurrentLogin = new LoginModel(),
                SchoolInfo = new SchoolModel(),
                GradesMultiComboBox = new GradesMultiComboBox()
                {
                    GradesMultiComboBoxItems = new ObservableCollection<GradesMultiComboBoxItem>(),
                    GradesMultiComboBoxCheckedItems = new ObservableCollection<GradesMultiComboBoxItem>(),
                },
                FeeMonthsMultiComboBox = new FeeMonthsMultiComboBox()
                {
                    FeeMonthsMultiComboBoxItems = new ObservableCollection<FeeMonthsMultiComboBoxItem>(),
                    FeeMonthsMultiComboBoxCheckedItems = new ObservableCollection<FeeMonthsMultiComboBoxItem>(),
                }

            };

            FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxItems.CollectionChanged += GradesMultiComboBoxItems_CollectionChanged;
            FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxItems.CollectionChanged += FeeMonthsMultiComboBoxItems_CollectionChanged;

            //Get Global Objects
            this.GetGlobalObjects();

            // Get Lists
            this.GetDropDownLists();

            //Get Settings
            this.GetSettings();
            // Set pagination
            this.ResetPagination();

            //Subscribe to Model's Property changed event
            this.FeeAllocation.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedItemInFeeAllocationList")
                {
                    FeeAllocation.Fees = FeeAllocation.SelectedItemInFeeAllocationList;
                    if (FeeAllocation.Fees != null)
                    {
                        FeeAllocation.Fees.FeeCategory = FeeAllocation.FeeCategoriesList.Find(x => x.id_offline == FeeAllocation.Fees.fee_category_id);

                        this.MarkSelectedGrades();

                        this.MarkSelectedFeeMonths();

                    }
                    this.ShowForm();
                }
            };



            //Get Initial FeeAllocation list
            this.GetFeeAllocationList();

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewFeeAllocationCommand = new RelayCommand(AddNewFeeAllocation, CanAddNewFeeAllocation);
            _cancelNewFeeAllocationCommand = new RelayCommand(CancelNewFeeAllocation, CanCancelNewFeeAllocation);
            _saveFeeAllocationCommand = new RelayCommand(SaveFeeAllocation, CanSaveFeeAllocation);

            this.ShowList();
        }

        #endregion

        #region Properties

        public FeeAllocationModel FeeAllocation
        {
            get
            {
                return _FeeAllocation;
            }
            set
            {
                _FeeAllocation = value;
                OnPropertyChanged("FeeAllocation");
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
                FeeAllocation.pageNo++;
                FeeAllocation.PageNo = "Page No : " + FeeAllocation.pageNo;
                FeeAllocation.fromRowNo = FeeAllocation.toRowNo + 1;
                FeeAllocation.toRowNo = FeeAllocation.pageNo * FeeAllocation.NoOfRecordsPerPage;
                this.GetFeeAllocationList();
                if (FeeAllocation.pageNo > 1 && FeeAllocation.FeeAllocationList.Count == 0)
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
                if (FeeAllocation.pageNo > 1)
                {
                    FeeAllocation.pageNo--;
                    FeeAllocation.PageNo = "Page No : " + FeeAllocation.pageNo;
                    FeeAllocation.toRowNo = FeeAllocation.fromRowNo - 1;
                    FeeAllocation.fromRowNo = (FeeAllocation.toRowNo + 1) - FeeAllocation.NoOfRecordsPerPage;
                    this.GetFeeAllocationList();
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

        #region AddNewFeeAllocationCommand

        public ICommand AddNewFeeAllocationCommand
        {
            get { return _addNewFeeAllocationCommand; }
        }


        public bool CanAddNewFeeAllocation(object obj)
        {
            return true;
        }


        public void AddNewFeeAllocation(object obj)
        {
            try
            {
                FeeAllocation.Fees = new FeeAllocationListModel()
                {
                   
                };
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

        #region CancelNewFeeAllocationCommand

        public ICommand CancelNewFeeAllocationCommand
        {
            get { return _cancelNewFeeAllocationCommand; }
        }


        public bool CanCancelNewFeeAllocation(object obj)
        {
            return true;
        }


        public void CancelNewFeeAllocation(object obj)
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

        #region SaveFeeAllocationCommand
        public ICommand SaveFeeAllocationCommand
        {
            get { return _saveFeeAllocationCommand; }
        }


        public bool CanSaveFeeAllocation(object obj)
        {
            return FeeAllocation.Fees != null;

        }

        public void SaveFeeAllocation(object obj)
        {
            try
            {

                if (FeeAllocationManager.CreateOrModfiyFeeAllocation(FeeAllocation.Fees, FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems, FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems, FeeAllocation.CurrentLogin, FeeAllocation.SchoolInfo,FeeAllocation.CurrentSession))
                {
                    GeneralMethods.ShowNotification("Notification", "Fee Allocated Successfully");
                    this.GetFeeAllocationList();
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


        #region Private Functions
        private void GetFeeAllocationList()
        {
            try
            {
                FeeAllocation.FeeAllocationList = FeeAllocationManager.GetFeeAllocationList(FeeAllocation.fromRowNo, FeeAllocation.toRowNo);
                FeeAllocation.NoRecordsFound = FeeAllocation.FeeAllocationList.Count > 0 ? "Collapsed" : "Visible";
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
            FeeAllocation.ListVisibility = "Collapsed";
            FeeAllocation.FormVisibility = "Visible";
        }

        private void ShowList()
        {
            FeeAllocation.ListVisibility = "Visible";
            FeeAllocation.FormVisibility = "Collapsed";
        }

        private void GetGlobalObjects()
        {
            //Get the Current Login
            FeeAllocation.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get School Info
            FeeAllocation.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
            //Get Current Session
            FeeAllocation.CurrentSession = (sessionsModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentSession);
        }

        private void ResetPagination()
        {
            FeeAllocation.fromRowNo = 1;
            FeeAllocation.pageNo = 1;
            FeeAllocation.PageNo = "Page No : " + FeeAllocation.pageNo;
            FeeAllocation.NoOfRecordsPerPage = FeeAllocation.NoOfRecords;
            FeeAllocation.toRowNo = FeeAllocation.pageNo * FeeAllocation.NoOfRecordsPerPage;
        }



        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            FeeAllocation.NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }

        private void GetDropDownLists()
        {
            FeeAllocation.FeeCategoriesList = FeeCategoriesManager.GetAllFeeCategories();
            FeeAllocation.GradesList = GradesSetupManager.GetAllGrades();
            FeeAllocation.FeeMonthsList = SessionsSetupManager.GetFeeMonthsOfCurrentSession();
            FeeAllocation.AllocateFeeToList = GetListManager.GetAllocateFeeToList();
            // GradesMultiComboBox
            FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxItems.Add(new GradesMultiComboBoxItem(new gradesModel() { name = "All" }));
            foreach(gradesModel grade in FeeAllocation.GradesList)
                FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxItems.Add(new GradesMultiComboBoxItem(grade));
            // FeeMonthsMultiComboBox
            FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxItems.Add(new FeeMonthsMultiComboBoxItem( new ListModel() { id ="All", name = "All" } ));
            foreach (ListModel feeMonth in FeeAllocation.FeeMonthsList)
                FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxItems.Add(new FeeMonthsMultiComboBoxItem(feeMonth));
        }

        private void GradesMultiComboBoxItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (GradesMultiComboBoxItem item in e.OldItems)
                {
                    item.PropertyChanged -= GradesMultiComboBoxItem_PropertyChanged;
                    FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (GradesMultiComboBoxItem item in e.NewItems)
                {
                    item.PropertyChanged += GradesMultiComboBoxItem_PropertyChanged;
                    if (item.IsChecked) FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems.Add(item);
                }
            }
            GradesMultiComboBoxText();
        }

        private void GradesMultiComboBoxItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                GradesMultiComboBoxItem item = (GradesMultiComboBoxItem)sender;
                
                if (item.IsChecked)
                {
                    if (item.Grade.name == "All")
                    {
                        item.Grade.name = "All Selected"; // just to come out of infinite loop
                        for (int i = 0; i < FeeAllocation.GradesList.Count + 1; i++)
                        {
                            FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxItems[i].IsChecked = true;
                        }
                    }
                    if(item.Grade.name != "All Selected" && !FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems.Contains(item))
                        FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems.Add(item);
                }
                else
                {
                    if (item.Grade.name == "All Selected")
                    {
                        item.Grade.name = "All"; // just to come out of infinite loop
                        for (int i = 0; i < FeeAllocation.GradesList.Count + 1; i++)
                        {
                            FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxItems[i].IsChecked = false;
                        }
                    }
                    FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems.Remove(item);
                }
                GradesMultiComboBoxText();
            }
        }

        private void GradesMultiComboBoxText()
        {
            switch (FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems.Count)
            {
                case 0:
                    FeeAllocation.GradesMultiComboBox.Text = "";
                    break;
                case 1:
                    FeeAllocation.GradesMultiComboBox.Text = FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems[0].Grade.name;
                    break;
                default:
                    if (FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxCheckedItems.Count == FeeAllocation.GradesList.Count)
                        FeeAllocation.GradesMultiComboBox.Text = "<All>";
                    else
                        FeeAllocation.GradesMultiComboBox.Text = "<Multiple>";
                    break;
            }
        }

        private void FeeMonthsMultiComboBoxItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (FeeMonthsMultiComboBoxItem item in e.OldItems)
                {
                    item.PropertyChanged -= FeeMonthsMultiComboBoxItem_PropertyChanged;
                    FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (FeeMonthsMultiComboBoxItem item in e.NewItems)
                {
                    item.PropertyChanged += FeeMonthsMultiComboBoxItem_PropertyChanged;
                    if (item.IsChecked) FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems.Add(item);
                }
            }
            FeeMonthsMultiComboBoxText();
        }

        private void FeeMonthsMultiComboBoxItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                FeeMonthsMultiComboBoxItem item = (FeeMonthsMultiComboBoxItem)sender;

                if (item.IsChecked)
                {
                    if (item.FeeMonth.id == "All")
                    {
                        item.FeeMonth.id = "All Selected"; // just to come out of infinite loop
                        for (int i = 0; i < FeeAllocation.FeeMonthsList.Count + 1; i++)
                        {
                            FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxItems[i].IsChecked = true;
                        }
                    }
                    if (item.FeeMonth.id != "All Selected" && !FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems.Contains(item))
                        FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems.Add(item);
                }
                else
                {
                    if (item.FeeMonth.id == "All Selected")
                    {
                        item.FeeMonth.id = "All"; // just to come out of infinite loop
                        for (int i = 0; i < FeeAllocation.FeeMonthsList.Count + 1; i++)
                        {
                            FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxItems[i].IsChecked = false;
                        }
                    }
                    FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems.Remove(item);
                }
                FeeMonthsMultiComboBoxText();
            }
        }

        private void FeeMonthsMultiComboBoxText()
        {
            switch (FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems.Count)
            {
                case 0:
                    FeeAllocation.FeeMonthsMultiComboBox.Text = "";
                    break;
                case 1:
                    FeeAllocation.FeeMonthsMultiComboBox.Text = FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems[0].FeeMonth.name;
                    break;
                default:
                    if (FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxCheckedItems.Count == FeeAllocation.FeeMonthsList.Count)
                        FeeAllocation.FeeMonthsMultiComboBox.Text = "<All>";
                    else
                        FeeAllocation.FeeMonthsMultiComboBox.Text = "<Multiple>";
                    break;
            }
        }
        private void MarkSelectedGrades()
        {
            foreach (GradesMultiComboBoxItem gradesMultiComboBoxItem in FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxItems)
                gradesMultiComboBoxItem.IsChecked = false;
            string[] gradeIDs = FeeAllocation.Fees.AppliedToGradeIDs.Split(',');
            foreach (string gradeID in gradeIDs)
            {
                foreach (GradesMultiComboBoxItem gradesMultiComboBoxItem in FeeAllocation.GradesMultiComboBox.GradesMultiComboBoxItems)
                {
                    if (gradesMultiComboBoxItem.Grade.id_offline == gradeID.ToLower().Trim())
                    {
                        gradesMultiComboBoxItem.IsChecked = true;
                        break;
                    }
                }
            }
        }
        private void MarkSelectedFeeMonths()
        {
            foreach (FeeMonthsMultiComboBoxItem feeMonthsMultiComboBoxItem in FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxItems)
                feeMonthsMultiComboBoxItem.IsChecked = false;
            string[] feeMonths = FeeAllocation.Fees.fee_cources.Split(',');
            foreach (string feeMonth in feeMonths)
            { 
                foreach (FeeMonthsMultiComboBoxItem feeMonthsMultiComboBoxItem in FeeAllocation.FeeMonthsMultiComboBox.FeeMonthsMultiComboBoxItems)
                {
                    if (feeMonthsMultiComboBoxItem.FeeMonth.id != "All" && Convert.ToDateTime(feeMonthsMultiComboBoxItem.FeeMonth.id) == Convert.ToDateTime(feeMonth.ToLower().Trim()))
                    {
                        feeMonthsMultiComboBoxItem.IsChecked = true;
                        break;
                    }
                }
            }
        }
        #endregion
    }

}



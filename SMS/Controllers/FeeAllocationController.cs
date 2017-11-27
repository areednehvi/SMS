using SMS.Models;
using SMS.Shared;
using SMS_Businness_Layer.Businness;
using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            ViewModel();
       

            _FeeAllocation = new FeeAllocationModel()
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
            this.FeeAllocation.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedItemInFeeAllocationList")
                {
                    FeeAllocation.FeeAllocation = FeeAllocation.SelectedItemInFeeAllocationList;
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
                FeeAllocation.FeeAllocation = new FeeAllocationListModel()
                {
                    CreatedBy = FeeAllocation.CurrentLogin.User.full_name
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
            return FeeAllocation.FeeAllocation != null;

        }

        public void SaveFeeAllocation(object obj)
        {
            try
            {

                if (FeeAllocationManager.CreateOrModfiyFeeAllocation(FeeAllocation.FeeAllocation, FeeAllocation.CurrentLogin, FeeAllocation.SchoolInfo))
                {
                    GeneralMethods.ShowNotification("Notification", "Fee Category Saved Successfully");
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
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////
        private Dictionary<string, object> _items;
        private Dictionary<string, object> _selectedItems;


        public Dictionary<string, object> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public Dictionary<string, object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                
                OnPropertyChanged("SelectedItems");
            }
        }
        private string _ShowSelectedItems;

        public string ShowSelectedItems
        {
            get
            {
                return _ShowSelectedItems;
            }
            set
            {
                _ShowSelectedItems = value;
                OnPropertyChanged("ShowSelectedItems");
            }
        }





        private void ViewModel()
        {
            Items = new Dictionary<string, object>();
            Items.Add("Chennai", "MAS");
            Items.Add("Trichy", "TPJ");
            Items.Add("Bangalore", "SBC");
            Items.Add("Coimbatore", "CBE");

            SelectedItems = new Dictionary<string, object>();
            SelectedItems.Add("Chennai", "MAS");
            SelectedItems.Add("Trichy", "TPJ");
            Submit();
        }

        private void Submit()
        {
            ShowSelectedItems = "";
            foreach (KeyValuePair<string, object> s in SelectedItems)
            {
                ShowSelectedItems += s.Key + ",";
            }
        }


    }
}

using SMS.Models;
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
            
       

            _FeeAllocation = new FeeAllocationModel()
            {
                CurrentLogin = new LoginModel(),
                SchoolInfo = new SchoolModel(),
                Items = new ObservableCollection<Item>(),
                mCheckedItems = new HashSet<Item>(),
                
            };

            _FeeAllocation.Items.CollectionChanged += Items_CollectionChanged;

            //Get Global Objects
            GetGlobalObjects();

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

            /////////////////////////////////////////////////////////////////////////////////////
            

            // Adding test data
            for (int i = 0; i < 10; ++i)
            {
                FeeAllocation.Items.Add(new Item(string.Format("Item {0}", i.ToString("00"))));
            }
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

                if (FeeAllocationManager.CreateOrModfiyFeeAllocation(FeeAllocation.Fees, FeeAllocation.CurrentLogin, FeeAllocation.SchoolInfo))
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

        private void GetDropDownLists()
        {
            FeeAllocation.FeeCategoriesList = FeeCategoriesManager.GetAllFeeCategories();
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////

        

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Item item in e.OldItems)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                    FeeAllocation.mCheckedItems.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Item item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                    if (item.IsChecked) FeeAllocation.mCheckedItems.Add(item);
                }
            }
            UpdateText();
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                Item item = (Item)sender;
                if (item.IsChecked)
                {
                    FeeAllocation.mCheckedItems.Add(item);
                }
                else
                {
                    FeeAllocation.mCheckedItems.Remove(item);
                }
                UpdateText();
            }
        }

        private void UpdateText()
        {
            switch (FeeAllocation.mCheckedItems.Count)
            {
                case 0:
                    FeeAllocation.Text = "<none>";
                    break;
                //case 1:
                //    Text = mCheckedItems.First().Name;
                //    break;
                default:
                    FeeAllocation.Text = "<multiple>";
                    break;
            }
        }
    }
    
}



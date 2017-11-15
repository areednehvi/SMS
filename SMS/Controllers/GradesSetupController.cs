using SMS.Models;
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


        private FeeCollectionStudentListModel _selectedItemInFeeCollectionStudentList;
        private FeeCollectionListFiltersModel _FeeCollectionListFilters;
        private FeeCollectionListOtherFiledsModel _FeeCollectionListOtherFileds;
        private GradesModel _selectedGradeModel;
        private SectionsModel _selectedSectionModel;
        private DataGrid _dataGrid;
        private int NoOfRecords;
        private int fromRowNo,pageNo, NoOfRecordsPerPage, toRowNo;
        private string _NoRecordsFound;

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

            _FeeCollectionListFilters = new FeeCollectionListFiltersModel();
            _FeeCollectionListOtherFileds = new FeeCollectionListOtherFiledsModel();
            // Get Lists
            this.GetDropDownLists();
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

            this.GradesSetup.GradesList.CollectionChanged += GradeList_CollectionChanged;

            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewGradeCommand = new RelayCommand(AddNewGrade, CanAddNewGrade);
            _cancelNewGradeCommand = new RelayCommand(CancelNewGrade, CanCancelNewGrade);
            _saveGradesCommand = new RelayCommand(SaveGrades, CanSaveGrades);

            NoRecordsFound = "Visible";

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


        public FeeCollectionStudentListModel SelectedItemInFeeCollectionStudentList
        {
            get
            {
                return _selectedItemInFeeCollectionStudentList;
            }

            set
            {
                _selectedItemInFeeCollectionStudentList = value;
                loadCollectFeeWindow();
            }

        }

        public FeeCollectionListFiltersModel FeeCollectionListFilters
        {
            get
            {
                return _FeeCollectionListFilters;
            }
            set
            {
                _FeeCollectionListFilters = value;
            }
        }

        public FeeCollectionListOtherFiledsModel FeeCollectionListOtherFileds
        {
            get
            {
                return _FeeCollectionListOtherFileds;
            }
            set
            {
                _FeeCollectionListOtherFileds = value;
            }
        }

        public GradesModel SelectedGrade
        {
            get { return _selectedGradeModel; }
            set
            {
                if (_selectedGradeModel != value)
                {
                    _selectedGradeModel = value;
                    FeeCollectionListFilters.Grade = this.SelectedGrade;
                    this.LoadFeeCollectionAsFiltersHaveChanged();
                }
            }
        }

        public SectionsModel SelectedSection
        {
            get { return _selectedSectionModel; }
            set
            {
                if (_selectedSectionModel != value)
                {
                    _selectedSectionModel = value;
                    FeeCollectionListFilters.Section = this.SelectedSection;
                    this.LoadFeeCollectionAsFiltersHaveChanged();
                }
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
        public string NoRecordsFound
        {
            get
            {
                return _NoRecordsFound;
            }
            set
            {
                _NoRecordsFound = value;
                OnPropertyChanged("NoRecordsFound");
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
                GradesListDataGrid.ItemsSource = null;
                pageNo++;
                FeeCollectionListOtherFileds.PageNo = "Page No : " + pageNo;
                fromRowNo = toRowNo + 1;
                toRowNo = pageNo * NoOfRecordsPerPage;
                this.GetGradesList();
                if (pageNo > 1 && GradesSetup.GradesList.Count == 0)
                    MoveToPreviousPage(obj);
                GradesListDataGrid.ItemsSource = GradesSetup.GradesList;
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
                if (pageNo > 1)
                {
                    GradesListDataGrid.ItemsSource = null;
                    pageNo--;
                    FeeCollectionListOtherFileds.PageNo = "Page No : " + pageNo;
                    toRowNo = fromRowNo - 1;
                    fromRowNo = (toRowNo + 1) - NoOfRecordsPerPage;
                    this.GetGradesList();
                    GradesListDataGrid.ItemsSource = GradesSetup.GradesList;
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
                GradesSetup.GradesList = GradesSetupManager.GetGradesList(fromRowNo, toRowNo);
                NoRecordsFound = GradesSetup.GradesList.Count > 0 ? "Collapsed" : "Visible";
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
            fromRowNo = 1;
            pageNo = 1;
            FeeCollectionListOtherFileds.PageNo = "Page No : " + pageNo;
            NoOfRecordsPerPage = NoOfRecords;
            toRowNo = pageNo * NoOfRecordsPerPage;
        }

        private void GetDropDownLists()
        {

            FeeCollectionListFilters.GradesList = GetListManager.GetGrades();
            FeeCollectionListFilters.SectionsList = GetListManager.GetSections();
        }

        private void GetSettings()
        {
            string noOfRecords = SettingsManager.GetSetting(SettingDefinitions.NoOfRowsInGrids);
            NoOfRecords = noOfRecords != null ? Convert.ToInt32(noOfRecords) : 50;
        }

        private void LoadFeeCollectionAsFiltersHaveChanged()
        {
            ResetPagination();
            this.GetGradesList();
            if (GradesListDataGrid != null)
            {
                GradesListDataGrid.ItemsSource = null;
                GradesListDataGrid.ItemsSource = GradesSetup.GradesList;
            }
        }

        private void loadCollectFeeWindow()
        {
            if (SelectedItemInFeeCollectionStudentList != null)
            {
                FeeCollect objFeeCollectWindow = new FeeCollect(SelectedItemInFeeCollectionStudentList);
                objFeeCollectWindow.Show();
            }
        }

        public void GradeList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {            
            /*if (e.NewItems != null)
                foreach (FeeBalancesModel item in e.NewItems)
                    item.PropertyChanged += FeeBalancesModel_PropertyChanged;

            if (e.OldItems != null)
                foreach (FeeBalancesModel item in e.OldItems)
                    item.PropertyChanged -= FeeBalancesModel_PropertyChanged;*/
        }

       



    }
}

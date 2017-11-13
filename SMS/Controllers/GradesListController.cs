using SMS.Models;
using SMS.Shared;
using SMS.Views;
using SMS_Businness_Layer.Businness;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SMS_Models.Models.DBModels;

namespace SMS.Controllers
{
    public class GradesListController :INotifyPropertyChanged
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
        #endregion

        #region Constructor
        public GradesListController()
        {

            _GradesSetup = new GradesSetupModel();

            _FeeCollectionListFilters = new FeeCollectionListFiltersModel();
            _FeeCollectionListOtherFileds = new FeeCollectionListOtherFiledsModel();
            // Get Lists
            this.GetDropDownLists();
            //Get Settings
            this.GetSettings();
            // Set pagination
            this.ResetPagination();

            //Subscribe to Model's Property changed event
            //this.FeeCollectionListFilters.PropertyChanged += (s, e) => {
            //    this.LoadFeeCollectionAsFiltersHaveChanged();
            //};
                        
            //Get Initial Grades list
            this.GetGradesList();
            //Initialize  Commands
            _nextPageCommand = new RelayCommand(MoveToNextPage, CanMoveToNextPage);
            _previousPageCommand = new RelayCommand(MoveToPreviousPage, CanMoveToPreviousPage);
            _addNewGradeCommand = new RelayCommand(AddNewGrade, CanAddNewGrade);

            NoRecordsFound = "Visible";
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

        public DataGrid FeeCollectionListDataGrid
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
                FeeCollectionListDataGrid.ItemsSource = null;
                pageNo++;
                FeeCollectionListOtherFileds.PageNo = "Page No : " + pageNo;
                fromRowNo = toRowNo + 1;
                toRowNo = pageNo * NoOfRecordsPerPage;
                this.GetGradesList();
                if (pageNo > 1 && GradesSetup.GradesList.Count == 0)
                    MoveToPreviousPage(obj);
                FeeCollectionListDataGrid.ItemsSource = GradesSetup.GradesList;
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
                    FeeCollectionListDataGrid.ItemsSource = null;
                    pageNo--;
                    FeeCollectionListOtherFileds.PageNo = "Page No : " + pageNo;
                    toRowNo = fromRowNo - 1;
                    fromRowNo = (toRowNo + 1) - NoOfRecordsPerPage;
                    this.GetGradesList();
                    FeeCollectionListDataGrid.ItemsSource = GradesSetup.GradesList;
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
                GradesView winGradesView = new GradesView();
                winGradesView.Show();
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
                GradesSetup.GradesList = GradesListManager.GetGradesList(fromRowNo, toRowNo);
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
            if (FeeCollectionListDataGrid != null)
            {
                FeeCollectionListDataGrid.ItemsSource = null;
                FeeCollectionListDataGrid.ItemsSource = GradesSetup.GradesList;
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

    }
}

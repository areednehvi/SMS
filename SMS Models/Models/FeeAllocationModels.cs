using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using static SMS_Models.Models.DBModels;

namespace SMS.Models
{
    public class FeeAllocationModel :NotifyPropertyChanged
    {
        private FeeAllocationListModel _SelectedItemInFeeAllocationList;
        private FeeAllocationListModel _Fees;
        private ObservableCollection<FeeAllocationListModel> _FeeAllocationList;
        private List<fee_categoriesModel> _FeeCategoriesList;
        private List<gradesModel> _GradesList;
        private List<ListModel> _FeeMonthsList;
        private List<ListModel> _AllocateFeeToList;
        private LoginModel _CurrentLogin;
        private SchoolModel _SchoolInfo;
        private string _ListVisibility;
        private string _FormVisibility;
        private string _PageNo;
        private string _NoRecordsFound;

        public int NoOfRecords{get; set;}
        public int fromRowNo { get; set; }
        public int pageNo { get; set; }
        public int NoOfRecordsPerPage { get; set; }
        public int toRowNo { get; set; }      
        
        public ObservableCollection<FeeAllocationListModel> FeeAllocationList
        {
            get
            {
                return _FeeAllocationList;
            }
            set
            {
                _FeeAllocationList = value;
                OnPropertyChanged("FeeAllocationList");
            }
        }
        public List<fee_categoriesModel> FeeCategoriesList
        {
            get
            {
                return _FeeCategoriesList;
            }
            set
            {
                _FeeCategoriesList = value;
                OnPropertyChanged("FeeCategoriesList");
            }
        }
        public List<ListModel> AllocateFeeToList
        {
            get
            {
                return _AllocateFeeToList;
            }
            set
            {
                _AllocateFeeToList = value;
                OnPropertyChanged("AllocateFeeToList");
            }
        }
        public List<gradesModel> GradesList
        {
            get
            {
                return _GradesList;
            }
            set
            {
                _GradesList = value;
                OnPropertyChanged("GradesList");
            }
        }
        public List<ListModel> FeeMonthsList
        {
            get
            {
                return _FeeMonthsList;
            }
            set
            {
                _FeeMonthsList = value;
                OnPropertyChanged("FeeMonthsList");
            }
        }
        public GradesMultiComboBox GradesMultiComboBox
        {
            get;set;
        }
        public FeeMonthsMultiComboBox FeeMonthsMultiComboBox
        {
            get; set;
        }
        public string ListVisibility
        {
            get
            {
                return _ListVisibility;
            }
            set
            {
                _ListVisibility = value;
                OnPropertyChanged("ListVisibility");
            }
        }
        public string FormVisibility
        {
            get
            {
                return _FormVisibility;
            }
            set
            {
                _FormVisibility = value;
                OnPropertyChanged("FormVisibility");
            }
        }
 
        public string PageNo
        {
            get
            {
                return _PageNo;
            }
            set
            {
                _PageNo = value;
                OnPropertyChanged("PageNo");
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

        public FeeAllocationListModel SelectedItemInFeeAllocationList
        {
            get
            {
                return _SelectedItemInFeeAllocationList;
            }
            set
            {
                _SelectedItemInFeeAllocationList = value;
                OnPropertyChanged("SelectedItemInFeeAllocationList");
            }
        }
        public FeeAllocationListModel Fees
        {
            get
            {
                return _Fees;
            }
            set
            {
                _Fees = value;
                OnPropertyChanged("Fees");
            }
        }

        public LoginModel CurrentLogin
        {
            get
            {
                return _CurrentLogin;
            }
            set
            {
                _CurrentLogin = value;
            }
        }
        public SchoolModel SchoolInfo
        {
            get
            {
                return _SchoolInfo;
            }
            set
            {
                _SchoolInfo = value;
            }
        }

        
    }

    public class FeeAllocationListModel : feesModel , INotifyPropertyChanged
    {
        private fee_categoriesModel _FeeCategory;
        private ListModel _AllocateFeeTo;
        public fee_categoriesModel FeeCategory
        {
            get
            {
                return _FeeCategory;
            }
            set
            {
                _FeeCategory = value;
                OnPropertyChanged("FeeCategory");
            }
        }
        public ListModel AllocateFeeTo
        {
            get
            {
                return _AllocateFeeTo;
            }
            set
            {
                _AllocateFeeTo = value;
                OnPropertyChanged("AllocateFeeTo");
            }
        }
        public string GradesAppliedTo { get; set; }
        public string AppliedToGradeIDs { get; set; }
        public string CreatedBy { get; set; }
        public Int64 StudentCount { get; set; }

        #region INotify Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    public class GradesMultiComboBoxItem : NotifyPropertyChanged
    {
        public gradesModel Grade { get; set; }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
                    
            }
        }
        

        public GradesMultiComboBoxItem(gradesModel _grade)
        {
            Grade = _grade;
        }

        public override string ToString()
        {
            return Grade.name;
        }
    }

    public class GradesMultiComboBox :NotifyPropertyChanged
    {
        private ObservableCollection<GradesMultiComboBoxItem> _GradesMultiComboBoxItems;
        private string _text;

        public ObservableCollection<GradesMultiComboBoxItem> GradesMultiComboBoxItems
        {
            get { return _GradesMultiComboBoxItems; }
            set
            {
                _GradesMultiComboBoxItems = value;
                OnPropertyChanged("GradesMultiComboBoxItems");
            }
        }
        public ObservableCollection<GradesMultiComboBoxItem> GradesMultiComboBoxCheckedItems
        {
            get; set;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
    }

    public class FeeMonthsMultiComboBoxItem : NotifyPropertyChanged
    {
        public ListModel FeeMonth { get; set; }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");

            }
        }


        public FeeMonthsMultiComboBoxItem(ListModel _FeeMonth)
        {
            FeeMonth = _FeeMonth;
        }

        public override string ToString()
        {
            return FeeMonth.ToString();
        }
    }

    public class FeeMonthsMultiComboBox : NotifyPropertyChanged
    {
        private ObservableCollection<FeeMonthsMultiComboBoxItem> _FeeMonthsMultiComboBoxItems;
        private string _text;

        public ObservableCollection<FeeMonthsMultiComboBoxItem> FeeMonthsMultiComboBoxItems
        {
            get { return _FeeMonthsMultiComboBoxItems; }
            set
            {
                _FeeMonthsMultiComboBoxItems = value;
                OnPropertyChanged("FeeMonthsMultiComboBoxItems");
            }
        }
        public ObservableCollection<FeeMonthsMultiComboBoxItem> FeeMonthsMultiComboBoxCheckedItems
        {
            get; set;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
    }


}

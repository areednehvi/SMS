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
        private FeeAllocationListModel _FeeAllocation;
        private ObservableCollection<FeeAllocationListModel> _FeeAllocationList;
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
        public FeeAllocationListModel FeeAllocation
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

    public class FeeAllocationListModel : feesModel
    {
        public fee_categoriesModel fee_categories { get; set; }
        public string GradesAppliedTo { get; set; }
        public Int64 StudentCount { get; set; }
    }


}

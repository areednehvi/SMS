using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SMS.Models
{
    public class SectionsSetupModel : INotifyPropertyChanged
    {
        private SectionsListModel _SelectedItemInSectionsList;
        private SectionsListModel _Section;
        private ObservableCollection<SectionsListModel> _SectionsList;
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
        public ObservableCollection<SectionsListModel> SectionsList
        {
            get
            {
                return _SectionsList;
            }
            set
            {
                _SectionsList = value;
                OnPropertyChanged("SectionsList");
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

        public SectionsListModel SelectedItemInSectionsList
        {
            get
            {
                return _SelectedItemInSectionsList;
            }
            set
            {
                _SelectedItemInSectionsList = value;
                OnPropertyChanged("SelectedItemInSectionsList");
            }
        }
        public SectionsListModel Section
        {
            get
            {
                return _Section;
            }
            set
            {
                _Section = value;
                OnPropertyChanged("Section");
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
    }

    public class SectionsListModel
    {
        public string id_offline { get; set; }
        public string id_online { get; set; }
        public string school_id { get; set; }
        public int capacity { get; set; }
        public string name { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
        public string createdBy { get; set; }
    }


}

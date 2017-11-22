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
    public class StudentsModel : INotifyPropertyChanged
    {
        private StudentsListModel _SelectedItemInStudentsList;
        private StudentsListModel _Student;
        private ObservableCollection<StudentsListModel> _StudentsList;
        private List<gradesModel> _GradesList;
        private List<sectionsModel> _SectionsList;
        private List<ListModel> _BloodGroupList;
        private List<ListModel> _GenderList;
        private List<ListModel> _StatusList;
        private LoginModel _CurrentLogin;
        private SchoolModel _SchoolInfo;
        private PasswordBox _PasswordBox;
        private string _ListVisibility;
        private string _FormVisibility;
        private string _PageNo;
        private string _NoRecordsFound;


        public int NoOfRecords{get; set;}
        public int fromRowNo { get; set; }
        public int pageNo { get; set; }
        public int NoOfRecordsPerPage { get; set; }
        public int toRowNo { get; set; }
        public ObservableCollection<StudentsListModel> StudentsList
        {
            get
            {
                return _StudentsList;
            }
            set
            {
                _StudentsList = value;
                OnPropertyChanged("StudentsList");
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
        public List<sectionsModel> SectionsList
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
        public List<ListModel> BloodGroupList
        {
            get
            {
                return _BloodGroupList;
            }
            set
            {
                _BloodGroupList = value;
                OnPropertyChanged("BloodGroupList");
            }
        }
        public List<ListModel> GenderList
        {
            get
            {
                return _GenderList;
            }
            set
            {
                _GenderList = value;
                OnPropertyChanged("GenderList");
            }
        }
        public List<ListModel> StatusList
        {
            get
            {
                return _StatusList;
            }
            set
            {
                _StatusList = value;
                OnPropertyChanged("StatusList");
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
        public PasswordBox PasswordBox
        {
            get
            {
                return _PasswordBox;
            }
            set
            {
                _PasswordBox = value;
                OnPropertyChanged("PasswordBox");
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

        public StudentsListModel SelectedItemInStudentsList
        {
            get
            {
                return _SelectedItemInStudentsList;
            }
            set
            {
                _SelectedItemInStudentsList = value;
                OnPropertyChanged("SelectedItemInStudentsList");
            }
        }
        public StudentsListModel Student
        {
            get
            {
                return _Student;
            }
            set
            {
                _Student = value;
                OnPropertyChanged("Student");
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

    public class StudentsListModel : studentsModel, INotifyPropertyChanged 
    {
        private usersModel _User;
        private parentsModel _Parents;
        private gradesModel _Grade;
        private sectionsModel _Section;
        private ListModel _BloodGroup;
        private ListModel _Gender;
        private ListModel _Status;
        public usersModel User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPropertyChanged("User");
            }
        }

        public parentsModel Parents
        {
            get
            {
                return _Parents;
            }
            set
            {
                _Parents = value;
                OnPropertyChanged("Parents");
            }
        }
        public gradesModel Grade
        {
            get
            {
                return _Grade;
            }
            set
            {
                _Grade = value;
                OnPropertyChanged("Grade");
            }
        }
        public sectionsModel Section
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
        public ListModel BloodGroup
        {
            get
            {
                return _BloodGroup;
            }
            set
            {
                _BloodGroup = value;
                OnPropertyChanged("BloodGroup");
            }
        }
        public ListModel Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                _Gender = value;
                OnPropertyChanged("Gender");
            }
        }
        public ListModel Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
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



}

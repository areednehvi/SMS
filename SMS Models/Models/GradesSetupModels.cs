using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static SMS_Models.Models.DBModels;

namespace SMS.Models
{
    public class GradesSetupModel : INotifyPropertyChanged
    {
        private GradesListModel _SelectedItemInGradesList;
        private GradesListModel _NewGrade;
        private ObservableCollection<GradesListModel> _GradesList;
        private Window _window;
        private LoginModel _CurrentLogin;
        private SchoolModel _SchoolInfo;

        public ObservableCollection<GradesListModel> GradesList
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

        public GradesListModel SelectedItemInGradesList
        {
            get
            {
                return _SelectedItemInGradesList;
            }
            set
            {
                _SelectedItemInGradesList = value;
                OnPropertyChanged("SelectedItemInGradesList");
            }
        }
        public GradesListModel NewGrade
        {
            get
            {
                return _NewGrade;
            }
            set
            {
                _NewGrade = value;
                OnPropertyChanged("NewGrade");
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

        public Window Window
        {
            get
            {
                return _window;
            }
            set
            {
                _window = value;
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

    public class GradesListModel
    {
        public string id_offline { get; set; }
        public string id_online { get; set; }
        public string school_id { get; set; }
        public string block { get; set; }
        public string name { get; set; }
        public string order { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
        public string createdBy { get; set; }
    }


}

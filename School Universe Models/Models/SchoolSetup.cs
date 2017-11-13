using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static School_Universe_Models.Models.DBModels;

namespace School_Universe.Models
{
    public class SchoolSetupModel : INotifyPropertyChanged
    {
        private bool _IsSetupInProgress;
        private string _SetupStatus;
        private string _Key;
        private SchoolModel _SchoolInfo;

        public bool IsSetupInProgress
        {
            get { return _IsSetupInProgress; }
            set
            {
                _IsSetupInProgress = value;
                OnPropertyChanged("IsSetupInProgress");
            }
        }
        public string SetupStatus
        {
            get
            {
                return _SetupStatus;
            }
            set
            {
                _SetupStatus = value;
                OnPropertyChanged("SetupStatus");
            }
        }

        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
                OnPropertyChanged("Key");
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
    


}

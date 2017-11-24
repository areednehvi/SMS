using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace SMS.Models
{
    public class LoginModel : INotifyPropertyChanged
    {
        private UsersListModel _User;
        private string _message;

        public UsersListModel User
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

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
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

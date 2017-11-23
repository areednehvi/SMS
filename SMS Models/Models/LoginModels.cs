using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Models
{
    public class LoginModel : INotifyPropertyChanged
    {     
        private string _username;
        private string _full_name;
        private string _password;
        private string _message;

        public string ID { get; set; }
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                Message = null;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                Message = null;
                OnPropertyChanged("Password");
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
        public string full_name
        {
            get
            {
                return _full_name;
            }
            set
            {
                _full_name = value;
                OnPropertyChanged("full_name");
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

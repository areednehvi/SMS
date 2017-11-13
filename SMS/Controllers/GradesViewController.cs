using MaterialDesignThemes.Wpf;
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
    public class GradesViewController :INotifyPropertyChanged
    {
        #region Fields
        
        private ICommand _saveGradesCommand;
        private ICommand _closeCommand;
        private ICommand _minimizeCommand;
        private Window _window;
        private LoginModel _CurrentLogin;
        private SchoolModel _SchoolInfo;
        #endregion

        #region Constructor
        public GradesViewController()
        {
            //Initialize Commands
            _saveGradesCommand = new RelayCommand(SaveGrades,CanSaveGrades);
            _closeCommand = new RelayCommand(Close, CanClose);
            _minimizeCommand = new RelayCommand(Minimize, CanMinimize);
        }        

        #endregion

        #region Properties
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
        #endregion

        #region SaveGradesCommand
        public ICommand SaveGradesCommand
        {
            get { return _saveGradesCommand; }        
        }

      
        public bool CanSaveGrades(object obj)
        {          
            return true;
        }

        public void SaveGrades(object obj)
        {
            try
            {
                
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

        #region CloseCommand

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
        }


        public bool CanClose(object obj)
        {
            return true;
        }


        public void Close(object obj)
        {
            Window.Close();
        }
        #endregion

        #region MinimizeCommand

        public ICommand MinimizeCommand
        {
            get { return _minimizeCommand; }
        }


        public bool CanMinimize(object obj)
        {
            return true;
        }


        public void Minimize(object obj)
        {
            Window.WindowState = WindowState.Minimized;
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

    }
    
}

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
    public class GradesViewController : GradesListController,INotifyPropertyChanged
    {
        #region Fields
        private GradesSetupModel _GradeSetup;

        private ICommand _saveGradesCommand;
        private ICommand _closeCommand;
        private ICommand _minimizeCommand;        
        
        #endregion

        #region Constructor
        public GradesViewController()
        {
            _GradeSetup = new GradesSetupModel()
            {
                CurrentLogin = new LoginModel(),
                SchoolInfo = new SchoolModel(),
                NewGrade = new GradesListModel()
            };

            //Get Global Objects
            GetGlobalObjects();

            //Initialize Commands
            _saveGradesCommand = new RelayCommand(SaveGrades,CanSaveGrades);
            _closeCommand = new RelayCommand(Close, CanClose);
            _minimizeCommand = new RelayCommand(Minimize, CanMinimize);
        }        

        #endregion

        #region Properties
        
        public GradesSetupModel GradeSetup
        {
            get
            {
                return _GradeSetup;
            }
            set
            {
                _GradeSetup = value;
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
                if (GradesSetupManager.CreateOrModfiyGrades(GradeSetup.NewGrade, GradeSetup.CurrentLogin, GradeSetup.SchoolInfo))
                {
                    GeneralMethods.ShowNotification("Notification", "Grade Saved Successfully");
                    
                    //GradesListController obj2 = (GradesListController)this; // cast to base class 
                    //obj2.GradesSetup.GradesList = null;
                    //obj2.GradesListDataGrid.ItemsSource = null;

                    GradeSetup.Window.Close();


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
            GradeSetup.Window.Close();
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
            GradeSetup.Window.WindowState = WindowState.Minimized;
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

        #region Private Functions
        private void GetGlobalObjects()
        {
            //Get the Current Login
            GradeSetup.CurrentLogin = (LoginModel)GeneralMethods.GetGlobalObject(GlobalObjects.CurrentLogin);
            //Get School Info
            GradeSetup.SchoolInfo = (SchoolModel)GeneralMethods.GetGlobalObject(GlobalObjects.SchoolInfo);
        }
        #endregion

    }

}

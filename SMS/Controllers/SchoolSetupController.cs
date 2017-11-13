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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SMS.Controllers
{
    public class SchoolSetupController :INotifyPropertyChanged
    {
        #region Fields
        private SchoolSetupModel _SchoolSetup;
        private ICommand _setupSchoolCommand;
        private ICommand _minimizeCommand;
        private ICommand _closeCommand;
        #endregion

        #region Constructor
        public SchoolSetupController()
        {
            _SchoolSetup = new SchoolSetupModel();
            //Initialize  Commands
            _setupSchoolCommand = new RelayCommand(SetupSchool, CanSetupSchool);
            _closeCommand = new RelayCommand(CloseLogin, CanClose);
            _minimizeCommand = new RelayCommand(MinimizeLogin, CanMinimize);
        }

        #endregion

        #region Properties

        public Window Window
        {
            get; set;   
        }
        public SchoolSetupModel SchoolSetup
        {
            get
            {
                return _SchoolSetup;
            }
            set
            {
                _SchoolSetup = value;
            }
        }
        #endregion

        #region SetupSchoolCommand
        public ICommand SetupSchoolCommand
        {
            get { return _setupSchoolCommand; }        
        }

      
        public bool CanSetupSchool(object obj)
        {
            return !SchoolSetup.IsSetupInProgress && !string.IsNullOrEmpty(SchoolSetup.Key);
        }

        public void SetupSchool(object obj)
        {
            try
            {
                SchoolSetup.IsSetupInProgress = true;
                
                BackgroundWorker objBackgroundWorker = new BackgroundWorker();

                // Configure the function that will run when started
                objBackgroundWorker.DoWork += new DoWorkEventHandler(Setup);

                /*The progress reporting is not needed with this implementation and is therefore
                commented out.  However, if our School App Grows into a more complex application, we may have a use for
                for this.

                //Enable progress and configure the progress function
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                */

                // Configure the function to run when completed
                objBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SetupCompleted);

                // Launch the sync               
                objBackgroundWorker.RunWorkerAsync();

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


        public void CloseLogin(object obj)
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


        public void MinimizeLogin(object obj)
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

        #region Private Functions
        private void Setup(object sender, DoWorkEventArgs e)
        {
            try
            {
                //BackgroundWorker worker = sender as BackgroundWorker;

                SchoolSetup.SetupStatus = SchoolSetupNotifications.CheckingInternetConnection;
                if (!GeneralMethods.IsInternetAvailable())
                {
                    SchoolSetup.SetupStatus = SchoolSetupNotifications.InternetNotAvailable;
                    return;
                }
                SchoolSetup.SetupStatus = SchoolSetupNotifications.SetupStarted;
                Thread.Sleep(100);
                SchoolSetup.SchoolInfo = new SchoolModel();
                //get School Info from online API
                //.....
                //SchoolSetupManager.SetSchooInfo(SchoolSetup.SchoolInfo);
                //....

                SchoolSetup.SetupStatus = SchoolSetupNotifications.SetupCompleted;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }


        }

        void SetupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                SchoolSetup.IsSetupInProgress = false;
                if (SchoolSetup.SetupStatus == SchoolSetupNotifications.InternetNotAvailable)
                    GeneralMethods.ShowNotification("Internet Not Available!", "Please check your Internet Connection!", true);
                else if (SchoolSetup.SetupStatus == SchoolSetupNotifications.SetupCompleted)
                {
                    CreateSchoolGlobalObject();

                    Main winMain = new Main();
                    winMain.Show();
                    Window.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        private void CreateSchoolGlobalObject()
        {
            //Maintain state of School Info
            SchoolSetup.SchoolInfo = SchoolSetupManager.GetSchooInfo();
            GeneralMethods.CreateGlobalObject(GlobalObjects.SchoolInfo, SchoolSetup.SchoolInfo);
        }
        #endregion

    }
}

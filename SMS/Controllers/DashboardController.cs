using SMS.Shared;
using SMS_Businness_Layer.Businness;
using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;

namespace SMS.Controllers
{
    public class DashboardController : NotifyPropertyChanged
    {
        private DashboardModel _Dashboard;

        public DashboardModel Dashboard
        {
            get { return _Dashboard; }
            set
            {
                _Dashboard = value;
                OnPropertyChanged("Dashboard");
            }
        }
        
        public DashboardController()
        {
            this.Dashboard = new DashboardModel()
            {
                StudentGenderRatioWidget = new StudentGenderRatioWidgetModel(){ Widget = new WidgetModel() },
                StudentPaymentAsPerMonthWidget = new StudentPaymentAsPerMonthWidgetModel() { Widget = new WidgetModel() },
            };          
        }

        #region Private Functions
        public void SetupStudentRatioWidget()
        {
            try
            {
                List<Keyvalue> tempList = new List<Keyvalue>();
                Dashboard.StudentGenderRatioWidget.Widget.DataList = DashboardManager.GetStudentGenderRatio();
                Chart dynamicChart = new Chart() { Height = 280};
                PieSeries series = new PieSeries();
                series.ItemsSource = DashboardManager.GetStudentGenderRatio();
                series.DependentValuePath = "Value";
                series.IndependentValuePath = "Key";
                dynamicChart.Series.Add(series);
                Dashboard.StudentGenderRatioWidget.GBStudentGenderRatioWidget.Content = dynamicChart;
            }
            catch(Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }
            
        }
        public void SetupStudentPaymentAsPerMonthWidget()
        {
            try
            {
                List<Keyvalue> tempList = new List<Keyvalue>();
                Dashboard.StudentPaymentAsPerMonthWidget.Widget.DataList = DashboardManager.GetStudentPaymentsAsPerMonthRatio();
                Chart dynamicChart = new Chart() { Height = 280 };
                ColumnSeries series = new ColumnSeries();

                Style styleLegand = new Style { TargetType = typeof(Control) };
                styleLegand.Setters.Add(new Setter(Control.WidthProperty, 0d));
                styleLegand.Setters.Add(new Setter(Control.HeightProperty, 0d));
                dynamicChart.LegendStyle = styleLegand;

                series.ItemsSource = DashboardManager.GetStudentPaymentsAsPerMonthRatio();
                series.DependentValuePath = "Value";
                series.IndependentValuePath = "Key";
                dynamicChart.Series.Add(series);
                Dashboard.StudentPaymentAsPerMonthWidget.GBStudentPaymentAsPerMonthWidget.Content = dynamicChart;
            }
            catch (Exception ex)
            {
                var errorMessage = "Please notify about the error to Admin \n\nERROR : " + ex.Message + "\n\nSTACK TRACE : " + ex.StackTrace;
                GeneralMethods.ShowDialog("Error", errorMessage, true);
            }
            finally
            {

            }

        }
        #endregion

    }
}

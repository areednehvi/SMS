using Prism.Mvvm;
using SMS_Models.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;

namespace SMS.Controllers
{
    class TestViewModel : BindableBase
    {
        private TestModel testModel = new TestModel();
        public TestViewModel(GroupBox GroupBoxDynamicChart)
        {
            List<Keyvalue> tempList = new List<Keyvalue>();
            tempList.Add(new Keyvalue() { Key = "jan", Value = 50 });
            tempList.Add(new Keyvalue() { Key = "feb", Value = 18 });
            tempList.Add(new Keyvalue() { Key = "march", Value = 25 });
            tempList.Add(new Keyvalue() { Key = "april", Value = 5 });

            //Bind the staic Chart                     
            TestModel.DataList = tempList;
            //Create the Dynamic Chart, Bind it and add it into the 2nd GroupBox
            Chart dynamicChart = new Chart() { Height = 300, Width = 400 };
            BarSeries pieSeries = new BarSeries();
            pieSeries.ItemsSource = tempList;
            pieSeries.DependentValuePath = "Value";
            pieSeries.IndependentValuePath = "Key";
            dynamicChart.Series.Add(pieSeries);
            GroupBoxDynamicChart.Content = dynamicChart;
        }
        public TestModel TestModel
        {
            get { return testModel; }
            set { SetProperty(ref testModel, value); }
        }

    }
}

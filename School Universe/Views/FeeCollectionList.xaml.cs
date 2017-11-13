using School_Universe.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace School_Universe.Views
{
    /// <summary>
    /// Interaction logic for FeeCollectionList.xaml
    /// </summary>
    public partial class FeeCollectionList : UserControl
    {
        public FeeCollectionList()
        {
            InitializeComponent();
            double height = SystemParameters.PrimaryScreenHeight - 250;
            GridLength gl = new GridLength(height);
            grdRowNo2.Height = gl;
            ((FeeCollectionListController)grdFeeCollectionList.DataContext).FeeCollectionListDataGrid = this.dataGridFeeCollectionList;
            //((FeeCollectionListController)grdFeeCollectionList.DataContext).Window = this;
        }
    }
}

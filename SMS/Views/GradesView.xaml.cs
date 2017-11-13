using SMS.Controllers;
using SMS.Models;
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

namespace SMS.Views
{
    /// <summary>
    /// </summary>
    public partial class GradesView : Window
    {
        public GradesView()
        {
            InitializeComponent();
            //((FeeCollectController)grdPaymentHistory.DataContext).PaymentHistorListDataGrid = this.dataGridPaymentHistoryList;
            ((GradesViewController)dpGradesView.DataContext).Window = this;
        }
        public GradesView(FeeCollectionStudentListModel objFeeCollectionStudentList)
        {
            InitializeComponent();
            ((GradesViewController)dpGradesView.DataContext).Window = this;
            //((FeeCollectController)grdPaymentHistory.DataContext).PaymentHistorListDataGrid = this.dataGridPaymentHistoryList;
            //((FeeCollectController)grdPaymentHistory.DataContext).FeeDueListDataGrid = this.dataGridFeeDueList;
            //((FeeCollectController)grdPaymentHistory.DataContext).PendingMonthlyFeesMaterialDesignCard = this.mdPendingMonthlyFees;
            //((FeeCollectController)grdPaymentHistory.DataContext).MakePaymentScreenMaterialDesignCard = this.mdMakePaymentScreen;
            //((FeeCollectController)grdPaymentHistory.DataContext).FeeCollectionStudentList = objFeeCollectionStudentList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //((FeeCollectController)grdPaymentHistory.DataContext).SelectedTabItem = 0;
        }

        private void chkbxSelectedFeeBalance_Click(object sender, RoutedEventArgs e)
        {
            //((FeeCollectController)grdPaymentHistory.DataContext).CalculateSumOfSelectedFeesAndPopulateSelectedFeeListForMakePayment();
        }
    }
}

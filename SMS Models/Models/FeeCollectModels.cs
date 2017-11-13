using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Models
{
    public class PendingMonthlyFeeModel : INotifyPropertyChanged
    {
        private Boolean _IsSelected;
        public string Period { get; set; }
        public Double Total { get; set; }        
        public ObservableCollection<FeeBalancesModel> FeeBalancesList { get; set; }
        public Boolean IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
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
    public class FeeBalancesModel : INotifyPropertyChanged 
    {
        private Boolean _IsSelected;
        private Double _fine;
        private Double _balance_amount;
        public string id { get; set; } // StudentFeeID
        public DateTime apply_from { get; set; }
        public int last_day { get; set; }
        public Double fine_per_day { get; set; }
        public string fees_category { get; set; }
        public string student_id { get; set; }
        public string fees_id { get; set; }
        public Double fee_amount { get; set;}
        public string payment_mode { get; set; }
        public DateTime payment_date { get; set; }
        public string recept_no { get; set; }
        public string comment { get; set; }
        public Double payment_amount { get; set; }
        public Double payment_fine { get; set; }
        public Double fine
        {
            get
            {
                return _fine;
            }
            set
            {
                _fine = value;
                balance_amount = amount_to_pay - concession_amount + fine;                 
                OnPropertyChanged("fine");
            }
        }
        public Double concession_amount { get; set; }
        public Double amount_to_pay { get; set; }
        public Double fine_to_pay { get; set; }
        public Double paid_amount { get; set; }
        public Double fine_paid { get; set; }
        public Double balance_amount
        {
            get
            {
                return _balance_amount;
            }
            set
            {
                _balance_amount = value;
                OnPropertyChanged("balance_amount");
            }
        }
        public string period { get; set; }
        public Boolean IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
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
    public class PaymentModel : INotifyPropertyChanged // represents mostly student_payments Table
    {
        private string _comment;
        private string _recept_no;
        private Double _amount;
        private Double _fine;
        private DateTime _payment_date;
        private string _payment_mode;

        public string id { get; set; }
        public Guid id_offline { get; set; }
        public string school_id { get; set; }
        public string student_fees_id { get; set; }
        public Double amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged("amount");
            }
        }
        public Double fine
        {
            get
            {
                return _fine;
            }
            set
            {
                _fine = value;
                OnPropertyChanged("fine");
            }
        }
        public string recept_no
        {
            get
            {
                return _recept_no;
            }
            set
            {
                _recept_no = value;
                OnPropertyChanged("recept_no");
            }
        }
        public string payment_mode
        {
            get
            {
                return _payment_mode;
            }
            set
            {
                _payment_mode = value;
                OnPropertyChanged("payment_mode");
            }
        }
        public DateTime payment_date
        {
            get
            {
                return _payment_date;
            }
            set
            {
                _payment_date = value;
                OnPropertyChanged("payment_date");
            }
        }
        public Double concession_amount { get; set; }
        public string month { get; set; }
        public DateTime apply_from { get; set; }
        public DateTime apply_to { get; set; }
        public Double fee_amount { get; set; }
        public string category_name { get; set; }
        public string ip { get; set; }
        public string comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged("comment");
            }
        }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }

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

    public class FeeDueModel : INotifyPropertyChanged // Represents mostly student_fees Table
    {
        private Boolean _IsSelected;
        public string id { get; set; }
        public string student_id { get; set; }
        public Double student_balance { get; set; }
        public Double concession_amount { get; set; }
        public DateTime apply_from { get; set; }
        public DateTime apply_to { get; set; }
        public Double fine { get; set; }
        public string category_name { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
        public Boolean IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
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
    public class FeeCollectOtherFieldsModel : INotifyPropertyChanged
    {
        private string _pageNo;

        public string PageNo
        {
            get
            {
                return _pageNo;
            }
            set
            {
                _pageNo = value;
                OnPropertyChanged("PageNo");
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

    public class FeeDueFormFieldsModel : INotifyPropertyChanged
    {
        private Double _fine;
        private Double _concession;
        private float _concessionPercentage;

        public Double Fine
        {
            get
            {
                return _fine;
            }
            set
            {
                _fine = value;
                OnPropertyChanged("Fine");
            }
        }

        public Double Concession
        {
            get
            {
                return _concession;
            }
            set
            {
                _concession = value;
                OnPropertyChanged("Concession");
            }
        }

        public float ConcessionPercentage
        {
            get
            {
                return _concessionPercentage;
            }
            set
            {
                _concessionPercentage = value;
                OnPropertyChanged("ConcessionPercentage");
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

    public class FeeDueListFiltersModel : INotifyPropertyChanged
    {
        private FeeCategoryModel _FeeCategory;
        private ObservableCollection<FeeCategoryModel> _FeeCategoryList;


        public FeeCategoryModel FeeCategory
        {
            get
            {
                return _FeeCategory;
            }
            set
            {
                _FeeCategory = value;
                OnPropertyChanged("FeeCategory");
            }
        }

        public ObservableCollection<FeeCategoryModel> FeeCategoryList
        {
            get
            {
                return _FeeCategoryList;
            }
            set
            {
                _FeeCategoryList = value;
                OnPropertyChanged("FeeCategoryList");
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

    public class FeeCategoryModel
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class PaymentModeModel
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class MakePaymentModel :INotifyPropertyChanged
    {
        private ObservableCollection<FeeBalancesModel> _SelectedFeeBalances;
        private PaymentModeModel _SelectedPaymentMode;
        private Double _TotalOfSelectedFeeBalancesFee;
        private Double _TotalOfSelectedFeeBalancesAmount;
        private Double _TotalOfAlreadyPaidAmount;
        private Double _TotalBalance;
        private Double _TotalOfSelectedFeeBalancesFine;
        private Double _TotalOfSelectedFeeBalancesGrandTotal;
        private Double _TotalOfSelectedFeeBalancesConcession;
        public PaymentModel Payment{ get; set;}
        public PaymentModeModel SelectedPaymentMode
        {
            get { return _SelectedPaymentMode; }
            set
            {
                _SelectedPaymentMode = value;
                OnPropertyChanged("SelectedPaymentMode");
            }
        }
        public ObservableCollection<FeeBalancesModel> SelectedFeeBalances
        {
            get
            {
                return _SelectedFeeBalances;
            }
            set
            {
                _SelectedFeeBalances = value;
                OnPropertyChanged("SelectedFeeBalances");
            }
        }
        public Double TotalOfSelectedFeeBalancesFee
        {
            get
            {
                return _TotalOfSelectedFeeBalancesFee;
            }
            set
            {
                _TotalOfSelectedFeeBalancesFee = value;
                OnPropertyChanged("TotalOfSelectedFeeBalancesFee");
            }
        }
        public Double TotalOfSelectedFeeBalancesFine
        {
            get
            {
                return _TotalOfSelectedFeeBalancesFine;
            }
            set
            {
                _TotalOfSelectedFeeBalancesFine = value;
                OnPropertyChanged("TotalOfSelectedFeeBalancesFine");
            }
        }
        public Double TotalOfSelectedFeeBalancesConcession
        {
            get
            {
                return _TotalOfSelectedFeeBalancesConcession;
            }
            set
            {
                _TotalOfSelectedFeeBalancesConcession = value;
                OnPropertyChanged("TotalOfSelectedFeeBalancesConcession");
            }
        }
        public Double TotalOfSelectedFeeBalancesGrandTotal
        {
            get
            {
                return _TotalOfSelectedFeeBalancesGrandTotal;
            }
            set
            {
                _TotalOfSelectedFeeBalancesGrandTotal = value;
                OnPropertyChanged("TotalOfSelectedFeeBalancesGrandTotal");
            }
        }
        public Double TotalOfSelectedFeeBalancesAmount
        {
            get
            {
                return _TotalOfSelectedFeeBalancesAmount;
            }
            set
            {
                _TotalOfSelectedFeeBalancesAmount = value;
                OnPropertyChanged("TotalOfSelectedFeeBalancesAmount");
            }
        }
        public Double TotalOfAlreadyPaidAmount
        {
            get
            {
                return _TotalOfAlreadyPaidAmount;
            }
            set
            {
                _TotalOfAlreadyPaidAmount = value;
                OnPropertyChanged("TotalOfAlreadyPaidAmount");
            }
        }
        public Double TotalBalance
        {
            get
            {
                return _TotalBalance;
            }
            set
            {
                _TotalBalance = value;
                OnPropertyChanged("TotalBalance");
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

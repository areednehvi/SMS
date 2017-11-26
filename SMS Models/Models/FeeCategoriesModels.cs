﻿using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using static SMS_Models.Models.DBModels;

namespace SMS.Models
{
    public class FeeCategoriesModel :NotifyPropertyChanged
    {
        private FeeCategoriesListModel _SelectedItemInFeeCategoriesList;
        private FeeCategoriesListModel _FeeCategory;
        private ObservableCollection<FeeCategoriesListModel> _FeeCategoriesList;
        private LoginModel _CurrentLogin;
        private SchoolModel _SchoolInfo;
        private PasswordBox _PasswordBox;
        private string _ListVisibility;
        private string _FormVisibility;
        private string _PageNo;
        private string _NoRecordsFound;


        public int NoOfRecords{get; set;}
        public int fromRowNo { get; set; }
        public int pageNo { get; set; }
        public int NoOfRecordsPerPage { get; set; }
        public int toRowNo { get; set; }
        public ObservableCollection<FeeCategoriesListModel> FeeCategoriesList
        {
            get
            {
                return _FeeCategoriesList;
            }
            set
            {
                _FeeCategoriesList = value;
                OnPropertyChanged("FeeCategoriesList");
            }
        }
        public string ListVisibility
        {
            get
            {
                return _ListVisibility;
            }
            set
            {
                _ListVisibility = value;
                OnPropertyChanged("ListVisibility");
            }
        }
        public string FormVisibility
        {
            get
            {
                return _FormVisibility;
            }
            set
            {
                _FormVisibility = value;
                OnPropertyChanged("FormVisibility");
            }
        }
        public PasswordBox PasswordBox
        {
            get
            {
                return _PasswordBox;
            }
            set
            {
                _PasswordBox = value;
                OnPropertyChanged("PasswordBox");
            }
        }
        public string PageNo
        {
            get
            {
                return _PageNo;
            }
            set
            {
                _PageNo = value;
                OnPropertyChanged("PageNo");
            }
        }
        public string NoRecordsFound
        {
            get
            {
                return _NoRecordsFound;
            }
            set
            {
                _NoRecordsFound = value;
                OnPropertyChanged("NoRecordsFound");
            }
        }

        public FeeCategoriesListModel SelectedItemInFeeCategoriesList
        {
            get
            {
                return _SelectedItemInFeeCategoriesList;
            }
            set
            {
                _SelectedItemInFeeCategoriesList = value;
                OnPropertyChanged("SelectedItemInFeeCategoriesList");
            }
        }
        public FeeCategoriesListModel FeeCategory
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

        
    }

    public class FeeCategoriesListModel : fee_categoriesModel
    {
        public string CreatedBy { get; set; }
    }


}

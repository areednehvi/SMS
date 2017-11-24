﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Models
{
    public class FeeCollectionStudentListModel
    {
        public string id_offline { get; set; } // student ID
        public string school_id { get; set; }
        public string registration_id { get; set; }
        public string status { get; set; }
        public string file_id { get; set; }
        public string session_id { get; set; }
        public string fees_id { get; set; }
        public string grade_fees_id { get; set; }
        public DateTime? apply_from { get; set; }
        public DateTime? apply_to { get; set; }
        public Double concession_amount { get; set; }
        public Double fine { get; set; }
        public string grade_id { get; set; }
        public string section_id { get; set; }
        public string full_name { get; set; }
        public string roll_number { get; set; }
        public string section_name { get; set; }
        public string order { get; set; }
        public string grade_section { get; set; }
        public Int64 allocated_fee_cource_count { get; set; }
        public string parentage { get; set; }
    }
    public class FeeCollectionListFiltersModel : INotifyPropertyChanged
    {
        private GradesModel _grade;
        private SectionsModel _section;
        private string _rollNumber;
        private string _registrationID;
        private string _concessionAmount;
        private ObservableCollection<GradesModel> _objGradesModelList;
        private ObservableCollection<SectionsModel> _objSectionModelList;

        public GradesModel Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                _grade = value;
                OnPropertyChanged("Grade");
            }
        }


        public SectionsModel Section
        {
            get
            {
                return _section;
            }
            set
            {
                _section = value;
                OnPropertyChanged("Section");
            }

        }

        public string RollNumber
        {
            get
            {
                return _rollNumber;
            }
            set
            {
                _rollNumber = value;
                OnPropertyChanged("RollNumber");
            }

        }

        public string RegistrationID
        {
            get
            {
                return _registrationID;
            }
            set
            {
                _registrationID = value;
                OnPropertyChanged("RegistrationID");
            }

        }

        public string ConcessionAmount
        {
            get
            {
                return _concessionAmount;
            }
            set
            {
                _concessionAmount = value;
                OnPropertyChanged("ConcessionAmount");
            }

        }

        public ObservableCollection<GradesModel> GradesList
        {
            get
            {
                return _objGradesModelList;
            }
            set
            {
                _objGradesModelList = value;
                OnPropertyChanged("GradesList");
            }
        }

        public ObservableCollection<SectionsModel> SectionsList
        {
            get
            {
                return _objSectionModelList;
            }
            set
            {
                _objSectionModelList = value;
                OnPropertyChanged("SectionsList");
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

    public class FeeCollectionListOtherFiledsModel : INotifyPropertyChanged
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

    public class GradesModel
    {
        public string id_offline { get; set; }
        public string id_online { get; set; }
        public string name { get; set; }
    }

    public class SectionsModel
    {
        public string id_offline { get; set; }
        public string id_online { get; set; }
        public string name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace SMS.Models
{
    public class GradesSetupModel : INotifyPropertyChanged
    {
        private ObservableCollection<GradesListModel> _GradesList;

        public ObservableCollection<GradesListModel> GradesList
        {
            get
            {
                return _GradesList;
            }
            set
            {
                _GradesList = value;
                OnPropertyChanged("GradesList");
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

    public class GradesListModel
    {
        public string id_offline { get; set; }
        public string id_online { get; set; }
        public string school_id { get; set; }
        public string block { get; set; }
        public string name { get; set; }
        public string order { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
        public string createdBy { get; set; }
    }


}

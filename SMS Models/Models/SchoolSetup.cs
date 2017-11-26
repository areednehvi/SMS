using SMS_Models.Models;
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
    public class SchoolSetupModel :NotifyPropertyChanged
    {
        private SchoolModel _SchoolInfo;

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
    


}

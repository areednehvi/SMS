﻿using SMS_Models.Models;

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

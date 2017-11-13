using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Universe.Models
{
    public class SchoolModel 
    {     
        public string id { get; set; }
        public string database_id { get; set; }
        public string subdomain { get; set; }
        public string domain { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string theme { get; set; }
        public DateTime created_on { get; set; }
    }
}

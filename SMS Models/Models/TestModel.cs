using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace SMS_Models.Models
{
    public class TestModel : BindableBase
    {
        private List<Keyvalue> _DataList;
        public List<Keyvalue> DataList { get { return _DataList; } set { SetProperty(ref _DataList, value); } }
    }
    public class Keyvalue : BindableBase
    {
        private string _Key;
        public string Key { get { return _Key; } set { SetProperty(ref _Key, value); } }

        private int _Value;
        public int Value { get { return _Value; } set { SetProperty(ref _Value, value); } }
    }
}

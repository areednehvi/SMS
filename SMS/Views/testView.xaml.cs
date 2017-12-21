﻿using SMS.Controllers;
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
using System.Windows.Shapes;

namespace SMS.Views
{
    /// <summary>
    /// Interaction logic for testView.xaml
    /// </summary>
    public partial class testView : Window
    {
        public testView()
        {
            InitializeComponent();
            this.DataContext = new TestViewModel(GroupBoxDynamicChart);
        }
    }
}

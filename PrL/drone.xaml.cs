﻿using System;
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

namespace PrL
{
    /// <summary>
    /// Interaction logic for drone.xaml
    /// </summary>
    public partial class drone : Window
    {
        IBL.BO.BL bl = new();
        public drone(IBL.BO.BL main_bl)
        {
            InitializeComponent();
            bl = main_bl;
        }

    }
}

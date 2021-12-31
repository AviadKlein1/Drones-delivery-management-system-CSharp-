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
using MahApps.Metro.Controls;
using MahApps.Metro.Accessibility;
using BlApi;
using ConsoleUI_BL;
using System.Diagnostics;
using ControlzEx.Theming;
using System.Runtime.InteropServices;


namespace PrL
{
    /// <summary>
    /// Interaction logic for UserCustomer.xaml
    /// </summary>
    public partial class UserCustomer : Window
    {
        string UserName;
        BlApi.BO.BL bl;
        public static BlApi.BO.Customer customer = new();

        public UserCustomer(string UserName, IBl _bl)
        {
            InitializeComponent();
            bl = (BlApi.BO.BL)_bl;

        }

        private void SubmitDitails_Click(object sender, RoutedEventArgs e)
        {
            customer.Id = int.Parse(ID.Text);
            customer.Name = (string)Name.Text;
            customer.PhoneNumber = (string)phoneNumber.Text;
            customer.Location.Latitude = double.Parse(Latitude.Text);
            customer.Location.Longitude = double.Parse(Longitude.Text);
            bl.Addcustomer(customer);
            MessageBox.Show("Success!");
            new User(customer.Id, bl).Show();
            Close();
        }
    }
}

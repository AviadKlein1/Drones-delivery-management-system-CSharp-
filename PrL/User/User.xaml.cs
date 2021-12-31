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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : MetroWindow
    {
        int Id;
        BlApi.BO.BL bl;
        public static BlApi.BO.Customer customer = new();
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        string name;
        private void Timer_Click(object sender, EventArgs e)
        {
            customer = bl.DisplayCustomer(Id);
        }
        public User(int UserId , IBl _bl )
        {
            Id = UserId;
            InitializeComponent();
            bl = (BlApi.BO.BL)_bl;

            customer = bl.DisplayCustomer(UserId);
            name = customer.Name;
            UserParcelRecievedComboBox.ItemsSource = customer.ParcelsRecieved.Select(item => item.Id);
            UserParcelRecievedComboBox.Items.ToString();
            UserParcelSentComboBox.ItemsSource = customer.ParcelsSent.Select(item => item.Id);
            UserParcelSentComboBox.Items.ToString();

            DisplayUserCustomer.DataContext = customer;
            double minLat = ((double)(customer.Location.Latitude - (int)customer.Location.Latitude) * 60);
            double minLon = ((double)(customer.Location.Longitude - (int)customer.Location.Longitude) * 60);
            double secLat = ((double)(minLat - (int)minLat) * 60);
            double secLon = ((double)(minLon - (int)minLon) * 60);
            UserCustomerLocationBox.Text = $"{ (int)customer.Location.Latitude }° { (int)minLat }' { (int)secLat}\" N { (int)customer.Location.Longitude }° {(int)minLon}' {(int)secLon}\" E";

        }

        private void UserAddParcel_Click(object sender, RoutedEventArgs e)
        {
            new UserAddParcel(Id, bl).Show();
        }

        private void UserUpdatNameAndPhone_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in LoginScreen.accounts)
            {
                if (item.UserName == name)
                    item.UserName = (string)UserNameBox.Text;
            }
            customer.Name = (string)UserNameBox.Text;
            customer.PhoneNumber = (string)UserPhoneBox.Text;
            bl.UpdateCustomer(customer.Id, customer.Name, customer.PhoneNumber);
            
            
            MessageBox.Show("success!");
        }

        private void UserParcelRecievedComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(bl.DisplayParcel((int)UserParcelRecievedComboBox.SelectedItem).ToString());
        }

        private void UserParcelSentComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(bl.DisplayParcel((int)UserParcelSentComboBox.SelectedItem).ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

namespace PrL
{
    public partial class Customer : MetroWindow
    {
        BlApi.BO.Customer customer = new();
        BlApi.BO.CustomerToList customerToList = new();
        BlApi.BO.BL bl;

        public Customer(BlApi.BO.BL mainBl)
        {
            InitializeComponent();
            Title = "Add new Customer";
            bl = mainBl;
            AddNewCustomer.Visibility = Visibility.Visible;

        }
        public Customer(BlApi.BO.BL mainBl, BlApi.BO.CustomerToList mainDrone)
        {
            ThemeManager.Current.ChangeTheme(this, "Dark.blue");
            InitializeComponent();
            Title = "Customers Diatels";
            bl = mainBl;
            customerToList = mainDrone;
            customer = bl.DisplayCustomer(customerToList.Id);
            var v = bl.DisplayCustomer(customerToList.Id).ParcelsSent.Select(item => item.Id + " ");
            ParcelSentBox.Text = $"{v}";
            ParcelRecievedComboBox.ItemsSource = bl.DisplayCustomer(customerToList.Id).ParcelsRecieved;




            DisplayCustomer.DataContext = customer;
            DisplayCustomer.Visibility = Visibility.Visible;
            double minLat = ((double)(customer.Location.Latitude - (int)customer.Location.Latitude) * 60);
            double minLon = ((double)(customer.Location.Longitude - (int)customer.Location.Longitude) * 60);
            double secLat = ((double)(minLat - (int)minLat) * 60);
            double secLon = ((double)(minLon - (int)minLon) * 60);
            CustomerLocationBox.Text = $"{ (int)customer.Location.Latitude }° { (int)minLat }' { (int)secLat}\" N { (int)customer.Location.Longitude }° {(int)minLon}' {(int)secLon}\" E";
        }


        private void SubmitCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                customer.Id= int.Parse(AddCustomerIdBox.Text);
                customer.Name = (string)AddCustomerNameBox.Text;
                customer.PhoneNumber = (string)AddCustomerPhoneNumberBox.Text;
                customer.Location.Latitude = double.Parse(AddCustomerLattiudeBox.Text);
                customer.Location.Longitude = double.Parse(AddCustomerLongitudeBox.Text);
                bl.Addcustomer(customer);
            }
            catch (System.NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            MessageBox.Show("success!");
            Close();
        }
        private void CancelButton3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdatNameAndPhone_Click(object sender, RoutedEventArgs e)
        {
            customerToList.Name = (string)NameBox.Text;
            customerToList.PhoneNumber = (string)PhoneBox.Text;
            bl.UpdateCustomer(customerToList.Id, customerToList.Name, customerToList.PhoneNumber);
            
            MessageBox.Show("success!");
            Close();
        }
    }
}

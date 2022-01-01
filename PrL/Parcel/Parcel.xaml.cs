using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

namespace PrL
{
    public partial class Parcel : MetroWindow
    {
        BlApi.BO.Parcel parcel = new();
        BlApi.BO.ParcelToList parcelToList = new();
        BlApi.BO.BL bl;
        bool IsScheduled = false;
        int droneId = 0;
        string empty = "---";
        public Parcel(BlApi.BO.BL mainBl)
        {
            InitializeComponent();
            Title = "Add new Parcel";
            bl = mainBl;
            AddSenderIdComboBox.ItemsSource = bl.GetCustomersList(BlApi.BO.BL.AllCustomers).Select(item => item.Id + " " + item.Name);
            AddSenderIdComboBox.Items.ToString();
            AddReceiverIsComboBox.ItemsSource = bl.GetCustomersList(BlApi.BO.BL.AllCustomers).Select(item => item.Id + " " + item.Name);
            AddReceiverIsComboBox.Items.ToString();
            AddWeightselectorComboBox.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.WeightCategory));
            AddPriorityComboBox.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.PriorityLevel));

            AddNewParcel.Visibility = Visibility.Visible;

        }
        public Parcel(BlApi.BO.BL mainBl, BlApi.BO.ParcelToList mainParcel)
        {
            ThemeManager.Current.ChangeTheme(this, "Dark.blue");
            InitializeComponent();
            Title = "Parcel Diatels";
            bl = mainBl;
            parcelToList = mainParcel;
            parcel = bl.DisplayParcel(parcelToList.Id);
            DisplayParcel.Visibility = Visibility.Visible;
            if (parcel.DroneInParcel.Id != 0) IsScheduled = true;
            var v = bl.GetDrones();
            foreach (var item in v)
            {
                if (item.DeliveredParcelId == parcel.Id)droneId = item.Id;
            }
            if(IsScheduled)droneId = bl.DisplayDrone(parcel.DroneInParcel.Id).Id;


            try
            {
                RecieverNameBox.Text = bl.DisplayCustomer(parcel.Sender.Id).Name;
                SenderNameBox.Text = bl.DisplayCustomer(parcel.Receiver.Id).Name;
                DisplayParcel.DataContext = parcel;
                if (IsScheduled) DroneInParcelIdBox.Text = $"{droneId}";
                else DroneInParcelIdBox.Text = empty;
                DisplayParcel.DataContext = parcel;
                if (parcel.Scheduled != null && parcel.PickedUp == null)
                {
                    DeleteParcel.Visibility = Visibility.Collapsed;
                    PickupButton.Visibility = Visibility.Visible;
                }
                if (parcel.PickedUp != null && parcel.Delivered == null)
                {
                    DeleteParcel.Visibility = Visibility.Collapsed;

                    PickupButton.Visibility = Visibility.Collapsed;
                    DeliverButton.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
           
        }
        private void CancelButton3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void SubmitParcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var CustomersId = bl.GetCustomersList(BlApi.BO.BL.AllCustomers).Select(item => item.Id);
                parcel.Sender.Id = CustomersId.ElementAt(AddSenderIdComboBox.SelectedIndex);
                parcel.Receiver.Id = CustomersId.ElementAt(AddReceiverIsComboBox.SelectedIndex);
                parcel.Weight = (DalApi.DO.MyEnums.WeightCategory)AddWeightselectorComboBox.SelectedItem;
                parcel.Priority = (DalApi.DO.MyEnums.PriorityLevel)AddPriorityComboBox.SelectedItem;
                bl.AddParcel(parcel);
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

        private void DeleteParcel_Click(object sender, RoutedEventArgs e)
        {
            bl.DeleteParcel(parcel);
            MessageBox.Show("success!");
            Close();
        }

        private void PickupButton_Click(object sender, RoutedEventArgs e)
        {
            bl.PickUpParcel(droneId);
            MessageBox.Show("success!");
            Close();
        }

        private void DeliverButton_Click(object sender, RoutedEventArgs e)
        {
            bl.DeliverParcel(droneId);
            MessageBox.Show("success!");
            Close();

        }
    }
}

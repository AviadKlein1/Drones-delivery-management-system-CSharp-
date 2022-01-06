using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

namespace PrL
{
    public partial class AddDrone: MetroWindow
    {
        BlApi.BO.BL bl;
        BlApi.BO.Drone drone = new();
        BlApi.BO.DroneToList droneToList = new();

        public AddDrone(BlApi.BO.BL mainBl)
        {
            ThemeManager.Current.ChangeTheme(this, "Light.blue");
            InitializeComponent();
            Title = "Add new drone";
            bl = mainBl;
            AddWeightselectorCombo.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.WeightCategory));
            var StationsNameId = bl.GetStationsList(BlApi.BO.BL.AllStations).Select(item => item.Id + " " + item.Name);
            AddIdOfFirstChargeSelectorCombo.ItemsSource = StationsNameId;
            AddNewDrone.Visibility = Visibility.Visible;
        }
        public AddDrone(BlApi.BO.BL mainBl, BlApi.BO.DroneToList mainDrone)
        {
            ThemeManager.Current.ChangeTheme(this, "light.blue");
            InitializeComponent();
            Title = "Drone Diatels";
            bl = mainBl;
            droneToList = mainDrone;
            DisplayDrone.DataContext = droneToList;
            DisplayDrone.Visibility = Visibility.Visible;

            double minLat = ((double)(droneToList.Location.Latitude - (int)droneToList.Location.Latitude) * 60);
            double minLon = ((double)(droneToList.Location.Longitude - (int)droneToList.Location.Longitude) * 60);
            double secLat = ((double)(minLat - (int)minLat) * 60);
            double secLon = ((double)(minLon - (int)minLon) * 60);

            LocationBox.Text = $"{ (int)droneToList.Location.Latitude }° { (int)minLat }' { (int)secLat}\" N { (int)droneToList.Location.Longitude }° {(int)minLon}' {(int)secLon}\" E";
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.available)
            {
                SendDroneToChargePanel.Visibility = Visibility.Visible;
                ScheduleParcelToDronePanel.Visibility = Visibility.Visible;
            }
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.maintenance)
            {
                EndChargePanel.Visibility = Visibility.Visible;
            }
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.delivery)
            {
                if (bl.PickedUpButNotDelivered(droneToList.DeliveredParcelId)) DeliverParcelPanel.Visibility = Visibility.Visible;
                else PickUpParcelPanel.Visibility = Visibility.Visible;
            }
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                drone.Id = int.Parse(AddDroneIdBox.Text);
                drone.Model = (string)AddDroneModelBox.Text;
                drone.Weight = (DalApi.DO.MyEnums.WeightCategory)AddWeightselectorCombo.SelectedItem;
                var StationsNameId = bl.GetStationsList(BlApi.BO.BL.AllStations).Select(item => item.Id);
                drone.FirstChargeStationId = StationsNameId.ElementAt(AddIdOfFirstChargeSelectorCombo.SelectedIndex);
                bl.AddDrone(drone);
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
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DeleteDrone_Click(object sender, RoutedEventArgs e)
        {
            
            bl.DeleteDrone(bl.DisplayDrone(droneToList.Id));
            Close();
        }

        private void UpdateModel_Click(object sender, RoutedEventArgs e)
        {
            droneToList.Model = (string)DroneModelBox.Text;
            bl.UpdateDrone(droneToList.Id, droneToList.Model);
            MessageBox.Show("success!");
            Close();
            
        }
        private void SendDroneToCharge_Click(object sender, RoutedEventArgs e)
        {
            if (bl.ChargeDrone(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("Faild!");
            Close();
        }

        private void EndCharge_Click(object sender, RoutedEventArgs e)
        {
            if (bl.ReleaseDroneFromCharge(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("failed!");
            Close();

        }

        private void ScheduleParcelToDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.ScheduleParcelToDrone(droneToList.Id);
            }
            catch(Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("success!");
            Close();

        }

        private void PickUpParcel_Click(object sender, RoutedEventArgs e)
        {
            if (bl.PickUpParcel(droneToList.Id))  MessageBox.Show("success!");
            else MessageBox.Show("failed!");
            Close();
        }

        private void DeliverParcel_Click(object sender, RoutedEventArgs e)
        {
            if (bl.DeliverParcel(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("Faild!");

            Close();

        }
        private void DeliveredParcelIdBox_DClick(object sender, RoutedEventArgs e)
        {
            var v = int.Parse(DeliveredParcelIdBox.Text);
            
            try
            {
                MessageBox.Show(bl.DisplayDeliveredParcel(droneToList.Id).ToString());
            }
            catch(Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
                return;
            }
        }
        
    }
}

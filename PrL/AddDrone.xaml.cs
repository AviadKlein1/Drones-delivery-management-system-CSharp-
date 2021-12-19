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
            
namespace PrL
{
    public partial class AddDrone : Window
    {
        BlApi.BO.BL bl;
        BlApi.BO.Drone drone = new();
        BlApi.BO.DroneToList droneToList = new();

        public AddDrone(BlApi.BO.BL mainBl)
        {
            InitializeComponent();
            bl = mainBl;
            AddWeightselectorCombo.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.WeightCategory));
            AddNewDrone.Visibility = Visibility.Visible;
        }
        public AddDrone(BlApi.BO.BL mainBl, BlApi.BO.DroneToList mainDrone)
        {
            InitializeComponent();

            bl = mainBl;
            droneToList = mainDrone;
            DisplayDrone.DataContext = droneToList;
            DisplayDrone.Visibility = Visibility.Visible;
            if(droneToList.Status == BlApi.BO.MyEnums.DroneStatus.available)
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
                if(bl.ScheduledButNotPickedUp(droneToList.DeliveredParcelId)) PickUpParcelPanel.Visibility = Visibility.Visible;
                if (bl.PickedUpButNotDelivered(droneToList.DeliveredParcelId)) DeliverParcelPanel.Visibility = Visibility.Visible;
            }
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                drone.Id = int.Parse(AddDroneIdBox.Text);
                drone.Model = (string)AddDroneModelBox.Text;
                drone.Weight = (DalApi.DO.MyEnums.WeightCategory)AddWeightselectorCombo.SelectedItem;
                drone.FirstChargeStationId = int.Parse(AddIdOfFirstChargeSlotBox.Text);
                bl.AddDrone(drone);
            }
            catch(Exception ex)
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

        private void UpdateModel_Click(object sender, RoutedEventArgs e)
        {
            droneToList.Model = (string)DroneModelBox.Text;
            bl.UpdateDrone(droneToList.Id, droneToList.Model);
            MessageBox.Show("success!");
        }

        private void SendDroneToCharge_Click(object sender, RoutedEventArgs e)
        {
            if (bl.ChargeDrone(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("Faild!");
        }

        private void EndCharge_Click(object sender, RoutedEventArgs e)
        {
            int chargeTime = int.Parse(ChrgeTimeBox.Text);
            if (bl.ReleaseDroneFromCharge(droneToList.Id, chargeTime)) MessageBox.Show("success!");
            else MessageBox.Show("failed!");
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
        }

        private void PickUpParcel_Click(object sender, RoutedEventArgs e)
        {
            if (bl.PickUpParcelByDrone(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("failed!");
        }

        private void DeliverParcel_Click(object sender, RoutedEventArgs e)
        {
            if (bl.DeliverParcelByDrone(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("Faild!");
        }
    }
}

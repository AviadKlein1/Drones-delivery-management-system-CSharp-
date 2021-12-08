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
        IBL.BO.BL bl = new();
        IBL.BO.Drone drone = new();
        IBL.BO.DroneToList droneToList = new();

        public AddDrone(IBL.BO.BL mainBl)
        {
            InitializeComponent();
            bl = mainBl;
            AddWeightselectorCombo.ItemsSource = Enum.GetValues(typeof(IDAL.DO.MyEnums.WeightCategory));
            AddNewDrone.Visibility = Visibility.Visible;
        }
        public AddDrone(IBL.BO.BL mainBl, IBL.BO.DroneToList mainDrone)
        {
            InitializeComponent();
            bl = mainBl;
            droneToList = mainDrone;
            DisplayDrone.DataContext = droneToList;
            DisplayDrone.Visibility = Visibility.Visible;
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            drone.Id = int.Parse(AddDroneIdBox.Text);
            drone.Model = (string)AddDroneModelBox.Text;
            drone.Weight = (IDAL.DO.MyEnums.WeightCategory)AddWeightselectorCombo.SelectedItem;
            drone.FirstChargeStationId = int.Parse(AddIdOfFirstChargeSlotBox.Text);
            bl.AddDrone(drone);
            MessageBox.Show("succsses!");
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
            MessageBox.Show("succsses!");
        }
    }
}

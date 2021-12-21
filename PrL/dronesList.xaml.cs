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
using MahApps.Metro.Controls;

using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
namespace PrL
{
    /// <summary>
    /// Interaction logic for dronesList.xaml
    /// </summary>
    public partial class dronesList : MetroWindow
    {
        BlApi.BO.BL bl;
        public dronesList(IBl mainBl)
        {
            InitializeComponent();
            bl = (BlApi.BO.BL)mainBl;
            dronesListView.ItemsSource = bl.GetDrones();

            weightSelector.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.WeightCategory));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BlApi.BO.MyEnums.DroneStatus));

        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.maintenance)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInMaintenance);
            if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.available)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInAvailable);
            if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.delivery)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInDelivery);
        }

        private void weightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.heavy)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInHeavy);
            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.medium)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInMedium);
            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.light)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInLight);
        }



        private void dclick(object sender, MouseButtonEventArgs e)
        {
           
            new AddDrone(bl, (BlApi.BO.DroneToList)dronesListView.SelectedItem).Show();
        }

        private void AddNewDrone_Click(object sender, RoutedEventArgs e)
        {
            new AddDrone(bl).Show();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            dronesListView.Items.Refresh();
           
        }
    }
}

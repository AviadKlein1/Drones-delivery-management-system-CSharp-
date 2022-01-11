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
using ControlzEx.Theming;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using BlApi;
namespace PrL
{
    /// <summary>
    /// Interaction logic for dronesList.xaml
    /// </summary>
    public partial class dronesList : MetroWindow
    {
        BlApi.BO.BL bl;
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        private void Timer_Click(object sender, EventArgs e)
        {
            dronesListView.ItemsSource = DronesFromBlToPl(bl.GetDrones());
            dronesListView.Items.Refresh();
        }
        public dronesList(IBl mainBl)
        {
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            InitializeComponent();
            ThemeManager.Current.ChangeTheme(this, "Light.blue");
            bl = (BlApi.BO.BL)mainBl;

            var v = DronesFromBlToPl(bl.GetDrones());
            dronesListView.ItemsSource = v;
            weightSelector.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.WeightCategory));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BlApi.BO.MyEnums.DroneStatus));
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.maintenance)) dronesListView.ItemsSource = DronesFromBlToPl(bl.GetDronesList(bl.allDronesInMaintenance));
            else if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.available)) dronesListView.ItemsSource = DronesFromBlToPl(bl.GetDronesList(bl.allDronesInAvailable));
            else if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.delivery)) dronesListView.ItemsSource = DronesFromBlToPl(bl.GetDronesList(bl.allDronesInDelivery));
        }
        private void weightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.heavy)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInHeavy);
            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.medium)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInMedium);
            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.light)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInLight);
        }
        private void dclick(object sender, MouseButtonEventArgs e)
        {
            Timer.Stop();
            try
            {
                new AddDrone(bl, (DroneModelToList)dronesListView.SelectedItem).Show();
            }
            catch (Exception) { }
        }
        private void AddNewDrone_Click(object sender, RoutedEventArgs e)
        {
            new AddDrone(bl).Show();
        }
        private void AddGrouping(string header)
        {
            if (dronesListView.ItemsSource == null) return;
            dronesListView.Items.SortDescriptions.Clear();
            if (header == $"Id") dronesListView.Items.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            if (header == $"Battery") dronesListView.Items.SortDescriptions.Add(new SortDescription("Battery", ListSortDirection.Ascending));
            if (header == $"Status") dronesListView.Items.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
            if (header == $"Weight") dronesListView.Items.SortDescriptions.Add(new SortDescription("Weight", ListSortDirection.Ascending));
            else return;
        }
        private void dronesListView_Click(object sender, RoutedEventArgs e)
        {
            AddGrouping(((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString());
        }
        private void StatusSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dronesListView.ItemsSource = DronesFromBlToPl(bl.GetDrones());
        }
        private void weightSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dronesListView.ItemsSource = DronesFromBlToPl(bl.GetDrones());
        }
        public SolidColorBrush percentToColor(double battery)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            double r, g;
            {

                g = ((int)battery * (2.55));
                r = 255 - g;
            }
            mySolidColorBrush.Color = Color.FromArgb(255, (byte)r, (byte)g, 0);

            return mySolidColorBrush;
        }
        public List<DroneModelToList> DronesFromBlToPl(IEnumerable<BlApi.BO.DroneToList> list)
        {
            List<DroneModelToList> Mlist = new();
            Mlist.Clear();
            foreach (var item in list)
            {
                DroneModelToList temp = new();
                temp.Id = item.Id;
                temp.BatteryNum = item.Battery;
                temp.Bcolor = percentToColor(item.Battery);
                temp.LeftMargin = new Thickness(item.Battery, 0, 0, 0);
                temp.DeliveredParcelId = item.DeliveredParcelId;
                temp.Location = item.Location;
                temp.Model = item.Model;
                temp.Status = item.Status;
                temp.Weight = item.Weight;
                Mlist.Add(temp);
            }
            return Mlist;
        }

    }
}
public class DroneModelToList
{

    public int Id { get; set; }
    public string Model { get; set; }
    public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
    public BlApi.BO.MyEnums.DroneStatus Status { get; set; }
    public double BatteryNum { get; set; }
    public Thickness LeftMargin { get; set; }
    public SolidColorBrush Bcolor { get; set; }
    public BlApi.BO.Location Location { get; set; }
    public int DeliveredParcelId { get; set; }
    public override string ToString()
    {
        return $"ID: { Id }\nModel: { Model }\nWeight Category: { Weight }\nStatus: { Status }\nBattery: " +
           $" { BatteryNum} \n{Location}\nCurrent parcel's id: { DeliveredParcelId }";
    }
}
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

        List<DroneModelToList> Mlist = new();
        List<BlApi.BO.DroneToList> list = new();

        private void Timer_Click(object sender, EventArgs e)
        {
            list = bl.GetDrones();
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
            dronesListView.ItemsSource = Mlist;
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

            list = bl.GetDrones();
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
            dronesListView.ItemsSource = Mlist;
            weightSelector.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.WeightCategory));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BlApi.BO.MyEnums.DroneStatus));

             
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.maintenance)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInMaintenance);
            else if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.available)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInAvailable);
            else if (e.AddedItems.Contains(BlApi.BO.MyEnums.DroneStatus.delivery)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInDelivery);
        }

        private void weightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.heavy)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInHeavy);
            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.medium)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInMedium);
            if (e.AddedItems.Contains(DalApi.DO.MyEnums.WeightCategory.light)) dronesListView.ItemsSource = bl.GetDronesList(bl.allDronesInLight);
        }

        private void dclick(object sender, MouseButtonEventArgs e)
        {
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
            dronesListView.ItemsSource = Mlist;
        }

        private void weightSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dronesListView.ItemsSource = Mlist;
        }
        SolidColorBrush percentToColor(int battery)
        {
            float percent = battery / 100;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            if (percent < 0 || percent > 1)
            {
                mySolidColorBrush.Color =
                Color.FromArgb(255, 255, 255, 255);
                return mySolidColorBrush;
            }
            int r, g;
            if (percent < 0.5)
            {
                r = 255;
                g = (int)(255 * percent / 0.5);  //closer to 0.5, closer to yellow (255,255,0)
            }
            else
            {
                g = 255;
                r = 255 - ((int)(255 * (percent - 0.5) / 0.5)); //closer to 1.0, closer to green (0,255,0)
            }
            mySolidColorBrush.Color = Color.FromArgb(170, (byte)r, (byte)g, 0);

            return mySolidColorBrush;
        }
        //public SolidColorBrush GetColorOf(int value)
        //{
        //    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
        //    double minValue = 0.0;
        //    double maxValue = 1;
        //    var currect = value/100;

        //    if (value == 50)
        //    {
        //        mySolidColorBrush.Color = Color.FromArgb(100, 255, 255, 255);
        //        return mySolidColorBrush;
        //    }
        //    if (value < 25)
        //    {
        //        currect = (currect) * -1;
        //    }
            

           

        //    var g = (int)(240 * currect / maxValue);
        //    var r = (int)(240 * currect / minValue);

        //   var color = (value > 0
        //        ? Color.FromArgb(255, (byte)(240 - g) , (byte)(255 - (int)(g * ((255 - 155) / 240.0))), (byte)(240 - g))
        //        : Color.FromArgb(255, (byte)(255 - (int)(r * ((255 - 230) / 240.0))), (byte)(240 - r), (byte)(240 - r)));
        //    mySolidColorBrush.Color = color;
        //    return mySolidColorBrush;
        //}

    }
}
public class DroneModelToList
{
    public int Id { get; set; }
    public string Model { get; set; }
    public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
    public BlApi.BO.MyEnums.DroneStatus Status { get; set; }
    public int BatteryNum { get; set; }
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
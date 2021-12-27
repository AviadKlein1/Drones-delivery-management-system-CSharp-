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
using System.Collections.ObjectModel;
using BlApi;

namespace PrL
{
    /// <summary>
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : MetroWindow
    {
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        private void Timer_Click(object sender, EventArgs e)
        {
            StationsListView.ItemsSource = bl.GetStationsList(AllStations);
            StationsListView.Items.Refresh();
        }
        BlApi.BO.BL bl;
        static bool AllStations(DalApi.DO.Station s) { return true; }
        System.Predicate<DalApi.DO.Station> allStations = AllStations;
        public StationsList(IBl mainBl)
        {
            bl = (BlApi.BO.BL)mainBl;
            var stationsList = bl.GetStationsList(allStations);
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            InitializeComponent();
            ThemeManager.Current.ChangeTheme(this, "Light.blue");
            try 
            {
                StationsListView.ItemsSource = stationsList;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddNewStation_Click(object sender, RoutedEventArgs e)
        {
            new Station(bl).Show();
            StationsListView_SourceUpdated(sender, e);
        
            }
        private void StationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                new Station(bl, (BlApi.BO.StationToList)StationsListView.SelectedItem).Show();
            }
            catch (Exception) { }
        }

        private void StationsListView_SourceUpdated(object sender, RoutedEventArgs e)
        {
            StationsListView.Items.Refresh();
        }
        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            StationsListView.Items.Refresh();

        }
    }
}

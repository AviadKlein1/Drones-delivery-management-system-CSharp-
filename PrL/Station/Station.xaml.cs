using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

namespace PrL
{
    /// <summary>
    /// Interaction logic for Station.xaml
    /// </summary>
    public partial class Station : MetroWindow
    {
        BlApi.BO.Station station = new();
        BlApi.BO.StationToList stationToList = new();
        BlApi.BO.BL bl;

        public Station(BlApi.BO.BL mainBl)
        {
            InitializeComponent();
            Title = "Add new Station";
            bl = mainBl;
            AddNewStation.Visibility = Visibility.Visible;

        }
        public Station(BlApi.BO.BL mainBl, BlApi.BO.StationToList mainDrone)
        {
            ThemeManager.Current.ChangeTheme(this, "Dark.blue");
            InitializeComponent();
            Title = "Stations Diatels";
            bl = mainBl;
            stationToList = mainDrone;
            station = bl.DisplayStation(stationToList.Id);

            DronesInChargeComboBox.ItemsSource = station.DronesInCharge.Select(item => item.Id);
            DronesInChargeComboBox.Items.ToString();

            DisplayStation.DataContext = station;
            DisplayStation.Visibility = Visibility.Visible;
            double minLat = ((double)(station.Location.Latitude - (int)station.Location.Latitude) * 60);
            double minLon = ((double)(station.Location.Longitude - (int)station.Location.Longitude) * 60);
            double secLat = ((double)(minLat - (int)minLat) * 60);
            double secLon = ((double)(minLon - (int)minLon) * 60);
            StationLocationBox.Text = $"{ (int)station.Location.Latitude }° { (int)minLat }' { (int)secLat}\" N { (int)station.Location.Longitude }° {(int)minLon}' {(int)secLon}\" E";
        }
        private void CancelButton3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void SubmitStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                station.Id = int.Parse(AddStationIdBox.Text);
                station.Name = (string)AddStationNameBox.Text;
                station.NumOfChargeSlots = int.Parse(AddStationNumOfCSBox.Text);
                station.NumOfAvailableChargeSlots = station.NumOfChargeSlots;
                station.Location= new BlApi.BO.Location(double.Parse(AddStationLattiudeBox.Text),double.Parse(AddStationLongitudeBox.Text));
                bl.AddStation(station);
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

        private void DronesInChargeComboBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var v = (int)DronesInChargeComboBox.SelectedItem;
            MessageBox.Show(bl.DisplayParcel(v).ToString());
        }

        private void UpdatNameAndChargeSlots_Click(object sender, RoutedEventArgs e)
        {
            station.Name = (string)NameStationBox.Text;

            station.NumOfChargeSlots = int.Parse(NumOfChargeSlotsBox.Text);
            bl.UpdateStation(station.Id, station.Name, station.NumOfChargeSlots, station.NumOfAvailableChargeSlots);

            MessageBox.Show("success!");
            Close();

        }
            
    }
}


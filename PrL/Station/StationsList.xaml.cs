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
using System.Windows.Shapes;
using BlApi;

namespace PrL
{
    /// <summary>
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : MetroWindow
    {
        BlApi.BO.BL bl;
        public StationsList(IBl mainBl)
        {
            InitializeComponent();
            ThemeManager.Current.ChangeTheme(this, "Light.blue");
            bl = (BlApi.BO.BL)mainBl;
            try
            {
                StationsListView.ItemsSource = bl.GetStationsList(BlApi.BO.BL.AllStations);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddNewStation_Click(object sender, RoutedEventArgs e)
        {
            new Station(bl).Show();
            StationsListView.Items.Refresh();
        }
        private void StationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new Station(bl, (BlApi.BO.StationToList)StationsListView.SelectedItem).Show();
            StationsListView.Items.Refresh();
        }

        //private void StRefresh_Click(object sender, RoutedEventArgs e)
        //{
        //    StationsListView.Items.Refresh();
        //}
    }
}

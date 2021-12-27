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
    /// Interaction logic for dronesList.xaml
    /// </summary>
    public partial class CustomersList : MetroWindow
    {
        BlApi.BO.BL bl;
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        private void Timer_Click(object sender, EventArgs e)
        {
            CustomersListView.Items.Refresh();
        }
        public CustomersList(IBl mainBl)
        {
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            InitializeComponent();
            ThemeManager.Current.ChangeTheme(this, "Light.blue");
            bl = (BlApi.BO.BL)mainBl;
            CustomersListView.ItemsSource = bl.GetCustomersList(BlApi.BO.BL.AllCustomers);
        }
        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            new Customer(bl).Show();
        }
        private void DuobleClickCustomer(object sender, MouseButtonEventArgs e)
        {
            new Customer(bl, (BlApi.BO.CustomerToList)CustomersListView.SelectedItem).Show();
        }
    }
}

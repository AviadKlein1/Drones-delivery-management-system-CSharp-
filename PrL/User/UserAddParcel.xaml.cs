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
using MahApps.Metro.Controls;
using MahApps.Metro.Accessibility;
using BlApi;
using ConsoleUI_BL;
using System.Diagnostics;
using ControlzEx.Theming;
using System.Runtime.InteropServices;


namespace PrL
{
    /// <summary>
    /// Interaction logic for UserAddParcel.xaml
    /// </summary>
    public partial class UserAddParcel : Window
    {
        BlApi.BO.BL bl;
        int UserId;
        public UserAddParcel(int UserId, IBl _bl)
        {
            InitializeComponent();
            bl = (BlApi.BO.BL)_bl;
            this.UserId = UserId;
            UserAddReceiverId.ItemsSource = bl.GetCustomersList(BlApi.BO.BL.AllCustomers).Select(item => item.Id + " " + item.Name);
            UserAddReceiverId.Items.ToString();
            UserAddWeightselector.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.WeightCategory));
            UserAddPriority.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.PriorityLevel));
        }

        private void SubmitDitails_Click(object sender, RoutedEventArgs e)
        {
            BlApi.BO.Parcel parcel = new();
            var CustomersId = bl.GetCustomersList(BlApi.BO.BL.AllCustomers).Select(item => item.Id);
            parcel.Sender.Id = UserId;
            parcel.Receiver.Id = CustomersId.ElementAt(UserAddReceiverId.SelectedIndex);
            parcel.Weight = (DalApi.DO.MyEnums.WeightCategory)UserAddWeightselector.SelectedItem;
            parcel.Priority = (DalApi.DO.MyEnums.PriorityLevel)UserAddPriority.SelectedItem;
            bl.AddParcel(parcel);
            MessageBox.Show("Success!");
            new User(UserId, bl).Show();
            Close();
        }
    }
}

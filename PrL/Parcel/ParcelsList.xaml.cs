using BlApi;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
namespace PrL
{
    /// <summary>
    /// Interaction logic for dronesList.xaml
    /// </summary>
    public partial class ParcelsList : MetroWindow
    {
        BlApi.BO.BL bl;
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        CollectionView view;
        private void Timer_Click(object sender, EventArgs e)
        {
            ParcelsListView.ItemsSource = bl.GetParcelsList(allParcels);
            ParcelsListView.Items.Refresh();
        }
        static bool AllParcels(DalApi.DO.Parcel p) { return true; }
        System.Predicate<DalApi.DO.Parcel> allParcels = AllParcels;
        public ParcelsList(IBl mainBl)
        {
            bl = (BlApi.BO.BL)mainBl;
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            InitializeComponent();
            ParcelsListView.ItemsSource = bl.GetParcelsList(BlApi.BO.BL.AllParcels);
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);
            ThemeManager.Current.ChangeTheme(this, "Light.blue");
        }
        private void AddNewParcel_Click(object sender, RoutedEventArgs e)
        {
            new Parcel(bl).Show();
        }
        private void ParcelsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new Parcel(bl, (BlApi.BO.ParcelToList)ParcelsListView.SelectedItem).Show();
        }
        public void AddSort(string header)
        {
            if (ParcelsListView.ItemsSource == null) return;
            ParcelsListView.Items.SortDescriptions.Clear();
            view.GroupDescriptions.Clear();
            if (header == $"Id") ParcelsListView.Items.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            if (header == $"Weight") ParcelsListView.Items.SortDescriptions.Add(new SortDescription("Weight", ListSortDirection.Ascending));
            if (header == $"Priority") ParcelsListView.Items.SortDescriptions.Add(new SortDescription("Priority", ListSortDirection.Ascending));
            if (header == $"ParcelStatus") ParcelsListView.Items.SortDescriptions.Add(new SortDescription("ParcelStatus", ListSortDirection.Ascending));
            else return;
        }
        private void ParcelsListView_Click(object sender, RoutedEventArgs e)
        {
            AddSort(((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString());
        }
        private void ReceiverNameSelector_Click(object sender, RoutedEventArgs e)
        {
            view.GroupDescriptions.Clear();
            view.GroupDescriptions.Add(new PropertyGroupDescription("ReceiverName"));
        }
        private void SenderNameSelector_Click_1(object sender, RoutedEventArgs e)
        {
            view.GroupDescriptions.Clear();
            view.GroupDescriptions.Add(new PropertyGroupDescription("SenderName"));
        }
    }

}

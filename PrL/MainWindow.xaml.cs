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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            dateTimeBlock.Text =d.Day+"/"+ d.Month+"/"+ d.Year+"  "+ d.Hour + " : " + d.Minute + " : " + d.Second;
        }

        public IBl bl;
        public MainWindow(string userName)
        {
            ShowCloseButton = false;
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            ThemeManager.Current.ChangeTheme(this, "Light.blue");
            InitializeComponent();

            try
            {
                bl = (BlApi.BO.BL)BlFactory.GetBl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            bool found = false;
            var c = bl.GetCustomersList(BlApi.BO.BL.allCustomers);
            if (userName != "")
            {
                foreach (var item in c)
                {
                    if (item.Name == userName)
                    {
                        new User(item.Id, bl).Show();
                        found = true;
                        Close();
                    }
                }
                if (!found)
                {
                    new UserCustomer(userName, bl).Show();
                    Close();
                }
            }
        }
        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/AviadKlein1/dotNet5782_2679_3080") { UseShellExecute = true });
        }

        private void MailUs_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("mailto:tomerperetz55@gmail.com?subject=SubjectExample&body=BodyExample") { UseShellExecute = true });
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region DllImport
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();
        #endregion

        private void AdminAccssesButton_Click(object sender, RoutedEventArgs e)
        {
            AllocConsole();

            #region ADMIN API
            Console.WriteLine("\n-- Welcome to -- Delivery by Drones --  Management interface --\n\n");

            int id = 0;
            int choice1 = -1;
            Input myInputOutput = new();
            try
            {
                //allow user to manage and coordinate deliveries
                while (choice1 != 0)
                {
                    int choice2 = -1;
                    Console.WriteLine("Choose one of the following:\n" +
                    "1: Add a new item\n" +
                    "2: Update an item\n" +
                    "3: Display an item\n" +
                    "4: Display a list of items\n" +
                    "0: exit\n");
                    int.TryParse(Console.ReadLine(), out choice1);

                    switch (choice1)
                    {
                        //add items
                        case 1:
                            while (choice2 != 0)
                            {
                                Console.WriteLine(
                                "To add station enter 1" +
                                "\nTo add a drone enter 2" +
                                "\nTo add a customer enter 3" +
                                "\nTo add a parcel enter 4" +
                                "\nTo return to main menu enter 0\n");
                                int.TryParse(Console.ReadLine(), out choice2);
                                switch (choice2)
                                {

                                    //add station
                                    case 1:
                                        try
                                        {
                                            bl.AddStation(Input.AddStation());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    //add drone
                                    case 2:
                                        try
                                        {
                                            bl.AddDrone(Input.AddDrone());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    //add customer
                                    case 3:
                                        try
                                        {
                                            bl.Addcustomer(Input.AddCustomer());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    //add parcel
                                    case 4:
                                        try
                                        {
                                            bl.AddParcel(myInputOutput.AddParcel());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                        //updates
                        case 2:
                            while (choice2 != 0)
                            {
                                Console.WriteLine("To update drone's data enter 1" +
                                "\nTo update station's data enter 2" +
                                "\nTo update customer's data enter 3" +
                                "\nTo charge drone enter 4" +
                                "\nTo end drone charge enter 5" +
                                "\nTo schedule a delivery enter 6" +
                                "\nTo pick-up parcel enter 7" +
                                "\nTo deliver parcel enter 8" +
                                "\nTo return to main menu enter 0\n");
                                int.TryParse(Console.ReadLine(), out choice2);
                                int cin1;
                                string string1 = "";
                                string string2 = "";

                                switch (choice2)
                                {
                                    //update dron's data
                                    case 1:

                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine("enter new model:\n");
                                        string1 = Console.ReadLine();
                                        var flag1 = false;
                                        try
                                        {
                                            flag1 = bl.UpdateDrone(id, string1);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (flag1)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("update failed\n");
                                        break;

                                    case 2:
                                        Console.WriteLine("enter station id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine("enter new station name:\n");
                                        string1 = Console.ReadLine();
                                        Console.WriteLine("enter amount of charge slots:\n");
                                        int.TryParse(Console.ReadLine(), out cin1);
                                        var flag2 = false;
                                        try
                                        {
                                            flag2 = bl.UpdateStation(id, string1, cin1, bl.DisplayStation(id).NumOfAvailableChargeSlots);

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (flag2)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("update failed\n");
                                        break;

                                    case 3:
                                        Console.WriteLine("enter customer id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine("enter new name:\n");
                                        string1 = Console.ReadLine();
                                        Console.WriteLine("enter new phone:\n");
                                        string2 = Console.ReadLine();
                                        var flag3 = false;
                                        try
                                        {
                                            flag3 = bl.UpdateCustomer(id, string1, string2);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (flag3)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("update failed failed\n");
                                        break;

                                    case 4:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        var flag4 = false;
                                        try
                                        {
                                            flag4 = bl.ChargeDrone(id);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (flag4)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("update failed failed\n");
                                        break;

                                    case 5:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine("enter required charging duration (in minutes):\n");
                                        int.TryParse(Console.ReadLine(), out cin1);
                                        var flag5 = false;
                                        try
                                        {
                                            flag5 = bl.ReleaseDroneFromCharge(id, cin1);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (flag5)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("update failed\n");
                                        break;

                                    case 6:

                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        var flag6 = false;
                                        try
                                        {
                                            flag6 = bl.ScheduleParcelToDrone(id);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (flag6)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("update failed\n");
                                        break;

                                    case 7:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        var flag7 = false;
                                        try
                                        {
                                            flag7 = bl.PickUpParcel(id);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (flag7)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("update failed\n");
                                        break;
                                    case 8:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        var flag8 = false;
                                        try
                                        {
                                            flag8 = bl.DeliverParcel(id);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (flag8)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("update failed\n");
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                        //display item
                        case 3:
                            while (choice2 != 0)
                            {
                                Console.WriteLine("To display a station enter 1" +
                                "\nTo display a drone enter 2" +
                                "\nTo display a customer enter 3" +
                                "\nTo display a parcel enter 4" +
                                "\nTo return to main menu enter 0\n");
                                int.TryParse(Console.ReadLine(), out choice2);
                                switch (choice2)
                                {
                                    //display station
                                    case 1:
                                        Console.WriteLine("enter station id\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        try
                                        {
                                            Console.WriteLine(bl.DisplayStation(id));
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    //display drone
                                    case 2:
                                        Console.WriteLine("enter drone id\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        try
                                        {
                                            Console.WriteLine(bl.DisplayDrone(id));
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    //display customer
                                    case 3:
                                        Console.WriteLine("enter customer id\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        try
                                        {
                                            Console.WriteLine(bl.DisplayCustomer(id));
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    //display parcel
                                    case 4:
                                        Console.WriteLine("enter parcel id\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        try
                                        {
                                            Console.WriteLine(bl.DisplayParcel(id));
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                        //display list of items
                        case 4:
                            while (choice2 != 0)
                            {
                                Console.WriteLine("To display list of stations enter 1" +
                                "\nTo display list of drones enter 2" +
                                "\nTo display list of customers enter 3" +
                                "\nTo display list of parcels enter 4" +
                                "\nTo display list of not associated parcels enter 5" +
                                "\nTo display list of available to charge stations enter 6" +
                                "\nTo return to main menu enter 0\n");
                                int.TryParse(Console.ReadLine(), out choice2);
                                switch (choice2)
                                {
                                    //display list of stations
                                    case 1:
                                        static bool AllStations(DalApi.DO.Station s) { return true; }
                                        System.Predicate<DalApi.DO.Station> allStations = AllStations;
                                        var stationsList = bl.GetStationsList(allStations);
                                        foreach (var element in stationsList)
                                            Console.WriteLine(element + "\n");
                                        break;
                                    //display list of drones
                                    case 2:
                                        static bool AllDrones(BlApi.BO.DroneToList d) { return true; }
                                        System.Predicate<BlApi.BO.DroneToList> allDrones = AllDrones;
                                        var dronesList = bl.GetDronesList(allDrones);
                                        foreach (var element in dronesList)
                                            Console.WriteLine(element + "\n");
                                        break;
                                    //display list of customers
                                    case 3:
                                        static bool AllCustomers(DalApi.DO.Customer c) { return true; }
                                        System.Predicate<DalApi.DO.Customer> allCustomers = AllCustomers;
                                        var customerList = bl.GetCustomersList(allCustomers);
                                        foreach (var element in customerList)
                                            Console.WriteLine(element + "\n");
                                        break;
                                    //display list of parcels
                                    case 4:
                                        static bool AllParcels(DalApi.DO.Parcel p) { return true; }
                                        System.Predicate<DalApi.DO.Parcel> allParcels = AllParcels;
                                        var parcelsList = bl.GetParcelsList(allParcels);
                                        foreach (var element in parcelsList)
                                            Console.WriteLine(element + "\n");
                                        break;
                                    //display list of not associated parcels
                                    case 5:
                                        static bool UnassociatedParcels(DalApi.DO.Parcel p) { return (p.Scheduled == DateTime.MinValue); }
                                        System.Predicate<DalApi.DO.Parcel> unassociatedParcels = UnassociatedParcels;
                                        var notAssociatedParcelsList = bl.GetParcelsList(unassociatedParcels);
                                        foreach (var element in notAssociatedParcelsList)
                                            Console.WriteLine(element + "\n");
                                        break;
                                    //display list of available to charge stations
                                    case 6:
                                        static bool AvailableForCharge(DalApi.DO.Station s) { return (s.NumOfAvailableChargeSlots > 0); }
                                        System.Predicate<DalApi.DO.Station> availableForCharge = AvailableForCharge;
                                        var avilableToChargeStations = bl.GetStationsList(availableForCharge);
                                        foreach (var element in avilableToChargeStations)
                                            Console.WriteLine(element + "\n");
                                        break;

                                    default:
                                        break;
                                }
                            }
                            break;
                        //end
                        case 0:
                            Console.WriteLine("see you soon");
                            FreeConsole();
                            return;

                        default:
                            break;

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        #endregion ADMIN API

        private void AdminDrones_Click(object sender, RoutedEventArgs e)
        {
            new dronesList(bl).Show();
        }

        private void AdminCustomers_Click(object sender, RoutedEventArgs e)
        {
            new CustomersList(bl).Show();
        }

        private void AdminStations_Click(object sender, RoutedEventArgs e)
        {
            new StationsList(bl).Show();
        }

        private void AdminParcels_Click(object sender, RoutedEventArgs e)
        {
            new ParcelsList(bl).Show();

        }
    }
}

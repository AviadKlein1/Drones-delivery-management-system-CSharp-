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
using System.Threading;

namespace PrL
{
    public partial class AddDrone : MetroWindow
    {
        //System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        BlApi.BO.BL bl;
        BlApi.BO.Drone drone = new();
        DroneModelToList droneToList = new();
        internal BackgroundWorker DroneSimultor;

        public AddDrone(BlApi.BO.BL mainBl)
        {

            ThemeManager.Current.ChangeTheme(this, "Light.blue");
            InitializeComponent();
            Title = "Add new drone";
            bl = mainBl;

            AddWeightselectorCombo.ItemsSource = Enum.GetValues(typeof(DalApi.DO.MyEnums.WeightCategory));
            var StationsNameId = bl.GetStationsList(BlApi.BO.BL.AllStations).Select(item => item.Id + " " + item.Name);
            AddIdOfFirstChargeSelectorCombo.ItemsSource = StationsNameId;
            AddNewDrone.Visibility = Visibility.Visible;
        }
        public AddDrone(BlApi.BO.BL mainBl, DroneModelToList mainDrone)
        {
            //Timer.Tick += new EventHandler(Timer_Click);
            //Timer.Interval = new TimeSpan(0, 0, 1);
            //Timer.Start();
            ThemeManager.Current.ChangeTheme(this, "light.blue");
            InitializeComponent();
            Title = "Drone Details";
            bl = mainBl;
            droneToList = mainDrone;
            Load(droneToList.Id);
            DisplayDrone.Visibility = Visibility.Visible;

        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                drone.Id = int.Parse(AddDroneIdBox.Text);
                drone.Model = (string)AddDroneModelBox.Text;
                drone.Weight = (DalApi.DO.MyEnums.WeightCategory)AddWeightselectorCombo.SelectedItem;
                var StationsNameId = bl.GetStationsList(BlApi.BO.BL.AllStations).Select(item => item.Id);
                drone.FirstChargeStationId = StationsNameId.ElementAt(AddIdOfFirstChargeSelectorCombo.SelectedIndex);
                bl.AddDrone(drone);
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
            Load(droneToList.Id);
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DeleteDrone_Click(object sender, RoutedEventArgs e)
        {
            bl.DeleteDrone(bl.DisplayDrone(droneToList.Id));
            Load(droneToList.Id);
            Close();
        }
        private void UpdateModel_Click(object sender, RoutedEventArgs e)
        {
            droneToList.Model = (string)DroneModelBox.Text;
            bl.UpdateDrone(droneToList.Id, droneToList.Model);
            MessageBox.Show("success!");
            Load(droneToList.Id);

        }
        private void SendDroneToCharge_Click(object sender, RoutedEventArgs e)
        {
            if (bl.ChargeDrone(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("Faild!");
            Load(droneToList.Id);
        }
        private void EndCharge_Click(object sender, RoutedEventArgs e)
        {
            if (bl.ReleaseDroneFromCharge(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("failed!");
            Load(droneToList.Id);

        }
        private void ScheduleParcelToDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.ScheduleParcelToDrone(droneToList.Id);
                Load(droneToList.Id);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("success!");
        }
        private void PickUpParcel_Click(object sender, RoutedEventArgs e)
        {
            if (bl.PickUpParcel(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("failed!");
            Load(droneToList.Id);

        }
        private void DeliverParcel_Click(object sender, RoutedEventArgs e)
        {
            if (bl.DeliverParcel(droneToList.Id)) MessageBox.Show("success!");
            else MessageBox.Show("Faild!");
            Load(droneToList.Id);

        }
        private void DeliveredParcelIdBox_DClick(object sender, RoutedEventArgs e)
        {
            var v = int.Parse(DeliveredParcelIdBox.Text);

            try
            {
                MessageBox.Show(bl.DisplayDeliveredParcel(droneToList.Id).ToString());
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
                return;
            }
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
        public bool SimPickUpParcel(int droneId, double _droneSpeed)
        {
            BlApi.BO.DroneToList dtl = new();
            double earlyBattery = 0;
            double droneSpeed = _droneSpeed;
            double tick = 0.5;
            double distance = 0;
            bool found = false;
            bool droneExistFlag = false;
            var v = bl.GetDrones();
            int idOfThisParcel = 0;
            DalApi.DO.Location ourSenderLocation = new();

            foreach (var item in
            //search drone
            from item in v
            where item.Id == droneId
            select item)
            {
                idOfThisParcel = item.DeliveredParcelId;
                droneExistFlag = true;
            }

            if (droneExistFlag == false)
                throw new Exception($"wrong id: {droneId}");
            if (bl.ScheduledButNotPickedUp(idOfThisParcel))
            {
                var parcelsList = bl.GetParcelsList(BlApi.BO.BL.AllParcels);
                foreach (var item in from item in parcelsList//find parcel
                                     where item.Id == idOfThisParcel
                                     select item)
                {
                    ourSenderLocation = bl.SenderLocation(item.Id);
                    //update parcel
                    bl.DalPickUpParcel(droneId, item.Id);
                    found = true;
                    //update drone
                    BlApi.BO.DroneToList temp = new();

                    for (int i = 0; i < v.Count; i++)
                    {
                        BlApi.BO.DroneToList dItem = v[i];
                        if (dItem.Id == droneId)
                        {
                            earlyBattery = dItem.Battery;
                            DalApi.DO.Location earlyDroneLocation = new(dItem.Location.Longitude, dItem.Location.Latitude);
                            var senderLocat = new BlApi.BO.Location(ourSenderLocation);
                            distance = bl.DalGetDistance(earlyDroneLocation, ourSenderLocation) / 100;

                            double time = (distance / droneSpeed);//time = way/speed represented in seconds
                            var TicksNum = time / tick;
                            var DistanceForTick = (droneSpeed * tick);
                            var BatteryforAllJourny = (int)bl.BlBatteryRequirementForVoyage(droneId, distance * 100);
                            var BatteryForTick = BatteryforAllJourny / TicksNum;
                            for (int k = 0; k < (int)TicksNum; k++)
                            {
                                temp.Id = dItem.Id;
                                temp.Model = dItem.Model;
                                temp.Status = dItem.Status;
                                temp.Battery = dItem.Battery;
                                temp.Weight = dItem.Weight;
                                temp.DeliveredParcelId = item.Id;
                                //uodate location
                                temp.Location = new BlApi.BO.Location(dItem.Location.Latitude + Math.Abs(dItem.Location.Latitude - ourSenderLocation.Latitude),
                                    dItem.Location.Longitude + Math.Abs(dItem.Location.Longitude - ourSenderLocation.Longitude));
                                ////update battery
                                temp.Battery = dItem.Battery - BatteryForTick;
                                if (temp.Battery < 0) temp.Battery = 0;
                                v[i] = temp;
                                Load(temp);
                            }
                            temp.Id = dItem.Id;
                            temp.Model = dItem.Model;
                            temp.Battery = dItem.Battery;
                            temp.Status = dItem.Status;
                            temp.Weight = dItem.Weight;
                            temp.DeliveredParcelId = item.Id;
                            //uodate location
                            temp.Location = new BlApi.BO.Location(ourSenderLocation.Latitude, ourSenderLocation.Longitude);

                            ////update battery
                            temp.Battery = earlyBattery - bl.BlBatteryRequirementForVoyage(droneId, distance);
                            if (temp.Battery < 0) temp.Battery = 0;
                            v[i] = temp;
                            dtl = temp;

                        }
                    }
                        //Console.WriteLine("not our parcel");
                }
            }
            return found;
        }
        public bool SimDeliverParcel(int droneId, double _droneSpeed)
        {
            double earlyBattery = 0;
            double droneSpeed = _droneSpeed;
            double tick = 0.5;
            double distance = 0;
            bool flag = false;
            var droneExistFlag = false;
            var v = bl.GetDrones();
            int idOfThisParcel = 0;
            DalApi.DO.Location ourReciverLocation = new();
            foreach (var item in
            //search drone
            from item in v
            where item.Id == droneId
            select item)
            {
                idOfThisParcel = item.DeliveredParcelId;
                droneExistFlag = true;
            }

            if (droneExistFlag == false)
                throw new Exception( $"wrong id: { droneId }");
            if (!bl.PickedUpButNotDelivered(idOfThisParcel))
                Console.WriteLine("this parcel is not in the right status\n");
            var parcelsList = bl.GetParcelsList(BlApi.BO.BL.AllParcels);
            foreach (var item in from item in parcelsList//find our parcel
                                 where item.Id == idOfThisParcel
                                 select item)
            {
                ourReciverLocation = bl.ReceiverLocation(item.Id);
                //update parcel
                bl.DalDeliverParcel(droneId, item.Id);
                flag = true;
                //update drone
                BlApi.BO.DroneToList temp = new();

                for (int i = 0; i < v.Count; i++)
                {
                    BlApi.BO.DroneToList dItem = v[i];
                    if (dItem.Id == droneId)
                    {
                        earlyBattery = dItem.Battery;
                        DalApi.DO.Location earlyDroneLocation = new(dItem.Location.Longitude, dItem.Location.Latitude);
                        var senderLocat = new BlApi.BO.Location(ourReciverLocation);
                        distance = bl.DalGetDistance(earlyDroneLocation, ourReciverLocation);
                        double time = distance / droneSpeed;//time = way/speed represented in seconds
                        var TicksNum = time / tick;
                        var DistanceForTick = droneSpeed * 0.5;
                        var BatteryforAllJourny = (int)bl.BlBatteryRequirementForVoyage(droneId, distance);
                        var BatteryForTick = BatteryforAllJourny / TicksNum;

                        for (int k = 0; k < (int)TicksNum; k++)
                        {
                            temp.Id = dItem.Id;
                            temp.Model = dItem.Model;
                            temp.Status = dItem.Status;
                            temp.Battery = dItem.Battery;

                            temp.Weight = dItem.Weight;
                            temp.DeliveredParcelId = item.Id;
                            //uodate location
                            temp.Location = new BlApi.BO.Location(dItem.Location.Latitude + Math.Abs(dItem.Location.Latitude - ourReciverLocation.Latitude),
                                dItem.Location.Longitude + Math.Abs(dItem.Location.Longitude - ourReciverLocation.Longitude));

                            ////update battery
                            temp.Battery = dItem.Battery - BatteryForTick;
                            if (temp.Battery < 0) temp.Battery = 0;
                            v[i] = temp;
                            //Thread.CurrentThread.Join(50);
                            Load(temp);

                        }
                        temp.Id = dItem.Id;
                        temp.Model = dItem.Model;
                        temp.Battery = dItem.Battery;
                        temp.Status = dItem.Status;
                        temp.Weight = dItem.Weight;
                        temp.DeliveredParcelId = item.Id;
                        //uodate location
                        temp.Location = new BlApi.BO.Location(ourReciverLocation.Latitude, ourReciverLocation.Longitude);
                        ////update battery
                        temp.Battery = earlyBattery - bl.BlBatteryRequirementForVoyage(droneId, distance);
                        if (temp.Battery < 0) temp.Battery = 0;
                        v[i] = temp;
                    }
                }
            }
            return flag;
        }
        public bool SimChargeDrone(int droneId, double _droneSpeed)
        {
           
            BlApi.BO.DroneToList temp = new();
            bool found = false;
            double droneSpeed = _droneSpeed;
            double earlyBattery = 0;
            double tick = 0.5;
            double distance = 0;
            var v = bl.GetDrones();
            DalApi.DO.Location itemLocation = new DalApi.DO.Location();
            //search drone
            foreach (var item in v)
            {
                if (item.Id == droneId)
                {
                    itemLocation = new(item.Location.Longitude, item.Location.Latitude);
                    found = true;
                }
            }

            var tempStation = bl.DalNearestReachableChargeSlot(itemLocation, droneId);
            //if all stations are occupied
            if (tempStation.Id == 0)
            {
                throw new Exception("not available station to charge drone\n");
            }
            // send drone to charge
            bl.DalDecreaseChargeSlot(tempStation.Id);
            //
            for (int i = 0; i < v.Count; i++)
            {
                BlApi.BO.DroneToList dItem = v[i];
                if (dItem.Id == droneId)
                {
                    earlyBattery = dItem.Battery;
                    DalApi.DO.Location earlyDroneLocation = new(dItem.Location.Longitude, dItem.Location.Latitude);
                    var chargeSlotLocation = new DalApi.DO.Location(tempStation.Location.Latitude, tempStation.Location.Longitude);
                    distance = bl.DalGetDistance(earlyDroneLocation, chargeSlotLocation);

                    var time = distance / droneSpeed;//time = way/speed represented in seconds
                    var TicksNum = time / tick;
                    var DistanceForTick = droneSpeed * 0.5;
                    var BatteryforAllJourny = (int)bl.BlBatteryRequirementForVoyage(droneId, distance);
                    var BatteryForTick = BatteryforAllJourny / TicksNum;
                    for (int k = 0; k < (int)TicksNum; k++)
                    {
                        temp.Id = dItem.Id;
                        temp.Model = dItem.Model;
                        temp.Status = BlApi.BO.MyEnums.DroneStatus.maintenance;
                        temp.Battery = dItem.Battery;

                        temp.Weight = dItem.Weight;
                        temp.DeliveredParcelId = dItem.Id;
                        //uodate location

                        temp.Location = new BlApi.BO.Location(dItem.Location.Latitude + Math.Abs(dItem.Location.Latitude - chargeSlotLocation.Latitude),
                            dItem.Location.Latitude + Math.Abs(dItem.Location.Longitude - chargeSlotLocation.Longitude));
                        ////update battery
                        temp.Battery = dItem.Battery - BatteryForTick;
                        if (temp.Battery < 0) temp.Battery = 0;
                        v[i] = temp;
                    }
                    temp.Id = dItem.Id;
                    temp.Model = dItem.Model;
                    temp.Status = dItem.Status;
                    temp.Battery = dItem.Battery;
                    temp.Weight = dItem.Weight;
                    temp.DeliveredParcelId = dItem.DeliveredParcelId;
                    //uodate location
                    temp.Location = new BlApi.BO.Location(tempStation.Location.Latitude, tempStation.Location.Longitude);

                    ////update battery
                    temp.Battery = earlyBattery - bl.BlBatteryRequirementForVoyage(droneId, distance);
                    if (temp.Battery < 0) temp.Battery = 0;
                    v[i] = temp;
                    Load(temp);

                }

                bl.DalAddDroneCharge(droneId, tempStation.Id, DateTime.Now);
            }
            return found;
        }
        public void Load(int dId)
        {
            var v = bl.GetDrones();
            DroneModelToList temp = new DroneModelToList();
            foreach (var item in v)
            {
                if (item.Id == dId)
                {

                    temp.Id = item.Id;
                    temp.BatteryNum = item.Battery;
                    temp.Bcolor = percentToColor(item.Battery);
                    temp.LeftMargin = new Thickness(item.Battery, 0, 0, 0);
                    temp.DeliveredParcelId = item.DeliveredParcelId;
                    temp.Location = item.Location;
                    temp.Model = item.Model;
                    temp.Status = item.Status;
                    temp.Weight = item.Weight;
                }
            }
            droneToList = temp;
            DisplayDrone.DataContext = droneToList;
            double minLat = (double)(droneToList.Location.Latitude - (int)droneToList.Location.Latitude) * 60;
            double minLon = (double)(droneToList.Location.Longitude - (int)droneToList.Location.Longitude) * 60;
            double secLat = (double)(minLat - (int)minLat) * 60;
            double secLon = (double)(minLon - (int)minLon) * 60;

            LocationBox.Text = $"{ (int)droneToList.Location.Latitude }° { (int)minLat }' { (int)secLat}\" N { (int)droneToList.Location.Longitude }° {(int)minLon}' {(int)secLon}\" E";
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.available)
            {
                SendDroneToChargePanel.Visibility = Visibility.Visible;
                ScheduleParcelToDronePanel.Visibility = Visibility.Visible;
                EndChargePanel.Visibility = Visibility.Collapsed;
                PickUpParcelPanel.Visibility = Visibility.Collapsed;
                DeliverParcelPanel.Visibility = Visibility.Collapsed;
            }
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.maintenance)
            {
                EndChargePanel.Visibility = Visibility.Visible;
                SendDroneToChargePanel.Visibility = Visibility.Collapsed;
                ScheduleParcelToDronePanel.Visibility = Visibility.Collapsed;
                PickUpParcelPanel.Visibility = Visibility.Collapsed;
                DeliverParcelPanel.Visibility = Visibility.Collapsed;
            }
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.delivery)
            {
                EndChargePanel.Visibility = Visibility.Collapsed;
                SendDroneToChargePanel.Visibility = Visibility.Collapsed;
                ScheduleParcelToDronePanel.Visibility = Visibility.Collapsed;
                if (bl.PickedUpButNotDelivered(droneToList.DeliveredParcelId))
                {
                    PickUpParcelPanel.Visibility = Visibility.Collapsed;
                    DeliverParcelPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    PickUpParcelPanel.Visibility = Visibility.Visible;
                    DeliverParcelPanel.Visibility = Visibility.Collapsed;
                }
            }
            this.InitializeComponent();
        }
        public void Load(BlApi.BO.DroneToList item)
        {
            DroneModelToList temp = new DroneModelToList();
            temp.Id = item.Id;
            temp.BatteryNum = item.Battery;
            temp.Bcolor = percentToColor(item.Battery);
            temp.LeftMargin = new Thickness(item.Battery, 0, 0, 0);
            temp.DeliveredParcelId = item.DeliveredParcelId;
            temp.Location = item.Location;
            temp.Model = item.Model;
            temp.Status = item.Status;
            temp.Weight = item.Weight;
            droneToList = temp;
            DisplayDrone.DataContext = droneToList;
            double minLat = (double)(droneToList.Location.Latitude - (int)droneToList.Location.Latitude) * 60;
            double minLon = (double)(droneToList.Location.Longitude - (int)droneToList.Location.Longitude) * 60;
            double secLat = (double)(minLat - (int)minLat) * 60;
            double secLon = (double)(minLon - (int)minLon) * 60;

            LocationBox.Text = $"{ (int)droneToList.Location.Latitude }° { (int)minLat }' { (int)secLat}\" N { (int)droneToList.Location.Longitude }° {(int)minLon}' {(int)secLon}\" E";
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.available)
            {
                SendDroneToChargePanel.Visibility = Visibility.Visible;
                ScheduleParcelToDronePanel.Visibility = Visibility.Visible;
                EndChargePanel.Visibility = Visibility.Collapsed;
                PickUpParcelPanel.Visibility = Visibility.Collapsed;
                DeliverParcelPanel.Visibility = Visibility.Collapsed;
            }
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.maintenance)
            {
                EndChargePanel.Visibility = Visibility.Visible;
                SendDroneToChargePanel.Visibility = Visibility.Collapsed;
                ScheduleParcelToDronePanel.Visibility = Visibility.Collapsed;
                PickUpParcelPanel.Visibility = Visibility.Collapsed;
                DeliverParcelPanel.Visibility = Visibility.Collapsed;
            }
            if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.delivery)
            {
                EndChargePanel.Visibility = Visibility.Collapsed;
                SendDroneToChargePanel.Visibility = Visibility.Collapsed;
                ScheduleParcelToDronePanel.Visibility = Visibility.Collapsed;
                if (bl.PickedUpButNotDelivered(droneToList.DeliveredParcelId))
                {
                    PickUpParcelPanel.Visibility = Visibility.Collapsed;
                    DeliverParcelPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    PickUpParcelPanel.Visibility = Visibility.Visible;
                    DeliverParcelPanel.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            
            if (toggleSwitch.IsOn == true)
            {
                Simultor(); //call to function who creates the process.
                DroneSimultor.RunWorkerAsync(); //Run the process.
                progress.IsActive = true;
                progress.Visibility = Visibility.Visible;
                DeleteDrone.Visibility = Visibility.Hidden;
                DisplayDrone.Visibility = Visibility.Hidden;
            }
            else
            {
                DisplayDrone.Visibility =   Visibility.Visible;
                DroneSimultor.CancelAsync();
                progress.IsActive = false;
                progress.Visibility = Visibility.Collapsed;
                Load(droneToList.Id);
                DisplayDrone.IsEnabled = true;

            }
        }
        public void ReportProgressInSimultor()
        {
            DroneSimultor.ReportProgress(0);
        }
        public bool IsTimeRun()
        {
            return DroneSimultor.CancellationPending;
        }
        private void Simultor()
        {
            DroneSimultor = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            DroneSimultor.DoWork += DroneSimultor_DoWork; //Operation function.
            DroneSimultor.ProgressChanged += DroneSimultor_ProgressChanged; //changed function.
            DroneSimultor.RunWorkerCompleted += DroneSimultor_RunWorkerCompleted;
        }
        private void DroneSimultor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        private void DroneSimultor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DataContext = droneToList;

            //var v = bl.GetDrones();
            //BlApi.BO.DroneToList temp = new();
            //foreach (var item in v)
            //{
            //    if (item.Id == droneToList.Id)
            //        temp = item;
            //}
            //this.Load(temp);
        }
        private void DroneSimultor_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.PlaySimulator(bl, droneToList.Id, ReportProgressInSimultor, IsTimeRun);
        }
       
    }
}


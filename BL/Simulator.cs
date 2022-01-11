using System;
using System.Linq;
using System.Runtime.CompilerServices;
using BlApi;
using System.Threading;
using static BlApi.BO.BL;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlApi
{
    /// <summary>
    /// drone simulator.
    /// </summary>
    class Simulator
    {
        private const double kmh = 3600;
        public Simulator(BlApi.BO.BL _bl, int droneID, Action ReportProgressInSimultor, Func<bool> IsTimeRun)
        {
            double free = 0.05;
            double lightWeight = 0.1;
            double mediumeight = 0.2;
            double heavyWeight = 0.3;
            double dis;
            double b;
            BlApi.BO.BL bl = _bl;

            BlApi.BO.DroneToList droneToList = bl.GetDrones().First(x => x.Id == droneID);

            while (!IsTimeRun())
            {
                if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.available)
                {
                    try
                    {
                        bl.ScheduleParcelToDrone(droneID);
                        ReportProgressInSimultor();
                    }
                    catch
                    {
                        if (droneToList.Battery < 100)
                        {
                            b = droneToList.Battery;

                            var station = bl.DalNearestReachableChargeSlot(new DalApi.DO.Location(droneToList.Location.Latitude, droneToList.Location.Longitude), droneToList.Id);

                            dis = bl.DalGetDistance(station.Location, new DalApi.DO.Location(droneToList.Location.Latitude, droneToList.Location.Longitude));

                            while (dis > 0)
                            {
                                droneToList.Battery -= free;
                                ReportProgressInSimultor();
                                dis -= 1;
                                Thread.Sleep(1000);
                            }

                            droneToList.Battery = b;//הפונקציה שליחה לטעינה בודקת בודקת את המרחק ההתחלתי ולפי זה מחשבת את הסוללה ולכן צריך להחזיר למצב ההתחלתי
                            bl.ChargeDrone(droneToList.Id);
                            ReportProgressInSimultor();
                        }
                    }
                }
                if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.maintenance)
                {

                    //TimeSpan interval = DateTime.Now - bl.GetBaseCharge(droneID).StartChargeTime;
                    //double horsnInCahrge = interval.Hours + (((double)interval.Minutes) / 60) + (((double)interval.Seconds) / 3600);
                    //double batrryCharge = horsnInCahrge * 10000 + droneToList.BatteryStatus; //DroneLoadingRate == 10000

                    while (droneToList.Battery < 100)
                    {
                        //AccessIbl.GetDroneList().First(x => x.Id == droneID).BatteryStatus += 3; // כל שנייה הוא מתקדם ב3%
                        droneToList.Battery += 3; // כל שנייה הוא מתקדם ב3%
                        if (droneToList.Battery > 100)//בדיקה אם כבר עברנו את ה100%
                        {
                            droneToList.Battery = 100;
                        }
                        ReportProgressInSimultor();
                        Thread.Sleep(1000);
                    }

                    bl.ReleaseDroneFromCharge(droneToList.Id); //שחרור מטעינה ברגע שהרחפן מגיע ל100

                }
                if (droneToList.Status == BlApi.BO.MyEnums.DroneStatus.delivery)
                {

                    BlApi.BO.Drone MyDrone = bl.DisplayDrone(droneID);
                    if (bl.DisplayParcel(MyDrone.DeliveredParcel.Id).PickedUp == null)
                    {
                        b = droneToList.Battery;
                        DalApi.DO.Location d = new DalApi.DO.Location(droneToList.Location.Latitude, droneToList.Location.Longitude);
                        dis = MyDrone.DeliveredParcel.Distance;
                        while (dis > 1)
                        {
                            var senderLoc = bl.SenderLocation(MyDrone.DeliveredParcel.Id);
                            droneToList.Battery -= free;
                            dis -= 1;
                            locationSteps(new DalApi.DO.Location(MyDrone.Location.Latitude, MyDrone.Location.Longitude), senderLoc, MyDrone);
                            droneToList.Location = MyDrone.Location;
                            ReportProgressInSimultor();
                            Thread.Sleep(500);
                        }
                        droneToList.Location = new BlApi.BO.Location(d.Latitude, d.Longitude);
                        droneToList.Battery = b;
                        bl.PickUpParcel(MyDrone.Id);
                        ReportProgressInSimultor();
                    }
                    else // PickedUp != null
                    {
                        b = droneToList.Battery;
                        dis = MyDrone.DeliveredParcel.Distance;

                        while (dis > 1)
                        {
                            if (droneToList.Weight == DalApi.DO.MyEnums.WeightCategory.light) droneToList.Battery -= lightWeight;
                            if (droneToList.Weight == DalApi.DO.MyEnums.WeightCategory.medium) droneToList.Battery -= mediumeight;
                            if (droneToList.Weight == DalApi.DO.MyEnums.WeightCategory.heavy) droneToList.Battery -= heavyWeight;
                            ReportProgressInSimultor();
                            dis -= 1;
                            Thread.Sleep(500);
                        }

                        droneToList.Battery = b;
                        bl.DeliverParcel(MyDrone.Id);
                        ReportProgressInSimultor();
                    }

                }
                //ReportProgressInSimultor();
                Thread.Sleep(1000);
            }

            //switch (MyDrone.Statuses)
            //{
            //    case DroneStatuses.free:

            //        break;
            //    case DroneStatuses.inMaintenance:

            //        break;
            //    case DroneStatuses.busy:

            //        break;
            //    default:
            //        break;
            //}

        }
        private void locationSteps(DalApi.DO.Location locationOfDrone, DalApi.DO.Location locationOfNextStep, BlApi.BO.Drone myDrone)
        {
            double droneLatitude = locationOfDrone.Latitude;
            double droneLongitude = locationOfDrone.Longitude;

            double nextStepLatitude = locationOfNextStep.Latitude;
            double nextStepLongitude = locationOfNextStep.Latitude;

            if (droneLatitude < nextStepLatitude)// ++++++
            {
                double step = (nextStepLatitude - droneLatitude) / myDrone.DeliveredParcel.Distance;
                myDrone.Location.Latitude += step;
            }
            else
            {
                double step = (droneLatitude - nextStepLatitude) / myDrone.DeliveredParcel.Distance;
                myDrone.Location.Latitude -= step;

            }

            if (droneLongitude < nextStepLongitude)//+++++++
            {
                double step = (nextStepLongitude - droneLongitude) / myDrone.DeliveredParcel.Distance;
                myDrone.Location.Longitude += step;
            }
            else
            {
                double step = (droneLongitude - nextStepLongitude) / myDrone.DeliveredParcel.Distance;
                myDrone.Location.Longitude -= step;

            }
            // return myDrone.CurrentLocation; 
        }
    }

    //class Simulator
    //{
    //    //const double DroneSpeed = 2;//kilometers for sec
    //    //public Simulator(int droneId, Action Worker_ProgressChanged, Func<bool> IsTimeRun)
    //    //{
    //    //    while (IsTimeRun())
    //    //    {
    //    //        {
    //    //            bl.ScheduleParcelToDrone(droneId);
    //    //            bl.PickUpParcel(droneId, DroneSpeed, Worker_ProgressChanged);
    //    //            bl.DeliverParcel(droneId, DroneSpeed, Worker_ProgressChanged);
    //    //        }
    //    //        Thread.Sleep(10000);
    //    //    }
    //    //}
    //}
}

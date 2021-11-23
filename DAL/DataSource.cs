using System;
using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// defines and initialize lists of items
            /// </summary>
            public class DataSource
            {
                //random static variable
                private static Random rd = new Random();

                //lists for items
                internal static List<IDAL.DO.Station> stations = new List<IDAL.DO.Station>();
                internal static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>();
                internal static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>();
                internal static List<IDAL.DO.DroneCharge> droneCharges = new List<IDAL.DO.DroneCharge>();
                internal static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>();
                internal class Config
                {
                    public static int ParcelRunId = 100000;

                    public static double free = 15;
                    public static double lightWeight = 30;
                    public static double mediumWeight = 50;
                    public static double heavyWeight = 75;
                    public static double DroneLoadRate = 25;
                }

                /// <summary>
                /// randomly initializes first cells of list, 
                /// </summary>
                public static void Initialize()
                {
                    //stations
                    for (int i = 0; i < 2; i++)
                    {
                        IDAL.DO.Station myStation = new IDAL.DO.Station();
                        myStation.id = rd.Next(100, 1000);
                        myStation.name = "station" + (i + 1);
                        myStation.numOfChargeSlots = rd.Next(1, 5);
                        myStation.location = new Location(rd.NextDouble() + rd.Next(180), rd.NextDouble() + rd.Next(180));
                        myStation.numOfAvailableChargeSlots  = myStation.numOfChargeSlots - rd.Next(0,1);
                        stations.Add(myStation);
                    }
                    //drones
                    for (int i = 0; i < 5; i++)
                    {
                        IDAL.DO.Drone myDrone = new IDAL.DO.Drone();
                        myDrone.id = rd.Next(100, 1000);
                        myDrone.model = "drone" + (i + 1);
                        myDrone.weight = (IDAL.DO.MyEnums.WeightCategory)rd.Next(3);
                        drones.Add(myDrone);
                    }
                    //customers
                    for (int i = 0; i < 10; i++)
                    {
                        IDAL.DO.Customer myCustomer = new IDAL.DO.Customer();
                        myCustomer.id = rd.Next(100000000, 1000000000);
                        myCustomer.name = "customer" + (i + 1);
                        myCustomer.location = new Location(rd.NextDouble() + rd.Next(180), rd.NextDouble() + rd.Next(180));
                        string phoneNumber = "05" + rd.Next(9) + "-";
                        for (int j = 0; j < 7; j++)
                            phoneNumber += rd.Next(10);
                        myCustomer.phoneNumber = phoneNumber;
                        customers.Add(myCustomer);
                    }
                    //parcels
                    for (int i = 0; i < 10; i++)
                    {
                        IDAL.DO.Parcel myParcel = new IDAL.DO.Parcel();
                        myParcel.id = IDAL.DO.DalObject.DataSource.Config.ParcelRunId++;
                        myParcel.senderId = rd.Next(100000000, 1000000000);
                        myParcel.reciverId = rd.Next(100000000, 1000000000);
                        myParcel.droneId = 0;
                        myParcel.weight = (IDAL.DO.MyEnums.WeightCategory)rd.Next(3);
                        myParcel.priority = (IDAL.DO.MyEnums.PriorityLevel)rd.Next(3);
                        myParcel.requested = DateTime.Now;
                        parcels.Add(myParcel);
                    }
                    //drone charges
                    for (int i = 0; i < 2; i++)
                    {
                        IDAL.DO.DroneCharge myDroneCharge = new IDAL.DO.DroneCharge();
                        myDroneCharge.droneId = drones[i].id;
                        myDroneCharge.stationId = stations[i].id;
                        droneCharges.Add(myDroneCharge);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using DalApi;
namespace DalApi
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// defines and initialize lists of items
            /// </summary>
            static class DataSource
            {
                //random static variable
                private static readonly Random rd = new Random();

                //lists for items
                internal static List<DalApi.DO.Station> stations = new List<DalApi.DO.Station>();
                internal static List<DalApi.DO.Drone> drones = new List<DalApi.DO.Drone>();
                internal static List<DalApi.DO.Customer> customers = new List<DalApi.DO.Customer>();
                internal static List<DalApi.DO.DroneCharge> droneCharges = new List<DalApi.DO.DroneCharge>();
                internal static List<DalApi.DO.Parcel> parcels = new List<DalApi.DO.Parcel>();
                internal class Config
                {
                    public static int ParcelRunId = 100000;
                    public static double free = 0.15;
                    public static double lightWeight = 0.3;
                    public static double mediumWeight = 0.5;
                    public static double heavyWeight = 0.75;
                    public static double DroneLoadRate = 0.25;
                }
                /// <summary>
                /// randomly initializes first cells of list, 
                /// </summary>
                internal static void Initialize()
                {

                    //stations
                    for (int i = 0; i < 10; i++)
                    {
                        DalApi.DO.Station myStation = new DalApi.DO.Station();
                        myStation.Id = rd.Next(100, 1000);
                        myStation.Name = MyEnums.stationTos[i].Name;
                        myStation.NumOfChargeSlots = rd.Next(1, 5);
                        myStation.Location = MyEnums.stationTos[i].Location;
                        myStation.NumOfAvailableChargeSlots = myStation.NumOfChargeSlots;
                        stations.Add(myStation);
                    }
                    //drones
                    for (int i = 0; i < 5; i++)
                    {
                        DalApi.DO.Drone myDrone = new DalApi.DO.Drone();
                        myDrone.Id = rd.Next(100, 1000);
                        myDrone.Model = "drone" + (i + 1);
                        myDrone.weight = (DalApi.DO.MyEnums.WeightCategory)rd.Next(3);
                        drones.Add(myDrone);
                    }
                    //customers
                    for (int i = 0; i < 10; i++)
                    {
                        DalApi.DO.Customer myCustomer = new DalApi.DO.Customer();
                        myCustomer.Id = rd.Next(100000000, 1000000000);
                        myCustomer.Name = MyEnums.NamesForCustomers[i];
                        myCustomer.Location = MyEnums.stationTos[rd.Next(0,10)].Location; 
                        string phoneNumber = "05" + rd.Next(9) + "-";
                        for (int j = 0; j < 7; j++)
                            phoneNumber += rd.Next(10);
                        myCustomer.PhoneNumber = phoneNumber;
                        customers.Add(myCustomer);
                    }
                    int existReciver = 0;
                    //parcels
                    for (int i = 0; i < 10; i++)
                    {
                        DalApi.DO.Parcel myParcel = new DalApi.DO.Parcel();
                        myParcel.Id = DalApi.DO.DalObject.DataSource.Config.ParcelRunId++;
                        myParcel.Weight = (MyEnums.WeightCategory)rd.Next(3);
                        myParcel.Requested = DateTime.Now;
                        myParcel.Priority = (MyEnums.PriorityLevel)rd.Next(3);
                        if (MyEnums.ForRd[i] == 1)
                        {
                            myParcel.DroneId = drones[rd.Next(5)].Id;
                            myParcel.SenderId = customers[i].Id;
                            myParcel.Scheduled = DateTime.Now;

                            foreach (var item in customers)
                            {
                                if (item.Id != myParcel.SenderId && item.Id != existReciver)
                                {
                                    myParcel.ReciverId = item.Id;
                                    existReciver = item.Id;
                                    break;
                                }
                            }
                        }
                        parcels.Add(myParcel);
                    }
                    //drone charges
                    for (int i = 0; i < 2; i++)
                    {
                        DalApi.DO.DroneCharge myDroneCharge = new DalApi.DO.DroneCharge();
                        myDroneCharge.DroneId = drones[i].Id;
                        myDroneCharge.StationId = stations[i].Id;
                        droneCharges.Add(myDroneCharge);
                    }
                }
            }
        }
    }
}
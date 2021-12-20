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
            internal static class DataSource
            {
                //random static variable
                private static readonly Random rd = new();

                //lists for items
                internal static List<Station> stations = new();
                internal static List<Drone> drones = new();
                internal static List<Customer> customers = new();
                internal static List<DroneCharge> droneCharges = new();
                internal static List<Parcel> parcels = new();
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
                        Station myStation = new()
                        {
                            Id = rd.Next(100, 1000),
                            Name = MyEnums.stationTos[i].Name,
                            NumOfChargeSlots = rd.Next(1, 5),
                            Location = MyEnums.stationTos[i].Location
                        };
                        myStation.NumOfAvailableChargeSlots = myStation.NumOfChargeSlots;
                        stations.Add(myStation);
                    }
                    //drones
                    for (int i = 0; i < 5; i++)
                    {
                        Drone myDrone = new()
                        {
                            Id = rd.Next(100, 1000),
                            Model = "drone" + (i + 1),
                            Weight = (MyEnums.WeightCategory)rd.Next(3)
                        };
                        drones.Add(myDrone);
                    }
                    //customers
                    for (int i = 0; i < 10; i++)
                    {
                       Customer myCustomer = new();
                        myCustomer.Id = rd.Next(100000000, 1000000000);
                        myCustomer.Name = MyEnums.NamesOfCustomers[i];
                        myCustomer.Location = MyEnums.stationTos[rd.Next(0,10)].Location;
                        int[] areaCode = new int[] { 0, 2, 4, 8};
                        string phoneNumber = "05" + areaCode[rd.Next(4)] + "-";
                        for (int j = 0; j < 7; j++)
                            phoneNumber += rd.Next(10);
                        myCustomer.PhoneNumber = phoneNumber;
                        customers.Add(myCustomer);
                    }
                    int existReceiver = 0;
                    //parcels
                    for (int i = 0; i < 10; i++)
                    {
                       Parcel myParcel = new();
                        myParcel.Id = Config.ParcelRunId++;
                        myParcel.Weight = (MyEnums.WeightCategory)rd.Next(3);
                        myParcel.Requested = null;
                        myParcel.Priority = (MyEnums.PriorityLevel)rd.Next(3);
                        if (MyEnums.ForRd[i] == 1)
                        {
                            myParcel.DroneId = drones[rd.Next(5)].Id;
                            myParcel.SenderId = customers[i].Id;
                            myParcel.Scheduled = DateTime.Now;

                            foreach (var item in customers)
                            {
                                if (item.Id != myParcel.SenderId && item.Id != existReceiver)
                                {
                                    myParcel.ReceiverId = item.Id;
                                    existReceiver = item.Id;
                                    break;
                                }
                            }
                        }
                        parcels.Add(myParcel);
                    }
                    //drone charges --
                    for (int i = 0; i < 2; i++)
                    {
                        DalApi.DO.DroneCharge myDroneCharge = new();
                        myDroneCharge.DroneId = drones[i].Id;
                        myDroneCharge.StationId = stations[i].Id;
                        droneCharges.Add(myDroneCharge);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;

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

                /// <summary>
                /// battery consumption values
                /// </summary>
                internal class Config
                {
                    public static int ParcelRunId = 100000;
                    public static double free = 0.15;
                    public static double lightWeight = 0.1;
                    public static double mediumWeight = 0.2;
                    public static double heavyWeight = 0.3;
                    public static double DroneLoadRate = 0.4;
                }

                /// <summary>
                /// randomly initializes first cells of list, 
                /// </summary>
                internal static void Initialize()
                {
                    #region initialize stations
                    //stations
                    for (int i = 0; i < 10; i++)
                    {
                        Station myStation = new()
                        {
                            Id = rd.Next(100, 1000),
                            Name = stationTos[i].Name,
                            NumOfChargeSlots = rd.Next(1, 5),
                            Location = stationTos[i].Location,
                        };
                        myStation.IsActive = true;
                        myStation.NumOfAvailableChargeSlots = myStation.NumOfChargeSlots;
                        stations.Add(myStation);
                    }
                    #endregion
                    #region initialize drones
                    //drones
                    for (int i = 0; i < 5; i++)
                    {
                        Drone myDrone = new()
                        {
                            Id = rd.Next(100, 1000),
                            Model = "drone" + (i + 1),
                            Weight = (MyEnums.WeightCategory)rd.Next(3),
                            IsActive = true

                        };
                        drones.Add(myDrone);
                    }
                    #endregion
                    #region initialize customers
                    //customers
                    for (int i = 0; i < 10; i++)
                    {
                        double lat = rd.Next(33, 36) + rd.NextDouble();
                        double longi = rd.Next(29, 33) + rd.NextDouble();
                        Customer myCustomer = new()
                        {
                            Id = rd.Next(100000000, 1000000000),
                            Name = NamesOfCustomers[i],
                            Location = new Location(lat, longi),
                            IsActive = true

                        };
                        int[] areaCode = new int[] { 0, 2, 4, 8 };
                        string phoneNumber = "05" + areaCode[rd.Next(4)] + "-";
                        for (int j = 0; j < 7; j++)
                            phoneNumber += rd.Next(10);
                        myCustomer.PhoneNumber = phoneNumber;
                        customers.Add(myCustomer);
                    }
                    #endregion
                    #region initialize parcels
                    //parcels
                    int parcelIndex1 = rd.Next(10);
                    int parcelIndex2 = rd.Next(10);
                    //verify different indexes
                    while (parcelIndex1 == parcelIndex2)
                        parcelIndex2 = rd.Next(10);
                    int droneIndex1 = rd.Next(5);
                    int droneIndex2 = rd.Next(5);
                    //verify different indexes
                    while (droneIndex1 == droneIndex2)
                        droneIndex2 = rd.Next(5);
                    for (int i = 0; i < 10; i++)
                    {
                        //constructor
                        Parcel myParcel = new()
                        {
                            Id = Config.ParcelRunId++,
                            Weight = (MyEnums.WeightCategory)rd.Next(3),
                            Priority = (MyEnums.PriorityLevel)rd.Next(3),
                            IsActive = true
                        };

                        //initialize status randomly (delivery, maintenance, available)
                        //up to two parcels in delivery
                        if (i == parcelIndex1)
                        {
                            myParcel.DroneId = drones[droneIndex1].Id;
                            myParcel.SenderId = customers[i].Id;
                            myParcel.Requested = DateTime.Now;
                            myParcel.Scheduled = DateTime.Now;
                            myParcel.ReceiverId = customers[rd.Next(10)].Id;
                            //verify different customers initialized as sender and receiver
                            while (myParcel.ReceiverId == myParcel.SenderId)
                                myParcel.ReceiverId = customers[rd.Next(10)].Id;
                        }
                        else if (i == parcelIndex2)
                        {
                            myParcel.DroneId = drones[droneIndex2].Id;
                            myParcel.SenderId = customers[i].Id;
                            myParcel.Requested = DateTime.Now;
                            myParcel.Scheduled = DateTime.Now;
                            myParcel.ReceiverId = customers[rd.Next(10)].Id;
                            //verify different customers initialized as sender and receiver
                            while (myParcel.ReceiverId == myParcel.SenderId)
                                myParcel.ReceiverId = customers[rd.Next(10)].Id;
                        }
                        //if not associated
                        else
                        {
                            myParcel.SenderId = customers[i].Id;
                            myParcel.ReceiverId = customers[rd.Next(10)].Id;
                            while (myParcel.ReceiverId == myParcel.SenderId)
                                myParcel.ReceiverId = customers[rd.Next(10)].Id;
                            myParcel.DroneId = 0;
                            myParcel.Requested = DateTime.Now;
                            myParcel.Scheduled = null;
                        }
                        parcels.Add(myParcel);
                    }
                    #endregion
                }

                #region auxiliary arrays
                /// <summary>
                /// initializes customers and statoins according to 
                /// offered names and locations
                /// </summary>
                public class StationToInitialize
                {
                    public string Name { get; set; }
                    public Location Location { get; set; }
                    public StationToInitialize() { }
                    public StationToInitialize(string myName, double lati, double longi)
                    {
                        Name = myName;
                        Location = new Location(longi, lati);
                    }
                }
                public static string[] NamesOfCustomers =
                    { "Yael Katz",
                  "Yossi Mizrahi",
                  "Ronit Peretz",
                  "Moti Klein",
                  "Sigal Shtein",
                  "Eli Ankory",
                  "Tamar Tabib",
                  "Avi Gold",
                  "Shira Lasker",
                  "Yoni Biton" };

                public static StationToInitialize[] stationTos =
                    { new StationToInitialize("Jerusalem", 31.78029702774437, 35.22149040877181),
                  new StationToInitialize("Tel Aviv", 32.082937755612186, 34.7908251395941),
                  new StationToInitialize("Haifa", 32.805995463493694, 34.99025372436736),
                  new StationToInitialize("Ashdod", 31.794795027439164, 34.651403839531135),
                  new StationToInitialize("Ariel", 32.105139, 35.189928),
                  new StationToInitialize("Beer Sheva", 31.253107, 34.786712),
                  new StationToInitialize("Netanya", 32.332, 34.855),
                  new StationToInitialize("Nahariya", 33.008629, 35.097535),
                  new StationToInitialize("Ra'anana", 32.185769, 34.870202),
                  new StationToInitialize("Ramat Gan", 32.069038, 34.824135)
                };
                #endregion
            }
        }
    }
}
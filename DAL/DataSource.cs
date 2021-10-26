using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            
            public class DataSource
            {
                //random static variable
                private static Random rd = new Random();
               
                //arrays for items
                internal static IDAL.DO.Station[] stations = new Station[5];
                internal static IDAL.DO.Drone[] drones = new Drone[10];
                internal static IDAL.DO.Customer[] customers = new Customer[100];
                internal static IDAL.DO.DroneCharge[] droneCharges = new DroneCharge[5];
                internal static IDAL.DO.Parcel[] parcels = new Parcel[1000];

                internal class Config
                {
                    //indexes for first available index in arrays
                    internal static int stationIndex = 0;
                    internal static int droneIndex = 0;
                    internal static int customerIndex = 0;
                    internal static int droneChargeIndex = 0;
                    internal static int parcelIndex = 0;
                    //running id number for parcel
                    public static int ParcelRunId = 100000;
                }
                
                /// <summary>
                /// randomly initializes first cells of arrays, 
                /// promotes indexes of availabe cells in arrays.
                /// </summary>
                public static void Initialize()
                {           
                    //stations
                    for (int i = 0; i < 2; i++)
                    {
                        stations[i].id = rd.Next(100,1000);
                        stations[i].name = "station" + (i+1);
                        stations[i].longitude = rd.NextDouble() + rd.Next(180);
                        stations[i].lattitude = rd.NextDouble() + rd.Next(180);
                        stations[i].numOfChargeSlots = rd.Next(1, 5);
                        stations[i].numOfAvailableChargeSlots = stations[i].numOfChargeSlots;
                        Config.stationIndex++;
                    }
                    //drones
                    for (int i = 0; i < 5; i++)
                    {
                        drones[i].id = rd.Next(100, 1000);
                        drones[i].model = "drone" + (i+1);
                        drones[i].weight = (IDAL.DO.MyEnums.WeightCategory)rd.Next(3);
                        drones[i].status = (IDAL.DO.MyEnums.DroneStatus)rd.Next(3);
                        drones[i].battery = rd.Next(101);
                        Config.droneIndex++;
                    }
                    //customers
                    for (int i = 0; i < 10; i++)
                    {
                        customers[i].id = rd.Next(100000000, 1000000000);
                        customers[i].name= "customer" + (i + 1);
                        customers[i].longitude = rd.NextDouble() + rd.Next(180);
                        customers[i].lattitude = rd.NextDouble() + rd.Next(180);
                        string phoneNumber = "05" + rd.Next(9) + "-";
                        for (int j = 0; j < 7; j++)
                            phoneNumber += rd.Next(10);
                        customers[i].phoneNumber = phoneNumber;
                        Config.customerIndex++;
                    }
                    //parcels
                    for (int i = 0; i < 10; i++)
                    {
                        parcels[i].id = rd.Next(100, 1000);
                        parcels[i].senderId = rd.Next(100000000, 1000000000);
                        parcels[i].targetId = rd.Next(100000000, 1000000000);
                        parcels[i].droneId = 0;
                        parcels[i].weight = (IDAL.DO.MyEnums.WeightCategory)rd.Next(3);
                        parcels[i].priority = (IDAL.DO.MyEnums.PriorityLevel)rd.Next(3);
                        parcels[i].requested = DateTime.Now;
                        Config.parcelIndex++;
                    }
                    //drone charges
                    for (int i = 0; i < 2; i++) 
                    {
                        droneCharges[i].droneId = drones[i].id;
                        droneCharges[i].stationId = stations[i].id;
                    }
                }
            }
        }
    }
}

//int rndMonth1 = rd.Next(1, 12);
//int rndDay1 = rd.Next(1, 31);
//parcels[i].scheduled =  new DateTime(2021, rndMonth1, rndDay1);
//int rndMonth2 = rd.Next(1, 12);
//int rndDay2 = rd.Next(1, 31);
//parcels[i].pickedUp = new DateTime(2021, rndMonth2, rndDay2);
//int rndMonth3 = rd.Next(1, 12);
//int rndDay3 = rd.Next(1, 31);
//parcels[i].delivered = new DateTime(2021, rndMonth3, rndDay3);
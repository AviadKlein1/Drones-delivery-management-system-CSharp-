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
                private static Random rd = new Random();
                internal static IDAL.DO.Station[] stations = new Station[5];
                internal static IDAL.DO.Drone[] drones = new Drone[10];
                internal static IDAL.DO.Customer[] customers = new Customer[100];
                internal static IDAL.DO.DroneCharge[] droneCharges = new DroneCharge[5];
                internal static IDAL.DO.Parcel[] parcels = new Parcel[1000];

                internal class Config
                {
                    internal static int stationIndex = 0;
                    internal static int droneIndex = 0;
                    internal static int customerIndex = 0;
                    internal static int droneChargeIndex = 0;
                    internal static int parcelIndex = 0;
                    //run id number for parcel
                    public static int ParcelRunId = 100000;
                }
                public static void Initialize()
                {
                    
                    //stations
                    for (int i = 0; i < 2; i++)
                    {
                        stations[i].id = rd.Next(100,999);
                        stations[i].name = "station" + (i+1);
                        stations[i].longitude = rd.NextDouble() + rd.Next(0,179);
                        stations[i].lattitude = rd.NextDouble() + rd.Next(0, 179);
                        stations[i].numOfChargeSlots = rd.Next(1, 5);
                        stations[i].numOfAvailableChargeSlots = stations[i].numOfChargeSlots;
                        Config.stationIndex++;
                    }
                    //drones
                    for (int i = 0; i < 5; i++)
                    {
                        drones[i].id = rd.Next(100, 999);
                        drones[i].model = "drone" + (i+1);
                        drones[i].weight = MyEnums.WeightCategory.lite;
                        drones[i].status = MyEnums.DroneStatus.available;
                        drones[i].battery = rd.Next(0, 100);
                        Config.droneIndex++;
                    }
                    //customers
                    for (int i = 0; i < 10; i++)
                    {
                        customers[i].id = rd.Next(100000000, 999999999);
                        customers[i].name= "customer" + (i + 1);
                        customers[i].longitude = rd.NextDouble() + rd.Next(0, 179);
                        customers[i].lattitude = rd.NextDouble() + rd.Next(0, 179);
                        customers[i].phoneNumber = "100"+(i + 1); ;
                        Config.customerIndex++;
                    }
                    //parcels
                    for (int i = 0; i < 10; i++)
                    {
                        parcels[i].id = rd.Next(100, 999);
                        parcels[i].senderId = rd.Next(100000000, 999999999);
                        parcels[i].targetId = rd.Next(100000000, 999999999);
                        parcels[i].droneId = 0;
                        parcels[i].weight = MyEnums.WeightCategory.lite;
                        parcels[i].priority = MyEnums.PriorityLevel.regular;
                        //parcels[i].requested = DateTime.Now;
                        //parcels[i].scheduled = DateTime.Now;
                        //parcels[i].pickedUp = DateTime.Now;
                        //parcels[i].delivered = DateTime.Now;
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
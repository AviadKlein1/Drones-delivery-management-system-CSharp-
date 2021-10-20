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

                internal static IDAL.DO.Station[] stations = new Station[5];
                internal static IDAL.DO.Drone[] drones = new Drone[10];
                internal static IDAL.DO.Customer[] customers = new Customer[100];
                //internal static IDAL.DO.DroneCharge[] droneCharges = new DroneCharge[5];
                internal static IDAL.DO.Parcel[] parcels = new Parcel[1000];
                internal class Config
                {
                    internal static int stationIndex = 0;
                    internal static int droneIndex = 0;
                    internal static int customerIndex = 0;
                    //internal static int droneChargeIndex = 0;
                    internal static int parcelIndex = 0;

                    //שדה של מספר מזהה רץ עבור חבילות
                }
                public static void Initialize()
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Random rd = new Random();
                        stations[i].id = rd.Next(100,999);
                        stations[i].name = "station" + (i+1);
                        stations[i].longitude = rd.Next(10,100);
                        stations[i].lattitude = rd.Next(10, 100);
                        stations[i].numOfChargeSlots = rd.Next(1, 5);
                        Config.stationIndex++;
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        Random rd = new Random();
                        drones[i].id = rd.Next(100, 999);
                        drones[i].model = "drone" + (i+1);
                        drones[i].weight = MyEnums.WeightCategory.lite;
                        drones[i].status = MyEnums.DroneStatus.available;
                        drones[i].battery = rd.Next(0, 100);
                        Config.droneIndex++;
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        Random rd = new Random();
                        customers[i].id = rd.Next(100, 999);
                        customers[i].name= "customer" + (i + 1);
                        customers[i].lattitude= rd.Next(10,100);
                        customers[i].longitude = rd.Next(10,100);
                        customers[i].phoneNumber = "100"+(i + 1); ;
                        Config.customerIndex++;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        Random rd = new Random();
                        parcels[i].id = rd.Next(100, 999);
                        parcels[i].senderId = rd.Next(100, 999);
                        parcels[i].targetId = rd.Next(100, 999);
                        parcels[i].droneId = rd.Next(100, 999);
                        parcels[i].weight = MyEnums.WeightCategory.lite;
                        parcels[i].priority = MyEnums.PriorityLevel.regular;
                        parcels[i].requested = DateTime.Today;
                        parcels[i].scheduled = DateTime.Today;
                        parcels[i].pickedUp = DateTime.Today;
                        parcels[i].delivered = DateTime.Today;
                        Config.parcelIndex++;
                    }
                    //for (int i = 0; i < 2; i++) ChrgeStations[i] = addChrgeStation();
                }
                
            }
        }
    }
}
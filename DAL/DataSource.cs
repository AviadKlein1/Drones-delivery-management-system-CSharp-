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
                internal static List<IDAL.DO.Station> stations = new List<IDAL.DO.Station>();
                internal static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>();
                internal static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>();
                internal static List<IDAL.DO.DroneCharge> droneCharges = new List<IDAL.DO.DroneCharge>();
                internal static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>();
                

                //internal static IDAL.DO.Drone[] drones = new Drone[10];
                //internal static IDAL.DO.Customer[] customers = new Customer[100];
                //internal static IDAL.DO.DroneCharge[] droneCharges = new DroneCharge[5];
                //internal static IDAL.DO.Parcel[] parcels = new Parcel[1000];

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
                    for (int i = 0; i < 2; i++)
                    {
                        IDAL.DO.Station myStation = new IDAL.DO.Station();
                        myStation.id = rd.Next(100, 1000);
                        myStation.name = "station" + (i + 1);
                        myStation.longitude = rd.NextDouble() + rd.Next(180);
                        myStation.lattitude = rd.NextDouble() + rd.Next(180);
                        myStation.numOfChargeSlots = rd.Next(1, 5);
                        myStation.numOfAvailableChargeSlots = stations[i].numOfChargeSlots;
                        //Config.stationIndex++;
                        stations.Add(myStation);
                    }
                    //drones
                    for (int i = 0; i < 5; i++)
                    {
                        IDAL.DO.Drone myDrone = new IDAL.DO.Drone();
                        myDrone.id = rd.Next(100, 1000);
                        myDrone.model = "drone" + (i+1);
                        myDrone.weight = (IDAL.DO.MyEnums.WeightCategory)rd.Next(3);
                        //drones[i].status = (IDAL.DO.MyEnums.DroneStatus)rd.Next(3);
                        //drones[i].battery = rd.Next(101);
                        //Config.droneIndex++;
                        drones.Add(myDrone);
                    }
                    //customers
                    for (int i = 0; i < 10; i++)
                    {
                        IDAL.DO.Customer myCustomer = new IDAL.DO.Customer();
                        myCustomer.id = rd.Next(100000000, 1000000000);
                        myCustomer.name= "customer" + (i + 1);
                        myCustomer.longitude = rd.NextDouble() + rd.Next(180);
                        myCustomer.lattitude = rd.NextDouble() + rd.Next(180);
                        string phoneNumber = "05" + rd.Next(9) + "-";
                        for (int j = 0; j < 7; j++)
                            phoneNumber += rd.Next(10);
                        myCustomer.phoneNumber = phoneNumber;
                        //Config.customerIndex++;
                        customers.Add(myCustomer);
                    }
                    //parcels
                    for (int i = 0; i < 10; i++)
                    {
                        IDAL.DO.Parcel myParcel = new IDAL.DO.Parcel();
                        myParcel.id = rd.Next(100, 1000);
                        myParcel.senderId = rd.Next(100000000, 1000000000);
                        myParcel.targetId = rd.Next(100000000, 1000000000);
                        myParcel.droneId = 0;
                        myParcel.weight = (IDAL.DO.MyEnums.WeightCategory)rd.Next(3);
                        myParcel.priority = (IDAL.DO.MyEnums.PriorityLevel)rd.Next(3);
                        myParcel.requested = DateTime.Now;
                        //Config.parcelIndex++;
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


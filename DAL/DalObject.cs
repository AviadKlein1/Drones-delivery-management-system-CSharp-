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
            public class DalObject : IDal
            {

                public DalObject()
                {
                    IDAL.DO.DalObject.DataSource.Initialize();
                }
                /// <summary>
                /// add to array
                /// </summary>
                public void addStation(Station myStation)
                {
                    //insert station to 

                    IDAL.DO.DalObject.DataSource.stations.Add(myStation);
                }
                /// <summary>
                /// add it to array
                /// </summary>
                public void addDrone(Drone myDrone)
                {
                    //insert drone to array
                    IDAL.DO.DalObject.DataSource.drones.Add(myDrone);
                }
                /// <summary>
                ///  add it to array
                /// </summary>
                public void addcustomer(Customer myCustomer)
                {
                    //insert customer to array
                    IDAL.DO.DalObject.DataSource.customers.Add(myCustomer);
                }
                /// <summary>
                /// add it to array
                /// </summary>
                /// returns new parcel's id
                public void addParcel(Parcel myParcel)
                {
                    //insert parcel to array
                    //IDAL.DO.DalObject.DataSource.parcels[IDAL.DO.DalObject.DataSource.Config.parcelIndex] = myParcel;
                    IDAL.DO.DalObject.DataSource.parcels.Add(myParcel);
                }
                public int ParcelRunId()
                {
                    return IDAL.DO.DalObject.DataSource.Config.ParcelRunId++;
                }








                /// <summary>
                /// belong parcel to specific drone
                /// </summary>
                public void paracelToDrone(int parcelId)
                {
                    //search and available drone 
                    //int i;
                    //bool flag = false;
                    //for (i = 0; i < IDAL.DO.DalObject.DataSource.drones.Count; i++)
                    //    if(IDAL.DO.DalObject.DataSource.drones[i].status == MyEnums.DroneStatus.available)
                    //    {
                    //        flag = true;
                    //        break;
                    //    }
                    ////if available
                    //if (flag)
                    //{
                    //    IDAL.DO.Drone temp = IDAL.DO.DalObject.DataSource.drones[i];
                    //    temp.status = MyEnums.DroneStatus.delivery;
                    //    IDAL.DO.DalObject.DataSource.drones[i] = temp;

                    //    //change status to delivery
                    //    //IDAL.DO.DalObject.DataSource.drones[i].status = MyEnums.DroneStatus.delivery;
                    //    //search parcel
                    //    //int j = 0;
                    //    //while (IDAL.DO.DalObject.DataSource.parcels[j].id != parcelId)
                    //    //    j++;
                    //    ////belong parcel
                    //    //IDAL.DO.DalObject.DataSource.parcels[j].droneId = IDAL.DO.DalObject.DataSource.drones[i].id;

                    //    //drones at i
                    //    //parcels at j
                    //    for (int j = 0; j < IDAL.DO.DalObject.DataSource.parcels.Count; j++)
                    //    {
                    //        if (IDAL.DO.DalObject.DataSource.parcels[j].id == parcelId)
                    //        {
                    //            IDAL.DO.Parcel temp2 = IDAL.DO.DalObject.DataSource.parcels[i];
                    //            temp2.droneId = IDAL.DO.DalObject.DataSource.drones[i].id;
                    //            //update scheduled time
                    //            temp2.scheduled = DateTime.Now;
                    //            IDAL.DO.DalObject.DataSource.parcels[j] = temp2;
                    //        }
                    //    }
                    //}
                }

                /// <summary>
                /// report pacel picked up
                /// </summary>
                /// <param name="myId"></param>
                public void pickUp(int myId)
                {
                    //int j = 0;
                    //while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                    //    j++;
                    //IDAL.DO.DalObject.DataSource.parcels[j].pickedUp = DateTime.Now;
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.parcels[i].id == myId)
                        {
                            IDAL.DO.Parcel temp = IDAL.DO.DalObject.DataSource.parcels[i];
                            temp.pickedUp = DateTime.Now;
                            IDAL.DO.DalObject.DataSource.parcels[i] = temp;
                        }
                    }
                }

                /// <summary>
                /// report parcel delivered
                /// </summary>
                /// <param name="myId"></param>
                public void delivered(int myId)
                {
                    //int j = 0;
                    //while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                    //    j++;
                    //IDAL.DO.DalObject.DataSource.parcels[j].delivered = DateTime.Now;
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.parcels[i].id == myId)
                        {
                            IDAL.DO.Parcel temp = IDAL.DO.DalObject.DataSource.parcels[i];
                            temp.delivered = DateTime.Now;
                            IDAL.DO.DalObject.DataSource.parcels[i] = temp;
                        }
                    }
                }

                /// <summary>
                /// send drone to charge slot
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="stationId"></param>
                public void sendToCharge(int droneId, int stationId)
                {
                    //    //create a new item "drone charge"
                    //    IDAL.DO.DroneCharge myDroneCharge = new DroneCharge();
                    //    int j = 0;
                    //    while (IDAL.DO.DalObject.DataSource.drones[j].id != droneId)
                    //        j++;
                    //    //update drone status - maintenance
                    //    IDAL.DO.DalObject.DataSource.drones[j].status = MyEnums.DroneStatus.maintenance;
                    //    myDroneCharge.droneId = IDAL.DO.DalObject.DataSource.drones[j].id;

                    //    //int k = 0;
                    //    //while (IDAL.DO.DalObject.DataSource.stations[k].id != stationId)
                    //    //    k++;

                    //    //myDroneCharge.stationId = IDAL.DO.DalObject.DataSource.stations[k].id;
                    //    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    //    {
                    //        if (IDAL.DO.DalObject.DataSource.stations[i].id == stationId)
                    //        {
                    //            IDAL.DO.Station temp = IDAL.DO.DalObject.DataSource.stations[i];
                    //            temp.id = stationId;
                    //            //update number of available charge slots in station
                    //            temp.numOfAvailableChargeSlots--;
                    //            IDAL.DO.DalObject.DataSource.stations[i] = temp;
                    //        }
                    //    }
                    //    IDAL.DO.DalObject.DataSource.droneCharges.Add( myDroneCharge);
                }

                /// <summary>
                /// report drone ended charging
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="stationId"></param>
                public void endCharge(int droneId, int stationId)
                {
                    //int j = 0;
                    //while (IDAL.DO.DalObject.DataSource.drones[j].id != droneId)
                    //    j++;
                    ////update drone status to available
                    //IDAL.DO.DalObject.DataSource.drones[j].status = MyEnums.DroneStatus.available;

                    ////int k = 0;
                    ////while (IDAL.DO.DalObject.DataSource.stations[k].id != stationId)
                    ////    k++;
                    //for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    //{
                    //    if (IDAL.DO.DalObject.DataSource.stations[i].id == stationId)
                    //    {
                    //        IDAL.DO.Station temp = IDAL.DO.DalObject.DataSource.stations[i];
                    //        //update number of available charge slots in station
                    //        temp.numOfAvailableChargeSlots++;
                    //        IDAL.DO.DalObject.DataSource.stations[i] = temp;
                    //    }
                    //}
                }

                /// <summary>
                /// print stations details
                /// </summary>
                /// <param name="myId"></param>
                public Station getStation(int myId)
                {
                    Station temp = new Station();

                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    {
                        try
                        {
                            if (IDAL.DO.DalObject.DataSource.stations[i].id == myId)
                            {
                                temp = IDAL.DO.DalObject.DataSource.stations[i];
                            }
                        }
                    }
                    return temp;
                }
                public IDAL.DO.Drone getDrone(int myId)
                {
                    Drone temp = new Drone();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.drones.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.drones[i].id == myId)
                        {
                            temp = IDAL.DO.DalObject.DataSource.drones[i];
                        }
                    }
                    return temp;
                }
                public IDAL.DO.Customer getCustomer(int myId)
                {
                    Customer temp = new Customer();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.customers.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.customers[i].id == myId)
                        {
                            temp = IDAL.DO.DalObject.DataSource.customers[i];
                        }
                    }
                    return temp;
                }
                public IDAL.DO.Parcel getParcel(int myId)
                {
                    Parcel temp = new Parcel();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.parcels[i].id == myId)
                        {
                            temp = IDAL.DO.DalObject.DataSource.parcels[i];
                        }
                    }
                    return temp;
                }
                /// <summary>
                /// print drone details
                /// </summary>
                /// <param name="myId"></param>
                //public void droneDisplay(int myId)
                //{
                //    //int j = 0;
                //    //while (IDAL.DO.DalObject.DataSource.drones[j].id != myId)
                //    //    j++;
                //    //Console.WriteLine(IDAL.DO.DalObject.DataSource.drones[j]);
                //    for (int i = 0; i < IDAL.DO.DalObject.DataSource.drones.Count; i++)
                //    {
                //        if (IDAL.DO.DalObject.DataSource.drones[i].id == myId)
                //        {
                //            Console.WriteLine(IDAL.DO.DalObject.DataSource.drones[i]);
                //        }
                //    }
                //}

                ///// <summary>
                ///// print customer details
                ///// </summary>
                ///// <param name="myId"></param>
                //public void customerDisplay(int myId)
                //{
                //    //int j = 0;
                //    //while (IDAL.DO.DalObject.DataSource.customers[j].id != myId)
                //    //    j++;
                //    //Console.WriteLine(IDAL.DO.DalObject.DataSource.customers[j]);
                //    for (int i = 0; i < IDAL.DO.DalObject.DataSource.customers.Count; i++)
                //    {
                //        if (IDAL.DO.DalObject.DataSource.customers[i].id == myId)
                //        {
                //            Console.WriteLine(IDAL.DO.DalObject.DataSource.customers[i]);
                //        }
                //    }
                //}
                ///// <summary>
                ///// print parcel details
                ///// </summary>
                ///// <param name="myId"></param>
                //public void parcelDisplay(int myId)
                //{
                //    //int j = 0;
                //    //while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                //    //    j++;
                //    //Console.WriteLine(IDAL.DO.DalObject.DataSource.parcels[j]);
                //    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                //    {
                //        if (IDAL.DO.DalObject.DataSource.parcels[i].id == myId)
                //        {
                //            Console.WriteLine(IDAL.DO.DalObject.DataSource.parcels[i]);
                //        }
                //    }
                //}

                /// <summary>
                /// print all stations
                /// </summary>
                public IEnumerable<Station> getStations()
                {
                    //int size = IDAL.DO.DalObject.DataSource.Config.stationIndex;
                    ////create a new array for stations
                    //IDAL.DO.Station[] stationsToDisplay = new Station[IDAL.DO.DalObject.DataSource.Config.stationIndex];
                    //for (int i = 0; i < size; i++)
                    //    stationsToDisplay[i] = IDAL.DO.DalObject.DataSource.stations[i];
                    ////print stations
                    //for (int j = 0; j < size; j++)
                    //    Console.WriteLine(stationsToDisplay[j]);

                    List<IDAL.DO.Station> temp = new List<IDAL.DO.Station>();
                    temp = IDAL.DO.DalObject.DataSource.stations;
                    return temp;
                }


                /// <summary>
                /// print all drones
                /// </summary>
                public IEnumerable<Drone> getDrones()
                {
                    //int size = IDAL.DO.DalObject.DataSource.Config.droneIndex;
                    ////create a new array for drones
                    //IDAL.DO.Drone[] dronesToDisplay = new Drone[IDAL.DO.DalObject.DataSource.Config.droneIndex];
                    //for (int i = 0; i < size; i++)
                    //    dronesToDisplay[i] = IDAL.DO.DalObject.DataSource.drones[i];
                    ////print drones
                    //for (int j = 0; j < size; j++)
                    //    Console.WriteLine(dronesToDisplay[j]);

                    List<IDAL.DO.Drone> temp = new List<IDAL.DO.Drone>();
                    temp = IDAL.DO.DalObject.DataSource.drones;
                    return temp;
                }
                /// <summary>
                /// print all customers
                /// </summary>
                public IEnumerable<Customer> getCustomers()
                {
                    //int size = IDAL.DO.DalObject.DataSource.Config.customerIndex;
                    ////create a new array for customers
                    //IDAL.DO.Customer[] customersToDisplay = new Customer[IDAL.DO.DalObject.DataSource.Config.customerIndex];
                    //for (int i = 0; i < size; i++)
                    //    customersToDisplay[i] = IDAL.DO.DalObject.DataSource.customers[i];
                    ////print customers
                    //for (int j = 0; j < size; j++)
                    //    Console.WriteLine(customersToDisplay[j]);
                    List<IDAL.DO.Customer> temp = new List<IDAL.DO.Customer>();
                    temp = IDAL.DO.DalObject.DataSource.customers;
                    return temp;
                }

                /// <summary>
                /// print all parcels
                /// </summary>
                public IEnumerable<Parcel> getParcels()
                {
                    //int size = IDAL.DO.DalObject.DataSource.Config.parcelIndex;
                    ////create a new array for parcels
                    //IDAL.DO.Parcel[] parcelsToDisplay = new Parcel[IDAL.DO.DalObject.DataSource.Config.parcelIndex];
                    //for (int i = 0; i < size; i++)
                    //    parcelsToDisplay[i] = IDAL.DO.DalObject.DataSource.parcels[i];
                    ////print parcels
                    //for (int j = 0; j < size; j++)
                    //    Console.WriteLine(parcelsToDisplay[j]);
                    List<IDAL.DO.Parcel> temp = new List<IDAL.DO.Parcel>();
                    temp = IDAL.DO.DalObject.DataSource.parcels;
                    return temp;
                }

                /// <summary>
                /// print all parcels which are not associated with a drone
                /// </summary>
                public IEnumerable<Parcel> getNotAssociatedParcels()
                {
                    //find size of new array for parcels
                    List<IDAL.DO.Parcel> temp = new List<IDAL.DO.Parcel>();
                    temp = IDAL.DO.DalObject.DataSource.parcels;
                    int size = IDAL.DO.DalObject.DataSource.parcels.Count;
                    //int count = 0;
                    ////////for (int i = 0; i < size; i++)
                    ////////    if (IDAL.DO.DalObject.DataSource.parcels[i].droneId == 0)
                    ////////        temp.Add(IDAL.DO.DalObject.DataSource.parcels[i]);
                    //count++;
                    ////create a new array for parcels
                    //IDAL.DO.Parcel[] notAssociatedParcels = new Parcel[count];
                    //int j = 0;
                    //for (int i = 0; i < count; i++)
                    //    if (IDAL.DO.DalObject.DataSource.parcels[i].droneId == 0)
                    //        notAssociatedParcels[j++] = IDAL.DO.DalObject.DataSource.parcels[i];
                    ////print parcels
                    //for(int k=0; k < count; k++ )
                    //    Console.WriteLine(notAssociatedParcels[k]);

                    return temp;
                }

                /// <summary>
                /// print all available to charge stations
                /// </summary>
                public IEnumerable<Station> getAvailableToChargeStations()
                {
                    int size = IDAL.DO.DalObject.DataSource.stations.Count;
                    List<IDAL.DO.Station> temp = new List<IDAL.DO.Station>();
                    temp = IDAL.DO.DalObject.DataSource.stations;
                    //int count = 0;
                    //find size of new array for stations
                    for (int i = 0; i < size; i++)
                        if (IDAL.DO.DalObject.DataSource.stations[i].numOfAvailableChargeSlots > 0)
                            temp.Add(IDAL.DO.DalObject.DataSource.stations[i]);
                    return temp;
                    //count++;
                    ////create a new array for stations
                    //IDAL.DO.Station[] availableToChargeStattions = new Station[count];

                    //int j = 0;
                    //for (int i = 0; i < count; i++)
                    //    if (IDAL.DO.DalObject.DataSource.stations[i].numOfAvailableChargeSlots > 0)
                    //        availableToChargeStattions[j++] = IDAL.DO.DalObject.DataSource.stations[i];
                    ////ptint stations
                    //for (int i = 0; i < count; i++)
                    //    Console.WriteLine(availableToChargeStattions[i]);
                }

                public double[] droneElectricityConsumption()
                {
                    double[] droneElectricityConsumption = new double[5];
                    droneElectricityConsumption[0] = IDAL.DO.DalObject.DataSource.Config.avialable;
                    droneElectricityConsumption[1] = IDAL.DO.DalObject.DataSource.Config.lightWeight;
                    droneElectricityConsumption[2] = IDAL.DO.DalObject.DataSource.Config.mediumWeight;
                    droneElectricityConsumption[3] = IDAL.DO.DalObject.DataSource.Config.heavyWeight;
                    droneElectricityConsumption[4] = IDAL.DO.DalObject.DataSource.Config.DroneLoadRate;
                    return droneElectricityConsumption;
                }

                public IDAL.DO.Location stationLocate(int StationId)
                {
                    Location temp = new Location();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.stations[i].id == StationId)
                        {
                            temp.lattitude = IDAL.DO.DalObject.DataSource.stations[i].lattitude;
                            temp.longitude = IDAL.DO.DalObject.DataSource.stations[i].longitude;

                        }
                    }
                    return temp;
                }

                public bool thereAreParcelsThatNotDeliverdYet()
                {
                    var dalParcelsList = getParcels();
                    foreach (var element in dalParcelsList)
                    {
                        DateTime emptyDateTime = new DateTime();
                        if (element.delivered == emptyDateTime) return true;
                    }
                    return false;
                }
                public bool thisDroneIsAssociated(int droneId)
                {
                    var dalParcelsList = getParcels();
                    foreach (var element in dalParcelsList)
                    {
                        if (element.droneId == droneId) return true;
                    }
                    return false;
                }
                public bool myParcelScheduledButNotPickedUp(int parcelId)
                {
                    var dalParcelsList = getParcels();
                    foreach (var element in dalParcelsList)
                    {
                        if (element.id == parcelId)
                        {
                            DateTime emptyDateTime = new DateTime();
                            if (element.pickedUp == emptyDateTime && element.scheduled != emptyDateTime) return true;
                        }
                    }
                    return false;
                }

                public bool myParcelPickedUpButNotDeliverd(int parcelId)
                {
                    var dalParcelsList = getParcels();
                    foreach (var element in dalParcelsList)
                    {
                        if (element.id == parcelId)
                        {
                            DateTime emptyDateTime = new DateTime();
                            if (element.delivered == emptyDateTime && element.pickedUp != emptyDateTime) return true;
                        }
                    }
                    return false;
                }
                public IDAL.DO.Location theNearestToSenderStationLocation(int parcelId)
                {
                    Location tempLocation = new Location();
                    var dalParcelsList = getParcels();
                    foreach (var pElement in dalParcelsList)
                    {
                        if (pElement.id == parcelId)
                        {
                            var customersList = getCustomers();

                            foreach (var cElement in customersList)
                            {

                                (cElement.);
                            }
                        }
                    }
                    return tempLocation;
                }
                public Station theNearestStation(Location l)
                {
                    Station tempStation = new Station();
                    var stationList = getStations();
                    double min = 99999999999;
                    foreach (var element in stationList)
                    {
                        distance(l, element.)
                    }
                }
                public double distance(Location a, Location b)
                {
                    return Math.Sqrt(Math.Pow((b.longitude - a.longitude), 2) -
                        Math.Pow((b.lattitude - a.lattitude), 2));
                }
            }
        }
    }
}
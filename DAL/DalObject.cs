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
            public class DalObject
            {
                public DalObject()
                {
                    IDAL.DO.DalObject.DataSource.Initialize();
                }

                /// <summary>
                /// create a new station and add to array
                /// </summary>
                public void addStation()
                {
                    Station myStation = new Station();
                    //recieve details from user
                    int id;
                    Console.WriteLine("enter id");
                    int.TryParse(Console.ReadLine(), out id);
                    myStation.id = id;

                    Console.WriteLine("enter name");
                    myStation.name = (Console.ReadLine());

                    double longitude;
                    Console.WriteLine("enter longitude");
                    double.TryParse(Console.ReadLine(), out longitude);
                    myStation.longitude = longitude;

                    double lattitude;
                    Console.WriteLine("enter lattitude");
                    double.TryParse(Console.ReadLine(), out lattitude);
                    myStation.lattitude = lattitude;

                    int numOfChargeSlots;
                    Console.WriteLine("enter number of charge slots");
                    int.TryParse(Console.ReadLine(), out numOfChargeSlots);
                    myStation.numOfChargeSlots = numOfChargeSlots;
                    myStation.numOfAvailableChargeSlots = numOfChargeSlots;

                    //insert station to array
                    IDAL.DO.DalObject.DataSource.stations[IDAL.DO.DalObject.DataSource.Config.stationIndex] = myStation;
                    //print new station
                    Console.WriteLine("\n" + IDAL.DO.DalObject.DataSource.stations[IDAL.DO.DalObject.DataSource.Config.stationIndex]);
                    //promote index points to first empty cell
                    IDAL.DO.DalObject.DataSource.Config.stationIndex++;
                }

                /// <summary>
                /// create a new drone, and add it to array
                /// </summary>
                public void addDrone()
                {
                    Drone myDrone = new Drone();
                    //recieve details from user
                    int id;
                    Console.WriteLine("enter id");
                    int.TryParse(Console.ReadLine(), out id);
                    myDrone.id = id;

                    Console.WriteLine("enter model");
                    myDrone.model = (Console.ReadLine());

                    int choice=0;
                    Console.WriteLine("enter status (available = 1, maintenance = 2, delivery = 3)");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1) myDrone.status = MyEnums.DroneStatus.available;
                    if (choice == 2) myDrone.status = MyEnums.DroneStatus.maintenance;
                    if (choice == 3) myDrone.status = MyEnums.DroneStatus.delivery;

                    Console.WriteLine("enter weight (lite = 1, medium = 2, heavy = 3)");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1) myDrone.weight = MyEnums.WeightCategory.lite;
                    if (choice == 2) myDrone.weight = MyEnums.WeightCategory.medium;
                    if (choice == 3) myDrone.weight = MyEnums.WeightCategory.heavy;

                    int battaryStatus;
                    Console.WriteLine("enter battary status");
                    int.TryParse(Console.ReadLine(), out battaryStatus);
                    myDrone.battery = battaryStatus;

                    //insert srone to array
                    IDAL.DO.DalObject.DataSource.drones[IDAL.DO.DalObject.DataSource.Config.droneIndex] = myDrone;
                    //print new drone
                    Console.WriteLine("\n" + IDAL.DO.DalObject.DataSource.drones[IDAL.DO.DalObject.DataSource.Config.droneIndex]);
                    //promote index points to first empty cell
                    IDAL.DO.DalObject.DataSource.Config.droneIndex++;
                    IDAL.DO.DalObject.DataSource.drones[IDAL.DO.DalObject.DataSource.Config.droneIndex] = myDrone;
                }

                /// <summary>
                /// create a new customer and add it to array
                /// </summary>
                public void addcustomer()
                {
                    Customer myCustomer = new Customer();
                    //recieve daetauls from user
                    int id;
                    Console.WriteLine("enter id");
                    int.TryParse(Console.ReadLine(), out id);
                    myCustomer.id = id;

                    Console.WriteLine("enter name");
                    myCustomer.name = (Console.ReadLine());

                    Console.WriteLine("enter phone number");
                    myCustomer.phoneNumber = (Console.ReadLine());

                    double longitude;
                    Console.WriteLine("enter longitude");
                    double.TryParse(Console.ReadLine(), out longitude);
                    myCustomer.longitude = longitude;

                    double lattitude;
                    Console.WriteLine("enter lattitude");
                    double.TryParse(Console.ReadLine(), out lattitude);
                    myCustomer.lattitude = lattitude;

                    //insert customer to array
                    IDAL.DO.DalObject.DataSource.customers[IDAL.DO.DalObject.DataSource.Config.customerIndex] = myCustomer;
                    //print new customer
                    Console.WriteLine("\n" + IDAL.DO.DalObject.DataSource.customers[IDAL.DO.DalObject.DataSource.Config.customerIndex]);
                    //promote index points to first empty cell
                    IDAL.DO.DalObject.DataSource.Config.customerIndex++;
                }

                /// <summary>
                /// create a new parcel and add it to array
                /// </summary>
                /// returns new parcel's id
                public int addParcel()
                {
                    Parcel myParcel = new Parcel();
                    myParcel.id = IDAL.DO.DalObject.DataSource.Config.ParcelRunId;

                    int senderId;
                    Console.WriteLine("enter sender id");
                    int.TryParse(Console.ReadLine(), out senderId);
                    myParcel.senderId = senderId;

                    int targetId;
                    Console.WriteLine("enter target id");
                    int.TryParse(Console.ReadLine(), out targetId);
                    myParcel.targetId = targetId;

                    int droneId;
                    Console.WriteLine("enter drone id");
                    int.TryParse(Console.ReadLine(), out droneId);
                    myParcel.droneId = droneId;

                    int choice;
                    Console.WriteLine("enter weight (lite = 1, medium = 2, heavy = 3)");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1) myParcel.weight = MyEnums.WeightCategory.lite;
                    if (choice == 2) myParcel.weight = MyEnums.WeightCategory.medium;
                    if (choice == 3) myParcel.weight = MyEnums.WeightCategory.heavy;

                    Console.WriteLine("enter priority (regular = 1, quickly = 2, ergent = 3 )");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1) myParcel.priority = MyEnums.PriorityLevel.regular;
                    if (choice == 2) myParcel.priority = MyEnums.PriorityLevel.quickly;
                    if (choice == 3) myParcel.priority = MyEnums.PriorityLevel.ergent;                    
                    myParcel.requested = DateTime.Now;

                    //insert pacel to array
                    IDAL.DO.DalObject.DataSource.parcels[IDAL.DO.DalObject.DataSource.Config.parcelIndex] = myParcel;
                    //promote index to first empty cell
                    IDAL.DO.DalObject.DataSource.Config.parcelIndex++;
                    //print new parcel
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.parcels[IDAL.DO.DalObject.DataSource.Config.parcelIndex]);
                    //return parcel id
                    return IDAL.DO.DalObject.DataSource.Config.ParcelRunId;
                }

                /// <summary>
                /// belong parcel to specific drone
                /// </summary>
                public void paracelToDrone(int parcelId)
                {
                    //search and available drone 
                    int i;
                    bool flag = false;
                    for (i = 0; i < IDAL.DO.DalObject.DataSource.Config.droneIndex; i++)
                        if(IDAL.DO.DalObject.DataSource.drones[i].status == MyEnums.DroneStatus.available)
                        {
                            flag = true;
                            break;
                        }
                    //if available
                    if (flag)
                    {
                        //change status to delivery
                        IDAL.DO.DalObject.DataSource.drones[i].status = MyEnums.DroneStatus.delivery;
                        //search parcel
                        int j = 0;
                        while (IDAL.DO.DalObject.DataSource.parcels[j].id != parcelId)
                            j++;
                        //ubelong pacel
                        IDAL.DO.DalObject.DataSource.parcels[j].droneId = IDAL.DO.DalObject.DataSource.drones[i].id;
                        //update scheduled time
                        IDAL.DO.DalObject.DataSource.parcels[j].scheduled = DateTime.Now;
                    }
                }

                /// <summary>
                /// report pacel picked up
                /// </summary>
                /// <param name="myId"></param>
                public void pickUp(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                        j++;
                    IDAL.DO.DalObject.DataSource.parcels[j].pickedUp = DateTime.Now;
                }


                /// <summary>
                /// report parcel delivered
                /// </summary>
                /// <param name="myId"></param>
                public void delivered(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                        j++;
                    IDAL.DO.DalObject.DataSource.parcels[j].delivered = DateTime.Now;
                }
                
                /// <summary>
                /// send drone to charge slot
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="stationId"></param>
                public void sendToCharge(int droneId, int stationId)
                {
                    //create a new item "drone charge"
                    IDAL.DO.DroneCharge myDroneCharge = new DroneCharge();
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.drones[j].id != droneId)
                        j++;
                    //update drone status - maintenance
                    IDAL.DO.DalObject.DataSource.drones[j].status = MyEnums.DroneStatus.maintenance;
                    myDroneCharge.droneId = IDAL.DO.DalObject.DataSource.drones[j].id;

                    int k = 0;
                    while (IDAL.DO.DalObject.DataSource.stations[k].id != stationId)
                        k++;

                    myDroneCharge.stationId = IDAL.DO.DalObject.DataSource.stations[k].id;
                    //update number of available charge slots in station
                    IDAL.DO.DalObject.DataSource.stations[k].numOfAvailableChargeSlots--;
                    IDAL.DO.DalObject.DataSource.droneCharges[IDAL.DO.DalObject.DataSource.Config.droneChargeIndex++] = myDroneCharge;
                }

                /// <summary>
                /// report drone ended charging
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="stationId"></param>
                public void endCharge(int droneId, int stationId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.drones[j].id != droneId)
                        j++;
                    //update drone status to available
                    IDAL.DO.DalObject.DataSource.drones[j].status = MyEnums.DroneStatus.available;
                    int k = 0;
                    while (IDAL.DO.DalObject.DataSource.stations[k].id != stationId)
                        k++;
                    //update number of available charge slots in station
                    IDAL.DO.DalObject.DataSource.stations[k].numOfAvailableChargeSlots++;
                }

                /// <summary>
                /// print stations details
                /// </summary>
                /// <param name="myId"></param>
                public void stationDisplay(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.stations[j].id != myId)
                        j++;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.stations[j]);
                }

                /// <summary>
                /// print drone details
                /// </summary>
                /// <param name="myId"></param>
                public void droneDisplay(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.drones[j].id != myId)
                        j++;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.drones[j]);
                }

                /// <summary>
                /// print customer details
                /// </summary>
                /// <param name="myId"></param>
                public void customerDisplay(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.customers[j].id != myId)
                        j++;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.customers[j]);
                }

                /// <summary>
                /// print parcel details
                /// </summary>
                /// <param name="myId"></param>
                public void parcelDisplay(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                        j++;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.parcels[j]);
                }

                /// <summary>
                /// print all stations
                /// </summary>
                public void stationsDisplay()
                {
                    int size = IDAL.DO.DalObject.DataSource.Config.stationIndex;
                    //create a new array for stations
                    IDAL.DO.Station[] stationsToDisplay = new Station[IDAL.DO.DalObject.DataSource.Config.stationIndex];
                    for (int i = 0; i < size; i++)
                        stationsToDisplay[i] = IDAL.DO.DalObject.DataSource.stations[i];
                    //print stations
                    for (int j = 0; j < size; j++)
                        Console.WriteLine(stationsToDisplay[j]);
                }

                /// <summary>
                /// print all drones
                /// </summary>
                public void dronesDisplay()
                {
                    int size = IDAL.DO.DalObject.DataSource.Config.droneIndex;
                    //create a new array for drones
                    IDAL.DO.Drone[] dronesToDisplay = new Drone[IDAL.DO.DalObject.DataSource.Config.droneIndex];
                    for (int i = 0; i < size; i++)
                        dronesToDisplay[i] = IDAL.DO.DalObject.DataSource.drones[i];
                    //print drones
                    for (int j = 0; j < size; j++)
                        Console.WriteLine(dronesToDisplay[j]);
                }

                /// <summary>
                /// print all customers
                /// </summary>
                public void customersDisplay()
                {
                    int size = IDAL.DO.DalObject.DataSource.Config.customerIndex;
                    //create a new array for customers
                    IDAL.DO.Customer[] customersToDisplay = new Customer[IDAL.DO.DalObject.DataSource.Config.customerIndex];
                    for (int i = 0; i < size; i++)
                        customersToDisplay[i] = IDAL.DO.DalObject.DataSource.customers[i];
                    //print customers
                    for (int j = 0; j < size; j++)
                        Console.WriteLine(customersToDisplay[j]);
                }

                /// <summary>
                /// print all parcels
                /// </summary>
                public void parcelsDisplay()
                {
                    int size = IDAL.DO.DalObject.DataSource.Config.parcelIndex;
                    //create a new array for parcels
                    IDAL.DO.Parcel[] parcelsToDisplay = new Parcel[IDAL.DO.DalObject.DataSource.Config.parcelIndex];
                    for (int i = 0; i < size; i++)
                        parcelsToDisplay[i] = IDAL.DO.DalObject.DataSource.parcels[i];
                    //print parcels
                    for (int j = 0; j < size; j++)
                        Console.WriteLine(parcelsToDisplay[j]);
                }

                /// <summary>
                /// print all parcels which are not associated with a drone
                /// </summary>
                public void notAssociatedParcelsDisplay()
                {
                    //find size of new array for parcels
                    int size = IDAL.DO.DalObject.DataSource.Config.parcelIndex;
                    int count = 0;
                    for (int i = 0; i < size ; i++)
                        if (IDAL.DO.DalObject.DataSource.parcels[i].droneId == 0)
                            count++;
                    //create a new array for parcels
                    IDAL.DO.Parcel[] notAssociatedParcels = new Parcel[count];
                    int j = 0;
                    for (int i = 0; i < count; i++)
                        if (IDAL.DO.DalObject.DataSource.parcels[i].droneId == 0)
                            notAssociatedParcels[j++] = IDAL.DO.DalObject.DataSource.parcels[i];
                    //print parcels
                    for(int k=0; k < count; k++ )
                        Console.WriteLine(notAssociatedParcels[k]);
                }

                /// <summary>
                /// print all available to charge stations
                /// </summary>
                public void availableToChargeStattions()
                {
                    int size = IDAL.DO.DalObject.DataSource.Config.stationIndex;
                    int count = 0;
                    //find size of new array for stations
                    for (int i = 0; i < size; i++)
                        if (IDAL.DO.DalObject.DataSource.stations[i].numOfAvailableChargeSlots > 0)
                            count++;
                    //create a new array for stations
                    IDAL.DO.Station[] availableToChargeStattions = new Station[count];
  
                    int j = 0;
                    for (int i = 0; i < count; i++)
                        if (IDAL.DO.DalObject.DataSource.stations[i].numOfAvailableChargeSlots > 0)
                            availableToChargeStattions[j++] = IDAL.DO.DalObject.DataSource.stations[i];
                    //ptint stations
                    for (int i = 0; i < count; i++)
                        Console.WriteLine(availableToChargeStattions[i]);
                }
            }
        }
    }
}
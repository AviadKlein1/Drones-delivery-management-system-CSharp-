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

                public void addStation()
                {
                    Station myStation = new Station();
                    int id;
                    Console.WriteLine("enter id\n");
                    int.TryParse(Console.ReadLine(), out id); /// to check if works
                    myStation.id = id;

                    Console.WriteLine("enter name\n");
                    myStation.name = (Console.ReadLine());

                    double longitude;
                    Console.WriteLine("enter longitude\n");
                    double.TryParse(Console.ReadLine(), out longitude);
                    myStation.longitude = longitude;

                    double lattitude;
                    Console.WriteLine("enter lattitude\n");
                    double.TryParse(Console.ReadLine(), out lattitude);
                    myStation.lattitude = lattitude;

                    int numOfChargeSlots;
                    Console.WriteLine("enter number of charge slots\n");
                    int.TryParse(Console.ReadLine(), out numOfChargeSlots);
                    myStation.numOfChargeSlots = numOfChargeSlots;

                    IDAL.DO.DalObject.DataSource.stations[IDAL.DO.DalObject.DataSource.Config.stationIndex] = myStation;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.stations[IDAL.DO.DalObject.DataSource.Config.stationIndex]);
                    IDAL.DO.DalObject.DataSource.Config.stationIndex++;
                }//add new ststion
                public void addDrone()
                {
                    Drone myDrone = new Drone();
                    int id;
                    Console.WriteLine("enter id\n");
                    int.TryParse(Console.ReadLine(), out id); /// to check if works
                    myDrone.id = id;

                    Console.WriteLine("enter model\n");
                    myDrone.model = (Console.ReadLine());

                    int choice=0;
                    Console.WriteLine("enter status(available =1, maintenance = 2, delivery =3)\n");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1) myDrone.status = MyEnums.DroneStatus.available;
                    if (choice == 2) myDrone.status = MyEnums.DroneStatus.maintenance;
                    if (choice == 3) myDrone.status = MyEnums.DroneStatus.delivery;

                    Console.WriteLine("enter weight(lite =1, medium =2, heavy =3 )\n");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1) myDrone.weight = MyEnums.WeightCategory.lite;
                    if (choice == 2) myDrone.weight = MyEnums.WeightCategory.medium;
                    if (choice == 3) myDrone.weight = MyEnums.WeightCategory.heavy;

                    int battaryStatus;
                    Console.WriteLine("enter battary status\n");
                    int.TryParse(Console.ReadLine(), out battaryStatus);
                    myDrone.battery = battaryStatus;


                    IDAL.DO.DalObject.DataSource.drones[IDAL.DO.DalObject.DataSource.Config.droneIndex] = myDrone;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.drones[IDAL.DO.DalObject.DataSource.Config.droneIndex]);
                    IDAL.DO.DalObject.DataSource.Config.droneIndex++;
                }
                public void addcustomer()
                {
                    Customer myCustomer = new Customer();
                    int id;
                    Console.WriteLine("enter id\n");
                    int.TryParse(Console.ReadLine(), out id); /// to check if works
                    myCustomer.id = id;

                    Console.WriteLine("enter name\n");
                    myCustomer.name = (Console.ReadLine());

                    Console.WriteLine("enter phone number\n");
                    myCustomer.phoneNumber = (Console.ReadLine());

                    double longitude;
                    Console.WriteLine("enter longitude\n");
                    double.TryParse(Console.ReadLine(), out longitude);
                    myCustomer.longitude = longitude;

                    double lattitude;
                    Console.WriteLine("enter lattitude\n");
                    double.TryParse(Console.ReadLine(), out lattitude);
                    myCustomer.lattitude = lattitude;
                }
                public void addParcel()
                {
                    Parcel myParcel = new Parcel();
                    int id;
                    Console.WriteLine("enter id\n");
                    int.TryParse(Console.ReadLine(), out id); /// to check if works
                    myParcel.id = id;

                    int senderId;
                    Console.WriteLine("enter sender id\n");
                    int.TryParse(Console.ReadLine(), out senderId); /// to check if works
                    myParcel.senderId = senderId;

                    int targetId;
                    Console.WriteLine("enter sender id\n");
                    int.TryParse(Console.ReadLine(), out targetId); /// to check if works
                    myParcel.targetId = targetId;

                    int droneId;
                    Console.WriteLine("enter sender id\n");
                    int.TryParse(Console.ReadLine(), out droneId); /// to check if works
                    myParcel.droneId = droneId;

                    int choice;
                    Console.WriteLine("enter weight(lite =1, medium =2, heavy =3 )\n");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1) myParcel.weight = MyEnums.WeightCategory.lite;
                    if (choice == 2) myParcel.weight = MyEnums.WeightCategory.medium;
                    if (choice == 3) myParcel.weight = MyEnums.WeightCategory.heavy;

                    Console.WriteLine("enter priority(regular =1, quickly =2, ergent=3 )\n");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1) myParcel.priority = MyEnums.PriorityLevel.regular;
                    if (choice == 2) myParcel.priority = MyEnums.PriorityLevel.quickly;
                    if (choice == 3) myParcel.priority = MyEnums.PriorityLevel.ergent;
                    // only knew the create time of paracel
                    myParcel.requested = DateTime.Now;
                }

                //updates
                public void paracelToDrone(int myId)
                {
                    int i;
                    bool flag =false;
                    for (i=0; i <= 10; i++)
                        if(IDAL.DO.DalObject.DataSource.drones[i].status == MyEnums.DroneStatus.available)
                        {
                            flag = true;
                            break;
                        }
                    if (flag)//avialable drone
                    {
                        IDAL.DO.DalObject.DataSource.drones[i].status = MyEnums.DroneStatus.delivery;
                        int j = 0;
                        while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                            j++;
                        IDAL.DO.DalObject.DataSource.parcels[j].droneId = IDAL.DO.DalObject.DataSource.drones[i].id;
                        IDAL.DO.DalObject.DataSource.parcels[j].scheduled = DateTime.Now;
                    }
                }
                public void pickUp(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                        j++;
                    IDAL.DO.DalObject.DataSource.parcels[j].pickedUp = DateTime.Now;
                }
                public void delivered(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                        j++;
                    IDAL.DO.DalObject.DataSource.parcels[j].delivered = DateTime.Now;
                }
                public void sendToCharge(int droneId, int stationId)
                {
                    IDAL.DO.DroneCharge droneCharge = new DroneCharge();// new droneCharge

                    int j = 0;// id of drone to be charge
                    while (IDAL.DO.DalObject.DataSource.drones[j].id != droneId)
                        j++;
                    IDAL.DO.DalObject.DataSource.drones[j].status = MyEnums.DroneStatus.maintenance;
                    droneCharge.droneId = IDAL.DO.DalObject.DataSource.drones[j].id;

                    int k = 0;// id of the station we charge in it;
                    while (IDAL.DO.DalObject.DataSource.stations[k].id != stationId)
                        k++;
                    droneCharge.stationId = IDAL.DO.DalObject.DataSource.stations[k].id;
                    IDAL.DO.DalObject.DataSource.stations[k].numOfChargeSlots--;
                }
                // בעיה עם זיהוי של תחנת טעינה מכיון שאין מערך ואין דרך לגשת
                public void endCharge(int droneId, int stationId)
                {
                    int j = 0;// id of drone to be realese
                    while (IDAL.DO.DalObject.DataSource.drones[j].id != droneId)
                        j++;
                    IDAL.DO.DalObject.DataSource.drones[j].status = MyEnums.DroneStatus.available;

                    int k = 0;// id of the station we charge in it;
                    while (IDAL.DO.DalObject.DataSource.stations[k].id != stationId)
                        k++;
                    IDAL.DO.DalObject.DataSource.stations[k].numOfChargeSlots--;
                }

                // displays
                public void stationDisplay(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.stations[j].id != myId)
                        j++;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.stations[j]);
                }
                public void droneDisplay(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.drones[j].id != myId)
                        j++;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.drones[j]);
                }
                public void customerDisplay(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.customers[j].id != myId)
                        j++;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.customers[j]);
                }
                public void parcelDisplay(int myId)
                {
                    int j = 0;
                    while (IDAL.DO.DalObject.DataSource.parcels[j].id != myId)
                        j++;
                    Console.WriteLine(IDAL.DO.DalObject.DataSource.parcels[j]);
                }

                //lists displays
                public void stationsDisplay()
                {
                    for(int i =0; i<= IDAL.DO.DalObject.DataSource.Config.stationIndex; i++)
                        Console.WriteLine(IDAL.DO.DalObject.DataSource.stations[i]);
                }
                public void dronesDisplay()
                {
                    for (int i = 0; i <= IDAL.DO.DalObject.DataSource.Config.droneIndex; i++)
                        Console.WriteLine(IDAL.DO.DalObject.DataSource.drones[i]);
                }
                public void customersDisplay()
                {
                    for (int i = 0; i <= IDAL.DO.DalObject.DataSource.Config.customerIndex; i++)
                        Console.WriteLine(IDAL.DO.DalObject.DataSource.customers[i]);
                }
                public void parcelsDisplay()
                {
                    for (int i = 0; i <= IDAL.DO.DalObject.DataSource.Config.parcelIndex; i++)
                        Console.WriteLine(IDAL.DO.DalObject.DataSource.parcels[i]);
                }
                public void notAssociatedParcelsDisplay()
                {
                    for (int i = 0; i <= IDAL.DO.DalObject.DataSource.Config.parcelIndex; i++)
                       if( IDAL.DO.DalObject.DataSource.parcels[i].droneId!=0)
                            Console.WriteLine(IDAL.DO.DalObject.DataSource.parcels[i]);
                }
                public void availableToChargeStattions()
                {
                    for (int i = 0; i <= IDAL.DO.DalObject.DataSource.Config.parcelIndex; i++)
                        if (IDAL.DO.DalObject.DataSource.stations[i].numOfChargeSlots > 0)
                            Console.WriteLine(IDAL.DO.DalObject.DataSource.stations[i]);
                }
            }
        }
    }
}



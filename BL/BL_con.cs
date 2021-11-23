using System;
using System.Collections.Generic;
using System.Linq;
namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public Random rd = new Random();
            public IDAL.IDal dal;

            public List<DroneToList> dronesList = new List<DroneToList>();

            public static double free;
            public static double lightWeight;
            public static double mediumWeight;
            public static double heavyWeight;
            public static double DroneLoadRate;

            public BL()
            {
                dal = new IDAL.DO.DalObject.DalObject();

                free = dal.droneElectricityConsumption()[0];
                lightWeight = dal.droneElectricityConsumption()[1];
                mediumWeight = dal.droneElectricityConsumption()[2];
                heavyWeight = dal.droneElectricityConsumption()[3];
                DroneLoadRate = dal.droneElectricityConsumption()[4];
                DroneToList temp = new DroneToList();
                //insert all the drones to this list
                var dalDrones = dal.getDrones();
                foreach( var element in dalDrones)
                {
                    temp.id = element.id;
                    temp.model = element.model;
                    temp.weight = element.weight;
                    dronesList.Add(temp);
                }
                if(dronesList != null)
                    {
                    //search if one of our drones is associated
                    for (int i = 0; i < dronesList.Count; i++)
                    {
                        if (thisDroneIsAssociated(dronesList[i].id))
                        {
                            DroneToList Dtemp = dronesList[i];
                            temp.deliveredParcelId = AssociatedParcelId(dronesList[i].id);
                            dronesList[i] = temp;
                        }
                    }


                    foreach (var element in dronesList)
                    {
                        if (thisDroneIsAssociated(element.id) && IsAnyUnassociatedParcel())
                        {
                            //drone status
                            element.status = MyEnums.DroneStatus.delivery;

                            //drone location
                            if (ScheduledButNotPickedUp(element.deliveredParcelId)) element.location = new Location(NearestToSenderStation(element.deliveredParcelId).location);
                            if (PickedUpButNotDeliverd(element.deliveredParcelId)) element.location = new Location(SenderLocation(element.deliveredParcelId));

                            IDAL.DO.Location myLocation = new IDAL.DO.Location(element.location.longitude, element.location.lattitude);

                            //drone Electricity Consumption 
                            double lenghtOfDeliveryVoyage = dal.distance(myLocation, SenderLocation(element.deliveredParcelId));
                            IDAL.DO.Location locationOfNearestStation = (NearestToSenderStation(element.deliveredParcelId).location);
                            double distanceBetweenTargetToStation = dal.distance(myLocation, locationOfNearestStation);

                            int Battery = (int)(BatteryRequirementForVoyage(element.id, lenghtOfDeliveryVoyage + distanceBetweenTargetToStation));
                            element.battery = rd.Next(Battery, 101);

                        }
                        else
                        {
                            element.status = (MyEnums.DroneStatus)rd.Next(0, 2);

                            if (element.status == MyEnums.DroneStatus.maintenance)
                            {
                                var dalStationsList = dal.getStations();
                                int index = rd.Next(0, dalStationsList.Count());
                                element.location = new Location(dalStationsList.ElementAt(index).location);
                                element.battery = rd.Next(0, 21);
                            }
                            else// free
                            {
                                var customers = RecieversList();
                                int index = 0;
                                if (customers.Count > 0)
                                {
                                    index = rd.Next(0, customers.Count());
                                    element.location = new Location(customers.ElementAt(index).location);
                                }
                                else
                                {
                                    element.location = new Location();
                                }
                                IDAL.DO.Location myLocation = new IDAL.DO.Location(element.location.longitude, element.location.lattitude);

                                IDAL.DO.Location locationOfNearestChargeSlot = (NearestChargeSlot(myLocation).location);

                                double distanceBetweenTargetToStation = dal.distance(myLocation, locationOfNearestChargeSlot);
                                int minBattery = (int)(BatteryRequirementForVoyage(element.id, distanceBetweenTargetToStation));

                                element.battery = rd.Next(minBattery, 101);
                            }
                        }
                    }
                }
            }
        }
    }
}
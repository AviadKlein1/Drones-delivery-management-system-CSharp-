using System;
using System.Collections.Generic;
using System.Linq;
namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// object constructor
        /// </summary>
        public partial class BL : IBl
        {
            public IDAL.IDal dal;

            public Random rd = new();
            
            public List<DroneToList> dronesList = new();

            //electricity consumption fields
            public static double free;
            public static double lightWeight;
            public static double mediumWeight;
            public static double heavyWeight;
            public static double DroneLoadRate;

            //initializing
            public BL()
            {
                dal = new IDAL.DO.DalObject.DalObject();

                free = dal.DroneElectricityConsumption()[0];
                lightWeight = dal.DroneElectricityConsumption()[1];
                mediumWeight = dal.DroneElectricityConsumption()[2];
                heavyWeight = dal.DroneElectricityConsumption()[3];
                DroneLoadRate = dal.DroneElectricityConsumption()[4];

                //insert drones to list
                var dalDrones = dal.GetDrones();
                DroneToList temp = new();
                foreach ( var element in dalDrones)
                {
                    DroneToList temp1 = new DroneToList();
                    temp1.id = element.Id;
                    temp1.model = element.Model;
                    temp1.weight = element.weight;
                    dronesList.Add(temp1);
                }
                if(dronesList != null)
                {
                    //search for an associated drone
                    for (int i = 0; i < dronesList.Count; i++)
                    {
                        if (IsAssociatedDrone(dronesList[i].id))
                        {
                            DroneToList Dtemp = dronesList[i];
                            temp.deliveredParcelId = AssociatedParcelId(dronesList[i].id);
                            dronesList[i] = temp;
                        }
                    }

                    //
                    foreach (var element in dronesList)
                    {
                        DroneToList newDrone = new();
                        newDrone.id = element.id;
                        newDrone.model = element.model;
                        newDrone.status = element.status;
                        newDrone.weight = element.weight;
                        newDrone.battery = element.battery;
                        newDrone.deliveredParcelId = element.deliveredParcelId;

                        if (IsAssociatedDrone(element.id) && IsAnyUnassociatedParcel())
                        {
                            //drone status
                            newDrone.status = MyEnums.DroneStatus.delivery;

                            //drone location
                            Location myLocation = new();
                            if (ScheduledButNotPickedUp(element.deliveredParcelId))
                            {
                                myLocation = new Location(NearestToSenderStation(element.deliveredParcelId).Location);
                            }
                            if (PickedUpButNotDeliverd(element.deliveredParcelId))
                            {
                                myLocation = new Location(SenderLocation(element.deliveredParcelId));
                            }

                            newDrone.location = myLocation;
                            IDAL.DO.Location myDalLocation = new IDAL.DO.Location(myLocation.longitude , myLocation.lattitude);

                            //drone electricity consumption 
                            double lenghtOfDeliveryVoyage = dal.GetDistance(myDalLocation, SenderLocation(element.deliveredParcelId));
                            IDAL.DO.Location locationOfNearestStation = (NearestToSenderStation(element.deliveredParcelId).Location);
                            double distanceBetweenTargetToStation = dal.GetDistance(myDalLocation, locationOfNearestStation);

                            int Battery = (int)(BatteryRequirementForVoyage(element.id, lenghtOfDeliveryVoyage + distanceBetweenTargetToStation));
                            element.battery = rd.Next(Battery, 101);
                        }
                        else
                        {
                            element.status = (MyEnums.DroneStatus)rd.Next(0, 2);
                            if (element.status == MyEnums.DroneStatus.maintenance)
                            {
                                var dalStationsList = dal.GetStations();
                                int index = rd.Next(0, dalStationsList.Count());
                                element.location = new Location(dalStationsList.ElementAt(index).Location);
                                element.battery = rd.Next(0, 21);
                            }
                            //no cargo
                            else
                            {
                                var customers = RecieversList();
                                int index;
                                if (customers.Count > 0)
                                {
                                    index = rd.Next(0, customers.Count);
                                    element.location = new Location(customers.ElementAt(index).Location);
                                }
                                else
                                {
                                    element.location = new Location();
                                }
                                IDAL.DO.Location myLocation = new IDAL.DO.Location(element.location.longitude, element.location.lattitude);

                                IDAL.DO.Location locationOfNearestChargeSlot = (NearestChargeSlot(myLocation).Location);

                                double distanceBetweenTargetToStation = dal.GetDistance(myLocation, locationOfNearestChargeSlot);
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
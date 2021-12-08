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
                    temp1.Id = element.Id;
                    temp1.Model = element.Model;
                    temp1.Weight = element.weight;
                    dronesList.Add(temp1);
                }
                if(dronesList != null)
                {
                    //search for an associated drone
                    for (int i = 0; i < dronesList.Count; i++)
                    {
                        if (IsAssociatedDrone(dronesList[i].Id))
                        {
                            DroneToList Dtemp = dronesList[i];
                            Dtemp.DeliveredParcelId = AssociatedParcelId(dronesList[i].Id);
                            dronesList[i] = Dtemp;
                        }
                    }

                    //
                    for (int i = 0; i < dronesList.Count; i++)
                    {
                        DroneToList element = dronesList[i];
                        DroneToList newDrone = new();
                        newDrone.Id = element.Id;
                        newDrone.Model = element.Model;
                        newDrone.Status = element.Status;
                        newDrone.Weight = element.Weight;
                        newDrone.Battery = element.Battery;
                        newDrone.DeliveredParcelId = element.DeliveredParcelId;

                        if (IsAssociatedDrone(element.Id) && IsAnyUnassociatedParcel())
                        {
                            //drone status
                            newDrone.Status = MyEnums.DroneStatus.delivery;

                            //drone location
                            Location myLocation = new();
                            if (ScheduledButNotPickedUp(element.DeliveredParcelId))
                            {
                                myLocation = new Location(NearestToSenderStation(element.DeliveredParcelId).Location);
                            }
                            if (PickedUpButNotDeliverd(element.DeliveredParcelId))
                            {
                                myLocation = new Location(SenderLocation(element.DeliveredParcelId));
                            }

                            newDrone.Location = myLocation;
                            IDAL.DO.Location myDalLocation = new IDAL.DO.Location(myLocation.longitude , myLocation.lattitude);

                            //drone electricity consumption 
                            double lenghtOfDeliveryVoyage = dal.GetDistance(myDalLocation, SenderLocation(element.DeliveredParcelId));
                            IDAL.DO.Location locationOfNearestStation = (NearestToSenderStation(element.DeliveredParcelId).Location);
                            double distanceBetweenTargetToStation = dal.GetDistance(myDalLocation, locationOfNearestStation);

                            int Battery = (int)(BatteryRequirementForVoyage(element.Id, lenghtOfDeliveryVoyage + distanceBetweenTargetToStation));
                            newDrone.Battery = rd.Next(Battery, 101);
                        }
                        else // not in deliver
                        {
                            newDrone.Status = (MyEnums.DroneStatus)rd.Next(0, 2);

                            if (newDrone.Status == MyEnums.DroneStatus.maintenance)
                            {
                                var dalStationsList = dal.GetStationsList(allStations);
                                int index = rd.Next(0, dalStationsList.Count());
                                newDrone.Location = new Location(dalStationsList.ElementAt(index).Location);
                                newDrone.Battery = rd.Next(0, 21);
                            }
                            // Avilable drone
                            else
                            {
                                var customers = RecieversList();
                                int index;
                                if (customers.Count > 0)
                                {
                                    index = rd.Next(0, customers.Count);
                                    newDrone.Location = new Location(customers.ElementAt(index).Location);
                                }
                                else
                                {
                                    var dalStationsList = dal.GetStationsList(allStations);
                                    //newDrone.Location = new Location(31.783333,35.216667);
                                    newDrone.Location = new Location(dalStationsList.ElementAt(0).Location);
                                }

                                IDAL.DO.Location myLocation = new(newDrone.Location.longitude, newDrone.Location.lattitude);
                                IDAL.DO.Location locationOfNearestChargeSlot = (NearestAvailableChargeSlot(myLocation).Location);

                                double distanceBetweenTargetToStation = dal.GetDistance(myLocation, locationOfNearestChargeSlot);
                                int minBattery = (int)(BatteryRequirementForVoyage(newDrone.Id, distanceBetweenTargetToStation));

                                newDrone.Battery = rd.Next(minBattery, 101);
                            }
                        }
                        dronesList[i] = newDrone;
                    }
                }
            }
        }
    }
}
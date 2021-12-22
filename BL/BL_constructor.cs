using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// object constructor
        /// </summary>
        public sealed partial class BL : IBl
        {
            private static readonly IBl instance = new BL();
            public static IBl GetInstance() { return instance; }

            private readonly IDal dal;
            private readonly Random rd = new();
            private readonly List<DroneToList> dronesList = new();

            //electricity consumption fields
            public static double free;
            public static double lightWeight;
            public static double mediumWeight;
            public static double heavyWeight;
            public static double DroneLoadRate;

            //initializing
            private BL()
            {
                dal = DalFactory.GetDal();
                free = dal.DroneElectricityConsumption()[0];
                lightWeight = dal.DroneElectricityConsumption()[1];
                mediumWeight = dal.DroneElectricityConsumption()[2];
                heavyWeight = dal.DroneElectricityConsumption()[3];
                DroneLoadRate = dal.DroneElectricityConsumption()[4];

                //insert drones to list
                var dalDrones = dal.GetDrones();
                DroneToList temp = new();
                foreach (var element in dalDrones)
                {
                    DroneToList temp1 = new();
                    temp1.Id = element.Id;
                    temp1.Model = element.Model;
                    temp1.Weight = element.Weight;
                    dronesList.Add(temp1);
                }
                if (dronesList != null)
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
                                myLocation = new Location(NearestToSenderStation(element.DeliveredParcelId).Location);
                            if (PickedUpButNotDelivered(element.DeliveredParcelId))
                                myLocation = new Location(SenderLocation(element.DeliveredParcelId));

                            newDrone.Location = myLocation;
                            DalApi.DO.Location myDalLocation = new DalApi.DO.Location(myLocation.Longitude , myLocation.Latitude);

                            //drone electricity consumption 
                            double lenghtOfDeliveryVoyage = dal.GetDistance(myDalLocation, SenderLocation(element.DeliveredParcelId));
                            DalApi.DO.Location locationOfNearestStation = NearestToSenderStation(element.DeliveredParcelId).Location;
                            double distanceBetweenTargetToStation = dal.GetDistance(myDalLocation, locationOfNearestStation);

                            int Battery = (int)BatteryRequirementForVoyage(element.Id, lenghtOfDeliveryVoyage + distanceBetweenTargetToStation);
                            if (Battery > 100)
                                Battery = 100;
                            if (Battery < 0)
                                Battery = 0;
                            newDrone.Battery = rd.Next(Battery, 101);
                        }
                        else // not in delivery
                        {
                            newDrone.Status = (MyEnums.DroneStatus)rd.Next(0, 2);

                            if (newDrone.Status == MyEnums.DroneStatus.maintenance)
                            {
                                var dalStationsList = dal.GetStationsList(allStations);

                                int index = rd.Next(0, dalStationsList.Count());
                                newDrone.Location = new Location(dalStationsList.ElementAt(index).Location);
                                dal.DecreaseChargeSlot(dalStationsList.ElementAt(index).Id);
                                newDrone.Battery = rd.Next(0, 21);
                            }
                            //available drone
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

                                DalApi.DO.Location myLocation = new(newDrone.Location.Longitude, newDrone.Location.Latitude);
                                DalApi.DO.Location locationOfNearestChargeSlot = NearestAvailableChargeSlot(myLocation).Location;

                                double distanceBetweenTargetToStation = dal.GetDistance(myLocation, locationOfNearestChargeSlot);
                                int minBattery = (int)BatteryRequirementForVoyage(newDrone.Id, distanceBetweenTargetToStation);

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
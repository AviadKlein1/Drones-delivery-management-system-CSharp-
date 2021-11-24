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
        public partial class BL
        {
            public IDAL.IDal dal;

            public Random rd = new Random();
            
            public List<DroneToList> dronesList = new List<DroneToList>();

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
                DroneToList temp = new DroneToList();
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
                        if (IsAssociatedDrone(element.id) && IsAnyUnassociatedParcel())
                        {
                            //drone status
                            element.status = MyEnums.DroneStatus.delivery;

                            //drone location
                            if (ScheduledButNotPickedUp(element.deliveredParcelId)) element.location = new Location(NearestToSenderStation(element.deliveredParcelId).Location);
                            if (PickedUpButNotDeliverd(element.deliveredParcelId)) element.location = new Location(SenderLocation(element.deliveredParcelId));

                            IDAL.DO.Location myLocation = new IDAL.DO.Location(element.location.longitude, element.location.lattitude);

                            //drone electricity consumption 
                            double lenghtOfDeliveryVoyage = dal.Distance(myLocation, SenderLocation(element.deliveredParcelId));
                            IDAL.DO.Location locationOfNearestStation = (NearestToSenderStation(element.deliveredParcelId).Location);
                            double distanceBetweenTargetToStation = dal.Distance(myLocation, locationOfNearestStation);

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
                                int index = 0;
                                if (customers.Count > 0)
                                {
                                    index = rd.Next(0, customers.Count());
                                    element.location = new Location(customers.ElementAt(index).Location);
                                }
                                else
                                {
                                    element.location = new Location();
                                }
                                IDAL.DO.Location myLocation = new IDAL.DO.Location(element.location.longitude, element.location.lattitude);

                                IDAL.DO.Location locationOfNearestChargeSlot = (NearestChargeSlot(myLocation).Location);

                                double distanceBetweenTargetToStation = dal.Distance(myLocation, locationOfNearestChargeSlot);
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
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public Random rd = new Random();

            public List<DronesToList> dronesList;

            public IDAL.IDal dal = new IDAL.DO.DalObject.DalObject();

            public static double free;
            public static double lightWeight;
            public static double mediumWeight;
            public static double heavyWeight;
            public static double DroneLoadRate;


            //

            public BL()
            {
                free = dal.droneElectricityConsumption()[0];
                lightWeight = dal.droneElectricityConsumption()[1];
                mediumWeight = dal.droneElectricityConsumption()[2];
                heavyWeight = dal.droneElectricityConsumption()[3];
                DroneLoadRate = dal.droneElectricityConsumption()[4];

                //insert all the drones to this list
                var dalDrones = dal.getDrones();
                foreach (var element in dalDrones)
                {
                    DronesToList temp = new DronesToList();
                    temp.id = element.id;
                    temp.model = element.model;
                    temp.weight = element.weight;
                    dronesList.Add(temp);
                }
                //search if one of our drones is associated
                foreach (var element in dronesList)
                {
                    if (thisDroneIsAssociated(element.id))
                        element.deliveredParcelId = thisDronesAssociatedParcelId(element.id);
                }

                foreach (var element in dronesList)
                {
                    if (thisDroneIsAssociated(element.id) && thereAreParcelsThatNotDeliverdYet())
                    {
                        //drone status
                        element.status = MyEnums.DroneStatus.delivery;

                        //drone location
                        if (myParcelScheduledButNotPickedUp(element.deliveredParcelId)) element.location =  new Location (theNearestToSenderStation(element.deliveredParcelId).location);
                        if (myParcelPickedUpButNotDeliverd(element.deliveredParcelId)) element.location = new Location(theLocationOfThisParcelSender(element.deliveredParcelId));

                        IDAL.DO.Location myLocation = new IDAL.DO.Location(element.location.longitude, element.location.lattitude);

                        //drone Electricity Consumption 
                        double lenghtOfDeliveryVoyage = dal.distance(myLocation, theLocationOfThisParcelSender(element.deliveredParcelId));
                        IDAL.DO.Location locationOfNearestStation = (theNearestToSenderStation(element.deliveredParcelId).location);
                        double distanceBetweenTargetToStation = dal.distance(myLocation, locationOfNearestStation);

                        int Battery = (int)(BatteryRequiredForVoyage(element.id, lenghtOfDeliveryVoyage + distanceBetweenTargetToStation));
                        element.battery = rd.Next(Battery, 101);

                    }
                    else
                    {
                        element.status = (MyEnums.DroneStatus)rd.Next(0, 2);

                        if (element.status == MyEnums.DroneStatus.maintenance)
                        {
                            var dalStationsList = dal.getStations();
                            int index = rd.Next(0, dalStationsList.Count() + 1);
                            element.location = new Location(dalStationsList.ElementAt(index).location);
                            element.battery = rd.Next(0, 21);
                        }
                        else// free
                        {
                            var customers = CustomersWhoRecievedParcel();
                            int index = rd.Next(0, customers.Count() + 1);
                            element.location = new Location(customers.ElementAt(index).location);

                            IDAL.DO.Location myLocation = new IDAL.DO.Location(element.location.longitude, element.location.lattitude);

                            IDAL.DO.Location locationOfNearestChargeSlot = (theNearestChargeSlot(myLocation).location);

                            double distanceBetweenTargetToStation = dal.distance(myLocation, locationOfNearestChargeSlot);
                            int minBattery = (int)(BatteryRequiredForVoyage(element.id, distanceBetweenTargetToStation));

                            element.battery = rd.Next(minBattery, 101);
                        }
                    }
                }
            }
        }
    }
}
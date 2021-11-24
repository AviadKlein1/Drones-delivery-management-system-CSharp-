using System;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public bool updatDrone(int droneId, string newModel)
            {
                var flag = false;
                var dalDronesList = dal.getDrones();
                foreach (var item in dalDronesList)
                {
                    if (item.id == droneId)
                    {
                        dal.updateDrone(droneId, newModel);
                        flag = true;
                        break;
                    }
                }
                foreach (var dItem in dronesList)
                {
                    if (dItem.id == droneId)
                    {
                        dItem.model = newModel;
                        break;
                    }
                }
                return flag;
            }
            public bool updatStation(int stationId, string newName, int numOfChargeSlots)
            {
                var flag = false;
                var dalStationsList = dal.getStations();
                foreach (var item in dalStationsList)
                {
                    if (item.id == stationId)
                    {
                        dal.updatStation(stationId, newName, numOfChargeSlots);
                        flag = true;
                        break;
                    }
                }
                return flag;
            }
            public bool updateCustomer(int customerId, string newName, string newPhone)
            {
                var flag = false;
                var dalCustomersList = dal.getCustomers();
                foreach (var item in dalCustomersList)
                {
                    if (item.id == customerId)
                    {
                        dal.updateCustomer(customerId, newName, newPhone);
                        flag = true;
                        break;
                    }
                }
                return flag;
            }
            public bool sendDroneToCharge(int droneId)
            {
                var flag = false;
                var v = dronesList;
                foreach (var item in v)
                {
                    if (item.id == droneId)
                        if (item.status != MyEnums.DroneStatus.available)
                        {
                            System.Console.WriteLine("not available drone\n");
                            break;
                        }
                        else // is available 
                        {
                            IDAL.DO.Location itemLocation = new IDAL.DO.Location(item.location.longitude, item.location.lattitude);
                            var tempStation = theNearestAvailableChargeSlotAndThereIsBattery(itemLocation, droneId);
                            if (tempStation.id == 0)
                            {
                                System.Console.WriteLine("not available station to charge drone\n");
                                break;
                            }
                            // send drone to charge
                            dal.decriseChargeSlot(tempStation.id);
                            foreach (var dItem in dronesList)
                            {
                                if (dItem.id == droneId)
                                {
                                    dItem.status = MyEnums.DroneStatus.maintenance;
                                    dItem.location = new Location(tempStation.location);
                                    //battery
                                    var dis = dal.distance(itemLocation, tempStation.location);
                                    var neededBattery = (int)BatteryRequirementForVoyage(droneId, dis);
                                    dItem.battery = (dItem.battery - neededBattery);
                                    flag = true;
                                }
                            }
                        }
                }
                return flag;
            }
            public bool ReleaseDroneFromCharge(int droneId, int chargeTime)
            {
                var flag = false;
                var v = dronesList;
                foreach (var item in v)
                {
                    if (item.id == droneId)
                        if (item.status != MyEnums.DroneStatus.maintenance)
                        {
                            System.Console.WriteLine("not maintenance drone\n"); // exeption
                            break;
                        }
                        else // is maintenance 
                        {
                            IDAL.DO.Location itemLocation = new IDAL.DO.Location(item.location.longitude, item.location.lattitude);
                            var tempStation = NearestStation(itemLocation);
                            dal.increaseChargeSlot(tempStation.id);

                            foreach (var dItem in dronesList)
                            {
                                if (dItem.id == droneId)
                                {
                                    dItem.status = MyEnums.DroneStatus.available;
                                    //battery
                                    int newBattery = dItem.battery += (int)(DroneLoadRate * chargeTime);
                                    dItem.battery = (newBattery > 100 ? 100 : newBattery);
                                    flag = true;
                                }
                            }
                        }
                }
                return flag;
            }
            public bool SheduleParcelToDrone(int droneId)
            {
                var flag = false;
                var v = dronesList;
                foreach (var item in v)
                {
                    if (item.id == droneId)
                        if (item.status != MyEnums.DroneStatus.available)
                        {
                            System.Console.WriteLine("not available drone\n");
                            break;
                        }
                        else // is available 
                        {
                            int newParcelId = suitableParcel(droneId);
                            if (newParcelId == 0)
                                break;
                            else flag = true;
                            item.status = MyEnums.DroneStatus.delivery;
                            dal.SheduleParcelToDrone(newParcelId, droneId);


                        }
                }
                return flag;
            }
            public bool PickUpParcelByDrone(int droneId)
            {
                var flag = false;
                var droneExistFlag = false;
                var v = dronesList;
                int idOfThisParcel = 0;
                IDAL.DO.Location ourSenderLocation = new IDAL.DO.Location();
                foreach (var item in v)
                {
                    if (item.id == droneId)
                    {
                        idOfThisParcel = item.deliveredParcelId;
                        droneExistFlag = true;
                    }
                }
                if( droneExistFlag == false)// exeption
                {
                    Console.WriteLine("not such drone\n");
                    return false;
                }
                if (ScheduledButNotPickedUp(idOfThisParcel))// exeption
                {

                    var parcelsList = dal.getParcels();
                    foreach (var item in parcelsList)
                    {
                        //find our parcel
                        if (item.id == idOfThisParcel)
                        {
                            // our parcel belong to our drone
                            if (item.droneId == droneId)
                            {
                                ourSenderLocation = SenderLocation(item.id);
                                //update parcel
                                dal.PickUpParcelByDrone(droneId, item.id);
                                flag = true;
                                //update drone
                                DroneToList temp = new DroneToList();
                                for (int i = 0; i < v.Count; i++)
                                {
                                    DroneToList dItem = v[i];
                                    if (dItem.id == droneId)
                                    {
                                        temp.id = dItem.id;
                                        temp.model = dItem.model;
                                        temp.location = new Location(ourSenderLocation);
                                        temp.status = dItem.status;
                                        temp.weight = dItem.weight;
                                        temp.deliveredParcelId = item.id;
                                        IDAL.DO.Location earlyDroneLocation = new IDAL.DO.Location(dItem.location.longitude, dItem.location.lattitude);
                                        // update battery
                                        temp.battery = (temp.battery - (int)BatteryRequirementForVoyage(droneId, dal.distance(earlyDroneLocation, ourSenderLocation)));
                                        v[i] = temp;
                                    }
                                }
                            }
                            else System.Console.WriteLine("not our parcel");
                        }
                    }
                }
                return flag;
            }
            public bool DeliverParcelByDrone(int droneId)
            {
                var flag = false;
                var droneExistFlag = false;
                var v = dronesList;
                int idOfThisParcel = 0;
                IDAL.DO.Location ourReciverLocation = new IDAL.DO.Location();
                foreach (var item in v)
                {
                    if (item.id == droneId)
                    {
                        idOfThisParcel = item.deliveredParcelId;
                        droneExistFlag = true;
                    }
                }
                if(droneExistFlag == false) // exeption
                {
                    Console.WriteLine("not such drone\n");
                    return false;
                }
                if ( ! PickedUpButNotDeliverd(idOfThisParcel))
                {
                    System.Console.WriteLine("this parcel is not in the right status\n");
                }
                var parcelsList = dal.getParcels();
                foreach (var item in parcelsList)
                {
                    //find our parcel
                    if (item.id == idOfThisParcel)
                    {
                        // our parcel belong to our drone
                        if (item.droneId == droneId)
                        {
                            ourReciverLocation = ReciverLocation(item.id);
                            //update parcel
                            dal.DeliverParcelByDrone(droneId, item.id);
                            flag = true;
                            //update drone
                            DroneToList temp = new DroneToList();
                            for (int i = 0; i < v.Count; i++)
                            {
                                DroneToList dItem = v[i];
                                if (dItem.id == droneId)
                                {
                                    temp.id = dItem.id;
                                    temp.model = dItem.model;
                                    temp.location = new Location(ourReciverLocation);
                                    temp.status = MyEnums.DroneStatus.available;
                                    temp.weight = dItem.weight;
                                    temp.deliveredParcelId = item.id;
                                    IDAL.DO.Location earlyDroneLocation = new IDAL.DO.Location(dItem.location.longitude, dItem.location.lattitude);
                                    // update battery
                                    temp.battery = (temp.battery - (int)BatteryRequirementForVoyage(droneId, dal.distance(earlyDroneLocation, ourReciverLocation)));
                                    v[i] = temp;
                                }
                            }
                        }
                        else System.Console.WriteLine("not our parcel");
                    }
                }
                return flag;
            }
        }
    }
}
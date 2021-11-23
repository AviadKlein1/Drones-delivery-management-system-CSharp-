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
                    //else 
                    //    throw new WrongIdException(droneId, $"wrong id: {droneId}");
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
                                    var neededBattery = (int)BatteryRequiredForVoyage(droneId, dis);
                                    dItem.battery = (dItem.battery - neededBattery);
                                    flag = true;
                                }
                            }
                        }
                }
                return flag;
            }
        }
    }
}
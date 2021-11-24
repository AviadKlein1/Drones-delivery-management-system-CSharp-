namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public bool updatDrone(int droneId, string newModel)
            {
                var flag = false;
                var dalDronesList = dal.GetDrones();
                foreach (var item in dalDronesList)
                {
                    if (item.Id == droneId)
                    {
                        dal.UpdateDrone(droneId, newModel);
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
                var dalStationsList = dal.GetStations();
                foreach (var item in dalStationsList)
                {
                    if (item.Id == stationId)
                    {
                        dal.UpdatStation(stationId, newName, numOfChargeSlots);
                        flag = true;
                        break;
                    }
                }
                return flag;
            }
            
            public bool updateCustomer(int customerId, string newName, string newPhone)
            {
                var flag = false;
                var dalCustomersList = dal.GetCustomers();
                foreach (var item in dalCustomersList)
                {
                    if (item.Id == customerId)
                    {
                        dal.UpdateCustomer(customerId, newName, newPhone);
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
                            var tempStation = NearestReachableChargeSlot(itemLocation, droneId);
                            if (tempStation.Id == 0)
                            {
                                System.Console.WriteLine("not available station to charge drone\n");
                                break;
                            }
                            // send drone to charge
                            dal.DecriseChargeSlot(tempStation.Id);
                            foreach (var dItem in dronesList)
                            {
                                if (dItem.id == droneId)
                                {
                                    dItem.status = MyEnums.DroneStatus.maintenance;
                                    dItem.location = new Location(tempStation.Location);
                                    //battery
                                    var dis = dal.Distance(itemLocation, tempStation.Location);
                                    var neededBattery = (int)BatteryRequirementForVoyage(droneId, dis);
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
using System;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// contains item updating functions
        /// </summary>
        public partial class BL
        {
            /// <summary>
            /// updates drone data
            /// </summary>
            /// <param name="droneId"></param>
            /// <param name="newModel"></param>
            /// <returns></returns>
            public bool UpdateDrone(int droneId, string newModel)
            {
                bool flag = false;
                var dalDronesList = dal.GetDrones();
                //search drone
                foreach (var item in dalDronesList)
                {
                    if (item.Id == droneId)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    dal.UpdateDrone(droneId, newModel);
                    foreach (var dItem in dronesList)
                    {
                        if (dItem.Id == droneId)
                        {
                            dItem.Model = newModel;
                            break;
                        }
                    }
                }
                else
                    throw new WrongIdException(droneId, $"wrong id: {droneId}");
                return flag;
            }

            /// <summary>
            /// update station details
            /// </summary>
            /// <param name="stationId"></param>
            /// <param name="newName"></param>
            /// <param name="numOfChargeSlots"></param>
            /// <returns></returns>
            public bool UpdateStation(int stationId, string newName, int numOfChargeSlots)
            {
                var flag = false;
                var dalStationsList = dal.GetStationsList(allStations);
                //search station
                foreach (var item in dalStationsList)
                {
                    if (item.Id == stationId)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    dal.UpdateStation(stationId, newName, numOfChargeSlots);
                else
                    throw new WrongIdException(stationId, $"wrong id: {stationId}");
                return flag;
            }

            /// <summary>
            /// update customer details
            /// </summary>
            /// <param name="customerId"></param>
            /// <param name="newName"></param>
            /// <param name="newPhone"></param>
            /// <returns></returns>
            public bool UpdateCustomer(int customerId, string newName, string newPhone)
            {
                var flag = false;
                var dalCustomersList = dal.GetCustomersList(allCustomers);
                //search customer
                foreach (var item in dalCustomersList)
                {
                    if (item.Id == customerId)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    dal.UpdateCustomer(customerId, newName, newPhone);
                else
                    throw new WrongIdException(customerId, $"wrong id: {customerId}");
                return flag;
            }

            /// <summary>
            /// get drone and ssend it to charge. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            public bool ChargeDrone(int droneId)
            {
                var flag = false;
                var v = dronesList;
                //search drone
                foreach (var item in v)
                {
                    if (item.Id == droneId)
                    {
                        if (item.Status != MyEnums.DroneStatus.available)
                        {
                            Console.WriteLine("not available drone\n");
                            break;
                        }
                        else // is available 
                        {
                            DalApi.DO.Location itemLocation = new(item.Location.Longitude, item.Location.Latitude);
                            var tempStation = NearestReachableChargeSlot(itemLocation, droneId);
                            if (tempStation.Id == 0)
                            {
                                Console.WriteLine("not available station to charge drone\n");
                                break;
                            }
                            // send drone to charge
                            dal.DecriseChargeSlot(tempStation.Id);
                            foreach (var dItem in dronesList)
                            {
                                if (dItem.Id == droneId)
                                {
                                    dItem.Status = MyEnums.DroneStatus.maintenance;
                                    dItem.Location = new Location(tempStation.Location);
                                    //battery
                                    var dis = dal.GetDistance(itemLocation, tempStation.Location);
                                    var neededBattery = (int)BatteryRequirementForVoyage(droneId, dis);
                                    dItem.Battery -= neededBattery;
                                    flag = true;
                                }
                            }
                        }
                    }
                }
                return flag;
            }

            /// <summary>
            /// end drone charge. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <param name="chargeTime"></param>
            /// <returns></returns>
            public bool ReleaseDroneFromCharge(int droneId, int chargeTime)
            {
                var flag = false;
                var v = dronesList;
                //search drone
                foreach (var item in v)
                {
                    if (item.Id == droneId)
                    {
                        if (item.Status != MyEnums.DroneStatus.maintenance)
                        {
                            throw new WrongIdException
                                (droneId, $"wrong id, drone is not charging currently: {droneId}");
                        }
                        else // is maintenance 
                        {
                            DalApi.DO.Location itemLocation = new(item.Location.Longitude, item.Location.Latitude);
                            var tempStation = NearestStation(itemLocation);
                            dal.IncreaseChargeSlot(tempStation.Id);

                            foreach (var dItem in dronesList)
                            {
                                if (dItem.Id == droneId)
                                {
                                    dItem.Status = MyEnums.DroneStatus.available;
                                    //battery
                                    int newBattery = dItem.Battery += (int)(DroneLoadRate * chargeTime);
                                    dItem.Battery = newBattery > 100 ? 100 : newBattery;
                                    flag = true;
                                }
                            }
                        }
                    }
                }
                return flag;
            }
            /// <summary>
            /// associate drone with parcel. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            public bool ScheduleParcelToDrone(int droneId)
            {
                var flag = false;
           
                    var v = dronesList;
                //search drone
                foreach (var item in v)
                {
                    if (item.Id == droneId)
                    {
                        if (item.Status == MyEnums.DroneStatus.delivery)
                        {
                            throw new OccupiedDroneException(droneId, $"drone is occupied, try another: {droneId}");
                        }
                        else // is available 
                        {
                            int newParcelId = SuitableParcel(droneId);
                            if (newParcelId == 0)
                                break;
                            else flag = true;
                            item.Status = MyEnums.DroneStatus.delivery;
                            item.DeliveredParcelId = newParcelId;
                            dal.SheduleParcelToDrone(newParcelId, droneId);
                        }
                    }

                }
               
                return flag;
            }

            /// <summary>
            /// pick up parcel from sender. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            public bool PickUpParcelByDrone(int droneId)
            {
                var flag = false;
                var droneExistFlag = false;
                var v = dronesList;
                int idOfThisParcel = 0;
                DalApi.DO.Location ourSenderLocation = new();
                //search drone
                foreach (var item in v)
                {
                    if (item.Id == droneId)
                    {
                        idOfThisParcel = item.DeliveredParcelId;
                        droneExistFlag = true;
                    }
                }
                if (droneExistFlag == false)// exeption
                {
                    throw new WrongIdException(droneId, $"wrong id: {droneId}");
                }
                if (ScheduledButNotPickedUp(idOfThisParcel))// exeption
                {
                    var parcelsList = dal.GetParcelsList(allParcels);
                    foreach (var item in parcelsList)
                    {
                        //find our parcel
                        if (item.Id == idOfThisParcel)
                        {
                            // our parcel belong to our drone
                            if (item.DroneId == droneId)
                            {
                                ourSenderLocation = SenderLocation(item.Id);
                                //update parcel
                                dal.PickUpParcelByDrone(droneId, item.Id);
                                flag = true;
                                //update drone
                                DroneToList temp = new();
                                for (int i = 0; i < v.Count; i++)
                                {
                                    DroneToList dItem = v[i];
                                    if (dItem.Id == droneId)
                                    {
                                        temp.Id = dItem.Id;
                                        temp.Model = dItem.Model;
                                        temp.Location = new Location(ourSenderLocation);
                                        temp.Status = dItem.Status;
                                        temp.Weight = dItem.Weight;
                                        temp.DeliveredParcelId = item.Id;
                                        DalApi.DO.Location earlyDroneLocation =
                                            new(dItem.Location.Longitude, dItem.Location.Latitude);
                                        // update battery
                                        temp.Battery -= (int)BatteryRequirementForVoyage
                                            (droneId, dal.GetDistance(earlyDroneLocation, ourSenderLocation));
                                        if (temp.Battery < 0) temp.Battery = 0;
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

            /// <summary>
            /// deliver parcel to reciever. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            public bool DeliverParcelByDrone(int droneId)
            {
                var flag = false;
                var droneExistFlag = false;
                var v = dronesList;
                int idOfThisParcel = 0;
                DalApi.DO.Location ourReciverLocation = new();
                //search drone
                foreach (var item in v)
                {
                    if (item.Id == droneId)
                    {
                        idOfThisParcel = item.DeliveredParcelId;
                        droneExistFlag = true;
                    }
                }
                if (droneExistFlag == false) // exeption
                {
                    Console.WriteLine("not such drone\n");
                    return false;
                }
                if (!PickedUpButNotDelivered(idOfThisParcel))
                {
                    Console.WriteLine("this parcel is not in the right status\n");
                }
                var parcelsList = dal.GetParcelsList(allParcels);
                foreach (var item in parcelsList)
                {
                    //find our parcel
                    if (item.Id == idOfThisParcel)
                    {
                        // our parcel belong to our drone
                        if (item.DroneId == droneId)
                        {
                            ourReciverLocation = ReciverLocation(item.Id);
                            //update parcel
                            dal.DeliverParcelByDrone(droneId, item.Id);
                            flag = true;
                            //update drone
                            DroneToList temp = new();
                            for (int i = 0; i < v.Count; i++)
                            {
                                DroneToList dItem = v[i];
                                if (dItem.Id == droneId)
                                {
                                    temp.Id = dItem.Id;
                                    temp.Model = dItem.Model;
                                    temp.Location = new Location(ourReciverLocation);
                                    temp.Status = MyEnums.DroneStatus.available;
                                    temp.Weight = dItem.Weight;
                                    temp.DeliveredParcelId = item.Id;
                                    DalApi.DO.Location earlyDroneLocation = new(dItem.Location.Longitude, dItem.Location.Latitude);
                                    // update battery
                                    temp.Battery -= (int)BatteryRequirementForVoyage
                                        (droneId, dal.GetDistance(earlyDroneLocation, ourReciverLocation));
                                    if (temp.Battery < 0) temp.Battery = 0;
                                    v[i] = temp;
                                }
                            }
                        }
                        else Console.WriteLine("not our parcel");
                    }
                }
                var g = ChargeDrone(droneId);
                return flag;
            }
        }
    }
}
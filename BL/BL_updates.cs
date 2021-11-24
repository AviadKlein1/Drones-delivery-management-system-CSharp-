using System;

namespace IBL
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
            public void UpdateDrone(int droneId, string newModel)
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
                if (flag == true)
                {
                    dal.Update_Drone(droneId, newModel);
                    foreach (var dItem in dronesList)
                    {
                        if (dItem.id == droneId)
                        {
                            dItem.model = newModel;
                            break;
                        }
                    }
                }

                else
                    throw new WrongIdException(droneId, $"wrong id: {droneId}");
            }

            /// <summary>
            /// update station details
            /// </summary>
            /// <param name="stationId"></param>
            /// <param name="newName"></param>
            /// <param name="numOfChargeSlots"></param>
            /// <returns></returns>
            public void UpdateStation(int stationId, string newName, int numOfChargeSlots)
            {
                var flag = false;
                var dalStationsList = dal.GetStations();
                //search station
                foreach (var item in dalStationsList)
                {
                    if (item.Id == stationId)
                    {
                        flag = true;
                        break;
                    }
                }
                if(flag == true)
                    dal.Update_Station(stationId, newName, numOfChargeSlots);
                else
                    throw new WrongIdException(stationId, $"wrong id: {stationId}");
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
                var dalCustomersList = dal.GetCustomers();
                //search customer
                foreach (var item in dalCustomersList)
                {
                    if (item.Id == customerId)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == true)
                    dal.Update_Customer(customerId, newName, newPhone);
                else
                    throw new WrongIdException(customerId, $"wrong id: {customerId}");
                return flag;
            }

            /// <summary>
            /// 
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
                    if (item.id == droneId)
                    {
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
                }
                return flag;
            }

            /// <summary>
            /// end drone charge
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
                    if (item.id == droneId)
                    {
                        if (item.status != MyEnums.DroneStatus.maintenance)
                        {
                            throw new WrongIdException(droneId, $"wrong id, drone is not charging currently: {droneId}");
                        }
                        else // is maintenance 
                        {
                            IDAL.DO.Location itemLocation = new IDAL.DO.Location(item.location.longitude, item.location.lattitude);
                            var tempStation = NearestStation(itemLocation);
                            dal.increaseChargeSlot(tempStation.Id);

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
                }
                return flag;
            }
            /// <summary>
            /// associate drone with parcel
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
                    if (item.id == droneId)
                    {
                        if (item.status != MyEnums.DroneStatus.available)
                        {
                            throw new OccupiedDroneException(droneId, $"drone is occupied, try another: {droneId}");
                        }
                        else // is available 
                        {
                            int newParcelId = SuitableParcel(droneId);
                            if (newParcelId == 0)
                                break;
                            else flag = true;
                            item.status = MyEnums.DroneStatus.delivery;
                            dal.SheduleParcelToDrone(newParcelId, droneId);


                        }
                    }
                }
                return flag;
            }
            
            /// <summary>
            /// pick up parcel from sender
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            public void PickUpParcelByDrone(int droneId)
            {
                var flag = false;
                var droneExistFlag = false;
                var v = dronesList;
                int idOfThisParcel = 0;
                IDAL.DO.Location ourSenderLocation = new IDAL.DO.Location();
                //search drone
                foreach (var item in v)
                {
                    if (item.id == droneId)
                    {
                        idOfThisParcel = item.deliveredParcelId;
                        droneExistFlag = true;
                    }
                }
                if(droneExistFlag == false)// exeption
                {
                    throw new WrongIdException(droneId, $"wrong id: {droneId}");

                }
                if (ScheduledButNotPickedUp(idOfThisParcel))// exeption
                {
                    var parcelsList = dal.GetParcels();
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
                                        temp.deliveredParcelId = item.Id;
                                        IDAL.DO.Location earlyDroneLocation = new IDAL.DO.Location(dItem.location.longitude, dItem.location.lattitude);
                                        // update battery
                                        temp.battery = (temp.battery - (int)BatteryRequirementForVoyage(droneId, dal.Distance(earlyDroneLocation, ourSenderLocation)));
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
            /// deliver parcel to reciever
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            public bool DeliverParcelByDrone(int droneId)
            {
                var flag = false;
                var droneExistFlag = false;
                var v = dronesList;
                int idOfThisParcel = 0;
                IDAL.DO.Location ourReciverLocation = new IDAL.DO.Location();
                //search drone
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
                var parcelsList = dal.GetParcels();
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
                                    temp.deliveredParcelId = item.Id;
                                    IDAL.DO.Location earlyDroneLocation = new IDAL.DO.Location(dItem.location.longitude, dItem.location.lattitude);
                                    // update battery
                                    temp.battery = (temp.battery - (int)BatteryRequirementForVoyage(droneId, dal.Distance(earlyDroneLocation, ourReciverLocation)));
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
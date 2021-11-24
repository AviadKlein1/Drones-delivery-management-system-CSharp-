using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        //contains diefferent auxiliary functions 
        public partial class BL
        {
            /// <summary>
            /// checks if there is any unassociated parcel left
            /// </summary>
            /// returns boollean expression
            public bool IsAnyUnassociatedParcel()
            {
                var dalParcelsList = dal.getParcels();
                foreach(var element in dalParcelsList)
                {
                    DateTime emptyDateTime = new DateTime();
                    if (element.delivered == emptyDateTime)
                        return true;
                }
                return false;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            public bool thisDroneIsAssociated(int droneId)
            {
                var dalParcelsList = dal.getParcels();
                foreach(var element in dalParcelsList)
                {
                    if(element.droneId == droneId)
                       return true;
                }
                return false;
            }

            /// <summary>
            /// returns the id of the parcel associated to drone
            /// </summary>
            /// <param name="droneId"></param>
            /// returns parcel id
            public int AssociatedParcelId(int droneId)
            {
                var dalParcelsList = dal.getParcels();
                foreach(var element in dalParcelsList)
                {
                    if (element.droneId == droneId) 
                        return element.id;
                }
                return 0;
            }

            /// <summary>
            /// checks for minimum charge of battery required to make a certain distance
            /// </summary>
            /// <param name="myDroneId"></param>
            /// <param name="distance"></param>
            /// returns a number between 1 and 100
            public double BatteryRequirementForVoyage(int myDroneId, double distance)
            {
                var dalDronesList = dal.getDrones();
                foreach(var element in dalDronesList)
                {
                    //search drone
                    if(element.id == myDroneId)
                    {
                        var dalParcelsList = dal.getParcels();
                        //search parcel
                        foreach(var Pelement in dalParcelsList)
                        {
                            if (Pelement.droneId == element.id)
                            {
                                //calculate requirement
                                if(Pelement.weight == IDAL.DO.MyEnums.WeightCategory.light)
                                    return lightWeight * distance;
                                if(Pelement.weight == IDAL.DO.MyEnums.WeightCategory.medium)
                                    return mediumWeight * distance;
                                if(Pelement.weight == IDAL.DO.MyEnums.WeightCategory.heavy)
                                        return heavyWeight * distance;
                            }
                        }
                    }
                }
                return -1;
            }

            /// <summary>
            /// checks if parcel hasen't picked up yet
            /// </summary>
            /// <param name="parcelId"></param>
            ///  returns boolean type
            public bool ScheduledButNotPickedUp(int parcelId)
            {
                var dalParcelsList = dal.getParcels();
                //search parcel
                foreach(var element in dalParcelsList)
                {
                    if(element.id == parcelId)
                    {
                        DateTime emptyDateTime = new DateTime();
                        if(element.pickedUp == emptyDateTime && element.scheduled != emptyDateTime) 
                           return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// checks if parcel hasen't delivered yet
            /// </summary>
            /// <param name="parcelId"></param>
            /// returns boolean type
            public bool PickedUpButNotDeliverd(int parcelId)
            {
                var dalParcelsList = dal.getParcels();
                //search parcel
                foreach(var element in dalParcelsList)
                {
                    if(element.id == parcelId)
                    {
                        DateTime emptyDateTime = new DateTime();
                        if(element.delivered == emptyDateTime && element.pickedUp != emptyDateTime)
                           return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// search for parcel's sender location
            /// </summary>
            /// <param name="parcelId"></param>
            /// return location type (longitude, lattitude)
            public IDAL.DO.Location SenderLocation(int parcelId)
            {
                IDAL.DO.Location tempLocation = new IDAL.DO.Location();
                var dalParcelsList = dal.getParcels();
                //search parcel
                foreach(var pElement in dalParcelsList)
                {
                    if(pElement.id == parcelId)
                    {
                        var customersList = dal.getCustomers();
                        //search parcel's sender
                        foreach(var cElement in customersList)
                        {
                            if(cElement.id == pElement.senderId)
                                tempLocation = cElement.location;
                        }
                    }
                }
                return tempLocation;
            }

            public IDAL.DO.Location ReciverLocation(int parcelId)
            {
                IDAL.DO.Location tempLocation = new IDAL.DO.Location();
                var dalParcelsList = dal.getParcels();
                //search parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.id == parcelId)
                    {
                        var customersList = dal.getCustomers();
                        //search parcel's reciver
                        foreach (var cElement in customersList)
                        {
                            if (cElement.id == pElement.reciverId)
                                tempLocation = cElement.location;
                        }
                    }
                }
                return tempLocation;

            }

            /// <summary>
            /// finds the nearest to sender station (by parcel id)
            /// </summary>
            /// <param name="parcelId"></param>
            /// returns station
            public IDAL.DO.Station NearestToSenderStation(int parcelId)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var dalParcelsList = dal.getParcels();
                //search parcel
                foreach(var pElement in dalParcelsList)
                {
                    if(pElement.id == parcelId)
                    {
                        var customersList = dal.getCustomers();
                        //search sender of parcel
                        foreach(var cElement in customersList)
                        {
                            if(cElement.id == pElement.senderId)
                                tempStation = NearestStation(cElement.location);
                        }
                    }
                }
                return tempStation;
            }

            /// <summary>
            /// finds the nearest to sender available charge slot (by parcel id)
            /// </summary>
            /// <param name="parcelId"></param>
            /// returns station
            public IDAL.DO.Station NearestToSenderChargeSlot(int parcelId)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var dalParcelsList = dal.getParcels();
                //search parcel
                foreach(var pElement in dalParcelsList)
                {
                    if(pElement.id == parcelId)
                    {
                        var customersList = dal.getCustomers();
                        //search sender of parcel
                        foreach(var cElement in customersList)
                        {
                            if(cElement.id == pElement.senderId)
                                tempStation = NearestChargeSlot(cElement.location);
                        }
                    }
                }
                return tempStation;
            }

            /// <summary>
            /// finds the nearest station
            /// </summary>
            /// <param name="locate"></param>
            /// returns station
            public IDAL.DO.Station NearestStation(IDAL.DO.Location locate)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var stationList = dal.getStations();
                double min = 99999999999;
                foreach(var element in stationList)
                {
                    var dis = dal.distance(locate, element.location);
                    //if distance is smaller, update min distance
                    if(dis < min)
                    {
                        min = dis;
                        tempStation = element;
                    }
                }
                return tempStation;
            }

            /// <summary>
            /// finds the nearest available charge slot
            /// </summary>
            /// <param name="l"></param>
            /// returns station
            public IDAL.DO.Station NearestChargeSlot(IDAL.DO.Location l)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var stationList = dal.getStations();
                double min = 99999999999;
                foreach(var element in stationList)
                {
                    var dis = dal.distance(l, element.location);
                    //if distance is smaller, update min distance
                    if(dis < min && element.numOfAvailableChargeSlots > 0)
                    {
                        min = dis;
                        tempStation = element;
                    }
                }
                return tempStation;
            }

            /// <summary>
            /// returns a list of all customers who recieved a parcel
            /// </summary>
            /// returns list of customers
            public List<IDAL.DO.Customer> RecieversList()
            {
                List<IDAL.DO.Customer> temp = new List<IDAL.DO.Customer>();
                DateTime emptyDateTime = new DateTime();
                var dalParcelsList = dal.getParcels();
                //search parcel
                foreach(var pElement in dalParcelsList)
                {
                    if(pElement.delivered != emptyDateTime)
                    {
                        var customersList = dal.getCustomers();
                        //search sender of parcel
                        foreach (var cElement in customersList)
                        {
                            if(cElement.id == pElement.reciverId)
                                temp.Add(cElement);
                        }
                    }
                }
                return temp;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="parcelId"></param>
            /// <param name="customerId"></param>
            /// <returns></returns>
            public CustomerInParcel TheOtherSide(int parcelId, int customerId)
            {
                var parcelsList = dal.getParcels();
                CustomerInParcel other = new CustomerInParcel();
                int otherId = 0;
                foreach(var item in parcelsList)
                {
                    //search parcel
                    if(item.id == parcelId)
                    {
                        //checks if current customer is sender or reciever
                        if (item.senderId != customerId) 
                            otherId = item.reciverId;
                        if (item.reciverId != customerId)
                            otherId = item.senderId;
                    }
                }
                var customersList = dal.getCustomers();
                foreach(var item in customersList)
                {
                    if(otherId == item.id)
                    {
                        other.id = item.id;
                        other.name = item.name;
                    }
                }
                return other;
            }
            public int batteryAtDrone(int myDroneId)
            {
                int b = 0;
                var v = dronesList;
                foreach (var item in v)
                {
                    if (item.id == myDroneId) b = item.battery;
                }
                return b;
            }
            public int numOfDronesThatChargeingInThatStation(int stationId)
            {
                var dalStationsList = dal.getStations();
                var myStationLocation = new Location();
                int sum = 0;
                foreach (var item in dalStationsList)
                {
                    if (item.id == stationId) myStationLocation = new Location(item.location);
                }
                var dalDronesList = dronesList;
                foreach (var item in dalDronesList)
                {
                    if (item.status == MyEnums.DroneStatus.maintenance && item.location == myStationLocation)
                        sum++;
                }
                return sum;
            }
            public CustomerInParcel theOtherSide(int parcelId, int customerId)
            {
                var parcelsList = dal.getParcels();
                CustomerInParcel other = new CustomerInParcel();
                int otherId = 0;
                foreach (var item in parcelsList)
                {
                    //found our parcel
                    if (item.id == parcelId)
                    {
                        // our customer is the sender, so the other side would be the reciver
                        if (item.senderId != customerId) otherId = item.reciverId;
                        if (item.reciverId != customerId) otherId = item.senderId;
                    }
                }
                var customersList = dal.getCustomers();
                foreach (var item in customersList)
                {
                    if (otherId == item.id)
                    {
                        other.id = item.id;
                        other.name = item.name;
                    }
                }
                return other;
            }
            public IDAL.DO.Station theNearestAvailableChargeSlotAndThereIsBattery(IDAL.DO.Location l, int myDroneId)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var stationList = dal.getStations();
                double min = 99999999999;
                var neededBattery = (int)BatteryRequirementForVoyage(myDroneId, min);
                var existBattery = batteryAtDrone(myDroneId);
                foreach (var element in stationList)
                {
                    var dis = dal.distance(l, element.location);
                    if (dis < min && element.numOfAvailableChargeSlots > 0 && (neededBattery <= existBattery))
                    {
                        min = dis;
                        tempStation = element;
                    }
                }
                return tempStation;
            }
            public IDAL.DO.Station theNearestAvailableChargeSlot(IDAL.DO.Location l)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var stationList = dal.getStations();
                double min = 99999999999;
                foreach (var element in stationList)
                {
                    var dis = dal.distance(l, element.location);
                    if (dis < min && element.numOfAvailableChargeSlots > 0)
                    {
                        min = dis;
                        tempStation = element;
                    }
                }
                return tempStation;
            }
            public int suitableParcel(int droneId)
            {
                var v = dronesList;
                IDAL.DO.MyEnums.WeightCategory myDroneWeight = new IDAL.DO.MyEnums.WeightCategory();
                IDAL.DO.Location myDroneLocation = new IDAL.DO.Location();
                IDAL.DO.Parcel tempParcel = new IDAL.DO.Parcel();
                int myDroneBattery = 0;

                foreach (var item in v) // my drone data
                {
                    if(item.id == droneId)
                    {
                        myDroneWeight = item.weight;
                        myDroneLocation = new IDAL.DO.Location(item.location.longitude, item.location.lattitude);
                        myDroneBattery = item.battery;
                    }
                }
                // start searching
                List<IDAL.DO.Parcel> notSuitableParcels = new List<IDAL.DO.Parcel>();
                var dalParcelsList = dal.getParcels();

                foreach (var item in dalParcelsList) // remove not suitable weight
                {
                    if(item.weight > myDroneWeight)
                    {
                        notSuitableParcels.Add(item);
                    }
                }
                foreach (var item in dalParcelsList)
                {
                    // in the high priority, and not at our list
                    if(item.priority == HighPriority(dalParcelsList, notSuitableParcels) &&
                        ISNotAt_NotSuitableParcels(notSuitableParcels, item))
                    {
                        // the nearest parcel
                        tempParcel = theNearestParcel(dalParcelsList, myDroneLocation, notSuitableParcels);
                        //if we have enough battery
                        var senderLocation = SenderLocation(tempParcel.id);
                        var reciverLocation = ReciverLocation(tempParcel.id);
                        var chargeStation = theNearestAvailableChargeSlot(reciverLocation);

                        var dis1 = dal.distance(myDroneLocation, senderLocation);
                        var dis2 = dal.distance(senderLocation, reciverLocation);
                        var dis3 = dal.distance(reciverLocation, chargeStation.location);
                        var fullDistance = dis1 + dis2 + dis3;

                        double Consumption = 0;
                        if (tempParcel.weight == IDAL.DO.MyEnums.WeightCategory.light) Consumption = lightWeight;
                        if (tempParcel.weight == IDAL.DO.MyEnums.WeightCategory.medium) Consumption = mediumWeight;
                        if (tempParcel.weight == IDAL.DO.MyEnums.WeightCategory.heavy) Consumption = heavyWeight;
                        //
                        if (myDroneBattery < (fullDistance * Consumption))
                            notSuitableParcels.Add(item);
                        else return tempParcel.id;
                    }
                }
                Console.WriteLine(" not suitable parcel\n");
                return 0;
            }
            public bool ISNotAt_NotSuitableParcels(List<IDAL.DO.Parcel> list, IDAL.DO.Parcel myParcel)
            {
                bool flag = true;
                foreach (var item in list)
                {
                    if (item.id == myParcel.id)
                        flag = false;
                }
                return flag;
            }
            public IDAL.DO.MyEnums.PriorityLevel HighPriority(IEnumerable<IDAL.DO.Parcel> dalParcelsList, List<IDAL.DO.Parcel> notSuatableList)
            {
                IDAL.DO.MyEnums.PriorityLevel max = IDAL.DO.MyEnums.PriorityLevel.regular;
                foreach (var item in dalParcelsList)
                {
                    if(item.priority > max && ISNotAt_NotSuitableParcels(notSuatableList, item))
                    {
                        max = item.priority;
                    }
                }
                return max;
            }
            public IDAL.DO.Parcel theNearestParcel(IEnumerable<IDAL.DO.Parcel> dalParcelsList,IDAL.DO.Location myDroneLocation, List<IDAL.DO.Parcel> notSuatableList)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();
                var min = 9999999999;
                foreach (var item in dalParcelsList)
                {
                    var dis =dal.distance(SenderLocation(item.id), myDroneLocation);
                    if (dis < min && ISNotAt_NotSuitableParcels(notSuatableList, item))
                        min = dis;
                }
                foreach (var item in dalParcelsList)
                {
                    if (min == dal.distance(SenderLocation(item.id), myDroneLocation))
                        return item;
                }
                return temp;
            }
        }
    }
}
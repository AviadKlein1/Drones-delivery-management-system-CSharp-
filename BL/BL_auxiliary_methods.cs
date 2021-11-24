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
                var dalParcelsList = dal.GetParcels();
                foreach(var element in dalParcelsList)
                {
                    DateTime emptyDateTime = new DateTime();
                    if (element.Delivered == emptyDateTime)
                        return true;
                }
                return false;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            public bool IsAssociatedDrone(int droneId)
            {
                var dalParcelsList = dal.GetParcels();
                foreach(var element in dalParcelsList)
                {
                    if(element.DroneId == droneId)
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
                var dalParcelsList = dal.GetParcels();
                foreach(var element in dalParcelsList)
                {
                    if (element.DroneId == droneId) 
                        return element.Id;
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
                var dalDronesList = dal.GetDrones();
                foreach(var element in dalDronesList)
                {
                    //search drone
                    if(element.Id == myDroneId)
                    {
                        var dalParcelsList = dal.GetParcels();
                        //search parcel
                        foreach(var Pelement in dalParcelsList)
                        {
                            if (Pelement.DroneId == element.Id)
                            {
                                //calculate requirement
                                if(Pelement.Weight == IDAL.DO.MyEnums.WeightCategory.light)
                                    return lightWeight * distance;
                                if(Pelement.Weight == IDAL.DO.MyEnums.WeightCategory.medium)
                                    return mediumWeight * distance;
                                if(Pelement.Weight == IDAL.DO.MyEnums.WeightCategory.heavy)
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
                var dalParcelsList = dal.GetParcels();
                //search parcel
                foreach(var element in dalParcelsList)
                {
                    if(element.Id == parcelId)
                    {
                        DateTime emptyDateTime = new DateTime();
                        if(element.PickedUp == emptyDateTime && element.Scheduled != emptyDateTime) 
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
                var dalParcelsList = dal.GetParcels();
                //search parcel
                foreach(var element in dalParcelsList)
                {
                    if(element.Id == parcelId)
                    {
                        DateTime emptyDateTime = new DateTime();
                        if(element.Delivered == emptyDateTime && element.PickedUp != emptyDateTime)
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
                var dalParcelsList = dal.GetParcels();
                //search parcel
                foreach(var pElement in dalParcelsList)
                {
                    if(pElement.Id == parcelId)
                    {
                        var customersList = dal.GetCustomers();
                        //search parcel's sender
                        foreach(var cElement in customersList)
                        {
                            if(cElement.Id == pElement.SenderId)
                                tempLocation = cElement.Location;
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
                var dalParcelsList = dal.GetParcels();
                //search parcel
                foreach(var pElement in dalParcelsList)
                {
                    if(pElement.Id == parcelId)
                    {
                        var customersList = dal.GetCustomers();
                        //search sender of parcel
                        foreach(var cElement in customersList)
                        {
                            if(cElement.Id == pElement.SenderId)
                                tempStation = NearestStation(cElement.Location);
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
                var dalParcelsList = dal.GetParcels();
                //search parcel
                foreach(var pElement in dalParcelsList)
                {
                    if(pElement.Id == parcelId)
                    {
                        var customersList = dal.GetCustomers();
                        //search sender of parcel
                        foreach(var cElement in customersList)
                        {
                            if(cElement.Id == pElement.SenderId)
                                tempStation = NearestChargeSlot(cElement.Location);
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
                var stationList = dal.GetStations();
                double min = 99999999999;
                foreach(var element in stationList)
                {
                    var dis = dal.Distance(locate, element.Location);
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
                var stationList = dal.GetStations();
                double min = 99999999999;
                foreach(var element in stationList)
                {
                    var dis = dal.Distance(l, element.Location);
                    //if distance is smaller, update min distance
                    if(dis < min && element.NumOfAvailableChargeSlots > 0)
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
                var dalParcelsList = dal.GetParcels();
                //search parcel
                foreach(var pElement in dalParcelsList)
                {
                    if(pElement.Delivered != emptyDateTime)
                    {
                        var customersList = dal.GetCustomers();
                        //search sender of parcel
                        foreach (var cElement in customersList)
                        {
                            if(cElement.Id == pElement.ReciverId)
                                temp.Add(cElement);
                        }
                    }
                }
                return temp;
            }

            /// <summary>
            /// return charging level of drone by its id 
            /// </summary>
            /// <param name="myDroneId"></param>
            /// <returns></returns>
            public int ChargingLevel(int myDroneId)
            {
                int b = 0;
                var v = dronesList;
                foreach (var item in v)
                {
                    if (item.id == myDroneId)
                        b = item.battery;
                }
                return b;
            }

            /// <summary>
            /// return number of frones charging currently at station  
            /// </summary>
            /// <param name="stationId"></param>
            /// <returns></returns>
            public int NumofOccupiedChargeSlots(int stationId)
            {
                var dalStationsList = dal.GetStations();
                var myStationLocation = new Location();
                int num = 0;
                foreach (var item in dalStationsList)
                {
                    if(item.Id == stationId) 
                        myStationLocation = new Location(item.Location);
                }
                //search for drones charging currently at station
                var dalDronesList = dronesList;
                foreach(var item in dalDronesList)
                {
                    if(item.status == MyEnums.DroneStatus.maintenance && item.location == myStationLocation)
                        num++;
                }
                return num;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="parcelId"></param>
            /// <param name="customerId"></param>
            /// <returns></returns>
            public CustomerInParcel TheOtherSide(int parcelId, int customerId)
            {
                var parcelsList = dal.GetParcels();
                CustomerInParcel other = new CustomerInParcel();
                int otherId = 0;
                foreach (var item in parcelsList)
                {
                    //found our parcel
                    if (item.Id == parcelId)
                    {
                        // our customer is the sender, so the other side would be the reciver
                        if (item.SenderId != customerId) otherId = item.ReciverId;
                        if (item.ReciverId != customerId) otherId = item.SenderId;
                    }
                }
                var customersList = dal.GetCustomers();
                foreach (var item in customersList)
                {
                    if (otherId == item.Id)
                    {
                        other.id = item.Id;
                        other.name = item.Name;
                    }
                }
                return other;
            }

            /// <summary>
            /// returns the nearest available charge slot which is reachable for drone,
            /// considering his charging level
            /// </summary>
            /// <param name="l"></param>
            /// <param name="myDroneId"></param>
            /// <returns></returns>
            public IDAL.DO.Station NearestReachableChargeSlot(IDAL.DO.Location l, int myDroneId)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var stationList = dal.GetStations();
                double min = 99999999999;
                var requiredChargingLevel = (int)BatteryRequirementForVoyage(myDroneId, min);
                var currentChargingLevel = ChargingLevel(myDroneId);
                //search for nearer stations
                foreach (var element in stationList)
                {
                    var dis = dal.Distance(l, element.Location);
                    if (dis < min && element.NumOfAvailableChargeSlots > 0 && (requiredChargingLevel <= currentChargingLevel))
                    {
                        min = dis;
                        tempStation = element;
                    }
                }
                return tempStation;
            }
            //public IDAL.DO.Station NearestChargeSlot(IDAL.DO.Location l)
            //{
            //    IDAL.DO.Station tempStation = new IDAL.DO.Station();
            //    var stationList = dal.getStations();
            //    double min = 99999999999;
            //    foreach (var element in stationList)
            //    {
            //        var dis = dal.distance(l, element.location);
            //        if (dis < min && element.numOfAvailableChargeSlots > 0)
            //        {
            //            min = dis;
            //            tempStation = element;
            //        }
            //    }
            //    return tempStation;
            //}
        }
    }
}
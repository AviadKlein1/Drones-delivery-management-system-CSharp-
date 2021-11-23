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
                        foreac(var cElement in customersList)
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
        }
    }
}
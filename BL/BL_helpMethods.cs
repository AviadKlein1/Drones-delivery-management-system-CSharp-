using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public bool thereAreParcelsThatNotDeliverdYet()
            {
                var dalParcelsList = dal.getParcels();
                foreach (var element in dalParcelsList)
                {
                    DateTime emptyDateTime = new DateTime();
                    if (element.delivered == emptyDateTime) return true;
                }
                return false;
            }
            public bool thisDroneIsAssociated(int droneId)
            {
                var dalParcelsList = dal.getParcels();
                foreach (var element in dalParcelsList)
                {
                    if (element.droneId == droneId) return true;
                }
                return false;
            }
            public int thisDronesAssociatedParcelId(int droneId)
            {
                var dalParcelsList = dal.getParcels();
                foreach (var element in dalParcelsList)
                {
                    if (element.droneId == droneId) return element.id;
                }
                return 0;
            }
            public double BatteryRequiredForVoyage(int myDroneId, double distance)
            {
                var dalDronesList = dal.getDrones();
                foreach (var element in dalDronesList)
                {
                    //find our drone
                    if (element.id == myDroneId)
                    {
                        var dalParcelsList = dal.getParcels();
                        //find our parcel
                        foreach (var Pelement in dalParcelsList)
                        {
                            if (Pelement.droneId == element.id)
                            {
                                if (Pelement.weight == IDAL.DO.MyEnums.WeightCategory.lite)
                                    return lightWeight * distance;
                                if (Pelement.weight == IDAL.DO.MyEnums.WeightCategory.medium)
                                    return mediumWeight * distance;
                                if (Pelement.weight == IDAL.DO.MyEnums.WeightCategory.heavy)
                                    return heavyWeight * distance;
                            }
                        }
                    }
                }
                return 0;
            }

            public bool myParcelScheduledButNotPickedUp(int parcelId)
            {
                var dalParcelsList = dal.getParcels();
                //find our parcel
                foreach (var element in dalParcelsList)
                {
                    if (element.id == parcelId)
                    {
                        DateTime emptyDateTime = new DateTime();
                        if (element.pickedUp == emptyDateTime && element.scheduled != emptyDateTime) return true;
                    }
                }
                return false;
            }

            public bool myParcelPickedUpButNotDeliverd(int parcelId)
            {
                var dalParcelsList = dal.getParcels();
                foreach (var element in dalParcelsList)
                {
                    if (element.id == parcelId)
                    {
                        DateTime emptyDateTime = new DateTime();
                        if (element.delivered == emptyDateTime && element.pickedUp != emptyDateTime) return true;
                    }
                }
                return false;
            }
            public IDAL.DO.Location theLocationOfThisParcelSender(int parcelId)
            {
                IDAL.DO.Location tempLocation = new IDAL.DO.Location();
                var dalParcelsList = dal.getParcels();
                //find our parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.id == parcelId)
                    {
                        var customersList = dal.getCustomers();
                        //find the sender of the parcel
                        foreach (var cElement in customersList)
                        {
                            if (cElement.id == pElement.senderId)
                                tempLocation = cElement.location;
                        }
                    }
                }
                return tempLocation;

            }
            public IDAL.DO.Station theNearestToSenderStation(int parcelId)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var dalParcelsList = dal.getParcels();
                //find our parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.id == parcelId)
                    {
                        var customersList = dal.getCustomers();
                        //find the sender of the parcel
                        foreach (var cElement in customersList)
                        {
                            if (cElement.id == pElement.senderId)
                            {
                                tempStation = (theNearestStation(cElement.location));
                            }
                        }
                    }
                }
                return tempStation;
            }
            public IDAL.DO.Station theNearestToSenderChargeSlotStation(int parcelId)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var dalParcelsList = dal.getParcels();
                //find our parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.id == parcelId)
                    {
                        var customersList = dal.getCustomers();
                        //find the sender of the parcel
                        foreach (var cElement in customersList)
                        {
                            if (cElement.id == pElement.senderId)
                            {
                                tempStation = theNearestChargeSlot(cElement.location);
                            }
                        }
                    }
                }
                return tempStation;
            }
            public IDAL.DO.Station theNearestStation(IDAL.DO.Location l)
            {
                IDAL.DO.Station tempStation = new IDAL.DO.Station();
                var stationList = dal.getStations();
                double min = 99999999999;
                foreach (var element in stationList)
                {
                    var dis = dal.distance(l, element.location);
                    if (dis < min)
                    {
                        min = dis;
                        tempStation = element;
                    }
                }
                return tempStation;
            }
            public IDAL.DO.Station theNearestChargeSlot(IDAL.DO.Location l)
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

            public List<IDAL.DO.Customer> CustomersWhoRecievedParcel()
            {
                List<IDAL.DO.Customer> temp = new List<IDAL.DO.Customer>();
                DateTime emptyDateTime = new DateTime();
                var dalParcelsList = dal.getParcels();
                //find our parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.delivered != emptyDateTime)
                    {
                        var customersList = dal.getCustomers();
                        //find the sender of the parcel
                        foreach (var cElement in customersList)
                        {
                            if (cElement.id == pElement.reciverId)
                                temp.Add(cElement);
                        }
                    }
                }
                return temp;
            }
            public CustomerInParcel theOtherSide(int parcelId, int customerId)
            {
                var parcelsList = dal.getParcels();
                CustomerInParcel other = new CustomerInParcel();
                int otherId = 0;
                foreach (var item in parcelsList)
                {
                    //found our parcel
                    if(item.id == parcelId)
                    {
                        // our customer is the sender, so the other side would be the reciver
                        if (item.senderId != customerId) otherId = item.reciverId;
                        if (item.reciverId != customerId) otherId = item.senderId;
                    }
                }
                var customersList = dal.getCustomers();
                foreach (var item in customersList)
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

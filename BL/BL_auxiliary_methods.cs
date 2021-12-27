using System;
using System.Collections.Generic;

namespace BlApi
{
    namespace BO
    {
        //contains diefferent auxiliary functions 
        public partial class BL : IBl
        {
            /// <summary>
            /// checks if there is any unassociated parcel left
            /// </summary>
            /// returns boollean expression
            internal bool IsAnyUnassociatedParcel()
            {
                var v = dal.GetParcelsList(unassociatedParcels);
                if (v == null) return false;
                else return true;
            }
            /// <summary>
            /// check is the drone is associated
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            internal bool IsAssociatedDrone(int droneId)
            {
                var dalParcelsList = dal.GetParcelsList(allParcels);
                foreach (var element in dalParcelsList)
                {
                    if (element.DroneId == droneId)
                        return true;
                }
                return false;
            }

            /// <summary>
            /// returns the id of the parcel associated to drone
            /// </summary>
            /// <param name="droneId"></param>
            /// returns parcel id
            internal int AssociatedParcelId(int droneId)
            {
                var dalParcelsList = dal.GetParcelsList(allParcels);
                foreach (var element in dalParcelsList)
                {
                    if (element.DroneId == droneId)
                        return element.Id;
                }
                return 0;
            }
            internal Parcel GetParcelBySendeId(int MySenderId)
            {
                Parcel myParcel = new();
                var dalParcelsList = dal.GetParcelsList(allParcels);
                foreach (var element in dalParcelsList)
                {
                    if (element.SenderId == MySenderId)
                        myParcel = new Parcel(element);
                }
                return myParcel;
            }
            internal Parcel GetParcelByReciverId(int MyReciverId)
            {
                Parcel myParcel = new();
                var dalParcelsList = dal.GetParcelsList(allParcels);
                foreach (var element in dalParcelsList)
                {
                    if (element.ReceiverId == MyReciverId)
                        myParcel = new Parcel(element);
                }
                return myParcel;
            }

            /// <summary>
            /// checks for minimum charge of battery required to make a certain distance
            /// </summary>
            /// <param name="myDroneId"></param>
            /// <param name="distance"></param>
            /// returns a number between 1 and 100
            internal double BatteryRequirementForVoyage(int myDroneId, double distance)
            {

                var dalDronesList = dal.GetDrones();
                foreach (var element in dalDronesList)
                {
                    //search drone
                    if (element.Id == myDroneId)
                    {
                        var dalParcelsList = dal.GetParcelsList(allParcels);
                        //search parcel
                        foreach (var myParcel in dalParcelsList)
                        {
                            if (myParcel.DroneId == element.Id)
                            {
                                //calculate requirement
                                if (myParcel.Weight == DalApi.DO.MyEnums.WeightCategory.light)
                                    return lightWeight * distance / 100;
                                if (myParcel.Weight == DalApi.DO.MyEnums.WeightCategory.medium)
                                    return mediumWeight * distance / 100;
                                if (myParcel.Weight == DalApi.DO.MyEnums.WeightCategory.heavy)
                                    return heavyWeight * distance / 100;
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
                var dalParcelsList = dal.GetParcelsList(scheduledButNotPickedUp);
                //search parcel
                foreach (var element in dalParcelsList)
                {
                    if (element.Id == parcelId)
                        return true;
                }
                return false;
            }

            /// <summary>
            /// checks if parcel hasen't delivered yet
            /// </summary>
            /// <param name="parcelId"></param>
            /// returns boolean type
            public bool PickedUpButNotDelivered(int parcelId)
            {
                var dalParcelsList = dal.GetParcelsList(pickedUpButNotDeliverd);
                //search parcel
                foreach (var element in dalParcelsList)
                {
                    if (element.Id == parcelId)
                        return true;
                }
                return false;
            }

            /// <summary>
            /// search for parcel's sender location (giving parcel's id)
            /// </summary>
            /// <param name="parcelId"></param>
            /// return location type (longitude, latitude)
            internal DalApi.DO.Location SenderLocation(int parcelId)
            {
                DalApi.DO.Location tempLocation = new();
                var dalParcelsList = dal.GetParcelsList(allParcels);
                //search parcel
                foreach (var parcel in dalParcelsList)
                {
                    if (parcel.Id == parcelId)
                    {
                        var customersList = dal.GetCustomersList(allCustomers);
                        //search parcel's sender
                        foreach (var customer in customersList)
                        {
                            if (customer.Id == parcel.SenderId)
                                tempLocation = customer.Location;
                        }
                    }
                    break;
                }
                return tempLocation;
            }

            internal DalApi.DO.Location ReciverLocation(int parcelId)
            {
                DalApi.DO.Location tempLocation = new();
                var dalParcelsList = dal.GetParcelsList(allParcels);
                //search parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.Id == parcelId)
                    {
                        var customersList = dal.GetCustomersList(allCustomers);
                        //search parcel's reciver
                        foreach (var cElement in customersList)
                        {
                            if (cElement.Id == pElement.ReceiverId)
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
            internal DalApi.DO.Station NearestToSenderStation(int parcelId)
            {
                DalApi.DO.Station tempStation = new();
                var dalParcelsList = dal.GetParcelsList(allParcels);
                //search parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.Id == parcelId)
                    {
                        var customersList = dal.GetCustomersList(allCustomers);
                        //search sender of parcel
                        foreach (var cElement in customersList)
                        {
                            if (cElement.Id == pElement.SenderId)
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
            internal DalApi.DO.Station NearestToSenderChargeSlot(int parcelId)
            {
                DalApi.DO.Station tempStation = new();
                var dalParcelsList = dal.GetParcelsList(allParcels);
                //search parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.Id == parcelId)
                    {
                        var customersList = dal.GetCustomersList(allCustomers);
                        //search sender of parcel
                        foreach (var cElement in customersList)
                        {
                            if (cElement.Id == pElement.SenderId)
                                tempStation = NearestAvailableChargeSlot(cElement.Location);
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
            internal DalApi.DO.Station NearestStation(DalApi.DO.Location locate)
            {
                DalApi.DO.Station tempStation = new();
                var stationList = dal.GetStationsList(allStations);
                double min = 99999999999.0;
                foreach (var element in stationList)
                {
                    var dis = dal.GetDistance(locate, element.Location);
                    //if distance is smaller, update min distance
                    if (dis < min)
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
            internal DalApi.DO.Station NearestAvailableChargeSlot(DalApi.DO.Location l)
            {
                DalApi.DO.Station tempStation = new();
                var stationList = dal.GetStationsList(allStations);
                double min = 99999999999.0;
                foreach (var element in stationList)
                {
                    var dis = dal.GetDistance(l, element.Location);
                    //if distance is smaller, update min distance
                    if (dis < min && element.NumOfAvailableChargeSlots > 0)
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
            internal List<DalApi.DO.Customer> RecieversList()
            {
                List<DalApi.DO.Customer> temp = new();
                DateTime? emptyDateTime = null;
                var dalParcelsList = dal.GetParcelsList(allParcels);
                //search parcel
                foreach (var pElement in dalParcelsList)
                {
                    if (pElement.Delivered != emptyDateTime)
                    {
                        var customersList = dal.GetCustomersList(allCustomers);
                        //search sender of parcel
                        foreach (var cElement in customersList)
                        {
                            if (cElement.Id == pElement.ReceiverId)
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
            internal int ChargingLevel(int myDroneId)
            {
                int b = 0;
                var v = dronesList;
                foreach (var item in v)
                {
                    if (item.Id == myDroneId)
                        b = item.Battery;
                }
                return b;
            }
            /// <summary>
            /// giving a parcel and acustomer,
            /// return the other side of the dekivery (sender/reciever)
            /// </summary>
            /// <param name="parcelId"></param>
            /// <param name="customerId"></param>
            /// <returns></returns>
            internal CustomerInParcel TheOtherSide(int parcelId, int customerId)
            {
                var parcelsList = dal.GetParcelsList(allParcels);
                CustomerInParcel other = new();
                int otherId = 0;
                foreach (var item in parcelsList)
                {
                    //found our parcel
                    if (item.Id == parcelId)
                    {
                        // our customer is the sender, so the other side would be the reciver
                        if (item.SenderId != customerId) otherId = item.ReceiverId;
                        if (item.ReceiverId != customerId) otherId = item.SenderId;
                    }
                }
                var customersList = dal.GetCustomersList(allCustomers);
                foreach (var item in customersList)
                {
                    if (otherId == item.Id)
                    {
                        other.Id = item.Id;
                        other.Name = item.Name;
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
            internal DalApi.DO.Station NearestReachableChargeSlot(DalApi.DO.Location l, int myDroneId)
            {
                DalApi.DO.Station tempStation = new();
                var stationList = dal.GetStationsList(allStations);
                double min = 99999999999;
                var requiredChargingLevel = (int)BatteryRequirementForVoyage(myDroneId, min);
                var currentChargingLevel = ChargingLevel(myDroneId);
                //search for nearer stations
                foreach (var element in stationList)
                {
                    var dis = dal.GetDistance(l, element.Location);
                    if (dis < min && element.NumOfAvailableChargeSlots > 0 && (requiredChargingLevel <= currentChargingLevel))
                    {
                        min = dis;
                        tempStation = element;
                    }
                }
                return tempStation;
            }

            /// <summary>
            /// find next parcel to deliver
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            internal int MostSuitableParcel(int droneId)
            {
                //retrieve drone's details
                List<DroneToList> myDrones = dronesList;
                DalApi.DO.MyEnums.WeightCategory myDroneWeight = new();
                DalApi.DO.Location myDroneLocation = new();
                int myDroneBattery = 0;
                foreach (DroneToList item in myDrones)
                {
                    if (item.Id == droneId)
                    {
                        myDroneWeight = item.Weight;
                        myDroneLocation = new DalApi.DO.Location(item.Location.Longitude, item.Location.Latitude);
                        myDroneBattery = item.Battery;
                       
                    }
                }

                //declare necessary objects and parameters
                List<DalApi.DO.Parcel> SuitableParcels = new();
                var dalParcelsList = dal.GetParcelsList(allParcels);
                var priorityArray = new int[1000];
                double max = 0;
                var sortByDIstanceParcels = new List<DalApi.DO.Parcel>();
                var tempP = new DalApi.DO.Parcel();

                //filter too heavy or too far or already associated parcels
                foreach (DalApi.DO.Parcel item in dalParcelsList)
                {
                    if (item.Weight <= myDroneWeight &&
                        item.DroneId == 0 &&
                        IsPossibleVoyage(item, myDroneLocation, myDroneBattery))
                    {
                        SuitableParcels.Add(item);
                    }
                }
                var distanceSortToErase = SuitableParcels;

                //score parcels by distance, weight and urgency
                //the highest scored parcel will be associated to drone

                //sort parcels list by distance - the min dis is in last index
                for (int j = SuitableParcels.Count; j > 0; j--)
                {
                    for (int i = 0; i < distanceSortToErase.Count; i++)//search the max
                    {
                        DalApi.DO.Parcel item = distanceSortToErase[i];
                        var dis = dal.GetDistance(myDroneLocation, SenderLocation(item.Id));
                        if (dis > max)
                        {
                            max = dis;
                            tempP = item;
                        }
                    }
                    sortByDIstanceParcels.Add(tempP);
                    //for (int i = 0; i < distanceSortToErase.Count; i++)// erase the max distance parcel from list
                    //{
                    //    DalApi.DO.Parcel item = distanceSortToErase[i];
                    //    if (max == dal.GetDistance(SenderLocation(item.Id), myDroneLocation))
                    //    {
                    //        distanceSortToErase.Remove(item);
                    //    }
                    //}
                }

                //1. score parcels by priority
                for (int i = 0; i < sortByDIstanceParcels.Count; i++)
                {
                    DalApi.DO.Parcel item = sortByDIstanceParcels[i];
                    if (item.Priority == DalApi.DO.MyEnums.PriorityLevel.urgent)
                        priorityArray[i] += 100000;
                    if (item.Priority == DalApi.DO.MyEnums.PriorityLevel.quickly)
                        priorityArray[i] += 50000;
                    if (item.Priority == DalApi.DO.MyEnums.PriorityLevel.regular)
                        priorityArray[i] += 20000;
                }

                //2. score parcels by weight
                for (int i = 0; i < sortByDIstanceParcels.Count; i++)
                {
                    DalApi.DO.Parcel item = sortByDIstanceParcels[i];
                    if ((myDroneWeight - item.Weight) == 0)
                        priorityArray[i] += 10000;
                    if ((myDroneWeight - item.Weight) == 1)
                        priorityArray[i] += 5000;
                    if ((myDroneWeight - item.Weight) == 2)
                        priorityArray[i] += 2000;
                }

                //3. score parcels by distance
                for (int i = 0; i < sortByDIstanceParcels.Count; i++)
                {
                    priorityArray[i] += (i + 1);
                }

                //if no suitable parcels
                if (sortByDIstanceParcels.Count == 0)
                    throw new Exception($"no suitable parcel { droneId }");

                //find the highest scored parcel
                max = 0;
                int index = -1;
                for (int i = 0; i < sortByDIstanceParcels.Count; i++)
                    if (priorityArray[i] > max)
                    {
                        max = priorityArray[i];
                        index = i;
                    }
                var nextParcel = sortByDIstanceParcels[index];
                //return highest scored parcel
                return nextParcel.Id;
            }

            /// <summary>
            /// giving a parcel, determines wether it is suitable for next delivery 
            /// </summary>
            /// <param name="list"></param>
            /// <param name="myParcel"></param>
            /// <returns></returns> a boolean type
            internal static bool IsSuitable(List<DalApi.DO.Parcel> list, DalApi.DO.Parcel myParcel)
            {
                bool isSuitable = true;
                //search parcel in list of unsuitable parcels
                foreach (var item in list)
                {
                    if (item.Id == myParcel.Id || item.DroneId != 0)
                        isSuitable = false;
                }
                return isSuitable;
            }

            /// <summary>
            /// returns the current highest priority on parcels list 
            /// </summary>
            /// <param name="dalParcelsList"></param>
            /// <param name="notSuatableList"></param>
            /// <returns></returns>
            internal bool IsPossibleVoyage(DalApi.DO.Parcel tempParcel,
                DalApi.DO.Location myDroneLocation, int myDroneBattery)
            {
                var senderLocation = SenderLocation(tempParcel.Id);
                var reciverLocation = ReciverLocation(tempParcel.Id);
                var chargeStation = NearestAvailableChargeSlot(reciverLocation);

                double dis1 = dal.GetDistance(myDroneLocation, senderLocation);
                double dis2 = dal.GetDistance(senderLocation, reciverLocation);
                double dis3 = dal.GetDistance(reciverLocation, chargeStation.Location);
                double fullDistance = (dis1 + dis2 + dis3) / 1000;

                double Consumption = 0;
                if (tempParcel.Weight == DalApi.DO.MyEnums.WeightCategory.light)
                    Consumption = lightWeight;
                if (tempParcel.Weight == DalApi.DO.MyEnums.WeightCategory.medium)
                    Consumption = mediumWeight;
                if (tempParcel.Weight == DalApi.DO.MyEnums.WeightCategory.heavy)
                    Consumption = heavyWeight;

                return myDroneBattery > (fullDistance * Consumption / 100);
            }

            /// <summary>
            /// returns the nearest parcel to current location
            /// </summary>
            /// <param name="dalParcelsList"></param>
            /// <param name="myDroneLocation"></param>
            /// <param name="notSuiatableList"></param>
            /// <returns></returns>
            internal DalApi.DO.Parcel TheNearestParcel
                (IEnumerable<DalApi.DO.Parcel> dalParcelsList, DalApi.DO.Location myDroneLocation, List<DalApi.DO.Parcel> notSuiatableList)
            {
                DalApi.DO.Parcel temp = new();
                var min = 9999999999.0;
                //search for neartest parcel
                foreach (var item in dalParcelsList)
                {
                    var dis = dal.GetDistance(SenderLocation(item.Id), myDroneLocation);
                    if (dis < min && IsSuitable(notSuiatableList, item))
                        min = dis;
                }
                //find chosen parcel
                foreach (var item in dalParcelsList)
                {
                    if (min == dal.GetDistance(SenderLocation(item.Id), myDroneLocation))
                        return item;
                }
                //return parcel
                return temp;
            }
        }
    }
}
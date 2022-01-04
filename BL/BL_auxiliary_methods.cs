using System;
using System.Collections.Generic;
using System.Linq;

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
                var v = GetParcelsList(unassociatedParcels);
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
                var dalParcelsList = GetParcelsList(item => item.IsActive == true);
                foreach (var element in dalParcelsList)
                {
                    if(element.ParcelStatus == DalApi.DO.MyEnums.ParcelStatus.scheduled)return true;
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
                var dalParcelsList = dal.GetParcelsList();
                foreach (var element in dalParcelsList)
                {
                    return element.Id;
                }
                return 0;
            }
            internal Parcel GetParcelBySendeId(int MySenderId)
            {
                Parcel myParcel = new();
                var dalParcelsList = dal.GetParcelsList();
                foreach (var element in dalParcelsList)
                {
                    myParcel = new Parcel(element);
                }

                return myParcel;
            }
            internal Parcel GetParcelByReciverId(int MyReciverId)
            {
                Parcel myParcel = new();
                var dalParcelsList = dal.GetParcelsList();
                foreach (var element in dalParcelsList)
                {
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
                foreach (var myParcel in from element in dronesList//search drone
                                         where element.Id == myDroneId
                                         let dalParcelsList = dal.GetParcelsList()//search parcel
                                         from myParcel in dalParcelsList
                                         where myParcel.DroneId == element.Id
                                         select myParcel)
                {
                    //calculate requirement
                    if (myParcel.Weight == DalApi.DO.MyEnums.WeightCategory.light)
                        return lightWeight * distance / 100;
                    if (myParcel.Weight == DalApi.DO.MyEnums.WeightCategory.medium)
                        return mediumWeight * distance / 100;
                    if (myParcel.Weight == DalApi.DO.MyEnums.WeightCategory.heavy)
                        return heavyWeight * distance / 100;
                }

                return free * distance / 100;
            }

            /// <summary>
            /// checks if parcel hasen't picked up yet
            /// </summary>
            /// <param name="parcelId"></param>
            ///  returns boolean type
            public bool ScheduledButNotPickedUp(int parcelId)
            {
                var dalParcelsList = GetParcelsList(scheduledButNotPickedUp);
                //search parcel
                foreach (var item in dalParcelsList)
                {
                    if (item.Id == parcelId) return true;
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
                var dalParcelsList = GetParcelsList(pickedUpButNotDeliverd);
                //search parcel
                foreach (var _ in
                from element in dalParcelsList
                where element.Id == parcelId
                select new { })
                {
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
                var dalParcelsList = dal.GetParcelsList();
                foreach (var customer in
                //search parcel
                from parcel in dalParcelsList
                where parcel.Id == parcelId
                let customersList = dal.GetCustomersList()//search parcel's sender
                from customer in customersList
                where customer.Id == parcel.SenderId
                select customer)
                {
                    tempLocation = customer.Location;
                }

                return tempLocation;
            }

            /// <summary>
            /// giving a parcel id, returns the location of the receiver
            /// </summary>
            /// <param name="parcelId"></param>
            /// <returns></returns>
            internal DalApi.DO.Location ReceiverLocation(int parcelId)
            {
                DalApi.DO.Location tempLocation = new();
                var dalParcelsList = dal.GetParcelsList();
                foreach (var cElement in
                //search parcel
                from pElement in dalParcelsList
                where pElement.Id == parcelId
                let customersList = dal.GetCustomersList()//search parcel's receiver
                from cElement in customersList
                where cElement.Id == pElement.ReceiverId
                select cElement)
                {
                    tempLocation = cElement.Location;
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
                var dalParcelsList = dal.GetParcelsList();
                var customersList = dal.GetCustomersList();
                DalApi.DO.Parcel ourParcel = new();
                DalApi.DO.Customer ourSender = new();
                foreach (var item in
                //search parcel
                from item in dalParcelsList
                where item.Id == parcelId
                select item)
                {
                    ourParcel = item;
                }

                foreach (var item in from item in customersList//search sender of parcel
                                     where ourParcel.SenderId == item.Id
                                     select item)
                {
                    ourSender = item;
                }

                tempStation = NearestStation(ourSender.Location);
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
                var dalParcelsList = dal.GetParcelsList();
                foreach (var cElement in
                //search parcel
                from pElement in dalParcelsList
                where pElement.Id == parcelId
                let customersList = dal.GetCustomersList()//search sender of parcel
                from cElement in customersList
                where cElement.Id == pElement.SenderId
                select cElement)
                {
                    tempStation = NearestAvailableChargeSlot(cElement.Location);
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
                var stationList = dal.GetStationsList();
                double min = 99999999999.0;
                foreach (var (element, dis) in from element in stationList
                                               let dis = dal.GetDistance(locate, element.Location)//if distance is smaller, update min distance
                                               where dis < min
                                               select (element, dis))
                {
                    min = dis;
                    tempStation = element;
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
                var stationList = dal.GetStationsList();
                double min = 99999999999.0;
                foreach (var (element, dis) in from element in stationList
                                               let dis = dal.GetDistance(l, element.Location)//if distance is smaller, update min distance
                                               where dis < min && element.NumOfAvailableChargeSlots > 0
                                               select (element, dis))
                {
                    min = dis;
                    tempStation = element;
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
                DateTime? emptyDateTime = DateTime.MinValue;
                var dalParcelsList = dal.GetParcelsList();
                temp.AddRange(
                //search parcel
                from pElement in dalParcelsList
                where pElement.Delivered != emptyDateTime
                let customersList = dal.GetCustomersList()//search sender of parcel
                from cElement in customersList
                where cElement.Id == pElement.ReceiverId
                select cElement);
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
                foreach (var item in from item in v
                                     where item.Id == myDroneId
                                     select item)
                {
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
                var parcelsList = dal.GetParcelsList();
                CustomerInParcel other = new();
                int otherId = 0;
                foreach (var item in from item in parcelsList//found our parcel
                                     where item.Id == parcelId
                                     select item)
                {
                    // our customer is the sender, so the other side would be the reciver
                    if (item.SenderId != customerId) otherId = item.ReceiverId;
                    if (item.ReceiverId != customerId) otherId = item.SenderId;
                }

                var customersList = dal.GetCustomersList();
                foreach (var item in from item in customersList
                                     where otherId == item.Id
                                     select item)
                {
                    other.Id = item.Id;
                    other.Name = item.Name;
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
                var stationList = dal.GetStationsList();
                double minDistance = 99999;
                var requiredChargingLevel = (int)BatteryRequirementForVoyage(myDroneId, 99999999);
                var currentChargingLevel = ChargingLevel(myDroneId);
                foreach (var (element, dis) in
                //search for nearer stations
                from element in stationList
                let dis = dal.GetDistance(l, element.Location)
                where dis < minDistance && element.NumOfAvailableChargeSlots > 0
                select (element, dis))
                {
                    requiredChargingLevel = (int)BatteryRequirementForVoyage(myDroneId, dis);
                    if (requiredChargingLevel <= currentChargingLevel)
                    {
                        minDistance = dis;
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
                foreach (var item in from DroneToList item in myDrones
                                     where item.Id == droneId
                                     select item)
                {
                    myDroneWeight = item.Weight;
                    myDroneLocation = new DalApi.DO.Location(item.Location.Longitude, item.Location.Latitude);
                    myDroneBattery = item.Battery;
                }

                //declare necessary objects and parameters
                List<DalApi.DO.Parcel> SuitableParcels = new();
                var dalParcelsList = dal.GetParcelsList();
                var priorityArray = new int[1000];
                double max = 0;
                var sortByDIstanceParcels = new List<DalApi.DO.Parcel>();
                var tempP = new DalApi.DO.Parcel();
                SuitableParcels.AddRange(
                //filter too heavy or too far or already associated parcels
                from DalApi.DO.Parcel item in dalParcelsList
                where item.Weight <= myDroneWeight &&
                item.DroneId == 0 &&
                IsPossibleVoyage(item, myDroneLocation, myDroneBattery)
                select item);
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
                foreach (var _ in
                //search parcel in list of unsuitable parcels
                from item in list
                where item.Id == myParcel.Id || item.DroneId != 0
                select new { })
                {
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
                var reciverLocation = ReceiverLocation(tempParcel.Id);
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
                foreach (var dis in
                //search for neartest parcel
                from item in dalParcelsList
                let dis = dal.GetDistance(SenderLocation(item.Id), myDroneLocation)
                where dis < min && IsSuitable(notSuiatableList, item)
                select dis)
                {
                    min = dis;
                }

                foreach (var item in
                //find chosen parcel
                from item in dalParcelsList
                where min == dal.GetDistance(SenderLocation(item.Id), myDroneLocation)
                select item)
                {
                    return item;
                }
                //return parcel
                return temp;
            }
        }
    }
}
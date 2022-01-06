using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
public delegate bool Predicate<in T>(T obj);

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// display list of items
        /// </summary>
        public partial class BL : IBl
        {
            /// <summary>
            /// display filtered by request stations list
            /// </summary>
            /// <returns></returns> return a filtered list of stations, according to conditions
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<StationToList> GetStationsList(System.Predicate<DalApi.DO.Station> match)
            {
                lock (dal)
                {
                    List<StationToList> newList = new();
                    var c = (List<DalApi.DO.Station>)dal.GetStationsList();
                    var v = c.FindAll(match);
                    foreach (var item in v)
                    {
                        StationToList newStation = new();
                        newStation.Id = item.Id;
                        newStation.Name = item.Name;
                        newStation.NumOfAvailableChargeSlots = item.NumOfAvailableChargeSlots;

                        newStation.NumOfOccupiedChargeSlots = (item.NumOfChargeSlots - item.NumOfAvailableChargeSlots);

                        newList.Add(newStation);
                    }
                    if (newList == null)
                        throw new System.Exception("empty list\n");
                    return newList;
                }
            }

            /// <summary>
            /// display Charge drones list
            /// </summary>
            /// <returns></returns> return list of drone Charges
            [MethodImpl(MethodImplOptions.Synchronized)]
            public List<DroneToList> GetDronesList(System.Predicate<DroneToList> match)
            {
                var v = dronesList.FindAll(match);
                if (v == null)
                    System.Console.WriteLine("empty list\n");
                return v;
            }
            public IEnumerable<DroneInCharge> GetDroneChargesList(int stationId)
            {

                lock (dal)
                {
                    var v = dal.GetDroneCharges();

                    if (v == null)
                        throw new System.Exception("empty list");
                    List<DroneInCharge> temp = new();
                    int tempBattery;
                    foreach (var (item, Ditem) in from item in v
                                                  from Ditem in dronesList
                                                  where item.DroneId == Ditem.Id && item.StationId == stationId
                                                  select (item, Ditem))
                    {
                        tempBattery = Ditem.Battery;
                        temp.Add(new DroneInCharge(item.DroneId, tempBattery, item.StartChargeTime));
                    }

                    return temp;
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            public List<DroneToList> GetDrones()
            {
                var v = dronesList;
                return v;
            }

            /// <summary>
            /// display customers list
            /// </summary>
            /// <returns></returns> return list of customers
            /// 
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<CustomerToList> GetCustomersList(System.Predicate<DalApi.DO.Customer> match)
            {

                lock (dal)
                {
                    List<CustomerToList> tmp1 = new();
                    var c = (List<DalApi.DO.Customer>)dal.GetCustomersList();
                    var v = c.FindAll(match);
                    foreach (var element in v)
                    {
                        CustomerToList myCustomer = new();
                        CustomerToList reciver = new();
                        myCustomer.Id = element.Id;
                        myCustomer.Name = element.Name;
                        myCustomer.PhoneNumber = element.PhoneNumber;
                        var parcelsList = dal.GetParcelsList();
                        foreach (var item in parcelsList)
                        {
                            if (item.SenderId == element.Id)
                            {
                                if (item.Delivered != DateTime.MinValue)
                                    myCustomer.ParcelsDelivered++;
                                if (PickedUpButNotDelivered(item.Id))
                                    myCustomer.ParcelsSentButNotDelivered++;
                                if (ScheduledButNotPickedUp(item.Id))
                                    myCustomer.ScheduledParcels++;
                            }
                            if (item.ReceiverId == element.Id)
                                if (item.Delivered != DateTime.MinValue)
                                    myCustomer.ReceivedParcels++;
                        }
                        tmp1.Add(myCustomer);
                    }
                    return tmp1;
                }
            }

            /// <summary>
            /// display parcels list
            /// </summary>
            /// <returns></returns> return list of parcels
            /// 
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<ParcelToList> GetParcelsList(System.Predicate<DalApi.DO.Parcel> match)
            {

                lock (dal)
                {
                    List<ParcelToList> tmpParcel = new();
                    var c = dal.GetParcelsList();
                    var d = (List<DalApi.DO.Parcel>)c;
                    var v = d.FindAll(match);

                    foreach (var element in v)
                    {
                        ParcelToList myParcel = new();
                        myParcel.Id = element.Id;
                        myParcel.Weight = element.Weight;
                        myParcel.Priority = element.Priority;
                        var customersList = dal.GetCustomersList();
                        //display customers (sender and receiver)
                        string senderName = null;
                        string receiverName = null;
                        foreach (var cElement in customersList)
                        {
                            if (cElement.Id == element.SenderId)
                                senderName = cElement.Name;
                            if (cElement.Id == element.ReceiverId)
                                receiverName = cElement.Name;
                        }
                        myParcel.SenderName = senderName;
                        myParcel.ReceiverName = receiverName;
                        if (element.Scheduled > DateTime.MinValue && element.PickedUp == DateTime.MinValue)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.scheduled;
                        if (element.Delivered == DateTime.MinValue && element.PickedUp > DateTime.MinValue)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.pickedUp;
                        if (element.Delivered > DateTime.MinValue)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.delivered;

                        tmpParcel.Add(myParcel);

                    }
                    return tmpParcel;
                }
            }
        }
    }
}
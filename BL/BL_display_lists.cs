using System.Collections.Generic;

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
            public IEnumerable<StationToList> GetStationsList(System.Predicate<DalApi.DO.Station> match)
            {
                List<StationToList> newList = new();
                var v = dal.GetStationsList(match);
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

            /// <summary>
            /// display drones list
            /// </summary>
            /// <returns></returns> reyurn list of drones
            public List<DroneToList> GetDronesList(System.Predicate<DroneToList> match)
            {
                var v = dronesList.FindAll(match);
                if (v == null)
                    System.Console.WriteLine("empty list\n");
                return v;
            }
            public List<DroneToList> GetDrones()
            {
                var v = dronesList;
                return v;

                //var v = dronesList;
                //int i = 1;
                //foreach (var item in v)
                //{
                //    System.Console.WriteLine("Drone #",i++, ": ");
                //    System.Console.WriteLine(item);

                //}
            }

            /// <summary>
            /// display customers list
            /// </summary>
            /// <returns></returns> return list of customers
            public IEnumerable<CustomerToList> GetCustomersList(System.Predicate<DalApi.DO.Customer> match)
            {
                List<CustomerToList> tmp1 = new();
                var v = dal.GetCustomersList(match);
                foreach (var element in v)
                {
                    CustomerToList myCustomer = new();
                    CustomerToList reciver = new();
                    myCustomer.Id = element.Id;
                    myCustomer.Name = element.Name;
                    myCustomer.PhoneNumber = element.PhoneNumber;
                    var parcelsList = dal.GetParcelsList(allParcels);
                    foreach (var item in parcelsList)
                    {
                        if (item.SenderId == element.Id)
                        {
                            if (item.Delivered != null)
                                myCustomer.ParcelsDelivered++;
                            if (PickedUpButNotDelivered(item.Id))
                                myCustomer.ParcelsSentButNotDelivered++;
                            if (ScheduledButNotPickedUp(item.Id))
                                myCustomer.ScheduledParcels++;
                        }
                        if (item.ReceiverId == element.Id)
                            if (item.Delivered != null)
                                myCustomer.ReceivedParcels++;
                    }
                    tmp1.Add(myCustomer);
                }
                return tmp1;
            }

            /// <summary>
            /// display parcels list
            /// </summary>
            /// <returns></returns> return list of parcels
            public IEnumerable<ParcelToList> GetParcelsList(System.Predicate<DalApi.DO.Parcel> match)
            {
                List<ParcelToList> tmp1 = new();
                var v = dal.GetParcelsList(match);
                foreach (var element in v)
                {
                    ParcelToList myParcel = new();
                    myParcel.Id = element.Id;
                    myParcel.Weight = element.Weight;
                    myParcel.Priority = element.Priority;
                    var customersList = dal.GetCustomersList(allCustomers);
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
                    if (ScheduledButNotPickedUp(element.Id))
                        myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.scheduled;
                    if (PickedUpButNotDelivered(element.Id))
                        myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.pickedUp;
                    if(element.Delivered != null)
                        myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.delivered;



                    tmp1.Add(myParcel);

                }
                return tmp1;
            }
        }
    }
}
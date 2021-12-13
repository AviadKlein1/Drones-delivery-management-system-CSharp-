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
            /// display stations list in condition
            /// </summary>
            /// <returns></returns> return list of stations accured to conditions
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
                    newStation.NumOfOccupiedChargeSlots = item.NumOfChargeSlots - item.NumOfAvailableChargeSlots;
                    newList.Add(newStation);
                }
                if (newList == null)
                    System.Console.WriteLine("empty list\n");
                return newList;
            }
            /// <summary>
            /// display drones list
            /// </summary>
            /// <returns></returns> reyurn list of drones
            /// 
            public List<BlApi.BO.DroneToList> GetDronesList(System.Predicate<BlApi.BO.DroneToList> match)
            {
                var v = dronesList.FindAll(match);
                if (v == null)
                    System.Console.WriteLine("empty list\n");
                return v;
            }
            public List<BlApi.BO.DroneToList> GetDrones()
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
            public IEnumerable<BlApi.BO.CustomerToList> GetCustomersList(System.Predicate<DalApi.DO.Customer> match)
            {
                List<BlApi.BO.CustomerToList> tmp1 = new();
                var v = dal.GetCustomersList(match);
                foreach (var element in v)
                {
                    CustomerToList myCustomer = new();
                    myCustomer.id = element.Id;
                    myCustomer.name = element.Name;
                    myCustomer.phoneNumber = element.PhoneNumber;
                    var parcelsList = dal.GetParcelsList(allParcels);
                    foreach (var item in parcelsList)
                    {
                        if (item.SenderId == element.Id)
                        {
                            if (item.Delivered != null) myCustomer.parcelsDelivered++;
                            if (PickedUpButNotDeliverd(item.Id)) myCustomer.parcelsSentButNotDelivered++;
                            if (ScheduledButNotPickedUp(item.Id)) myCustomer.ScheduledParcels++;
                        }
                    }
                    foreach (var item in parcelsList)
                        if (item.ReciverId == element.Id)
                            if (item.Delivered != null) myCustomer.recievedParcels++;

                    tmp1.Add(myCustomer);
                }
                return tmp1;
            }

            /// <summary>
            /// display parcels list
            /// </summary>
            /// <returns></returns> return list of parcels
            public IEnumerable<BlApi.BO.ParcelToList> GetParcelsList(System.Predicate<DalApi.DO.Parcel> match)
            {
                List<BlApi.BO.ParcelToList> tmp1 = new();
                var v = dal.GetParcelsList(match);
                foreach (var element in v)
                {
                    ParcelToList myParcel = new();
                    myParcel.Id = element.Id;
                    myParcel.Weight = element.Weight;
                    myParcel.Priority = element.Priority;
                    var customersList = dal.GetCustomersList(allCustomers);
                    //display customers (sender and reciever)
                    string senderName = null;
                    string reciverName = null;
                    foreach (var cElement in customersList)
                    {
                        if (cElement.Id == element.SenderId)
                            senderName = cElement.Name;
                        if (cElement.Id == element.ReciverId)
                            reciverName = cElement.Name;
                    }
                    myParcel.SenderName = senderName;
                    myParcel.RecieverName = reciverName;

                    tmp1.Add(myParcel);
                }
                return tmp1;
            }                      
        }
    }
}
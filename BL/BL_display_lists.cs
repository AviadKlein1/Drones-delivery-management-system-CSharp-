using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// display list of items
        /// </summary>
        public partial class BL
        {
            /// <summary>
            /// display stations list
            /// </summary>
            /// <returns></returns> return list of stations
            public List<StationToList> DisplayStations()
            {
                List<StationToList> tmp1 = new List<StationToList>();
                var v = dal.GetStations();
                foreach (var element in v)
                {
                    StationToList myStation = new StationToList();
                    myStation.Id = element.Id;
                    myStation.Name = element.Name;
                    myStation.NumOfAvailableChargeSlots = element.NumOfChargeSlots - NumofOccupiedChargeSlots(element.Id);
                    myStation.NumOfOccupiedChargeSlots = NumofOccupiedChargeSlots(element.Id);
                    tmp1.Add(myStation);
                }
                //if empty list
                if (tmp1 == null)
                    System.Console.WriteLine("empty list\n");
                return tmp1;
            }

            /// <summary>
            /// display drones list
            /// </summary>
            /// <returns></returns> reyurn list of drones
            public List<DroneToList> DisplayDrones()
            {
                var v = dronesList;
                return v;
            }

            /// <summary>
            /// display customers list
            /// </summary>
            /// <returns></returns> return list of customers
            public List<CustomerToList> DisplayCustomers()
            {
                List<CustomerToList> tmp1 = new List<CustomerToList>();
                List<IDAL.DO.Customer> tmp2 = new List<IDAL.DO.Customer>();
                var v = dal.GetCustomers();
                foreach (var element in v)
                {
                    CustomerToList myCustomer = new CustomerToList();
                    myCustomer.id = element.Id;
                    myCustomer.name = element.Name;
                    myCustomer.phoneNumber = element.PhoneNumber;
                    tmp1.Add(myCustomer);
                }
                return tmp1;
            }

            /// <summary>
            /// display parcels list
            /// </summary>
            /// <returns></returns> return list of parcels
            public List<ParcelToList> DisplayParcels()
            {
                List<ParcelToList> tmp1 = new List<ParcelToList>();
                List<IDAL.DO.Parcel> tmp2 = new List<IDAL.DO.Parcel>();
                var v = dal.GetParcels();
                foreach (var element in v)
                {
                    ParcelToList myParcel = new ParcelToList();
                    myParcel.Id = element.Id;
                    myParcel.Weight = element.Weight;
                    myParcel.Priority = element.Priority;
                    var customersList = dal.GetCustomers();
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
                    myParcel.SenderName = reciverName;

                    tmp1.Add(myParcel);
                }
                return tmp1;
            }

            /// <summary>
            /// display unassociated parcels list
            /// </summary>
            /// <returns></returns> return list of parcels
            public List<ParcelToList> DisplayUnassociatedParcels()
            {
                List<ParcelToList> tmp1 = new List<ParcelToList>();
                var v = dal.GetParcels();
                foreach (var element in v)
                {
                    if (element.DroneId == 0)
                    {
                        ParcelToList myParcel = new ParcelToList();
                        myParcel.Id = element.Id;
                        myParcel.Weight = element.Weight;
                        myParcel.Priority = element.Priority;
                        var customersList = dal.GetCustomers();
                        //disp;ay customers (sender and reciever)
                        string senderName = null;
                        string reciverName = null;
                        foreach (var cElement in customersList)
                        {
                            if (cElement.Id == element.SenderId) senderName = cElement.Name;
                            if (cElement.Id == element.ReciverId) reciverName = cElement.Name;
                        }
                        myParcel.SenderName = senderName;
                        myParcel.SenderName = reciverName;

                        tmp1.Add(myParcel);
                    }
                }
                return tmp1;
            }

            /// <summary>
            /// display available for charge stations list
            /// </summary>
            /// <returns></returns> return list of stations
            public List<StationToList> DisplayAvailableStations()
            {
                List<StationToList> tmp1 = new List<StationToList>();
                List<IDAL.DO.Station> tmp2 = new List<IDAL.DO.Station>();
                var v = dal.GetStations();
                foreach (var element in v)
                {
                    if (element.NumOfAvailableChargeSlots > 0)
                    {
                        StationToList myStation = new StationToList();
                        myStation.Id = element.Id;
                        myStation.Name = element.Name;
                        myStation.NumOfAvailableChargeSlots = element.NumOfAvailableChargeSlots;
                        myStation.NumOfOccupiedChargeSlots = element.NumOfChargeSlots - element.NumOfAvailableChargeSlots;
                        tmp1.Add(myStation);
                    }
                }
                return tmp1;
            }
        }
    }
}